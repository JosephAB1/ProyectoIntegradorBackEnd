using Microsoft.AspNetCore.Mvc;
using PRY.DataAcces.Interfaces;
using PRY.Domain.Entidades;

namespace PRY.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
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
        public async Task<IActionResult> Save([FromBody] Usuario usuario)
        {
            var response = await _service.Save(usuario);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Usuario usuario)
        {

            var response = await _service.Edit(usuario);
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

