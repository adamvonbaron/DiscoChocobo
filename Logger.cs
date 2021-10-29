using System;
using System.Threading.Tasks;

using Discord;


namespace DiscoChocobo
{
    public class Logger
    {
        public static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
