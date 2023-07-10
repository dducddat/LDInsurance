using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LDInsurance.Models
{
    public class ChangePass
    {
        public string OldPass { get; set; }
        public string NewPass { get; set; }
        public string NewPassConfirm { get; set; }
    }
}
