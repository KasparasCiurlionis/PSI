using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace WebProject
{
    public class processData
    {
        public static List<HtmlTableRow> process()
        {
            List<GasStation> list = GasData.getData();
            List<HtmlTableRow> rows = new List<HtmlTableRow>();
            List<string> keywords = new List<string> { "Location", "Compony" };
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
            foreach (GasStation station in list)
            {

                string[] types = new string[gasTypes.Length];
                HtmlTableRow row = new HtmlTableRow();
                foreach (var word in station.location)
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
                        if (row.Controls.Count > 0)
                        {
                            for (int i = 0; i < gasTypes.Length; i++)
                            {
                                HtmlTableCell cell3 = new HtmlTableCell();
                                cell3.Controls.Add(new LiteralControl(types[i]));
                                row.Cells.Add(cell3);
                            }
                            rows.Add(row);
                        }
                        row = new HtmlTableRow();
                        HtmlTableCell cell = new HtmlTableCell();
                        HtmlTableCell cell2 = new HtmlTableCell();
                        cell2.Controls.Add(new LiteralControl(word + " "));
                        row.Cells.Add(cell2);
                        cell.Controls.Add(new LiteralControl(station.name));
                        row.Cells.Add(cell);

                    }
                }

            }
            return rows;
        }
    }
}