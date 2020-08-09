using Elekta.Appointment.Data.Modles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elekta.Appointment.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<AppointmentModel>().HasData(
               new AppointmentModel
               {
                   Id = 1,
                   Status = true,
                   AppointmentDate = DateTime.Now,
                   Patient = { PatientId=1, 
                               PatientName="George",
                               PatientEmailId="george@test.com"}

               }
           );
        }
    }
}
