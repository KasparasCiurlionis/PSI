using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebProject.Data.GasTypes;

namespace WebProject.Data
{
    public static class SelectedGasStationStatusExtensions
    {
        public static string[] GetGasTypes(this SelectedGasStationStatus gasStationStatus)
        {
            switch (gasStationStatus)
            {
                case SelectedGasStationStatus.CircleKGas:
                    return CircleKGas.GetGasTypes();
                case SelectedGasStationStatus.NesteGas:
                    return NesteGas.GetGasTypes();
                case SelectedGasStationStatus.Viada:
                    return Viada.GetGasTypes();
                case SelectedGasStationStatus.BalticPetroleum:
                    return BalticPetroleum.GetGasTypes();
                case SelectedGasStationStatus.Alausa:
                    return Alausa.GetGasTypes();
                default:
                    throw new ArgumentException("Unknown gas station status");
            }
        }
    }
}