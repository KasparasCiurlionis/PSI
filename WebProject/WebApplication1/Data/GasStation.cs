using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Data;

namespace WebApplication1
{
    [Serializable] 
    public class GasStation : Location, IGasStation
    {
        public String address;
        public String[] prices;
        public int id;

        /*
        public GasStation(String address, String[] prices, Coords coords = new Coords(), int id = 0) : base(coords)
        { 
            this.address = address;
            this.prices = prices;
            this.id = id;
        }
        */

        public int getID()
        {
            return id;
        }

        public void setID(int id)
        {
            this.id = id;
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
    
    public interface IGasStation
    {
        int getID();
        void setID(int id);
        String getAddress();
        void setAddress(String address);
        String[] getPrices();
        void setPrices(String[] prices);
    }
}