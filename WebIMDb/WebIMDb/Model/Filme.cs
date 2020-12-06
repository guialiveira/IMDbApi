using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebIMDb.Model
{
    public class Filme
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Diretor { get; set; }
        public string Genero { get; set; }

        public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();

        public Filme() { }

        public Filme(int id, string nome, string diretor, string genero)
        {
            Id = id;
            Nome = nome;
            Diretor = diretor;
            Genero = genero;
        }

        //Add avaliação no filme
        public void AddSAvaliacao(Avaliacao avaliacao)
        {
            Avaliacoes.Add(avaliacao);
        }
    }
}
