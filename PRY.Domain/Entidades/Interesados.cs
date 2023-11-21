using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRY.Domain.Entidades
{
    public class Interesados
    {
        public int Id { get; set; }
        public string NombresYApellidos { get; set; }
        public string Correo { get; set; }
        public string NombreEmpresas { get; set; }
        public string Localizacion { get; set; }
        public int? IdUsuario { get; set; }
    }
}
