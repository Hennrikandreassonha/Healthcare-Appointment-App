using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Core.Models.Appointment;
using HealthCare.Core.Models.User;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Core.Data
{
    public class HealthcareContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<CareGiver> CareGiver { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Feedback> Feedback { get; set; }

        public HealthcareContext(DbContextOptions<HealthcareContext> options)
    : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}