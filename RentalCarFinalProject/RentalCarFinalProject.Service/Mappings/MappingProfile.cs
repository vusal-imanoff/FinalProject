using AutoMapper;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.BrandDTOs;
using RentalCarFinalProject.Service.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        #region Brand
            CreateMap<BrandPostDTO, Brand>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));

            CreateMap<Brand, BrandListDTO>();
            CreateMap<Brand, BrandGetDTO>();
            #endregion

            #region Category
            CreateMap<CategoryPostDTO,Category>()
                .ForMember(des=>des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Category, CategoryListDTO>();
            CreateMap<Category, CategoryGetDTO>();
        #endregion
        }


    }
}
