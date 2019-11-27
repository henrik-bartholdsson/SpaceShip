using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoService
{
    public class SpaceStation
    {
        private string name;

        public SpaceStation(string name)
        {
            this.name = name;
        }

        Dictionary<string, List<Cargo>> storage = new Dictionary<string, List<Cargo>>();

        public string GetStationName()
        {
            return name;
        }

        public void DropOff(string key, ICargoTransporter ship)
        {
            Cargo item;
            List<Cargo> load = new List<Cargo>();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{ship.GetShipName()} is unloading at {name}. Key: {key}");
            Console.ResetColor();
            if (!ship.HasCargo())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Nothing to unload.");
                Console.ResetColor();
                return;
            }

            if (storage.ContainsKey(key))
            {
                storage.TryGetValue(key, out load);
                load.Add(ship.RemoveCargo());
                storage.Remove(key);
                storage.Add(key, load);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("This is an addition to an allready existing cargo.");
                Console.ResetColor();
                return;
            }

            while ((item = ship.RemoveCargo()) != null)
            {
                load.Add(item);
            }

            storage.Add(key, load);
            Timer();
            Console.WriteLine("Done");
        }

        public void Pickup(string key, ICargoTransporter ship)
        {
            List<Cargo> load = new List<Cargo>();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{ship.GetShipName()} is picking up cargo at {name}. Key: {key}");
            Console.ResetColor();

            if (!storage.TryGetValue(key, out load))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Can't find load.");
                Console.ResetColor();
                return;
            }

            storage.Remove(key);

            Timer();

            while (load.Count > 0)
            {
                if (!ship.AddCargo(load.LastOrDefault()))
                {
                    storage.Add(key, load);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Not all items loaded, not enough space in ship.");
                    Console.ResetColor();
                    return;
                }

                load.RemoveAt(load.Count - 1);
            }
            Console.WriteLine("Done");
        }

        public int GetNumberOfLoads()
        {
            return storage.Count;
        }

        static public void Timer()
        {
            for (int i = 0; i < 4; i++)
            {
                System.Threading.Thread.Sleep(170);
                Console.Write(".");
            }
        }

    }
}
