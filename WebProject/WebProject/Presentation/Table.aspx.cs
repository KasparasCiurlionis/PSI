using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class Table : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            List<HtmlTableRow> rows = processData.process();
            
            for (int i = 0; i < rows.Count; i++)
            {
                Table1.Rows.Add(rows[i]);
            }
            /*
            foreach (string file in Directory.EnumerateFiles(Server.MapPath("~/App_Data/data/"), "*.txt"))
            {
                string contents = File.ReadAllText(file);
                HtmlTableRow row = new HtmlTableRow();
                string[] words = contents.Split('\n');
                

               HtmlTableCell cell = new HtmlTableCell();
                cell.Controls.Add(new LiteralControl(Path.GetFileNameWithoutExtension(file) + " "));
                
                

                row.Cells.Add(cell);

                foreach (var word in words)
                {


                    // Create a new cell and add it to the HtmlTableRow 
                    // Cells collection'
                    HtmlTableCell cell2 = new HtmlTableCell();
                    cell2.Controls.Add(new LiteralControl(word + " "));
                        row.Cells.Add(cell2);
                    
                }
            }

                */
            // Add the row to the HtmlTable Rows collection.



        }
    }
}