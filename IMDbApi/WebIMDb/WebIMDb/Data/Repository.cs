using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebIMDb.Helpers;
using WebIMDb.Model;

namespace WebIMDb.Data
{
    public class Repository : IRepository
    {
        private readonly WebIMDbContext _context;
        public Repository(WebIMDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public async Task<PageList<Filme>> GetAllFilmesAsync(PageParams pageParams)
        {
            IQueryable<Filme> query = _context.Filme;

            //Ordenando por quantidade de votos e depois por ordem alfabética
            query = query.AsNoTracking().OrderByDescending(a => a.Avaliacoes.Count).ThenBy(a => a.Nome);           

            //filtro
            if (!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(Filme => Filme.Nome
                                                .ToUpper()
                                                .Contains(pageParams.Nome.ToUpper()));
            if (!string.IsNullOrEmpty(pageParams.Diretor))
                query = query.Where(Filme => Filme.Diretor
                                                .ToUpper()
                                                .Contains(pageParams.Diretor.ToUpper()));
            if (!string.IsNullOrEmpty(pageParams.Genero))
                query = query.Where(Filme => Filme.Genero
                                                .ToUpper()
                                                .Contains(pageParams.Genero.ToUpper()));

            return await PageList<Filme>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<Filme> GetFilmeByIdAsync(int FilmeId)
        {
            IQueryable<Filme> query = _context.Filme;

            //Inclui avaliações
            query = query.Include(a => a.Avaliacoes);
           
            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(Filme => Filme.Id == FilmeId);

            return await query.FirstOrDefaultAsync();
        }
      
        public async Task<Avaliacao> GetAvaliacaoByIdAsync(int AvaliacaoId)
        {
            IQueryable<Avaliacao> query = _context.Avaliacao;


            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(Avaliacao => Avaliacao.Id == AvaliacaoId);

            return await query.FirstOrDefaultAsync();
        }        
    }
}
