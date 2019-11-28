using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoService;

namespace SpaceShip
{
    class Program
    {
        static void Main(string[] args)
        {
            var nostromo = new TransportShip("Nostromo", 20);
            var prometheus = new TransportShip("Prometheus", 20);
            var sulaco = new TransportShip("Sulaco", 7);
            var lifeBoat = new DroneShip("FTL Lifeboat", 3);
            var station = new SpaceStation("Solaris");
            const string keyA = "A";
            const string keyB = "B";
            const string keyC = "C";

            var beer = new Cargo("Beer", 6);
            var food = new Cargo("Food", 7);
            var tools = new Cargo("Tools", 8);
            var constMat = new Cargo("Construction materials", 11);
            var equipment = new Cargo("Equipment", 3);

            Console.WriteLine();

            prometheus.AddCargo(equipment);
            prometheus.AddCargo(beer);

            nostromo.AddCargo(beer);
            nostromo.AddCargo(food);

            lifeBoat.AddCargo(new Cargo("Egg", 1));

            nostromo.ListCargo();
            prometheus.ListCargo();
            sulaco.ListCargo();
            Console.WriteLine();

            station.DropOff(keyA, nostromo);
            Console.WriteLine();

            station.DropOff(keyA, lifeBoat);
            Console.WriteLine();

            station.DropOff(keyA, sulaco);
            Console.WriteLine();

            station.Pickup(keyB, prometheus);
            Console.WriteLine();

            station.Pickup(keyA, prometheus);
            Console.WriteLine();

            station.Pickup(keyA, sulaco);
            Console.WriteLine();

            station.DropOff(keyA, nostromo);
            Console.WriteLine();

            station.Pickup(keyA, nostromo);
            Console.WriteLine();


            Console.WriteLine();

            nostromo.ListCargo();
            prometheus.ListCargo();
            sulaco.ListCargo();
            lifeBoat.ListCargo();
            Console.WriteLine();

            Console.WriteLine($"Number of loads at {station.GetStationName()}: {station.GetNumberOfLoads()}");

            prometheus.GetDistance();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("End...");
            Console.ReadKey();

        }


    }
}