using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LDInsurance.Models
{
    public class Vehicle
    {
        public int ID { get; set; }
        public int? AccountID { get; set; }
        public Account Account { get; set; }
        public string OwnerName { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public int Rate { get; set; }
        public string VehicleNumber { get; set; }
        public int? VehicleTypeID { get; set; }
        public VehicleType VehicleType { get; set; }
        public bool Status { get; set; }
        public List<InsuranceRegistration> InsuranceRegistrations { get; set; }
        public List<Report> Reports { get; set; }
    }
}
