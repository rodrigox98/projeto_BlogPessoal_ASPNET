using BlogAPI.Src.Modelos;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlogAPI.Src.Repositorios
{
    public interface ITema
    {
        Task<List<Tema>> PegarTodosTemasAsync();
        Task<Tema> PegarTemaPeloIdAsync(int id);
        Task NovoTemaAsync(Tema tema);
        Task AtualizarTemaAsync(Tema tema);
        Task DeletarTemaAsync(int id);

    }
}
