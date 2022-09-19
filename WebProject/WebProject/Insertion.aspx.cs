using System;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class Insertion : System.Web.UI.Page
    {
        private string current_location;
        protected void Page_Load(object sender, EventArgs e)
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
                    // once all the 
                    GasStationSelected(sender, e);
                }
                

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
            foreach(string line in lines)
            {
                Location.Items.Add(line);
            }
            
            
            

        }

        protected void GasStationLocationSelected(object sender, EventArgs e)
        {
            // update the current_location with the selected location from dropdownlist
            current_location = Location.SelectedItem.Value;
            // change the label text to that string
            Label1.Text ="Last selected Location: " +  current_location;
        }

        protected void Btnsave_Click(object sender, EventArgs e)
        {
            // an image is selected in Fileholder FileUpload
            // we need to get the image
            HttpPostedFile postedFile = FileHolder.PostedFile;
            // we need to get the name of the image
            string fileName = Path.GetFileName(postedFile.FileName);
            // EDIT: Next we need to scan an image and search for numbers
           

        }
    }
    
}