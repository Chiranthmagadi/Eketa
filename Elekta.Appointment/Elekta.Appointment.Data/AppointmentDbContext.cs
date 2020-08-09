using Elekta.Appointment.Data.Modles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elekta.Appointment.Data
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options)
        {

        }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
               new Patient
               {
                   PatientId = 1,
                   PatientName = "George",
                   PatientEmailId = "george@test.com"
               },
                new Patient
                {
                    PatientId = 2,
                    PatientName = "Json",
                    PatientEmailId = "json@test.com"
                },
                new Patient
                {
                    PatientId = 3,
                    PatientName = "Luci",
                    PatientEmailId = "luci@test.com"
                }
           );

            modelBuilder.Entity<Hospital>().HasData(
               new Hospital
               {
                   HospitalId = 1,
                   HospitalName = "QE",
                   Address = "Woolwich"
               },
                new Hospital
                {
                    HospitalId = 2,
                    HospitalName = "GH",
                    Address = "London"
                },
                new Hospital
                {
                    HospitalId = 3,
                    HospitalName = "Test",
                    Address = "London"
                }
           );
            //modelBuilder.Seed();
        }
    }
}
