using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LDInsurance.Models;

namespace LDInsurance.Data
{
    public class LDInsuranceContext : DbContext
    {
        public LDInsuranceContext(DbContextOptions<LDInsuranceContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<InsuranceRegistration> InsuranceRegistrations { get; set; }
        public DbSet<InsuranceType> InsuranceTypes { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<TransactionHistory> TransactionHistory { get; set; }
    }
}
