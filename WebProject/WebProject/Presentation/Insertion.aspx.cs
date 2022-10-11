using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebProject.Business_logic;
using WebProject.Data;

namespace WebProject
{
    public partial class Insertion : System.Web.UI.Page
    {
        private string current_location;
        private string chosenGasStation;

        List<string> gasInfo = new List<string>();
        public ExistingGasModule gasModuleProcess = new ExistingGasModule();
        
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
            foreach (string line in lines)
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
            Label1.Text = "Last selected Location: " + current_location;
        }

        protected void Btnsave_Click(object sender, EventArgs e)
        {
            Label2.Visible = false;
            //Reads all the gas types and prices
            ReadInput();

            // an image is selected in FileUpload
            // we need to get the image
            HttpPostedFile postedFile = FileHolder.PostedFile;
            // we need to get the name of the image
            string fileName = Path.GetFileName(postedFile.FileName);
            // EDIT: Next we need to scan an image and search for numbers


        }

        protected void ReadInput()
        {
            if (AutoTextBox1.Text == "")
            {
                PriceValidation(GasPrice1.Text);
                PriceValidation(GasPrice2.Text);
                PriceValidation(GasPrice3.Text);
                PriceValidation(GasPrice4.Text);

                //edits the existing file
                EditFileInformation(gasInfo);
            }
            else
            {
                PriceValidation(AutoTextBox1.Text);
                PriceValidation(AutoTextBox2.Text);
                PriceValidation(AutoTextBox3.Text);
                PriceValidation(AutoTextBox4.Text);

                //edits the existing file
                EditFileInformation(gasInfo);
                EmptyAutoView();
                RemoveAutoView();
                gasModuleProcess.AutoModuleWorking = false;

            }
        }

        protected void PriceValidation(string gasPrice)
        {
            Regex rx = new Regex(@"(\d\.\d{3}?){1}$");

            if (rx.Match(gasPrice).Success)
            {
                gasInfo.Add(gasPrice);
            }
            else if (gasPrice == "")
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


            foreach (var gasInformation in info)
            {
                if (gasInformation.gasType == "95" && gasInformation.gasPrice != "-")
                {
                    fileInformation[index + 1] = "95 " + gasInfo[0] + " " + DateTime.Now;
                }

                if (gasInformation.gasType == "98" && gasInformation.gasPrice != "-")
                {
                    fileInformation[index + 2] = "98 " + gasInfo[1] + " " + DateTime.Now;
                }

                if (gasInformation.gasType == "D" && gasInformation.gasPrice != "-")
                {
                    fileInformation[index + 3] = "D " + gasInfo[2] + " " + DateTime.Now;
                }

                if (gasInformation.gasType == "GAS" || gasInformation.gasType == "LPG" && gasInformation.gasPrice != "-")
                {
                    fileInformation[index + 4] = "GAS " + gasInfo[3] + " " + DateTime.Now;
                }
            }

            string path2 = Server.MapPath("~/App_Data/data/" + "temp.txt");
            using (StreamWriter writer = new StreamWriter(path2))
            {
                foreach (var line in fileInformation)
                {
                    writer.WriteLine(line);
                }
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            Label1.Text = "Select Location";
        }

        protected void UploadFile(object sender, EventArgs e)
        {
            // getting absolute path
            HttpPostedFile postedFile = FileHolder.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileSavePath = Server.MapPath("~/App_Data/data/" + fileName);
            // we need to save the file in this App_data
            FileHolder.SaveAs(fileSavePath);
            // current gas station selected
            string gasStation = GasStation.SelectedValue;
            // create a module

            string module_id = ExistingGasModule.getModule(gasStation);
            if (module_id == null)
            {
                Label2.Visible = true;
                Label2.Text = "Error, selected Gas Station Automated Module does not exist";
            }
            else// module ID exists
            {
                gasModuleProcess.AutoModuleWorking = true;
                string PostOut = AutoInsert.RestPost(module_id, fileSavePath);
                string GetOut = AutoInsert.RestGet(module_id, PostOut);
                Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
                data = AutoInsert.returnValues(GetOut);
                // create a function: CalculateDictionaryKeyLength
                int len = 0;
                len = CalculateDictionaryKeyLength(data);
                len = InitAutoView(len);
                FillAutoView(data, len);
            }

        }

        public int CalculateDictionaryKeyLength(Dictionary<string, List<string>> data)
        {
            int len = 0;
            foreach (KeyValuePair<string, List<string>> entry in data)
            {
                len++;
            }

            return len;
        }

        public void FillAutoView(Dictionary<string, List<string>> data, int len)
        {
            // we should fill: Labels and TextBoxes
            if (len >= 1)
            {
                // use functions returnDataKey and returnDataValue
                // key should be written in Label, Value in TextBox
                AutoTextLabel1.Text = returnDataKey(data, 0);
                AutoTextBox1.Text = returnDataValue(data, 0);
            }
            if (len >= 2)
            {
                AutoTextLabel2.Text = returnDataKey(data, 1);
                AutoTextBox2.Text = returnDataValue(data, 1);
            }
            if (len >= 3)
            {
                AutoTextLabel3.Text = returnDataKey(data, 2);
                AutoTextBox3.Text = returnDataValue(data, 2);
            }
            if (len >= 4)
            {
                AutoTextLabel4.Text = returnDataKey(data, 3);
                AutoTextBox4.Text = returnDataValue(data, 3);
            }

        }

        public string returnDataValue(Dictionary<string, List<string>> data, int index)
        {
            string value = "";
            int i = 0;
            // we know that every key has different value, it may be even empty
            // we need to check if value is empty
            foreach (KeyValuePair<string, List<string>> entry in data)
            {
                if (i == index)
                {
                    foreach (string val in entry.Value)
                    {
                        value = val;
                        break;
                    }
                }
                i++;
            }

            return value;
        }

        public string returnDataKey(Dictionary<string, List<string>> data, int index)
        {
            string key = "";
            int i = 0;
            foreach (KeyValuePair<string, List<string>> entry in data)
            {
                if (i == index)
                {
                    key = entry.Key;
                }
                i++;
            }
            return key;
        }

        public int InitAutoView(int len)
        {
            int amount = 0;
            btndiscard.Visible = true;
            // we can make visible maximum amount of 4 labels and textBoxes:
            if (1 <= len)
            {
                AutoTextLabel1.Visible = true;
                AutoTextBox1.Visible = true;
                amount++;
            }
            if (2 <= len)
            {
                AutoTextLabel2.Visible = true;
                AutoTextBox2.Visible = true;
                amount++;
            }
            if (3 <= len)
            {
                AutoTextLabel3.Visible = true;
                AutoTextBox3.Visible = true;
                amount++;
            }
            if (4 <= len)
            {
                AutoTextLabel4.Visible = true;
                AutoTextBox4.Visible = true;
                amount++;
            }
            return amount;
        }

        public void EmptyAutoView()
        {
            // we should empty : Labels and TextBoxe
            AutoTextLabel1.Text = "";
            AutoTextBox1.Text = "";
            AutoTextLabel2.Text = "";
            AutoTextBox2.Text = "";
            AutoTextLabel3.Text = "";
            AutoTextBox3.Text = "";
            AutoTextLabel4.Text = "";
            AutoTextBox4.Text = "";

        }

        public void Btndiscard_Click(object sander, EventArgs e)
        {
            // this should empty all the view
            EmptyAutoView();
            RemoveAutoView();
            // make everything not visible
            // change boolean value to false
            gasModuleProcess.AutoModuleWorking = false;

        }
        public void RemoveAutoView()
        {
            AutoTextBox1.Visible = false;
            AutoTextLabel1.Visible = false;
            AutoTextBox2.Visible = false;
            AutoTextLabel2.Visible = false;
            AutoTextBox3.Visible = false;
            AutoTextLabel3.Visible = false;
            AutoTextBox4.Visible = false;
            AutoTextLabel4.Visible = false;
            btndiscard.Visible = false;
        }
    }

    }