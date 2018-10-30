using NPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Park.Rep
{
    public abstract class BaseRepository<T> where T : class, new()
    {
        protected NPocoDatabase db = null;

        #region 增删改查

        public Task<object> InsertAsync(T poco)
        {
            return db.InsertAsync(poco);
        }

        public Task InsertBatchAsync(IEnumerable<T> pocos)
        {
            return Task.Run(() =>
            {
                db.InsertBatch(pocos, new BatchOptions() { BatchSize = 100 });
            });
        }

        public Task InsertBulkAsync(IEnumerable<T> pocos)
        {
            return Task.Run(() =>
            {
                db.InsertBulk(pocos);
            });
        }

        public Task<int> DeleteAsync(T poco)
        {
            return db.DeleteAsync(poco);
        }

        public Task<int> UpdateAsync(T poco)
        {
            return db.UpdateAsync(poco);
        }

        public Task<int> UpdateAsync(T poco, IEnumerable<string> columns)
        {
            return db.UpdateAsync(poco, columns);
        }

        public Task<T> FirstOrDefaultAsync(string sql, params object[] args)
        {
            return db.FirstOrDefaultAsync<T>(sql, args);
        }

        public Task<T> SingleOrDefaultByIdAsync(object primaryKey)
        {
            return db.SingleOrDefaultByIdAsync<T>(primaryKey);
        }

        public Task<List<T>> FetchAsync()
        {
            return db.FetchAsync<T>();
        }

        public Task<List<T>> FetchAsync(Sql sql)
        {
            return db.FetchAsync<T>(sql);
        }

        public Task<List<T>> FetchAsync(string sql, params object[] args)
        {
            return db.FetchAsync<T>(sql, args);
        }

        public Task<List<T>> FetchAsync(long page, long itemsPerPage, Sql sql)
        {
            return db.FetchAsync<T>(page, itemsPerPage, sql);
        }

        public Task<List<T>> FetchAsync(long page, long itemsPerPage, string sql, params object[] args)
        {
            return db.FetchAsync<T>(page, itemsPerPage, sql, args);
        }

        public Task<Page<T>> PageAsync(long page, long itemsPerPage, Sql sql)
        {
            return db.PageAsync<T>(page, itemsPerPage, sql);
        }

        public Task<Page<T>> PageAsync(long page, long itemsPerPage, string sql, params object[] args)
        {
            return db.PageAsync<T>(page, itemsPerPage, sql, args);
        }

        public Task<List<T>> SkipTakeAsync(long skip, long take, Sql sql)
        {
            return db.SkipTakeAsync<T>(skip, take, sql);
        }

        public Task<List<T>> SkipTakeAsync(long skip, long take, string sql, params object[] args)
        {
            return db.SkipTakeAsync<T>(skip, take, sql, args);
        }

        #endregion

        protected Task<int> ExecuteAsync(Sql Sql)
        {
            return db.ExecuteAsync(Sql);
        }

        protected Task<int> ExecuteAsync(string sql, params object[] args)
        {
            return db.ExecuteAsync(sql, args);
        }

        protected Task<K> ExecuteScalarAsync<K>(Sql Sql)
        {
            return db.ExecuteScalarAsync<K>(Sql);
        }

        protected Task<K> ExecuteScalarAsync<K>(string sql, params object[] args)
        {
            return db.ExecuteScalarAsync<K>(sql, args);
        }
    }
}
