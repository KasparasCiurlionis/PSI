using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Data
{
    public class GasTypes
    {
        public class CircleKGas
        {
            internal static string[] GetGasTypes()
            {
                // it should have 4 types: 95, 98, D, LPG
                return new string[] { "95", "98", "D", "LPG" };
            }

        }
        public class NesteGas
        {
            internal static string[] GetGasTypes()
            {
                // it should have 3 types: 95, 98, D, D_PRO
                return new string[] { "95", "98", "D", "D_PRO" };
            }
        }
        public class Viada
        {
            internal static string[] GetGasTypes()
            {
                // it should have 3 types: 95, 98, D
                return new string[] { "95", "98", "D" };
            }
        }
        public class BalticPetroleum
        {
            internal static string[] GetGasTypes()
            {
                // it should have 4 types: 95, 98, D, SND
                return new string[] { "95", "98", "D", "SND" };
            }
        }
        public class Alausa
        {
            internal static string[] GetGasTypes()
            {
                // it should have 4 types: 95, 98, D, LPG
                return new string[] { "95", "98", "D", "LPG" };
            }
        }
    }

}