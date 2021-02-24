using System;
namespace ETLSystem.Service.Database
{
    public class DataS1
    {
        public Guid Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public int Floorcount { get; set; } 
    }
}
