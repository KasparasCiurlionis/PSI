using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace WebProject
{
    public partial class Table : System.Web.UI.Page
    {
        public string selectedGasStation;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<HtmlTableRow> rows = ProcessData.process("All", new GasStation());

                

                // add some data into the GasStation dropdownlist
                // the data is located in app_data folder

                List<String> lines = GasData.getBrand(); 
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
            


            var client2 = new RestClient("http://localhost:5050"); 
            var request2 = new RestRequest("/GasStationTable?id=" + GasStation.Items.IndexOf(GasStation.Items.FindByText(selectedGasStation)), Method.Get);
            RestResponse response2 = client2.Execute(request2);
            List<List<string>> output = JsonConvert.DeserializeObject<List<List<string>>>(response2.Content);
            


            
            foreach (var rows in output)
            {
                HtmlTableRow row = new HtmlTableRow();
                foreach (var cells in rows)
                {
                    HtmlTableCell cell = new HtmlTableCell();
                    cell.Controls.Add(new LiteralControl(cells));
                    row.Cells.Add(cell);
                }
                Table1.Rows.Add(row);
            }
        }
    }
}