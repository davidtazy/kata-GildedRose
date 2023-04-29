using System;
using System.Collections.Generic;

namespace GildedRose.Cli
{
    class Program
    {

        static void Main(string[] args)
        {

            var inn = new Inn();

            System.Console.WriteLine("OMGHAI!");
            System.Console.WriteLine("Type <enter> to pass to the next day or <esc> to quit:");
            System.Console.WriteLine();

            while (System.Console.ReadKey().Key != ConsoleKey.Escape)
            {
                System.Console.WriteLine("Go to next day...");
                inn.AdvanceOneDayAndUpdateQuality();
                System.Console.WriteLine(inn.ToString());
            }

            System.Console.WriteLine("Ciao...");
            System.Console.ReadLine();
        }


    }


}
