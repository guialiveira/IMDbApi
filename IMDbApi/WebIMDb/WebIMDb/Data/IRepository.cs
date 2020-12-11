using System.Collections.Generic;
using System.Threading.Tasks;
using WebIMDb.Helpers;
using WebIMDb.Model;

namespace WebIMDb.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        Task<PageList<Filme>> GetAllFilmesAsync(PageParams pageParams);
        Task<Filme> GetFilmeByIdAsync(int FilmeId);
        Task<Avaliacao> GetAvaliacaoByIdAsync(int AvaliacaoId);
    }
}
