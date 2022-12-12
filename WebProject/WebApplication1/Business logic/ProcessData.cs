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
            /*
            // ******************** Olgierd's additional linq method usage with query
            // trying to create a linq ienumerable method and write down a query with select
            IEnumerable<GasStations> query =
                from gasStation in list
                where gasStation.getName().Contains(selectedGasStation)
                select gasStation;

            // print query
            foreach (GasStations gasStation in query)
            {
                HtmlTableRow row = new HtmlTableRow();
                HtmlTableCell cell = new HtmlTableCell();
                cell.InnerText = gasStation.getName();
                row.Cells.Add(cell);
                rows.Add(row);
                foreach (GasStation station in gasStation.getStations())
                {
                    HtmlTableRow row2 = new HtmlTableRow();
                    HtmlTableCell cell2 = new HtmlTableCell();
                    cell2.InnerText = station.getAddress();
                    row2.Cells.Add(cell2);
                    rows.Add(row2);
                    foreach (string price in station.getPrices())
                    {
                        HtmlTableRow row3 = new HtmlTableRow();
                        HtmlTableCell cell3 = new HtmlTableCell();
                        cell3.InnerText = price;
                        row3.Cells.Add(cell3);
                        rows.Add(row3);
                    }
                }
            }
            // ***********************
            */







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