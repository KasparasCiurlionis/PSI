using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebProject
{
    public class ProcessData
    {

        public static List<HtmlTableRow> process(string selectedGasStation, IGasStation gasStation)
        {
            List<GasStations> list = GasData.getData(gasStation);
            List<HtmlTableRow> rows = new List<HtmlTableRow>();
            List<string> keywords = new List<string> { "Location", "Compony" };
           
            HtmlTableRow firstRow = new HtmlTableRow();
            for (int i = 0; i < keywords.Count(); i++)
            {
                HtmlTableCell firstCell = new HtmlTableCell();
                firstCell.Controls.Add(new LiteralControl(keywords[i]));
                firstRow.Cells.Add(firstCell);
            }
            string[] gasTypes = GasData.getTypes().ToArray();
            for (int i = 0; i < gasTypes.Count(); i++)
            {
                HtmlTableCell firstCell = new HtmlTableCell();
                firstCell.Controls.Add(new LiteralControl(gasTypes[i]));
                firstRow.Cells.Add(firstCell);
            }
            rows.Add(firstRow);
            
                if (selectedGasStation!="All") {
                list = list.Where(s => s.getName() == (selectedGasStation))
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
    
                    for (int i = 0; i < gasTypes.Count(); i++)
                    {
                        HtmlTableCell cell3 = new HtmlTableCell();
                        cell3.Controls.Add(new LiteralControl(station.getPrices()[i]));
                        row.Cells.Add(cell3);
                    }
                    rows.Add(row);  
                }
            }  
            return rows;
        }
    }
}