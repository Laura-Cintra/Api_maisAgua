using maisAgua.Application.DTOs.Readings;
using maisAgua.Application.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace maisAgua.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Leituras")]
    public class ReadingController : ControllerBase
    {

        private readonly ReadingService _service;

        public ReadingController(ReadingService serivce)
        {
            _service = serivce;
        }


        /// <summary>
        /// Retorna todas as leituras cadastradas no sistema.
        /// </summary>
        /// <returns>
        /// Código 200 OK com a lista de leituras.
        /// Código 500 Internal Server Error se ocorrer um erro no servidor.
        /// Código 503 Service Unavailable se o serviço estiver indisponível.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<List<ReadingReadDTO>>> GetAll()
        {
            var readingsDTO = await _service.GetAllReadingsAsync();
            return Ok(readingsDTO);
        }

        /// <summary>
        /// Retorna uma leitura específica pelo ID.
        /// </summary>
        /// <param name="id">
        /// Id da leitura a ser retornada.
        /// </param>
        /// <returns>
        /// Código 200 OK com a leitura encontrada.
        /// Código 404 Not Found se a leitura não for encontrada.
        /// Código 500 Internal Server Error se ocorrer um erro no servidor.
        /// Código 503 Service Unavailable se o serviço estiver indisponível.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ReadingReadDTO>> GetById(int id)
        {
            var readingDTO = await _service.GetReadingByIdAsync(id);
            return Ok(readingDTO);
        }


        /// <summary>
        /// Cria uma nova leitura no sistema.
        /// </summary>
        /// <param name="createDTO">
        /// Objeto de criação da leitura contendo os dados necessários.
        /// </param>
        /// <returns>
        /// Código 201 Created com a leitura criada.
        /// Código 400 Bad Request se os dados fornecidos forem inválidos. (por exemplo, se o nível de turbidez for negativo)
        /// Código 500 Internal Server Error se ocorrer um erro no servidor.
        /// Código 503 Service Unavailable se o serviço estiver indisponível.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ReadingReadDTO>> Create([FromBody] ReadingCreateDTO createDTO)
        {
            var readingDTO = await _service.AddReadingAsync(createDTO);
            return StatusCode(StatusCodes.Status201Created, readingDTO);
            //return CreatedAtAction(nameof(GetByIdAsync), new { id = reading.Id }, reading);
        }

        /// <summary>
        /// Atualiza uma leitura existente no sistema.
        /// </summary>
        /// <param name="id">
        /// Id da leitura a ser atualizada.
        /// </param>
        /// <param name="updateDTO">
        /// Objeto de atualização da leitura contendo os novos dados.
        /// </param>
        /// <returns>
        /// Código 200 OK com a leitura atualizada.
        /// Código 404 Not Found se a leitura não for encontrada.
        /// Código 400 Bad Request se os dados fornecidos forem inválidos. (por exemplo, se o nível de pH for fora do intervalo permitido)
        /// Código 500 Internal Server Error se ocorrer um erro no servidor.
        /// Código 503 Service Unavailable se o serviço estiver indisponível.
        /// </returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ReadingReadDTO>> Update(int id, [FromBody] ReadingUpdateDTO updateDTO)
        {
            var readingDTO = await _service.UpdateReadingAsync(id, updateDTO);
            return Ok(readingDTO);
        }

        /// <summary>
        /// Deleta uma leitura existente no sistema.
        /// </summary>
        /// <param name="id">
        /// Id da leitura a ser deletada.
        /// </param>
        /// <returns>
        /// Código 204 No Content se a leitura for deletada com sucesso.
        /// Código 404 Not Found se a leitura não for encontrada.
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
            await _service.DeleteReadingAsync(id);
            return NoContent();
        }
    }
}
