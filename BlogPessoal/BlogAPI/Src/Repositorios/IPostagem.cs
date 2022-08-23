using System.Threading.Tasks;
using System.Collections.Generic;
using BlogAPI.Src.Modelos;

namespace BlogAPI.Src.Repositorios
{
    public interface IPostagem
    {
        Task<List<Postagem>> PegarTodasPostagensAsync();
        Task<Postagem> PegarPostagemPeloIdAsync(int id);
        Task NovaPostagemAsync(Postagem postagem);
        Task AtualizarPostagemAsync(Postagem postagem);
        Task DeletarPostagemAsync(int id);
    }
}
