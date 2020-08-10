using Elekta.Appointment.Data.Modles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elekta.Appointment.Data.DataSeed
{
    /// <summary>
    /// Data seed class to seed data locally.
    /// </summary>
    public class DatabaseSeed
    {
        private readonly AppointmentDbContext _context;

        public DatabaseSeed(AppointmentDbContext context)
        {
            _context = context;
        }
        public void SeedDatabase()
        {
            var appointments = AddAppointments();
            var patients = AddPatients();


            LinkPatientsToAppointment(patients, appointments);

        }

        private List<Patient> AddPatients()
        {
            var patients = new List<Patient>
            {
                new Patient
                {
                    Id = 100,
                    PatientName = "Bill",
                    PatientEmailId = "bill@test.com"
                },
                new Patient
                {
                Id = 101,
                    PatientName = "Philbert",
                    PatientEmailId = "philbert@test.com"
                },
                new Patient
                {
                    Id = 102,
                    PatientName = "Stephen",
                    PatientEmailId = "stephen@test.com"
                }
            };

            _context.Patients.AddRange(patients);
            _context.SaveChanges();

            return patients;
        }

        private List<AppointmentModel> AddAppointments()
        {
            var date1 = new DateTime(2020, 08, 20, 9, 0, 0);
            var date2 = new DateTime(2020, 08, 20, 10, 0, 0);
            var date3 = new DateTime(2020, 08, 20, 11, 0, 0);
            var date4 = new DateTime(2020, 08, 20, 12, 0, 0);

            var appointments = new List<AppointmentModel>
            {
                new AppointmentModel
                {
                    Id=1,
                    AppointmentDate = date1.AddDays(25),
                    Status = true
                },
                new AppointmentModel
                {
                    Id=2,
                    AppointmentDate = date2.AddDays(30),
                    Status = true
                },
                new AppointmentModel
                {
                    Id=3,
                    AppointmentDate = date3.AddDays(35),
                    Status = true
                },
                new AppointmentModel
                {
                    Id=4,
                    AppointmentDate = date4.AddDays(40),
                    Status = true
                },
                new AppointmentModel
                {
                    Id=5,
                    AppointmentDate = date1.AddDays(45),
                    Status = true
                },
                new AppointmentModel
                {
                    Id=6,
                    AppointmentDate = date2.AddDays(50),
                    Status = true
                },
                new AppointmentModel
                {
                    Id=7,
                    AppointmentDate = date3.AddDays(55),
                    Status = true
                },
                new AppointmentModel
                {
                    Id=8,
                    AppointmentDate = date4.AddDays(60),
                    Status = true
                },
            };

            _context.Appointments.AddRange(appointments);
            _context.SaveChanges();

            return appointments;
        }

        private void LinkPatientsToAppointment(List<Patient> patients, List<AppointmentModel> appointments)
        {
            var count = 0;
            foreach (var appointment in appointments)
            {
                appointment.PatientId = patients[count++ % patients.Count].Id;
            }

            _context.SaveChanges();
        }
    }
}
