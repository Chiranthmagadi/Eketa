using Elekta.Appointment.Data;
using Elekta.Appointment.Services.Interfaces;
using Elekta.Appointment.Services.Requests;
using Elekta.Appointment.Services.Validation;
using System;
using System.Collections.Generic;
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
            var validationResult = _validator.ValidateRequest(request);
        }
    }
}
