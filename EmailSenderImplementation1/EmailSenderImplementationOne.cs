using EmailSenderInterfaces;
using System;

namespace EmailSenderImplementation1
{
    public class EmailSenderImplementationOne : IEmailSender
    {
        public bool SendEmail(string to, string body)
        {
            Console.WriteLine($"SendMailOne sends mail to {to} with body:\n{body}");
            return true;
        }
    }
}
