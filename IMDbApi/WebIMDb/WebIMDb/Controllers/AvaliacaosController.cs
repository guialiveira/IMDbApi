using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebIMDb.Data;
using WebIMDb.Dtos;
using WebIMDb.Model;

namespace WebIMDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaosController : ControllerBase
    {
        public readonly IRepository _repo;
        public readonly IMapper _mapper;

        public AvaliacaosController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }        

        // GET: api/Avaliacaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Avaliacao>> GetAvaliacao(int id)
        {
            var avaliacao =await _repo.GetAvaliacaoByIdAsync(id);
            if (avaliacao == null) return BadRequest("A Avaliacao não foi encontrado");


            var avaliacaoDto = _mapper.Map<AvaliacaoDto>(avaliacao);
            return Ok(avaliacaoDto);
        }

        // PUT: api/Avaliacaos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvaliacao(int id, AvaliacaoDto model)
        {
            var ava = await _repo.GetAvaliacaoByIdAsync(id);
            if (ava == null) return BadRequest("Avaliacao não encontrada");

            _mapper.Map(model, ava);
            _repo.Update(ava);
            if (_repo.SaveChanges())
            {
                return Created($"/api/avaliacao/{model.Id}", _mapper.Map<AvaliacaoDto>(ava));
            }

            return BadRequest("Avaliacao não Atualizada");
        }

        // POST: api/Avaliacaos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Avaliacao>> PostAvaliacao(AvaliacaoDto model)
        {
            if(model.Nota > 4)
            {
                return BadRequest("A nota maxima é 4");
            }
            var avaliacao = _mapper.Map<Avaliacao>(model);
            _repo.Add(avaliacao);
            if (_repo.SaveChanges())
            {
                return Created($"/api/avaliacao/{model.Id}", _mapper.Map<AvaliacaoDto>(avaliacao));
            }

            return BadRequest("Aluno não cadastrado");
        }

        // DELETE: api/Avaliacaos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Avaliacao>> DeleteAvaliacao(int id)
        {
            var alu = await _repo.GetAvaliacaoByIdAsync(id);
            if (alu == null) return BadRequest("Avaliacao não encontrada");

            _repo.Delete(alu);
            if (_repo.SaveChanges())
            {
                return Ok("Avaliacao deletada");
            }

            return BadRequest("Avaliacao não deletado");
        }
    }
}
