using System;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebProject.Data;

namespace WebProject
{
    public partial class Insertion : System.Web.UI.Page
    {
        private string current_location;
        private string chosenGasStation;

        List<string> gasInfo = new List<string>();
        protected void Page_Load(object sender, EventArgs e) // this line 
        {
           
            // once the page loads it should initialise with data once
            if (!IsPostBack)
            {
                // add some data into the GasStation dropdownlist
                // the data is located in app_data folder
                string path = Server.MapPath("~/App_Data/data/Gas Station.txt");
                string[] lines = File.ReadAllLines(path);
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
            string selectedGasStation = GasStation.SelectedValue;
            chosenGasStation = GasStation.SelectedValue;
            // we need to get the path to the file
            string path = Server.MapPath("~/App_Data/data/" + selectedGasStation + ".txt");
            // we need to read the file
            string[] lines = File.ReadAllLines(path);
            // we need to clear the Location dropdownlist
            Location.Items.Clear();
            // we need to add the data from the file into the Location dropdownlist
            foreach(string line in lines)
            {
                Location.Items.Add(line);
            }

            // update the manual module
            ManualGasStation.Text = selectedGasStation;
            ManualLocation.Text = Location.SelectedValue;
        }

        protected void GasStationLocationSelected(object sender, EventArgs e)
        {
            // update the current_location with the selected location from dropdownlist
            current_location = Location.SelectedItem.Value;
            // change the label text to that string
            ManualLocation.Text = current_location;
            Label1.Text ="Last selected Location: " +  current_location;
        }

        protected void Btnsave_Click(object sender, EventArgs e)
        {
            Label2.Visible = false;
            //Reads all the gas types and prices
            ReadInput();

            // an image is selected in Fileholder FileUpload
            // we need to get the image
            HttpPostedFile postedFile = FileHolder.PostedFile;
            // we need to get the name of the image
            string fileName = Path.GetFileName(postedFile.FileName);
            // EDIT: Next we need to scan an image and search for numbers
           

        }

        protected void ReadInput()
        {
            PriceValidation(GasPrice1.Text);
            PriceValidation(GasPrice2.Text);
            PriceValidation(GasPrice3.Text);
            PriceValidation(GasPrice4.Text);

            //edits the existing file
            EditFileInformation(gasInfo);
        }

        protected void PriceValidation(string gasPrice)
        {
            Regex rx = new Regex(@"(\d\,\d{3}?){1}$");

            if (rx.Match(gasPrice).Success)
            {
                gasInfo.Add(gasPrice);
            }
            else if(gasPrice == "")
            {
                gasInfo.Add("-");
            }
            else
            {
                Label2.Visible = true;
            }
        }

        protected void EditFileInformation(List<string> gasInfo)
        {
            string path = Server.MapPath("~/App_Data/data/" + GasStation.SelectedValue + ".txt");
            List<string> fileInformation = new List<string>();

            using (StreamReader reader = new StreamReader(path))
            {
                String line = reader.ReadLine();
                while (line != null)
                {
                    fileInformation.Add(line);
                    line = reader.ReadLine();
                }
            }

            string location = Location.SelectedValue;
            int index = fileInformation.IndexOf(location);

            var info = new List<GasInfo>()
            {
                new GasInfo() {gasType = "95", gasPrice = gasInfo[0], lastUpdate = DateTime.Now},
                new GasInfo() {gasType = "98", gasPrice = gasInfo[1], lastUpdate = DateTime.Now},
                new GasInfo() {gasType = "D", gasPrice = gasInfo[2], lastUpdate = DateTime.Now},
                new GasInfo() {gasType = "GAS", gasPrice = gasInfo[3], lastUpdate = DateTime.Now}
            };

            
            foreach(var gasInformation in info)
            {
                if(gasInformation.gasType == "95" && gasInformation.gasPrice != "-")
                {
                    fileInformation[index + 1] = "95 " + gasInfo[0] + " " + DateTime.Now;
                }

                if(gasInformation.gasType == "98" && gasInformation.gasPrice != "-")
                {
                    fileInformation[index + 2] = "98 " + gasInfo[1] + " " + DateTime.Now;
                }

                if (gasInformation.gasType == "D" && gasInformation.gasPrice != "-")
                {
                    fileInformation[index + 3] = "D " + gasInfo[2] + " " + DateTime.Now;
                }

                if (gasInformation.gasType == "GAS" && gasInformation.gasPrice != "-")
                {
                    fileInformation[index + 4] = "GAS " + gasInfo[3] + " " + DateTime.Now;
                }
            }

            string path2 = Server.MapPath("~/App_Data/data/" + "temp.txt");
            using (StreamWriter writer = new StreamWriter(path2))
            {
                foreach(var line in fileInformation)
                {
                    writer.WriteLine(line);
                }
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            Label1.Text = "Select Location";
        }
    }
    
}