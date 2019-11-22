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

            prometheus.AddCargo(constMat);
            prometheus.AddCargo(tools);


            nostromo.AddCargo(beer);
            nostromo.AddCargo(food);

            nostromo.ListCargo();
            prometheus.ListCargo();

            nostromo.MoveCargoToOtherShip(prometheus);

            nostromo.ListCargo();
            prometheus.ListCargo();

            prometheus.GetDistance();
            Console.ReadKey();

        }
    }
}