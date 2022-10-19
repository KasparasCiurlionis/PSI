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
using WebProject.Data.Repositories;

namespace WebProject
{
    public partial class Insertion : System.Web.UI.Page
    {
        private string current_location;

        List<string> gasInfo = new List<string>();
        protected void Page_Load(object sender, EventArgs e) // this line 
        {
           
            // once the page loads it should initialise with data once
            if (!IsPostBack)
            {
                // add some data into the GasStation dropdownlist
                // the data is located in app_data folder
                string path = Server.MapPath("~/App_Data/Gas Station.txt");
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
            // we need to get the path to the file
            string path = Server.MapPath("~/App_Data/data/" + selectedGasStation + ".txt");
            // we need to read the file
            string[] lines = File.ReadAllLines(path);
            // we need to clear the Location dropdownlist
            Location.Items.Clear();
            // we need to add the data from the file into the Location dropdownlist

            Regex rx = new Regex(@"[a-zA-Z]+ \w{1,2}. [0-9]{1,3}");
            Regex rx2 = new Regex(@"[a-zA-Z]+$");

            foreach(string line in lines)
            {
                if (rx.Match(line).Success || rx2.Match(line).Success)
                {
                    Location.Items.Add(line);
                }
                
            }

            // update the manual module
            ManualGasStation.Text = selectedGasStation;
            ManualLocation.Text = Location.SelectedValue;
            UpdateGasStationLabelView();
        }

        protected void GasStationLocationSelected(object sender, EventArgs e)
        {
            // update the current_location with the selected location from dropdownlist
            current_location = Location.SelectedItem.Value;
            // change the label text to that string
            ManualLocation.Text = current_location;
            Label1.Text ="Last selected Location: " +  current_location;
            UpdateGasStationLabelView();
        }

        protected void NullGasStationLabelView()
        {
            // update the manual module
            GasLabel1.Visible = false;
            GasLabel2.Visible = false;
            GasLabel3.Visible = false;
            GasLabel4.Visible = false;
            GasPrice1.Visible = false;
            GasPrice2.Visible = false;
            GasPrice3.Visible = false;
            GasPrice4.Visible = false;
            GasPrice1.Text = "";
            GasPrice2.Text = "";
            GasPrice3.Text = "";
            GasPrice4.Text = "";
            

        }
        protected void UpdateGasStationLabelView()
        {
            NullGasStationLabelView();
            var SelectedGasStationStatus = GetSelectedGasStationStatus();
            var gasTypes = SelectedGasStationStatus.GetGasTypes();
            // get the length of the array
            int length = gasTypes.Length;
            for(int i = 0; i < length; i++)
            {
                if(i == 0)
                {
                    GasLabel1.Text = gasTypes[i];
                    GasLabel1.Visible = true;
                    GasPrice1.Visible = true;
                }
                if (i == 1)
                {
                    GasLabel2.Text = gasTypes[i];
                    GasLabel2.Visible = true;
                    GasPrice2.Visible = true;
                    
                }
                if (i == 2)
                {
                    GasLabel3.Text = gasTypes[i];
                    GasLabel3.Visible = true;
                    GasPrice3.Visible = true;
                    
                }
                if (i == 3)
                {
                    GasLabel4.Text = gasTypes[i];
                    GasLabel4.Visible = true;
                    GasPrice4.Visible = true;
                }
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

        protected void ReadInput()
        {
            if (AutoTextBox1.Text == "") // if TextBox output from OCR is empty
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

            }
        }

        protected void PriceValidation(string gasPrice)
        {
            Regex rx = new Regex(@"(\d\.\d{3}?){1}$");

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
                gasInfo.Add("-");
            }
        }


        // create a function GetSelectedGasStationStatus() that returns SelectedGasStationStatus
        private SelectedGasStationStatus GetSelectedGasStationStatus()
        {
            // get the selected gas station from the dropdownlist
            string selectedGasStation = GasStation.SelectedValue;
            // create a switch statement
            switch (selectedGasStation)
            {
                case "Circle K":
                    return SelectedGasStationStatus.CircleKGas;
                case "Neste Lietuva":
                    return SelectedGasStationStatus.NesteGas;
                case "Viada":
                    return SelectedGasStationStatus.Viada;
                case "Baltic Petroleum":
                    return SelectedGasStationStatus.BalticPetroleum;
                case "Alauša":
                    return SelectedGasStationStatus.Alausa;
                default:
                    throw new ArgumentException("Unknown gas station status");
            }
        }
        private static GasStationItemContainer<string, string, DateTime>[] FillGenericContainers(string[] gasTypes, GasStationItemContainer<string, string, DateTime>[] genericContainers, List<string> gasPrices)
        {
            for (int i = 0; i < gasTypes.Length; i++)
            {
                genericContainers[i] = new GasStationItemContainer<string, string, DateTime>();
                genericContainers[i].Item1 = gasTypes[i];
                if (gasPrices[i] == "-") // realizing this contition in case of future changes
                {
                    genericContainers[i].Item2 = "-";
                }
                else
                {
                    genericContainers[i].Item2 = gasPrices[i];
                }
                genericContainers[i].Item3 = DateTime.Now;
                
            }
            return genericContainers;
        }

        private static List<string> UpdateFileInformation(List<string> fileInformation, GasStationDataContainer<string, GasStationItemContainer<string, string, DateTime>[]> gasStationDataContainer)
        {
            // lets find the index of the location
            int index = fileInformation.IndexOf(gasStationDataContainer.Item1);
            // now lets update the data
            for (int i = 0; i < gasStationDataContainer.Item2.Length; i++)
            {
                fileInformation[index + i + 1] = gasStationDataContainer.Item2[i].Item1 + " " + gasStationDataContainer.Item2[i].Item2 + " " + gasStationDataContainer.Item2[i].Item3;
            }


            return fileInformation;
        }


        protected void EditFileInformation(List<string> gasInfo)
        {
            string SelectedGasStation = GasStation.SelectedValue;
            var SelectedGasStationStatus = GetSelectedGasStationStatus();
            var gasTypes = SelectedGasStationStatus.GetGasTypes();

           
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
            
            GasStationDataContainer<string, GasStationItemContainer<string, string, DateTime>[]> gasStationDataContainer = new GasStationDataContainer<string, GasStationItemContainer<string, string, DateTime>[]>();
            GasStationItemContainer<string, string, DateTime>[] genericContainers = new GasStationItemContainer<string, string, DateTime>[gasTypes.Length];

            genericContainers = FillGenericContainers(genericContainers: genericContainers, gasPrices: gasInfo, gasTypes: gasTypes); // named argument usage of : gasTypes, genericContainers, gasInfo            gasStationDataContainer.Item1 = Location.SelectedValue; ;
            gasStationDataContainer.Item2 = genericContainers;
            gasStationDataContainer.Item1 = Location.SelectedValue;

            fileInformation = UpdateFileInformation(fileInformation, gasStationDataContainer);

            string path2 = Server.MapPath("~/App_Data/data/" + "temp.txt");
            using (StreamWriter writer = new StreamWriter(path2))
            {
                foreach(var line in fileInformation)
                {
                    writer.WriteLine(line);
                }
            }

            //deletes temporaty file and saves data to the selected gass stations' one
            if (File.Exists(path) && File.Exists(path2))
            {
                File.Delete(path);
                File.Move(path2, path);
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
                string PostOut = ParseGasDataFromPicture.RestPost(module_id, fileSavePath);
                string GetOut = ParseGasDataFromPicture.RestGet(module_id, PostOut);
                Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
                data = ParseGasDataFromPicture.returnValues(GetOut);
                // create a function: CalculateDictionaryKeyLength
                int len = 0;
                len = CalculateDictionaryKeyLength(data);
                len = InitAutoView(len);
                FillAutoView(data, len);
            }

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
        public void Btndiscard_Click(object sander, EventArgs e)
        {
            // this should empty all the view
            EmptyAutoView();
            RemoveAutoView();
            // make everything not visible
            // change boolean value to false

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
        public int CalculateDictionaryKeyLength(Dictionary<string, List<string>> data)
        {
            int len = 0;
            foreach (KeyValuePair<string, List<string>> entry in data)
            {
                len++;
            }

            return len;
        }
    }

}