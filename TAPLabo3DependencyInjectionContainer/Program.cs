using System;
using EmailSenderInterfaces;
using TinyDependencyInjectionContainer;

namespace TAPLabo3DependencyInjectionContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var resolver = new InterfaceResolver("TDIC_Configuration.txt");
            var sender = resolver.Instantiate<IEmailSender>();
            sender.SendEmail("pippo", "pluto");
        }
    }
}
