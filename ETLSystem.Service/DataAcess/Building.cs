using System;
namespace ETLSystem.Service.DataAccess
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public int Floorcount { get; set; }
        public int Floorarea { get; set; }
    }
}
