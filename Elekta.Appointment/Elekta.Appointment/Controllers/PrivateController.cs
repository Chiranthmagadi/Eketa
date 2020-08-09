using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elekta.Appointment.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elekta.Appointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrivateController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public PrivateController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
            //
        }

        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            try
            {
                return Ok(_appointmentService.GetAllAppointments());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
