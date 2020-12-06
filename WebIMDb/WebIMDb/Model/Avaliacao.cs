using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebIMDb.Model
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public Filme Filme { get; set; }
        public int FilmeId { get; set; }

        public Avaliacao()
        { }
        public Avaliacao(int id, int nota, Filme filme, int filmeId)
        {
            Id = id;
            Nota = nota;
            Filme = filme;
        }
    }
}
