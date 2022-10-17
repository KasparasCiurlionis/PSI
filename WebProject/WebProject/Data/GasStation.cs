using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProject.Data;

namespace WebProject
{
    public class GasStation : Location
    {
        public String address;
        public String[] prices;


        public GasStation(String address, String[] prices, Coords coords = new Coords()) : base(coords)
        { 
            this.address = address;
            this.prices = prices;
        }

        // generate setters and getters
        public String getAddress()
        {
            return address;
        }
  
        public void setAddress(String address)
        {
            this.address = address;
        }

        public String[] getPrices() {
            return prices;
        }

        public void setPrices(String[] prices)
        {
            this.prices = prices;
        }
    }
}