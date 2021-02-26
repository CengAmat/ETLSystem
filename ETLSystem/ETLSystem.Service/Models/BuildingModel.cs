using System;
namespace ETLSystem.Service.Models
{
    public class BuildingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Postcode { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double[] Coordinates { get; set; }
        public int Floorcount { get; set; }
        public int Floorarea { get; set; }

        //public int Id { get; set; }
        //public string Source { get; set; }
        //public string Name { get; set; }
        //public string Address { get; set; }
        //public float Lat { get; set; }
        //public float Lon { get; set; }
        //public int Floorcount { get; set; }
        //public int Floorarea { get; set; }
    }
}
