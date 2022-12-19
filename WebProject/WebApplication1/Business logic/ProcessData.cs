using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;


namespace WebApplication1
{
    public class ProcessData
    {

        public static List<List<String>> process(string selectedGasStation, IGasStation gasStation)
        {
            List<GasStations> list = GasData.getData(gasStation);
            List<List<String>> rows = new List<List<String>>();
            List<string> keywords = new List<string> { "Location", "Compony" };
           

            List<String> firstRow = new List<String>();
            for (int i = 0; i < keywords.Count(); i++)
            {
                firstRow.Add(keywords[i]);
            }
            string[] gasTypes = GasData.getTypes().ToArray();
            for (int i = 0; i < gasTypes.Count(); i++)
            {
                firstRow.Add(gasTypes[i]);
            }
            rows.Add(firstRow);
            
                if (selectedGasStation!="All") {
                list = list.Where(s => s.getName() == (selectedGasStation))
                                  .ToList<GasStations>();
                }

            foreach (GasStations stations in list)
            {
                List<String> row = new List<String>();
                foreach (var station in stations)
                {
                    row = new List<String>();
                    row.Add(station.getAddress() + " ");
                    row.Add(stations.getName());
    
                    for (int i = 0; i < gasTypes.Count(); i++)
                    {
                        row.Add(station.getPrices()[i]);
                    }
                    rows.Add(row);  
                }
            }  
            return rows;
        }
    }
}