using System;

namespace Targil0
{
    partial class Targil0
    {
        static void Main(string[] args)
        {
            Welcome5746();
            Welcome3923();
            Console.ReadKey();
        }

        static partial void Welcome3923();

        private static void Welcome5746()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("{0}, welcome to my first console application", name);
        }
    }
}
