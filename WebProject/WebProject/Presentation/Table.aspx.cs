using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class Table : System.Web.UI.Page
    {
        public string selectedGasStation;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            

            if (!IsPostBack)
            {
                List<HtmlTableRow> rows = processData.process("All");

               

                // add some data into the GasStation dropdownlist
                // the data is located in app_data folder
                string path = Server.MapPath("~/App_Data/Gas Station.txt");
                string[] lines = File.ReadAllLines(path);
                GasStation.Items.Add("All");
                foreach (string line in lines)
                {
                    GasStation.Items.Add(line);
                }
                GasStationSelected(sender, e);
            }

        }

        protected void GasStationSelected(object sender, EventArgs e)
        {
            // once we selected a proper GasStation, DropDownList Location should be updated
            // we need to check what is selected
            selectedGasStation = GasStation.SelectedValue;
            List<HtmlTableRow> rows = processData.process(selectedGasStation);

            for (int i = 0; i < rows.Count; i++)
            {
                Table1.Rows.Add(rows[i]);
            }

        }

        
    }
}