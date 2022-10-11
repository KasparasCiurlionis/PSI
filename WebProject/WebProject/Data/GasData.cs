using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace WebProject
{
    public class GasData
    {
        public static List<GasStation> getData()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory+"/App_Data/Data";
            List<GasStation> data=new List<GasStation>();
            foreach (string file in Directory.EnumerateFiles(path, "*.txt"))
            {
                string contents = File.ReadAllText(file);
                string name = (Path.GetFileNameWithoutExtension(file) + " ");
                string[] locations = contents.Split('\n');  
                data.Add(new GasStation(name, locations)); 
            }
            return data;
        }
    }
}