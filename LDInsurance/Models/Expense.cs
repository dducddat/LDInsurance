using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LDInsurance.Models
{
    public class Expense
    {
        public int ID { get; set; }
        public int? InsuranceRegistrationID { get; set; }
        public InsuranceRegistration InsuranceRegistration { get; set; }
        public int? ReportID { get; set; }
        public Report Report { get; set; }
        public string Confirm { get; set; }       

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public bool Status { get; set; }
    }
}
