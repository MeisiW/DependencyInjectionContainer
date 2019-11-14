using System;

namespace EmailSenderInterfaces
{
    
        public interface IEmailSender
        {
            bool SendEmail(string to, string body);
        }
    
}
