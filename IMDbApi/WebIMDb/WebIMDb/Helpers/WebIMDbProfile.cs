

using AutoMapper;
using WebIMDb.Dtos;
using WebIMDb.Model;

namespace WebIMDb.Helpers
{
    public class WebIMDbProfile : Profile
    {
        public WebIMDbProfile()
        {
            CreateMap<Filme, FilmeDto>();
                //.ForMember(
                //    dest => dest.Nome,
                //    opt => opt.MapFrom(src => $"")
                //);

            CreateMap<FilmeDto, Filme>();

            CreateMap<AvaliacaoDto, Avaliacao>();
            CreateMap<Avaliacao, AvaliacaoDto>().ReverseMap();
        }
    }
}
