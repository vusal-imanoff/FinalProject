using AutoMapper;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.BrandDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BrandPostDTO, Brand>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));

            CreateMap<Brand, BrandListDTO>();
            CreateMap<Brand, BrandGetDTO>();
        }
    }
}
