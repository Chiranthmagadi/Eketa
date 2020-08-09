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
        //private readonly ILogger<AppointmentController> _logger;

        public AppointmentController( IAppointmentService appointmentService)
        {
            //_logger = logger;
            _appointmentService = appointmentService;
         
        }

        [HttpGet]
        public string Get()
        {
            return "Hi";
        }
       
        //public AppointmentController(IAppointmentService appointmentService)
        //{
        //    _appointmentService = appointmentService;
        //    //var folderDetails = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{"Equipment\\Equipment.json"}");
        //    //var JSON = System.IO.File.ReadAllText(folderDetails);
        //    //dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(JSON);
        //}



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
