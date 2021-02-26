using System;
namespace ETLSystem.Service.DataAccess
{
    public class Building
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int Floorcount { get; set; }
        public int Floorarea { get; set; }
        public string Hash { get; set; }
    }
}
