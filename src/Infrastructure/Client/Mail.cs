using Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Client
{
    /// <summary>
    /// Mail Client
    /// </summary>
    public class Mail
    {
        private SmtpClient client = null;//邮件Client

        public Mail(string host, int port, string user, string possword)
        {
            client = new SmtpClient(host, port);
            client.Credentials = new NetworkCredential(user, possword);
            client.EnableSsl = true;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        public async Task SendAsync(string sender, string reciver, string subject, string body)
        {
            try
            {
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(sender);
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;

                    var list = reciver.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries); //多个收件人;隔开
                    foreach (var item in list)
                    {
                        message.To.Add(item);
                    }

                    await client.SendMailAsync(message);//发送邮件
                }
            }
            catch
            {
                Log.Error("发送邮件出错");
                throw;
            }
        }
    }
}
