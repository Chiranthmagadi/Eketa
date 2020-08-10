﻿using Elekta.Appointment.Data.Modles;
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
                    PatientId = 100,
                    PatientName = "Bill",
                    PatientEmailId = "bill@test.com"
                },
                new Patient
                {
                    PatientId = 101,
                    PatientName = "Philbert",
                    PatientEmailId = "philbert@test.com"
                },
                new Patient
                {
                    PatientId = 102,
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
            var appointments = new List<AppointmentModel>
            {
                new AppointmentModel
                {
                    Id=10,
                    AppointmentDate = DateTime.Now,
                    Status = true
                },
                new AppointmentModel
                {
                    Id=11,
                    AppointmentDate = DateTime.Now,
                    Status = false
                }
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
                appointment.PatientId = patients[count++ % patients.Count].PatientId;
            }

            _context.SaveChanges();
        }
    }
}
