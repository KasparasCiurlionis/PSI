using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Data.Repositories.EFQuerying
{
    public class Functions
    {
        // right there we need to store the functions
        // we know that EF Core has a new feature in LINQ-to-Entities where we can include c# functions in the query
        // lets create some functions

        // 1st function, get each gas station one location with the best fuel price
        // we need to get the best fuel price for each gas station
        // what will be the return type? we can make an object

        public static List<Price> getEachLocationBestPrice()
        {
            var context = new Model1();
            // we need to get only one price
            var query = context.Prices
                .GroupBy(p => p.LocationID)
                .Select(g => g.OrderBy(p => p.Price1).FirstOrDefault())
                .ToList();


            return query;
            // how to test this function? in main, type: var test = Functions.getEachGasStationBestPrice();
        }
        public static List<GasStation> getGasStationList()
        {

            var context = new Model1();
            var query = context.GasStations.ToList();
            return query;
        }

        public static List<Location> getGasStationLocationList(int pkey)
        {

            var context = new Model1();
            var query = context.Locations.Where(l => l.GasStationID == pkey).ToList();
            return query;
        }

    
    }
}