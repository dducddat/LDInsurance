using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LDInsurance.Models
{
    public class Insurance
    {
        public int ID { get; set; }
        public int? InsuranceTypeID { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public int? VehicleTypeID { get; set; }
        public VehicleType VehicleType { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Period { get; set; }
        public bool Status { get; set; }
        public List<InsuranceRegistration> InsuranceRegistrations { get; set; }
        public List<TransactionHistory> TransactionHistories { get; set; }
    }
}
