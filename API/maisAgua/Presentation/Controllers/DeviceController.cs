using maisAgua.Application.DTOs.DeviceDTO;
using maisAgua.Application.DTOs.Readings;
using maisAgua.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace maisAgua.Presentation.Controllers
{
    // TODO: Mover DTOs para a camada service, pois não é uma responsabilidade do Controller conhecer os DTOs de domínio.

    [Route("api/[controller]")]
    [ApiController]
    [Tags("Dispositivos")]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceService _service;

        public DeviceController(DeviceService service)
        {
            _service = service;
        }


        /// <summary>
        /// Retorna todos os dispositivos 
        /// </summary>
        /// <returns>
        /// Código 200 OK com a lista de dispositivos.
        /// Código 500 Internal Server Error se ocorrer um erro no servidor.
        /// Código 503 Service Unavailable se o serviço estiver indisponível.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<List<DeviceReadDTO>>> GetAll()
        {
            return Ok(await _service.GetAllDevicesAsync());
        }


        /// <summary>
        /// Retorna um dispositivo específico pelo ID.
        /// </summary>
        /// <param name="id">
        /// Id do dispositivo a ser retornado.
        /// </param>
        /// <returns>
        /// Código 200 OK com o dispositivo encontrado.
        /// Código 404 Not Found se o dispositivo não for encontrado.
        /// Código 500 Internal Server Error se ocorrer um erro no servidor.
        /// Código 503 Service Unavailable se o serviço estiver indisponível.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<DeviceReadDTO>> GetById(int id)
        {
            var deviceReadDTO = await _service.GetDeviceByIdAsync(id);
            return Ok(deviceReadDTO);
        }


        /// <summary>
        /// Cria um novo dispositivo.
        /// </summary>
        /// <param name="createDTO">
        /// Objeto que representa os dados de criação de um dispositivo. (Sem necessidade de Id, pois é gerado automaticamente pelo banco de dados.)
        /// </param>
        /// <returns>
        /// Código 201 Created com o dispositivo criado.
        /// Código 400 Bad Request se os dados de entrada forem inválidos. (por exemplo, se o nome já existir no banco de dados)
        /// Código 500 Internal Server Error se ocorrer um erro no servidor.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<DeviceReadDTO>> Create([FromBody] DeviceCreateDTO createDTO)
        {
            var deviceReadDTO = await _service.CreateDeviceAsync(createDTO);
            return CreatedAtAction("GetById", new { id = deviceReadDTO.Id }, deviceReadDTO);
        }



        /// <summary>
        /// Atualiza os dados completos ou parciais de um dispositivo existente.
        /// </summary>
        /// <param name="id">ID do dispositivo a ser atualizado</param>
        /// <param name="updateDTO">Objeto contendo as informações de atualização para o dispositivo existente</param>
        /// <returns>
        /// Código 200 OK com o dispositivo atualizado.
        /// Código 404 Not Found se o dispositivo não for encontrado.
        /// Código 400 Bad Request se os dados de entrada forem inválidos. (por exemplo, se a data de instalação for no futuro)
        /// Código 500 Internal Server Error se ocorrer um erro no servidor.
        /// Código 503 Service Unavailable se o serviço estiver indisponível.
        /// </returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<DeviceReadDTO>> Update(int id, [FromBody] DeviceUpdateDTO updateDTO)
        {
            var deviceReadDTO = await _service.UpdateDeviceAsync(id, updateDTO);
            return Ok(deviceReadDTO);
        }


        /// <summary>
        /// Deleta um dispositivo existente através do ID.
        /// </summary>
        /// <param name="id">
        /// Id do dispositivo a ser deletado.
        /// </param>
        /// <returns>
        /// Código 204 No Content se o dispositivo for deletado com sucesso.
        /// Código 404 Not Found se o dispositivo não for encontrado.
        /// Código 500 Internal Server Error se ocorrer um erro no servidor.
        /// Código 503 Service Unavailable se o serviço estiver indisponível.
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteDeviceAsync(id);
            return NoContent();
        }
    }
}
