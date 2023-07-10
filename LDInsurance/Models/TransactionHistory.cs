using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LDInsurance.Models
{
    public class TransactionHistory
    {
        public int ID { get; set; }
        public int? AccountID { get; set; }
        public Account Account { get; set; }
        public int? InsuranceID { get; set; }
        public Insurance Insurance { get; set; }
        public int? InsuranceRegistrationID { get; set; }
        public InsuranceRegistration InsuranceRegistration { get; set; }
        public string Card { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }
    }
}
