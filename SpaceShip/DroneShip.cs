using CargoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip
{
    class DroneShip : ICargoTransporter
    {// FTL Lifeboat
        private string name;
        private Queue<Cargo> storage = new Queue<Cargo>();
        private int available { get; set; }

        public DroneShip(string name, int size)
        {
            if (size < 0)
                throw new ArgumentException("Can't set account to a negative balance", "value");
            this.name = name;
            available = size;
        }

        public bool AddCargo(Cargo item)
        {
            if (item == null)
                throw new ArgumentException("Cargo must be a physical thing.");

            if (available - item.GetSize() < 0)
                return false;
            else
            {
                storage.Enqueue(item);
                available -= item.GetSize();
                return true;
            }
        }

        public string GetShipName()
        {
            return name;
        }

        public bool HasCargo()
        {
            if (storage.Count > 0)
                return true;
            return false;
        }

        public void ListCargo()
        {
            Console.Write($"--- ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(name);
            Console.ResetColor();
            Console.WriteLine($" cargo, Availible space: {available} ---");
            if (storage.Count < 1)
            {
                Console.WriteLine("<empty>");
            }
            foreach (var item in storage)
            {
                Console.WriteLine($"- {item.GetDescription()}");
            }

            Console.WriteLine();
        }

        public Cargo RemoveCargo()
        {
            if (storage.Count < 1)
                return null;

            var removedCargo = storage.Dequeue();

            if (removedCargo.GetSize() > 0)
                available += removedCargo.GetSize();

            return removedCargo;
        }
    }
}
