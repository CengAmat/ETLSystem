using System;
namespace ETLSystem.Service.Database
{
    public class DataS2
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public float Coordinates { get; set; }
        public int Floorarea { get; set; }
    }
}
