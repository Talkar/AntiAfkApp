using System;
using System.Runtime.InteropServices;
using System.Timers;

namespace AntiAfkApp
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the key, used to move your character forward:");
            var forwardKey = Console.ReadKey();
            Console.WriteLine("");
            Console.WriteLine("Enter the interval to press the key, in seconds, and press enter:");
            var interval = Console.ReadLine();
            var parseResult = double.TryParse(interval, out var parsedInterval);
            if (!parseResult)
            {
                Console.WriteLine("You didn't enter a valid amount of seconds. Only use numbers");
                Console.ReadKey();
                return;
            }
            var lastPressed = DateTime.Now;

            while (true)
            {
                if (DateTime.Now.AddSeconds(-parsedInterval) < lastPressed)
                    continue;
                keybd_event((byte)forwardKey.Key, 0, 0, 0);
                lastPressed = DateTime.Now;
            }
        }
    }
}
