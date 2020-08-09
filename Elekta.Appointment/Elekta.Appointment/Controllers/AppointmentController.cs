using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elekta.Appointment.Services.Interfaces;
using Elekta.Appointment.Services.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elekta.Appointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService )
        {
            _appointmentService = appointmentService;
        }

        [HttpPost()]
        public IActionResult MakeAppointment(AppointmentRequest request)
        {
            try
            {
                _appointmentService.MakeAppointment(request);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
