using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LDInsurance.Models
{
    public class VehicleType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<Insurance> Insurances { get; set; }

    }
}
