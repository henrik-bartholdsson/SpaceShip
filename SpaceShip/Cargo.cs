using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week2_day2
{
    class Cargo
    {
        private string description { get; set; }
        private int size { get; set; }
        public Cargo(string desc, int size)
        {
            if (size <= 0)
                throw new ArgumentException("Cargo must be a physical thing.");

            description = desc;
            this.size = size;
        }

        public string GetDescription()
        {
            return description;
        }

        public int GetSize()
        {
            return size;
        }
    }
}