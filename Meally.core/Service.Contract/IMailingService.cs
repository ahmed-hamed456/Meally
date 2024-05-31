using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Service.Contract
{
    public interface IMailingService
    {
        Task SendEmailAsync(string MailTo, string ConfirmationCode);

        //Task SendEmailAsync(string email, string message, string userName);

    }
}
