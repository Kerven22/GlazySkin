using AutoMapper;
using Entity.Models;
using Shared;

namespace GlazySkin.Mapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(d => d.Id, m => m.MapFrom(c => c.CategoryId))
            .ForMember(d => d.Name, m => m.MapFrom(c => c.Name));

        CreateMap<Product, ProductDto>();

        CreateMap<CategoryForCreationDto, Category>(); 
        

    }
}