using AutoMapper;
using SBIChallengeAPI.Models;

namespace SBIChallengeAPI.Helpers
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<ServerPost, Salida>()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Title));
        }
    }
}
