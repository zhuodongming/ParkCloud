﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Helper
{
    /// <summary>
    /// 雪花Id生成器
    /// </summary>
    public class IdWorker
    {
        //基准时间
        const long Twepoch = 1288834974657L;
        //机器标识位数
        const int WorkerIdBits = 5;
        //数据标志位数
        const int DatacenterIdBits = 5;
        //序列号识位数
        const int SequenceBits = 12;
        //机器ID最大值
        const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
        //数据标志ID最大值
        const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);
        //序列号ID最大值
        const long SequenceMask = -1L ^ (-1L << SequenceBits);
        //机器ID偏左移12位
        const int WorkerIdShift = SequenceBits;
        //数据ID偏左移17位
        const int DatacenterIdShift = SequenceBits + WorkerIdBits;
        //时间毫秒左移22位
        const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;

        private long _datacenterId = 0L;
        private long _workerId = 0L;
        private long _sequence = 0L;

        private long _lastTimestamp = -1L;

        public IdWorker(long workerId, long datacenterId)
        {
            // 如果超出范围就抛出异常
            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException(string.Format("worker Id 必须大于0，且不能大于MaxWorkerId： {0}", MaxWorkerId));
            }

            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException(string.Format("region Id 必须大于0，且不能大于MaxWorkerId： {0}", MaxDatacenterId));
            }

            //先检验再赋值
            _workerId = workerId;
            _datacenterId = datacenterId;
        }

        readonly object _lock = new Object();
        public long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();
                if (timestamp < _lastTimestamp)
                {
                    throw new Exception(string.Format("时间戳必须大于上一次生成ID的时间戳.  拒绝为{0}毫秒生成id", _lastTimestamp - timestamp));
                }
                else if (timestamp == _lastTimestamp)//如果上次生成时间和当前时间相同,在同一毫秒内
                {
                    //sequence自增，和sequenceMask相与一下，去掉高位
                    _sequence = (_sequence + 1) & SequenceMask;
                    //判断是否溢出,也就是每毫秒内超过1024，当为1024时，与sequenceMask相与，sequence就等于0
                    if (_sequence == 0)
                    {
                        //等待到下一毫秒
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    //如果和上次生成时间不同,重置sequence，就是下一毫秒开始，sequence计数重新从0开始累加,
                    //为了保证尾数随机性更大一些,最后一位可以设置一个随机数
                    _sequence = 0;//new Random().Next(10);
                }

                _lastTimestamp = timestamp;
                return ((timestamp - Twepoch) << TimestampLeftShift) | (_datacenterId << DatacenterIdShift) | (_workerId << WorkerIdShift) | _sequence;
            }
        }

        // 防止产生的时间比之前的时间还要小（由于NTP回拨等问题）,保持增量的趋势.
        private long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        // 获取当前的时间戳
        private long TimeGen()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}
