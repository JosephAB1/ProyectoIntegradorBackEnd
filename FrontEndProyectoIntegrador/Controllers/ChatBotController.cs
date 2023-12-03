using Microsoft.AspNetCore.Mvc;
using PRY.DataAcces.Bases;
using PRY.DataAcces.Interfaces;
using PRY.Domain.Entidades;

namespace PRY.API.Controllers
{
    [Route("api/boot")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private readonly IInteresadosService _service;
        private readonly IUsuarioService _usuarioService;
        private readonly IRestauranteService _restauranteService;

        public ChatBotController(IInteresadosService service, IUsuarioService usuarioService, IRestauranteService restauranteService)
        {
            _service = service;
            _usuarioService = usuarioService;
            _restauranteService = restauranteService;
        }

        [HttpPost("/ingresarInteresado")]
        public async Task<string> IngresarInteresados([FromBody] Interesados interesado)
        {
            var data = await _service.Save(interesado);
            var interesadoBd = await _service.GetByID(data.Data);
            return $"{interesadoBd.Data.NombresYApellidos}f sido ingresado";
        }

        [HttpPost("/registrarRestaurante")]
        public async Task<BaseResponse<int>> RegistrarRestaurante([FromBody] Restaurante restaurante)
        {
            return await _restauranteService.Save(restaurante);
        }

        [HttpPost("/registrarUsuario")]
        public async Task<string> RegistrarUsuario([FromBody] Usuario usuario)
        {
            var data = await _usuarioService.Save(usuario);
            var usuarioDd = await _usuarioService.GetByID(data.Data);
            return $"El Usuario {usuarioDd.Data.Nombre} fue ingresado conrrectamente";
        }
    }
}
