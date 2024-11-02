using AdOptimize.Models.Models;
using AutoMapper;
using AdOptimize.Models.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdOptimize.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Campanha, CampanhaDTO>().ReverseMap();
            CreateMap<Anuncio, AnuncioDTO>().ReverseMap();
        }
    }
}
