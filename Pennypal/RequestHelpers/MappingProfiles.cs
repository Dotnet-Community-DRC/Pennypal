using AutoMapper;
using Pennypal.DTOs;
using Pennypal.Entities;

namespace Pennypal.RequestHelpers;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
    }
}