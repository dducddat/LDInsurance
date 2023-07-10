﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LDInsurance.Models
{
    public class InsuranceType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public List<Insurance> Insurances { get; set; }
    }
}
