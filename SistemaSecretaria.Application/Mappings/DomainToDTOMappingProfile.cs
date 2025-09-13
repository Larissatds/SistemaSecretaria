using AutoMapper;
using SistemaSecretaria.Application.DTOs;
using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Usuario, LoginDTO>().ReverseMap();
        }
    }
}
