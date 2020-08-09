using Elekta.Appointment.Data.Modles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elekta.Appointment.Data.DataSeed
{
    public class DatabaseSeed
    {
        private readonly AppointmentDbContext _context;

        public DatabaseSeed(AppointmentDbContext context)
        {
            _context = context;
        }
        public void SeedDatabase()
        {
            var hospitals = AddHospitals();
            var appointments = AddAppointments();
            var patients = AddPatients();
            var equipments = AddEquipments();

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

        private List<Hospital> AddHospitals()
        {
            var hospitals = new List<Hospital>
            {
                new Hospital
                {
                    HospitalId=110,
                    HospitalName = "Test Hospital1",
                    Address = "London"
                },
                new Hospital
                {
                   HospitalId=111,
                    HospitalName = "Test Hospital2",
                    Address = "London"
                }
            };

            _context.Hospitals.AddRange(hospitals);
            _context.SaveChanges();

            return hospitals;
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

        private List<Equipment> AddEquipments()
        {
            var equipments = new List<Equipment>
            {
                new Equipment
                {
                    EquipmentId = 1,
                    Date = "2020-01-02T08:00:00.000Z",
                    IsAvailable = true
                },
                new Equipment
                {
                    EquipmentId = 1,
                    Date = "2020-01-02T09:00:00.000Z",
                    IsAvailable = false
                },
                new Equipment
                {
                    EquipmentId = 2,
                    Date = "2020-01-02T08:00:00.000Z",
                    IsAvailable = false
                },
                new Equipment
                {
                    EquipmentId = 2,
                    Date = "2020-01-02T09:00:00.000Z",
                    IsAvailable = true
                }
            };

            _context.Equipments.AddRange(equipments);
            _context.SaveChanges();

            return equipments;
        }

        private void LinkPatientsToAppointment(List<Patient> patients, List<AppointmentModel> appointments)
        {
            var count = 0;
            foreach (var appointment in appointments)
            {
                appointment.Patient.PatientId = patients[count++ % patients.Count].PatientId;
            }

            _context.SaveChanges();
        }
    }
}
