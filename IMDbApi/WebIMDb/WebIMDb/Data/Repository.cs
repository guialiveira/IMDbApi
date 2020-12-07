using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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



        public async Task<Filme[]> GetAllFilmesAsync(bool includeAvaliacao = false)
        {
            IQueryable<Filme> query = _context.Filme;

            if (includeAvaliacao)
            {
                query = query.Include(a => a.Avaliacoes);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return await query.ToArrayAsync();
        }

        public Filme[] GetAllFilmes(bool includeAvaliacao = false)
        {
            IQueryable<Filme> query = _context.Filme;

            if (includeAvaliacao)
            {
                query = query.Include(a => a.Avaliacoes);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }
        public async Task<Filme[]> GetAllFilmesByGeneroAsync(string genero, bool includeAvaliacao = false)
        {
            IQueryable<Filme> query = _context.Filme;

            if (includeAvaliacao)
            {
                query = query.Include(a => a.Avaliacoes);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(Filme => Filme.Genero == genero);

            return await query.ToArrayAsync();
        }
        public Filme[] GetAllFilmesByGenero(string genero, bool includeAvaliacao = false)
        {
            IQueryable<Filme> query = _context.Filme;

            if (includeAvaliacao)
            {
                query = query.Include(a => a.Avaliacoes);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(Filme => Filme.Genero == genero);

            return query.ToArray();
        }

        public async Task<Filme> GetFilmeByIdAsync(int FilmeId, bool includeAvaliacao = false)
        {
            IQueryable<Filme> query = _context.Filme;

            if (includeAvaliacao)
            {
                query = query.Include(a => a.Avaliacoes);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(Filme => Filme.Id == FilmeId);

            return await query.FirstOrDefaultAsync();
        }

        public Filme GetFilmeById(int FilmeId, bool includeAvaliacao = false)
        {
            IQueryable<Filme> query = _context.Filme;

            if (includeAvaliacao)
            {
                query = query.Include(a => a.Avaliacoes);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(Filme => Filme.Id == FilmeId);

            return query.FirstOrDefault();
        }
        public async Task<Avaliacao> GetAvaliacaoByIdAsync(int AvaliacaoId)
        {
            IQueryable<Avaliacao> query = _context.Avaliacao;


            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(Avaliacao => Avaliacao.Id == AvaliacaoId);

            return await query.FirstOrDefaultAsync();
        }
        public Avaliacao GetAvaliacaoById(int AvaliacaoId)
        {
            IQueryable<Avaliacao> query = _context.Avaliacao;


            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(Avaliacao => Avaliacao.Id == AvaliacaoId);

            return query.FirstOrDefault();
        }
    }
}
