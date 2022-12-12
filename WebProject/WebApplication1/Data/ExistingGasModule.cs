using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Data
{
    public class ExistingGasModule
    {
        
        public static string CircleKModuleID = "856a88e1-8fae-4bf9-be3e-cd68e90051b0";
        public static string NesteModuleID = "2eabd2b8-7475-43c8-9791-7359d578dec2";


        public interface GasModules
        {
            string getModule();
        }

        class CircleK : GasModules
        {
            public string getModule()
            {
                return CircleKModuleID;
            }
        }

        class Neste : GasModules
        {
            public string getModule()
            {
                return NesteModuleID;
            }
        }

        class Viada : GasModules
        {
            public string getModule()
            {
                return null;
            }
        }

        class BalticPetroleum : GasModules
        {
            public string getModule()
            {
                return null;
            }
        }

        class Alausa : GasModules
        {
            public string getModule()
            {
                return null;
            }
        }

        public static string getModule(string gasStation)
        {
            GasModules gasModule = null;
            switch (gasStation)
            {
                case "Circle_K":
                    gasModule = new CircleK();
                    break;
                case "Neste":
                    gasModule = new Neste();
                    break;
                case "Viada":
                    gasModule = new Viada();
                    break;
                case "Baltic_Petroleum":
                    gasModule = new BalticPetroleum();
                    break;
                case "Alauša":
                    gasModule = new Alausa();
                    break;
            }
            return gasModule.getModule();
        }
    }
}