using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebProject.Data
{
    public class LocationData
    {
        public static List<Location> getData(string name)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "/App_Data/coords";
            
            List<Location> data = new List<Location>();

            string name1 = name.Remove(name.Length - 1);

            string contents = File.ReadAllText(path + "\\" + name1 + ".txt");
            string[] temp = contents.Split('\n');
            for (int i = 0; i < temp.Length; i++)
            {
                string[] coords = temp[i].Split(' ');
                double x = double.Parse(coords[0]);
                double y = double.Parse(coords[1]);

                data.Add(new Location(new Coords(x, y)));
            }
            
            return data;
        }
    }
}