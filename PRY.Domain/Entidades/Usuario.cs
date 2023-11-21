using System;
namespace PRY.Domain.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Dni { get; set; }
        public int? IdRestaurante { get; set; }
        public int? IdRol { get; set; }
    }
}

