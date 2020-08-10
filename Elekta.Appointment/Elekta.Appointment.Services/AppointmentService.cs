using Elekta.Appointment.Data;
using Elekta.Appointment.Data.Modles;
using Elekta.Appointment.Services.Interfaces;
using Elekta.Appointment.Services.Requests;
using Elekta.Appointment.Services.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elekta.Appointment.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppointmentDbContext _context;
        private readonly IAppointmentRequestValidator _validator;

        public AppointmentService(AppointmentDbContext context, IAppointmentRequestValidator validator)
        {
            this._context = context;
            this._validator = validator;
        }

        public List<AppointmentModel> GetAllAppointments()
        {
            var result = _context.Appointments.Where(c => c.AppointmentDate == DateTime.Today).ToList();
            return result;
        }

        public async Task MakeAppointmentAsync(AppointmentRequest request)
        {
            var validationResult = await _validator.ValidateMakeAppointmentRequestAsync(request);
            if (!validationResult.PassedValidation)
            {
                throw new ArgumentException(validationResult.Errors.First());
            }
            var newAppointment = new AppointmentModel
            {
                PatientId = request.PatientId,
                AppointmentDate = request.AppointmentDate,
                Status = true
            };

            _context.Appointments.Add(newAppointment);
            _context.SaveChanges();
            SendEmailToNotify();
        }


        public void CancelAppointment(AppointmentRequest request)
        {
            var validationResult = _validator.ValidateCancelAppointmentRequest(request);
            if (!validationResult.PassedValidation)
            {
                throw new ArgumentException(validationResult.Errors.First());
            }

            var appointment = _context.Appointments.FirstOrDefault(c => c.PatientId == request.PatientId
                               && c.AppointmentDate == request.AppointmentDate);
            appointment.Status = false;
            _context.Update(appointment);
            _context.SaveChanges();
            SendEmailToNotify();
        }

        public async Task ChangeAppointmentAsync(AppointmentRequest request)
        {
            var validationResult = await _validator.ValidateChangeAppointmentRequestAsync(request);
            if (!validationResult.PassedValidation)
            {
                throw new ArgumentException(validationResult.Errors.First());
            }

            var appointment = _context.Appointments.FirstOrDefault(c => c.PatientId == request.PatientId
                               && c.AppointmentDate == request.AppointmentDate);
            appointment.Status = false;
            _context.Update(appointment);
            _context.SaveChanges();
            SendEmailToNotify();
        }

        private void SendEmailToNotify()
        {
            //send email
        }
    }
}
