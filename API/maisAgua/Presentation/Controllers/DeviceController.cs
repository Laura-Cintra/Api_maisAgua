using maisAgua.Application.DTOs.DeviceDTO;
using maisAgua.Application.Repository;
using maisAgua.Application.Service;
using maisAgua.Domain.Device;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace maisAgua.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Sensores")]
    public class DeviceController : ControllerBase
    {

        
        private readonly DeviceService _service;

        public DeviceController(DeviceService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<List<Device>>> GetAll()
        {
            return Ok(await _service.GetDevicesAsync());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<Device>> Create([FromBody] DeviceCreateDTO deviceDTO)
        {
            var device = await _service.CreateDeviceAsync(deviceDTO);
            return CreatedAtAction(nameof(GetById), new {id = device.Id}, device);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        private async Task<ActionResult<DeviceReadDTO>> GetById(int id)
        {


         
            var deviceDTO = new DeviceReadDTO()
            {

            };

            return Ok(deviceDTO);
        }
    }
}
