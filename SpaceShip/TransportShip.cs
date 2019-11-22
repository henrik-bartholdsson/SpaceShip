using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week2_day2
{
    class TransportShip
    {
        private int size { get; set; }
        private string name { get; set; }
        private Stack<Cargo> storage = new Stack<Cargo>();
        private int available { get; set; }


        public TransportShip(string name, int size)
        {
            if (size < 0)
                throw new ArgumentException("Can't set account to a negative balance", "value");
            this.name = name;
            this.size = size;
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
                storage.Push(item);
                available -= item.GetSize();
                return true;
            }
        }

        public bool MoveCargoToOtherShip(TransportShip ts)
        {
            Cargo cargoTemp;
            while (storage.Count > 0)
            {
                cargoTemp = storage.Pop();

                if (!ts.AddCargo(cargoTemp))
                {
                    this.AddCargo(cargoTemp);
                    return false;
                }
            }
            return true;
        }

        public Cargo RemoveCargo()
        {
            var removedCargo = storage.Pop();

            if (removedCargo.GetSize() > 0)
                available += removedCargo.GetSize();

            return removedCargo;
        }

        public void ListCargo()
        {
            Console.WriteLine($"--- {name}, Cargo, Availible space: {available} ---");
            if (storage.Count < 1)
            {
                Console.WriteLine("<empty>");
            }
            foreach (var item in storage)
                Console.WriteLine($"- {item.GetDescription()}");
            Console.WriteLine("-/");
            Console.WriteLine();
        }

        public int GetAvailableCargoSpace()
        {
            return available;
        }

        public void GetDistance()
        {
            int xSqr = 0;
            int ySqr = 0;
            int hypotenuseRote = 0;
            int distance = 0;
            var file = File.ReadLines("map.txt");
            List<char[]> map = new List<char[]>();

            foreach (var line in file)
            {
                map.Add(line.ToCharArray());
            }

            Console.WriteLine("####### The map #######");
            foreach (var line in map)
            {
                foreach (var ch in line)
                {
                    Console.Write(ch);
                }
                Console.WriteLine();
            }
            Console.WriteLine("#######################");

            var cordX = GetCordinate(map, 'X');
            var cordP = GetCordinate(map, 'P');

            // A square can never be a negative number, so we can avoid ugly multiplications by -1
            distance += (int)Math.Sqrt((int)Math.Pow((cordP[0] - cordX[0]), 2));
            distance += (int)Math.Sqrt((int)Math.Pow((cordP[1] - cordX[1]), 2));

            Console.WriteLine($"Distance to destination: {distance}");
            Console.WriteLine();

            // To plot your route in a 90 degree angle is just stupid when Pythagoras showed us a shorter way.... a^2 + b^2 = c^2
            xSqr = (int)Math.Pow((int)Math.Sqrt((int)Math.Pow((cordP[0] - cordX[0]), 2)), 2);
            ySqr = (int)Math.Pow((int)Math.Sqrt((int)Math.Pow((cordP[1] - cordX[1]), 2)), 2);
            hypotenuseRote = xSqr + ySqr;
            hypotenuseRote = (int)Math.Sqrt(hypotenuseRote);
            Console.WriteLine($"Shorter distance to destination: {hypotenuseRote}");
        }

        public int[] GetCordinate(List<char[]> map, char c)
        {
            int mapWidth = map[0].Length;
            int mapHeight = map.Count;
            int[] cord = new int[2];

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if (map[i][j] == c)
                    {
                        cord[0] = j;
                        cord[1] = i;
                        return cord;
                    }
                }
            }

            return cord;
        }
    }
}
