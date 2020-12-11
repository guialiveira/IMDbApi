

using AutoMapper;
using WebIMDb.Dtos;
using WebIMDb.Model;

namespace WebIMDb.Helpers
{
    public class WebIMDbProfile : Profile
    {
        public WebIMDbProfile()
        {
            CreateMap<Filme, FilmeDto>()
            .ForMember(
                dest => dest.NotaMedia,
                opt => opt.MapFrom(src => $"Somente na consulta por Id")
            );

            CreateMap<FilmeDto, Filme>();

            CreateMap<AvaliacaoDto, Avaliacao>();
            CreateMap<Avaliacao, AvaliacaoDto>().ReverseMap();
        }
    }
}
