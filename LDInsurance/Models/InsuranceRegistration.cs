using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LDInsurance.Models
{
    public class InsuranceRegistration
    {
        public int ID { get; set; }
        public int? AccountID { get; set; }
        public Account Account { get; set; }
        public int? VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }
        public int? InsuranceID { get; set; }
        public Insurance Insurance { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
