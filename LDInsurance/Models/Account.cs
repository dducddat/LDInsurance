using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LDInsurance.Models
{
    public class Account
    {
        
        public int ID { get; set; }

        [Required(ErrorMessage = "What's your Name?")]
        public string Name { get; set; }

        [Required(ErrorMessage = "What's your Number?")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "What's your SNN?")]
        public string SSN { get; set; }

        [Required(ErrorMessage = "What's your Username?")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "What's your Password?")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool Status { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Report> Reports { get; set; }
        public List<InsuranceRegistration> InsuranceRegistrations { get; set; }
        public List<TransactionHistory> TransactionHistories { get; set; }
    }
}
