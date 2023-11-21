using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRY.DataAcces.Interfaces;
using PRY.Domain.Entidades;

namespace PRY.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public AuthController(IUsuarioService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Usuario usuario)
        {

            var response = await _service.Login(usuario);
            return Ok(response);
        }

        [HttpGet("refreshToken")]
        public async Task<ActionResult> RefreshToken()
        {
            var identntity = HttpContext.User.Identity as ClaimsIdentity;
            var userclaims = identntity.Claims;
            var id = userclaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _service.GetByID(Convert.ToInt32(id));
            var response = await _service.refreshToken(user.Data);
            return Ok(response);
        }

    }
}

