using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Data
{
    public class GasInfo
    {
        public String gasType;
        public System.DateTime lastUpdate;
        public String gasPrice;

        public String getGasType()
        {
            return gasType;
        }

        public System.DateTime getLastUpdate()
        {
            return lastUpdate;
        }

        public String getGasPrice()
        {
            return gasPrice;
        }

        public void setGasType(String gasType)
        {
            this.gasType = gasType;
        }

        public void setLastUpdate(System.DateTime lastUpdate)
        {
            this.lastUpdate = lastUpdate;
        }

        public void setGasPrice(String gasPrice)
        {
            this.gasPrice = gasPrice;
        }

    }
}