using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ1.Models
{
    public class CanvasPoint
    {
        public double LatLonX { get; set; }
        public double LatLonY { get; set; }
        public double CanvasX { get; set; }
        public double CanvasY { get; set; }
        public int GridX { get; set; }
        public int GridY { get; set; }
    }
}
