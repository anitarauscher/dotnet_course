using System;

namespace Epam.Automation.src.Core
{
    public class Logger
    {
        private static Logger _log = new Logger();
        public static Logger log => _log;

        public static void Info(string message)
        {
            Console.WriteLine("[INFO] " + message);
        }

        public static void Error(string message)
        {
            Console.WriteLine("[ERROR] " + message);
        }

        public void InfoInstance(string message)
        {
            Console.WriteLine("[INFO] " + message);
        }

        public void ErrorInstance(string message)
        {
            Console.WriteLine("[ERROR] " + message);
        }
    }
}