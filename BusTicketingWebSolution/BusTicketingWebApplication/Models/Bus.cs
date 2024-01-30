using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusModelLibrary
{
    public class Bus
    {

        public int Id { get; set; }
        public string Type { get; set; }
        public float Cost { get; set; }
       
        public string Start { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public string End { get; set; }
        public string DriverName { get; set; }
        public int DriverAge { get; set; }
        public string DriverPhone { get; set; }
        public float DriverRating { get; set; }


    }
}