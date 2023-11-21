using PRY.DataAcces.Bases;
using PRY.Domain.Entidades;
using PRY.Domain.EntidadesSinLlaves;

namespace PRY.DataAcces.Interfaces
{
    public interface IUsuarioService : IBaseSevice<Usuario>
    {
        public Task<BaseResponse<string>> Login(Usuario usuario);
        public Task<BaseResponse<UsuarioToken>> refreshToken(Usuario usuario);
    }
}

