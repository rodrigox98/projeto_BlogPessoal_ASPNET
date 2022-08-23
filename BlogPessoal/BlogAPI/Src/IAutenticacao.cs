using System.Threading.Tasks;
using BlogAPI.Src.Modelos;
namespace BlogAPI.Src
{
    public interface IAutenticacao
    {
        string codificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(Usuario usuario);
        string GerarToken(Usuario usuario);
    }
}
