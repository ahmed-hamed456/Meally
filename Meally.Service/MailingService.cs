
using MailKit.Net.Smtp;
using Meally.core.Service.Contract;
using Meally.core.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Meally.Service
{
    public class MailingService : IMailingService
    {
        private readonly MailSettings _mailSetting;
        private readonly IMemoryCache _memoryCache;
      

        public MailingService(IOptions<MailSettings> mailSetting,
            IMemoryCache MemoryCache)
        {
            _mailSetting = mailSetting.Value;
            _memoryCache = MemoryCache;
            
        }

        //public async Task SendEmailAsync(string email, string message, string userName)
        //{
        //    using (var smtp = new SmtpClient())
        //    {

        //        await smtp.ConnectAsync(_mailSetting.Host, _mailSetting.Port,true);
        //        await smtp.AuthenticateAsync(_mailSetting.Email, _mailSetting.password);
        //        var bodyBuilder = new BodyBuilder
        //        {
        //            HtmlBody = $"{message}",
        //            TextBody = "Wellcome",
        //        };
        //        var sendMessage = new MimeMessage
        //        {
        //            Body = bodyBuilder.ToMessageBody(),
        //            Subject = $"Wellcome {userName}"
        //        };
        //        sendMessage.From.Add(new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Email));
        //        sendMessage.To.Add(new MailboxAddress(userName, email));
        //        await smtp.SendAsync(sendMessage);
        //        smtp.Disconnect(true);
        //    }
        //}


        public async Task SendEmailAsync(string MailTo, string ConfirmationCode)
        {
            var email = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_mailSetting.Email),
                Subject = "MeallyApi"
            };

            email.To.Add(MailboxAddress.Parse(MailTo));

            var builder = new BodyBuilder();

            builder.HtmlBody = ConfirmationCode;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSetting.Host, _mailSetting.Port);
            smtp.Authenticate(_mailSetting.Email, _mailSetting.password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            _memoryCache.Set("VerificationCode", ConfirmationCode, TimeSpan.FromMinutes(4));
        }


    } 
}
