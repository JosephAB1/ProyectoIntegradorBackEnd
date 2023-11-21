using System;
using Dapper;
using PRY.Common.Commands;
using System.Data;
using PRY.DataAcces.Bases;
using PRY.DataAcces.Interfaces;
using PRY.Domain.Context;
using PRY.Domain.Entidades;

namespace PRY.DataAcces.Servicios
{
    public class RestauranteService : IRestauranteService
    {
        private readonly Connection _context;

        public RestauranteService(Connection context)
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
                    await conexion.ExecuteAsync(RestauranteCommands.DELETE, parametos, commandType: CommandType.StoredProcedure);
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

        public async Task<BaseResponse<int>> Edit(Restaurante restaurante)
        {
            var response = new BaseResponse<int>();
            try
            {
                using (var conexion = _context.ObtenerConneccion())
                {

                    response.Data = await conexion.ExecuteScalarAsync<int>(RestauranteCommands.EDIT, restaurante, commandType: CommandType.StoredProcedure);
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

        public async Task<BaseResponse<Restaurante>> GetByID(int id)
        {
            var response = new BaseResponse<Restaurante>();
            try
            {
                using (var conexion = _context.ObtenerConneccion())
                {
                    var parametos = new DynamicParameters();
                    parametos.Add("Id", id);
                    response.Data = await conexion.QueryFirstAsync<Restaurante>(RestauranteCommands.GETBYID, parametos, commandType: CommandType.StoredProcedure);
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

        public async Task<BaseResponse<IEnumerable<Restaurante>>> Listar()
        {
            var response = new BaseResponse<IEnumerable<Restaurante>>();
            try
            {
                using (var conexion = _context.ObtenerConneccion())
                {
                    var parametos = new DynamicParameters();

                    response.Data = await conexion.QueryAsync<Restaurante>(RestauranteCommands.LIST, commandType: CommandType.StoredProcedure);
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

        public async Task<BaseResponse<int>> Save(Restaurante restaurante)
        {
            var response = new BaseResponse<int>();
            try
            {
                using (var conexion = _context.ObtenerConneccion())
                {

                    response.Data = await conexion.ExecuteScalarAsync<int>(RestauranteCommands.INSERT, restaurante, commandType: CommandType.StoredProcedure);
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

