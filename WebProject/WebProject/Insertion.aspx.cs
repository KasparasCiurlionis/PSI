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

namespace WebProject
{
    public partial class Insertion : System.Web.UI.Page
    {
        private string current_location;
        private string chosenGasStation;
        private int typeCount;

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
                GasTypeCountEntered(sender, e);
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

        protected void GasTypeCountEntered(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 1; i <= 5; ++i)
                {
                    GasTypeCount.Items.Add(i.ToString());
                }
            }

            string gasTypeCount = GasTypeCount.SelectedValue;
            typeCount = Int16.Parse(gasTypeCount);

            //loading dropdownlists and textboxes for the chosen number of gas types
            if (Int16.Parse(gasTypeCount) == 1)
            {
                InitializeWithData(1);
                GasType1.Visible = true;
                GasPrice1.Visible = true;

                GasType2.Visible = false;
                GasPrice2.Visible = false;

                GasType3.Visible = false;
                GasPrice3.Visible = false;

                GasType4.Visible = false;
                GasPrice4.Visible = false;

                GasPrice5.Visible = false;
                GasPrice5.Visible = false;

            }
            else if(Int16.Parse(gasTypeCount) == 2)
            {
                InitializeWithData(2);

                GasType1.Visible = true;
                GasPrice1.Visible = true;

                GasType2.Visible = true;
                GasPrice2.Visible = true;

                GasType3.Visible = false;
                GasPrice3.Visible = false;

                GasType4.Visible = false;
                GasPrice4.Visible = false;

                GasType5.Visible = false;
                GasPrice5.Visible = false;
            }
            else if(Int16.Parse(gasTypeCount) == 3)
            {
                InitializeWithData(3);

                GasType1.Visible = true;
                GasPrice1.Visible = true;

                GasType2.Visible = true;
                GasPrice2.Visible = true;

                GasType3.Visible = true;
                GasPrice3.Visible = true;

                GasType4.Visible = false;
                GasPrice4.Visible = false;

                GasType5.Visible = false;
                GasPrice5.Visible = false;
            }
            else if (Int16.Parse(gasTypeCount) == 4)
            {
                InitializeWithData(4);

                GasType1.Visible = true;
                GasPrice1.Visible = true;

                GasType2.Visible = true;
                GasPrice2.Visible = true;

                GasType3.Visible = true;
                GasPrice3.Visible = true;

                GasType4.Visible = true;
                GasPrice4.Visible = true;

                GasType5.Visible = false;
                GasPrice5.Visible = false;
            }
            else if (Int16.Parse(gasTypeCount) == 5)
            {
                InitializeWithData(5);

                GasType1.Visible = true;
                GasPrice1.Visible = true;

                GasType2.Visible = true;
                GasPrice2.Visible = true;

                GasType3.Visible = true;
                GasPrice3.Visible = true;

                GasType4.Visible = true;
                GasPrice4.Visible = true;

                GasType5.Visible = true;
                GasPrice5.Visible = true;
            }
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

        protected void InitializeWithData(int count)
        {
            if (!IsPostBack)
            {
                // add some data into the GasType dropdownlist
                // the data is located in app_data folder
                string path = Server.MapPath("~/App_Data/data/Gas Types.txt");
                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    GasType1.Items.Add(line);
                    GasType2.Items.Add(line);
                    GasType3.Items.Add(line);
                    GasType4.Items.Add(line);
                    GasType5.Items.Add(line);
                }
            }

        }
        protected void ReadInput()
        {
            switch (Int16.Parse(GasTypeCount.SelectedValue))
            {
                case 1:
                    PriceValidation(GasType1.SelectedValue, GasPrice1.Text);
                    break;

                case 2:
                    PriceValidation(GasType1.SelectedValue, GasPrice1.Text);
                    PriceValidation(GasType2.SelectedValue, GasPrice2.Text);
                    break;

                case 3:
                    PriceValidation(GasType1.SelectedValue, GasPrice1.Text);
                    PriceValidation(GasType2.SelectedValue, GasPrice2.Text);
                    PriceValidation(GasType3.SelectedValue, GasPrice3.Text);
                    break;

                case 4:
                    PriceValidation(GasType1.SelectedValue, GasPrice1.Text);
                    PriceValidation(GasType2.SelectedValue, GasPrice2.Text);
                    PriceValidation(GasType3.SelectedValue, GasPrice3.Text);
                    PriceValidation(GasType4.SelectedValue, GasPrice4.Text);
                    break;

                case 5:
                    PriceValidation(GasType1.SelectedValue, GasPrice1.Text);
                    PriceValidation(GasType2.SelectedValue, GasPrice2.Text);
                    PriceValidation(GasType3.SelectedValue, GasPrice3.Text);
                    PriceValidation(GasType4.SelectedValue, GasPrice4.Text);
                    PriceValidation(GasType5.SelectedValue, GasPrice5.Text);
                    break;
            }

            //edits the existing file
            EditFileInformation(gasInfo);
        }

        protected void PriceValidation(string gasType, string gasPrice)
        {
            Regex rx = new Regex(@"(\d\,\d{3}?){1}$");

            if (rx.Match(gasPrice).Success)
            {
                gasInfo.Add(gasType);
                gasInfo.Add(gasPrice);
            }
            else
            {
                Label2.Visible = true;
            }
        }

        protected void EditFileInformation(List<string> gasInfo)
        {
            string path = Server.MapPath("~/App_Data/data/" + GasStation.SelectedValue + ".txt");
            var lines = File.ReadAllLines(path).ToList();
            //TODO: figure out how to choose a location
            int a = 1;
            foreach(string s in gasInfo)
            {
                lines.Insert(a, s);
                a++;
            }

            //lines.Insert(1, gasPriceInfo);
            File.WriteAllLines(path, lines);
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            Label1.Text = "Select Location";
        }
    }
    
}