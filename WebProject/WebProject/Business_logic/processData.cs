using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace WebProject
{
    public class ProcessData
    {
        public static List<HtmlTableRow> process(string selectedGasStation)
        {
            List<GasStations> list = GasData.getData();
            List<HtmlTableRow> rows = new List<HtmlTableRow>();
            List<string> keywords = new List<string> { "Location", "Compony" };
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
            
            
            
            
            HtmlTableRow firstRow = new HtmlTableRow();
            for (int i = 0; i < keywords.Count(); i++)
            {
                HtmlTableCell firstCell = new HtmlTableCell();
                firstCell.Controls.Add(new LiteralControl(keywords[i]));
                firstRow.Cells.Add(firstCell);
            }
            string[] gasTypes = File.ReadAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Gas Types.txt");
            for (int i = 0; i < gasTypes.Count(); i++)
            {
                HtmlTableCell firstCell = new HtmlTableCell();
                firstCell.Controls.Add(new LiteralControl(gasTypes[i]));
                firstRow.Cells.Add(firstCell);
            }
            rows.Add(firstRow);
            
                if (selectedGasStation!="All") {
                list = list.Where(s => s.getName() == (selectedGasStation+" "))
                                  .ToList<GasStations>();
                }


            foreach (GasStations stations in list)
            {
                HtmlTableRow row = new HtmlTableRow();
                foreach (var station in stations)
                {
                    row = new HtmlTableRow();
                    HtmlTableCell cell = new HtmlTableCell();
                    HtmlTableCell cell2 = new HtmlTableCell();
                    cell2.Controls.Add(new LiteralControl(station.getAddress() + " "));
                    row.Cells.Add(cell2);
                    cell.Controls.Add(new LiteralControl(stations.getName()));
                    row.Cells.Add(cell);
    
                    for (int i = 0; i < gasTypes.Length; i++)
                    {
                        HtmlTableCell cell3 = new HtmlTableCell();
                        cell3.Controls.Add(new LiteralControl(station.prices[i]));
                        row.Cells.Add(cell3);
                    }
                    rows.Add(row);  
                }

            }  
            return rows;
        }
    }
}