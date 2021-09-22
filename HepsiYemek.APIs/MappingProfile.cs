using AutoMapper;
using HepsiYemek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiYemek.APIs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryResponseDto, Category>().ForMember(d => d.Id, m => m.Ignore());
            ;
            CreateMap<Category, CategoryResponseDto>();

            CreateMap<Product, ProductResponseDto>();
            
            CreateMap< ProductRequestDto, Product>()
                .ForMember(d => d.CategoryId, m => m.Ignore());
        }
    }
}
