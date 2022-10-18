using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;
using Newtonsoft.Json;
using WebProject.Data;

namespace WebProject
{
    public class GasData
    {
        public static List<GasStations> getData()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory+"/App_Data/Data";
            string[] gasTypes = File.ReadAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Gas Types.txt");
            List<GasStations> data=new List<GasStations>();
            foreach (string file in Directory.EnumerateFiles(path, "*.txt"))
            {
                List<GasStation> stations = new List<GasStation>();
                string contents = File.ReadAllText(file);
                string name = (Path.GetFileNameWithoutExtension(file) + " ");

                List<Location> coordinates = LocationData.getData(name);

                string[] locations = contents.Split('\n');



                string[] types = new string[gasTypes.Length];
                string adress = null;

                int index = 0;
                foreach (var word in locations)
                {

                    bool isNew = true;
                    for (int i = 0; i < gasTypes.Length; i++)
                    {
                        if (word.StartsWith(gasTypes[i] + " "))
                        {
                            types[i] = word.Substring(gasTypes[i].Length);
                            isNew = false;
                        }
                    }

                    if (isNew)
                    {
                        if (adress!=null)
                        {
                            Location location = coordinates[index];
                            var serializedParent = JsonConvert.SerializeObject(location);
                            GasStation station = JsonConvert.DeserializeObject<GasStation>(serializedParent);
                            station.setAddress(adress);
                            station.setPrices(types);
                            
                            stations.Add(station);
                            index++;
                        }
                        adress = word;
                        
                    }
                }
                data.Add(new GasStations(stations.ToArray(), name)); 
            }
            return data;
        }
    }
}
