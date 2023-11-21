using Microsoft.AspNetCore.Mvc;
using PRY.DataAcces.Interfaces;
using PRY.Domain.Entidades;

namespace PRY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteresadosController : ControllerBase
    {
        private readonly IInteresadosService _service;

        public InteresadosController(IInteresadosService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var response = await _service.Listar();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GeyById(int id)
        {

            var response = await _service.GetByID(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Interesados interesados)
        {
            var response = await _service.Save(interesados);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Interesados interesados)
        {

            var response = await _service.Edit(interesados);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {

            var response = await _service.Delete(id);
            return Ok(response);
        }
    }
}
