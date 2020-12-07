using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebIMDb.Model;

namespace WebIMDb.Data
{
    public class WebIMDbContext : DbContext
    {
        public WebIMDbContext (DbContextOptions<WebIMDbContext> options)
            : base(options)
        {
        }

        public DbSet<WebIMDb.Model.Filme> Filme { get; set; }

        public DbSet<WebIMDb.Model.Avaliacao> Avaliacao { get; set; }
    }
}
