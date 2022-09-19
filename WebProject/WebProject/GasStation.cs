using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject
{
    public class GasStation
    {
        public String name;
        
        public String[] location;

        public GasStation(String name, String[] location)
        {
            this.name = name;
            this.location = location;
        }

        // generate setters and getters
        public String getName()
        {
            return name;
        }

        public String[] getLocation()
        {
            return location;
        }

        public void setName(String name)
        {
            this.name = name;
        }
        public void setLocation(String[] location)
        {
            this.location = location;
        }
    }
}