using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LDInsurance.Models
{
    public class Report
    {
        public int ID { get; set; }
        public int? AccountID { get; set; }
        public Account Account { get; set; }
        public int? VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }
        public string Location { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Rate { get; set; }
        public int ClaimAmount { get; set; }
        public string Checker { get; set; }
        public bool Status { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
