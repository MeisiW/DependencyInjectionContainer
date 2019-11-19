using EmailSenderInterfaces;
using System;

namespace EmailSenderImplementation2
{
    public class EmailSenderImplementationTwo: IEmailSender
    {
        public bool SendEmail(string to, string body)
        {
            Console.WriteLine($"SendMailTwo sends mail to {to} with body:\n{body}");
            return true;
        }
    }
}
