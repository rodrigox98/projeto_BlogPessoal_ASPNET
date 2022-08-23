using BlogAPI.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositorios
{
    public interface IUsuarios
    {
        Task<Usuario> PegarUsuarioPeloEmailAsync(string email);
        Task NovoUsuarioAsync(Usuario usuario);
        Task<List<Usuario>> PegarTodosUsuariosAsync();
    }
}
