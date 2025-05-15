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

        CreateMap<CategoryForCreationDto, Category>()
            .ForMember(d=>d.Products, opt=>opt.MapFrom(s=>s.Products)); 
        
        CreateMap<Product, ProductDto>()
            .ForMember(d=>d.Id, opt=>opt.MapFrom(s=>s.ProductId));

        CreateMap<ProductForCreationDto, Product>();

        CreateMap<ProductForUpdateDto, Product>().ReverseMap();
        CreateMap<CategoryForUpdate, Category>(); 



    }
}