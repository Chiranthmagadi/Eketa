using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elekta.Appointment.Services.Interfaces;
using Elekta.Appointment.Services.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Elekta.Appointment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController( IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
         
        }

        [HttpGet]
        public string Get()
        {
            return "Welcome to Elekta Appointment.";
        }

        [HttpPost("addappointment")]
        public async Task<IActionResult> MakeAppointmentAsync(AppointmentRequest request)
        {
            try
            {
                await _appointmentService.MakeAppointmentAsync(request);
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
                _appointmentService.ChangeAppointmentAsync(request);
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
