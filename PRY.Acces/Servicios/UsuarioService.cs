using System.Data;
using Dapper;
using PRY.Common.Commands;
using PRY.DataAcces.Bases;
using PRY.DataAcces.Interfaces;
using PRY.Domain.Context;
using PRY.Domain.Entidades;
using PRY.Domain.EntidadesSinLlaves;
using Microsoft.EntityFrameworkCore;
using PRY.Common.Encript;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace PRY.DataAcces.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly Connection _context;
        private IConfiguration _configuration;

        public UsuarioService(Connection context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<BaseResponse<bool>> Delete(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                using (var conexion = _context.ObtenerConneccion())
                {
                    var parametos = new DynamicParameters();
                    parametos.Add("Id", id);
                    await conexion.ExecuteAsync(UsuarioCommands.DELETE, parametos, commandType: CommandType.StoredProcedure);
                    response.IsSucces = true;
                    response.Data = true;
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSucces = false;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<BaseResponse<int>> Edit(Usuario usuario)
        {
            var response = new BaseResponse<int>();
            try
            {
                using (var conexion = _context.ObtenerConneccion())
                {

                    response.Data = await conexion.ExecuteScalarAsync<int>(UsuarioCommands.EDIT, usuario, commandType: CommandType.StoredProcedure);
                    response.IsSucces = true;
                }
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<Usuario>> GetByID(int id)
        {
            var response = new BaseResponse<Usuario>();
            try
            {
                using (var conexion = _context.ObtenerConneccion())
                {
                    var parametos = new DynamicParameters();
                    parametos.Add("Id", id);
                    response.Data = await conexion.QueryFirstAsync<Usuario>(UsuarioCommands.GETBYID, parametos, commandType: CommandType.StoredProcedure);
                    response.IsSucces = true;

                }
            }
            catch (Exception ex)
            {

                response.IsSucces = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Usuario>>> Listar()
        {
            var response = new BaseResponse<IEnumerable<Usuario>>();
            try
            {
                using (var conexion = _context.ObtenerConneccion())
                {
                    var parametos = new DynamicParameters();

                    response.Data = await conexion.QueryAsync<Usuario>(UsuarioCommands.LIST, commandType: CommandType.StoredProcedure);
                    response.IsSucces = true;

                }
            }
            catch (Exception ex)
            {

                response.IsSucces = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<string>> Login(Usuario usuario)
        {
            var response = new BaseResponse<string>();
            try
            {
                var listausuario = (await Listar()).Data.ToList();
                var account = listausuario.Where(x => x.Correo == usuario.Correo).FirstOrDefault();
                if (account is not null)
                {
                    if (Encript.DesEncriptar(account.Password) == usuario.Password)
                    {
                        response.Data = GenerateToken(account);
                        response.IsSucces = true;
                    }
                    else
                    {
                        response.Message = "Contraseña Incorrecta";
                        response.IsSucces = false;
                    }
                }
                else
                {
                    response.Message = "Correo Incorrecto";
                    response.IsSucces = false;
                }

            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = ex.Message;
                response.InnerException = ex.InnerException!;
            }
            return response;
        }

        public string GenerateToken(Usuario user)
        {
            var securittyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var credencials = new SigningCredentials(securittyKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Correo),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.Nombre),
                new Claim(JwtRegisteredClaimNames.GivenName,user.Apellidos),
                new Claim(JwtRegisteredClaimNames.Jti,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,Guid.NewGuid().ToString(),ClaimValueTypes.Integer64),
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:Expires"])),
                notBefore: DateTime.UtcNow,
                signingCredentials: credencials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<BaseResponse<UsuarioToken>> refreshToken(Usuario usuario)
        {
            var response = new BaseResponse<UsuarioToken>();
            try
            {
                var usuarioResponse = new UsuarioToken();
                usuarioResponse = JsonSerializer.Deserialize<UsuarioToken>(JsonSerializer.Serialize(usuario));
                usuarioResponse.Token = GenerateToken(usuario);
                response.Data = usuarioResponse;
                response.IsSucces = true;
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = ex.Message;
                response.InnerException = ex.InnerException;
            }
            return response;
        }

        public async Task<BaseResponse<int>> Save(Usuario usuario)
        {
            var response = new BaseResponse<int>();
            try
            {
                usuario.Password = Encript.Encriptar(usuario.Password);
                using (var conexion = _context.ObtenerConneccion())
                {

                    response.Data = await conexion.ExecuteScalarAsync<int>(UsuarioCommands.INSERT, usuario, commandType: CommandType.StoredProcedure);
                    response.IsSucces = true;
                }
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

