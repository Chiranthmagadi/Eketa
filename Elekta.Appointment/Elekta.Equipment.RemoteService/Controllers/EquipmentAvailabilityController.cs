using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elekta.Appointment.Services.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elekta.Equipment.RemoteService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentAvailabilityController : ControllerBase
    {
        private readonly IService _service;
        public EquipmentAvailabilityController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public string Get()
        {
            return "Welcome to Elekta Equipment.";
        }

        [HttpPost]
        public IActionResult EquipmentAvailability(Request request)
        {
            var result = _service.IsEquipmentAvailable(request.AvailabilityDate);
            return Ok(result);
        }
    }
}
