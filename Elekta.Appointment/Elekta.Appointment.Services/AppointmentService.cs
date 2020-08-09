using Elekta.Appointment.Data;
using Elekta.Appointment.Data.Modles;
using Elekta.Appointment.Services.Interfaces;
using Elekta.Appointment.Services.Requests;
using Elekta.Appointment.Services.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void MakeAppointment(AppointmentRequest request)
        {

            var validationResult = _validator.ValidateMakeAppointmentRequest(request);
            if (!validationResult.PassedValidation)
            {
                throw new ArgumentException(validationResult.Errors.First());
            }
            var newAppointment = new Appointments
            {
                BookingDate = request.BookingDate,

            };

            _context.Appointments.Add(newAppointment);
            _context.SaveChanges();
            SendEmailToNotify();
        }

        private void SendEmailToNotify()
        {
            //send email
        }
    }
}
