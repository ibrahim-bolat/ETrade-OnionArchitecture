using AutoMapper;
using ETrade.Application.Features.RoleOperations.DTOs;
using ETrade.Domain.Entities.Identity;

namespace ETrade.Application.Features.RoleOperations.MappingProfiles;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<AppRole, RoleDto>().ReverseMap();
    }
}