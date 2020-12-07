using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebIMDb.Dtos
{
    public class AvaliacaoDto
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public int FilmeId { get; set; }
    }
}
