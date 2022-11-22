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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using WebProject.Business_logic;
using Microsoft.Ajax.Utilities;
using System.Diagnostics;
using System.Threading;

namespace WebProject
{
    public partial class Insertion : System.Web.UI.Page
    {
        private string current_location;

        private static Lazy<List<string>> lazyGasInfo = new Lazy<List<string>>();
        protected void Page_Load(object sender, EventArgs e) // this line 
        {

            // once the page loads it should initialise with data once
            if (!IsPostBack)
            {
                GasStation.Items.Clear();
                foreach (var item in RetrieveGasStations.getGasStations())
                {
                    // now we can add items to the DropDownList
                    GasStation.Items.Add(item.getName());
                }
                GasStationSelected(sender, e);

            }
        }

        protected void GasStationSelected(object sender, EventArgs e)
        {
            string selectedGasStation = GasStation.SelectedValue;
            Location.Items.Clear();

            List<string> locations = new List<string>();
            int pkey = RetrieveGasStations.getGasStationID(selectedGasStation);
            var obj = RetrieveGasStationLocations.getGasStationLocations(selectedGasStation, pkey, new GasStation());

            // use enum to iterate through the array?
            foreach (var item in obj.getStations())
            {
                Location.Items.Add(item.getAddress());
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
            Label1.Text = "Last selected Location: " + current_location;
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
            for (int i = 0; i < length; i++)
            {
                if (i == 0)
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

            if (InputNotEmpty()) // we need to add the data to Lazy obj only then, when the input is not empty
            {
                lazyGasInfo.Value.AddRange(ReadAndGetInput());
                string SelectedGasStation = GasStation.SelectedValue;
                var SelectedGasStationStatus = GetSelectedGasStationStatus();
                var gasTypes = SelectedGasStationStatus.GetGasTypes();
                List<int> gasTypesListID = RetrieveGasStationLocationPrice.getGasTypesID(gasTypes);
                List<string> temp = lazyGasInfo.Value;
                List<float> gasInfoList = lazyGasInfo.Value.Select(float.Parse).ToList();
                // TO-DO: it works a bit incorrect: for example: we got 4 types overall, but photo (or user input) has 2 types filled
                // so this should pass a struct of 2 types and 2 prices

                UpdateGasStationLocationPrice.updateGasStationLocationPrice(
                    RetrieveGasStations.getGasStationID(GasStation.Text),
                    RetrieveGasStationLocations.getGasStationLocationID(Location.Text),
                    gasTypesListID,
                    gasInfoList
                    );
                EmptyAutoView();
                RemoveAutoView();
                UpdateGasStationLabelView();
            }
        }


        protected List<string> ReadAndGetInput(List<string> gasInfo = null)
        {
            // if gasInfo is null, then create a new List<string>
            if (gasInfo == null)
            {
                gasInfo = new List<string>();
            }
            if (AutoTextBox1.Text == "") // if TextBox output from OCR is empty
            {
                gasInfo = PriceValidation(GasPrice1.Text, gasInfo);
                gasInfo = PriceValidation(GasPrice2.Text, gasInfo);
                gasInfo =  PriceValidation(GasPrice3.Text, gasInfo);
                gasInfo = PriceValidation(GasPrice4.Text, gasInfo);
            }
            else
            {
                gasInfo = PriceValidation(AutoTextBox1.Text, gasInfo);
                gasInfo = PriceValidation(AutoTextBox2.Text, gasInfo);
                gasInfo = PriceValidation(AutoTextBox3.Text, gasInfo);
                gasInfo = PriceValidation(AutoTextBox4.Text, gasInfo);
            }

            return gasInfo;
        }

        protected bool InputNotEmpty()
        {

            if (GasPrice1.Text != "" || GasPrice2.Text != "" || GasPrice3.Text != "" || GasPrice4.Text != ""
          || AutoTextBox1.Text != "" || AutoTextBox2.Text != "" || AutoTextBox3.Text != "" || AutoTextBox4.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    

        public List<string> PriceValidation(string gasPrice, List<string> gasInfo)
        {
            Regex rx = new Regex(@"(\d\.\d{3}?){1}$");

            if (rx.Match(gasPrice).Success)
            {
                gasInfo.Add(gasPrice);
            }
            else if (gasPrice == "")
            {
                gasInfo = null;
            }
            else if(!rx.Match(gasPrice).Success)
            {
                gasInfo = null;
                InvalidInput();
            }
            return gasInfo;
        }

        public void InvalidInput()
        {
            Label2.Visible = true;
        }

        // create a function GetSelectedGasStationStatus() that returns SelectedGasStationStatus
        private SelectedGasStationStatus GetSelectedGasStationStatus()
        {
            // get the selected gas station from the dropdownlist
            string selectedGasStation = GasStation.SelectedValue;
            // create a switch statement
            switch (selectedGasStation)
            {
                case "Circle_K":
                    return SelectedGasStationStatus.CircleKGas;
                case "Neste":
                    return SelectedGasStationStatus.NesteGas;
                case "Viada":
                    return SelectedGasStationStatus.Viada;
                case "Baltic_Petroleum":
                    return SelectedGasStationStatus.BalticPetroleum;
                case "Alauša":
                    return SelectedGasStationStatus.Alausa;
                default:
                    throw new ArgumentException("Unknown gas station status");
            }
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            Label1.Text = "Select Location";
        }

        public EventHandler ClickEvent;

        protected void UploadFile(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = FileHolder.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileSavePath = Server.MapPath("~/App_Data/data/" + fileName);

            UploadHandling<Var> uploadHandling= new UploadHandling<Var>(); // publisher
            MessageService mesService = new MessageService(); // subscriber
            uploadHandling.FileUploaded += mesService.FileUpload;

            uploadHandling.Upload(fileName, fileSavePath, GasStation.SelectedValue, FileHolder.PostedFile);
                       
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

        public void UpdateMessageToUser(string str)
        {
            Label2.Visible = true;
            Label2.Text = str;
        }
        public void ResetMessageToUser()
        {
            Label2.Visible = false;
            Label2.Text = "";
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
        }

        public void EmptyAutoView()
        {
            // we should empty : Labels and TextBox
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