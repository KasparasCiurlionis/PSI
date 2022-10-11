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
        public static List<HtmlTableRow> process() {
            List<GasStation> list = GasData.getData();
            List<HtmlTableRow> rows = new List<HtmlTableRow>();
            foreach (GasStation station in list) {
                HtmlTableRow row = new HtmlTableRow();
                HtmlTableCell cell = new HtmlTableCell();
                cell.Controls.Add(new LiteralControl(station.name));
                row.Cells.Add(cell);

                foreach (var word in station.location)
                {
                    HtmlTableCell cell2 = new HtmlTableCell();
                    cell2.Controls.Add(new LiteralControl(word + " "));
                    row.Cells.Add(cell2);
                }
                rows.Add(row);
            }
            return rows;
        }
    }
}