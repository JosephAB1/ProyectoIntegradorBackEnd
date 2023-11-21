using System;
using PRY.Domain.Entidades;

namespace PRY.Domain.EntidadesSinLlaves
{
    public class UsuarioToken : Usuario
    {
        public string Token { get; set; }
    }
}

