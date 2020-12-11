

using System.Collections.Generic;
using WebIMDb.Model;

namespace WebIMDb.Dtos
{
    public class FilmeDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Diretor { get; set; }
        public string Genero { get; set; }
        public string NotaMedia { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
    }
}
