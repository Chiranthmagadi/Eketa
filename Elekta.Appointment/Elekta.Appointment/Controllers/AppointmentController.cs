using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elekta.Appointment.Services.Interfaces;
using Elekta.Appointment.Services.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elekta.Appointment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService )
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("addappointment")]
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

        [HttpPost("cancelappointment")]
        public IActionResult CancelAppointment(AppointmentRequest request)
        {
            try
            {
                _appointmentService.CancelAppointment(request);
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

        [HttpPost("changeappointment")]
        public IActionResult ChangeAppointment(AppointmentRequest request)
        {
            try
            {
                _appointmentService.ChangeAppointment(request);
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
