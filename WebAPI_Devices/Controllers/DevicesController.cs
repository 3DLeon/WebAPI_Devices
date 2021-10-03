using Entities_Devices;
using Microsoft.AspNetCore.Mvc;
using Service_Devices;

namespace WebAPI_Devices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly DeviceInterface _devicesInterface;
        public DevicesController(DeviceInterface devicesInterface)
        {
            _devicesInterface = devicesInterface;
        }

        [HttpGet("GetDevices", Name = "GetDevices")]
        public IActionResult GetDevices()
        {
            return Ok(_devicesInterface.GetDevices());
        }

        [HttpGet("GetDevice", Name ="GetDevice")]
        public IActionResult GetDevice(string propertyName, string propertyValue)
        {
            return Ok(_devicesInterface.GetDevice(propertyName, propertyValue));
        }

        [HttpPost("AddDevice", Name = "AddDevice")]
        public IActionResult AddDevice(Device device)
        {
            _devicesInterface.AddDevice(device);
            return Ok(device);
        }

        [HttpPut("UpdateDevice", Name = "UpdateDevice")]
        public IActionResult UpdateDevice(Device device)
        {
            return Ok(_devicesInterface.UpdateDevice(device));
        }

        [HttpDelete("DeleteDevice", Name = "DeleteDevice")]
        public IActionResult DeleteDevice(string propertyName, string propertyValue)
        {
            _devicesInterface.DeleteDevice(propertyName, propertyValue);
            return NoContent();
        }
    }
}
