using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoService
{
    public interface ICargoTransporter
    {
        bool AddCargo(Cargo item);
        Cargo RemoveCargo();
        void ListCargo();
        string GetShipName();
        bool HasCargo();
    }
}
