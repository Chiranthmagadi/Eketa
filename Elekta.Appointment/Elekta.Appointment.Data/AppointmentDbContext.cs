using Elekta.Appointment.Data.Modles;
using Microsoft.EntityFrameworkCore;
using System;

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

    }
}
