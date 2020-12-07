using System.Threading.Tasks;
using WebIMDb.Model;

namespace WebIMDb.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        Task<Filme[]> GetAllFilmesAsync(bool includeAvaliacao = false);
        Filme[] GetAllFilmes(bool includeAvaliacao = false);
        Task<Filme[]> GetAllFilmesByGeneroAsync(string genero, bool includeAvaliacao = false);
        Filme[] GetAllFilmesByGenero(string genero, bool includeAvaliacao = false);
        Task<Filme> GetFilmeByIdAsync(int FilmeId, bool includeAvaliacao = false);
        Filme GetFilmeById(int FilmeId, bool includeAvaliacao = false);
        Task<Avaliacao> GetAvaliacaoByIdAsync(int AvaliacaoId);
        Avaliacao GetAvaliacaoById(int AvaliacaoId);
    }
}
