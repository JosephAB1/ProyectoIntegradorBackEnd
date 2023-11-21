using System.Data;
using Dapper;
using PRY.Common.Commands;
using PRY.DataAcces.Bases;
using PRY.DataAcces.Interfaces;
using PRY.Domain.Context;
using PRY.Domain.Entidades;

namespace PRY.DataAcces.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly Connection _context;

        public UsuarioService(Connection context)
        {
            _context = context;
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

        public async Task<BaseResponse<int>> Save(Usuario usuario)
        {
            var response = new BaseResponse<int>();
            try
            {
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

