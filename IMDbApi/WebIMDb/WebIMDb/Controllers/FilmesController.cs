using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebIMDb.Data;
using WebIMDb.Dtos;
using WebIMDb.Helpers;
using WebIMDb.Model;

namespace WebIMDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        public readonly IRepository _repo;
        public readonly IMapper _mapper;

        public FilmesController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // GET: api/filmes?pageNumber=2&pageSize=2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filme>>> GetFilme([FromQuery]PageParams pageParams)
        {
            var result = await _repo.GetAllFilmesAsync(pageParams);
            var filmeDto = _mapper.Map<IEnumerable<FilmeDto>>(result);
            Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
            return Ok(filmeDto);
        }

        // GET: api/filmes/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> GetFilme(int id)
        {
            var filme = await _repo.GetFilmeByIdAsync(id);
            if (filme == null) return BadRequest("O Filme não foi encontrado");

            var filmeDto = _mapper.Map<FilmeDto>(filme);
            int mont = 0;
            foreach(Avaliacao nota in filmeDto.Avaliacoes)
            {
                mont =  mont + nota.Nota;
            }
            filmeDto.NotaMedia = Convert.ToString(mont / filmeDto.Avaliacoes.Count);
            
            return Ok(filmeDto);
        }

        // PUT: api/Filmes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilme(int id, FilmeDto model)
        {
            var filme = await _repo.GetFilmeByIdAsync(id);
            if (filme == null) return BadRequest("Filme não encontrado");

            _mapper.Map(model, filme);

            _repo.Update(filme);
            if (_repo.SaveChanges())
            {
                return Created($"/api/filme/{model.Id}", _mapper.Map<FilmeDto>(filme));
            }

            return BadRequest("Filme não Atualizado");
        }

        // POST: api/Filmes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Filme>> PostFilme(FilmeDto model)
        {
            var filme = _mapper.Map<Filme>(model);
            _repo.Add(filme);
            if (_repo.SaveChanges())
            {
                return Created($"/api/filme/{model.Id}", _mapper.Map<FilmeDto>(filme));
            }

            return BadRequest("Filme não cadastrado");
        }

        // DELETE: api/Filmes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Filme>> DeleteFilme(int id)
        {
            var alu = await _repo.GetFilmeByIdAsync(id);
            if (alu == null) return BadRequest("Filme não encontrado");

            _repo.Delete(alu);
            if (_repo.SaveChanges())
            {
                return Ok("Filme deletado");
            }

            return BadRequest("Filme não deletado");
        }
    }
}
