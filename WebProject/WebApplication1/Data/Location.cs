using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApplication1
{
    public class Location
    {
        Coords coords;
        public Location(Coords coords = new Coords())
        {
            this.coords = coords;
        }

        public Coords getCoords()
        {
            return coords;
        }

        public void setCoords(Coords coords)
        {
            this.coords = coords;
        }
    }

    public struct Coords
    {
        public Coords(double x=0, double y=0)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }

        public override string ToString() => $"({X}, {Y})";
    }
}