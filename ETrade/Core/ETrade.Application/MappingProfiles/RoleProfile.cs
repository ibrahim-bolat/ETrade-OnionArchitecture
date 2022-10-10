using AutoMapper;
using ETrade.Application.DTOs.RoleDtos;
using ETrade.Domain.Entities.Identity;

namespace ETrade.Application.MappingProfiles;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<AppRole, RoleDto>().ReverseMap();
    }
}