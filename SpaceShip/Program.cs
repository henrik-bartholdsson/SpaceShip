using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week2_day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var nostromo = new TransportShip("USCSS Nostromo", 20);
            var prometheus = new TransportShip("USCSS Prometheus", 20);

            var beer = new Cargo("Beer", 6);
            var food = new Cargo("Food", 7);
            var tools = new Cargo("Tools", 8);
            var constMat = new Cargo("Construction materials", 11);
            var equipment = new Cargo("Equipment", 3);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Initial state.");
            Console.ResetColor();
            Console.WriteLine();

            prometheus.AddCargo(equipment);
            prometheus.AddCargo(beer);

            nostromo.AddCargo(beer);
            nostromo.AddCargo(food);

            nostromo.ListCargo();
            prometheus.ListCargo();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Traveling at the speed of 7 parsec/min.");
            Console.ResetColor();
            prometheus.GetDistance();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Transferring cargo from one ship to another.");
            Console.ResetColor();
            var preCursorPos = Console.CursorTop;
            for (int i = 0; i <= 100; i += 20)
            {
                Console.CursorTop = preCursorPos;
                Console.Write(".");
                System.Threading.Thread.Sleep(600);
            }
            Console.CursorTop = preCursorPos;
            Console.WriteLine();
            Console.WriteLine();

            nostromo.MoveCargoToOtherShip(prometheus);

            nostromo.ListCargo();
            prometheus.ListCargo();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("End...");
            Console.ReadKey();

        }
    }
}