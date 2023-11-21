using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PRY.Domain.Context
{
    public class Connection
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public Connection(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        }
        public SqlConnection ObtenerConneccion()
        {

            var connexion = new SqlConnection(_connectionString);
            connexion.Open();
            return connexion;
        }
    }
}

