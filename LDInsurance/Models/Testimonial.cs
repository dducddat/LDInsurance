using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LDInsurance.Models
{
    public class Testimonial
    {
        public int ID { get; set; }
        public int? AccountID { get; set; }
        public Account Account { get; set; }
        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
