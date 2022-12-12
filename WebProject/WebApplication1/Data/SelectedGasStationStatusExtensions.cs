using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebApplication1.Data.GasTypes;

namespace WebApplication1.Data
{
    public static class SelectedGasStationStatusExtensions
    {
        
        public static string[] GetGasTypes(this SelectedGasStationStatus gasStationStatus, SelectedGasStationStatus result = SelectedGasStationStatus.CircleKGas)
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