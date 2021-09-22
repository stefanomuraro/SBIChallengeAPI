using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
