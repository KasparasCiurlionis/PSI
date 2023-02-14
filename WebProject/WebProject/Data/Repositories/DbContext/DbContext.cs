using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Data.Repositories.DbContext
{
    public class DbContext
    {
        public class GasTypes
        {
            public int LocationID { get; set; }
            public string LocationName { get; set; }
            public int GasStationID { get; set; }

        }
        
        public class GasStations
        {
            public int GasStationID { get; set; }
            public string GasStationName { get; set; }
        }
        
        public class Locations
        {
            public int LocationID { get; set; }
            public string LocationName { get; set; }
            public int GasStationID { get; set; }       
        }
        
        public class Prices
        {
            public int PriceID { get; set; }
            public float Price { get; set; }
            public DateTime DateModified { get; set; }
            public int GasTypeID { get; set; }
            public int LocationID { get; set; }
                   
        }

        // now we need to add association between tables
        // we need to add foreign keys to GasTypes and Prices tables
        // like one-to-many relationship
        // so, we need to add foreign key to GasTypes table
        /*
         
         */
    }
}