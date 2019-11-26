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
                cargoTemp = RemoveCargo();

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
            {
                Console.WriteLine($"- {item.GetDescription()}");
            }

            Console.WriteLine();
        }

        public int GetAvailableCargoSpace()
        {
            return available;
        }

        public void GetDistance()
        {
            int distance = 0;
            var file = File.ReadLines("map.txt");
            List<char[]> map = new List<char[]>();
            int yReference = 0;
            var plotMapSpotY = Console.CursorTop;
            var plotMapSpotX = 0;

            foreach (var line in file)
            {
                map.Add(line.ToCharArray());
            }
            var cordX = GetCordinate(map, 'X');
            var cordP = GetCordinate(map, 'P');

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("The map.");
            Console.ResetColor();
            yReference = Console.CursorTop;
            plotMapSpotY = cordX[1] + yReference;
            plotMapSpotX = cordX[0];
            foreach (var line in map)
            {
                foreach (var ch in line)
                {
                    Console.Write(ch);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.CursorVisible = false;

            while (plotMapSpotX != cordP[0])
            {
                System.Threading.Thread.Sleep(200);
                Console.BackgroundColor = ConsoleColor.Red;
                if (cordX[0] > cordP[0])
                {
                    plotMapSpotX--;
                    Console.SetCursorPosition(plotMapSpotX, plotMapSpotY);
                    Console.Write(map[plotMapSpotY - yReference][plotMapSpotX]);
                }
                else if (cordX[0] < cordP[0])
                {
                    plotMapSpotX++;
                    Console.SetCursorPosition(plotMapSpotX, plotMapSpotY);
                    Console.Write(map[plotMapSpotY - yReference][plotMapSpotX]);
                }
            }

            while ((plotMapSpotY - yReference) != cordP[1])
            {
                System.Threading.Thread.Sleep(200);
                Console.BackgroundColor = ConsoleColor.Red;
                if (cordX[1] > cordP[1])
                {
                    plotMapSpotY--;
                    Console.SetCursorPosition(plotMapSpotX, plotMapSpotY);
                    Console.Write(map[plotMapSpotY - yReference][plotMapSpotX]);
                }
                else if (cordX[1] < cordP[1])
                {
                    plotMapSpotY++;
                    var yp = plotMapSpotY - yReference;
                    Console.SetCursorPosition(plotMapSpotX, plotMapSpotY);
                    Console.Write(map[plotMapSpotY - yReference][plotMapSpotX]);
                }
            }
            Console.CursorTop = yReference + map.Count;
            Console.CursorLeft = 0;
            Console.ResetColor();
            Console.CursorVisible = true;

            distance += (int)Math.Sqrt((int)Math.Pow((cordP[0] - cordX[0]), 2));
            distance += (int)Math.Sqrt((int)Math.Pow((cordP[1] - cordX[1]), 2));

            Console.WriteLine($"Distance to destination: {distance}");
            Console.WriteLine();

            // A straight line is a better way to travel.... a^2 + b^2 = c^2
            //xSqr = (int)Math.Pow((int)Math.Sqrt((int)Math.Pow((cordP[0] - cordX[0]), 2)), 2);
            //ySqr = (int)Math.Pow((int)Math.Sqrt((int)Math.Pow((cordP[1] - cordX[1]), 2)), 2);
            //hypotenuseRote = xSqr + ySqr;
            //hypotenuseRote = (int)Math.Sqrt(hypotenuseRote);
            //Console.WriteLine($"Shorter distance to destination: {hypotenuseRote}");
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
                    if (map[i][j].ToString().ToLower() == c.ToString().ToLower())
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
