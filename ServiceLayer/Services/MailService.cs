using CoreLayer.Dtos.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class MailService
    {
        public bool SendMail(MailDto mailDto)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("ht241224@gmail.com");
                message.Subject = mailDto.Subject;
                message.To.Add(new MailAddress(mailDto.Contact));
                message.Body = mailDto.Body;
                message.IsBodyHtml = true;
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("ht241224@gmail.com", "kpgzddcyriovphrd"),
                    EnableSsl = true
                };
                smtpClient.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
