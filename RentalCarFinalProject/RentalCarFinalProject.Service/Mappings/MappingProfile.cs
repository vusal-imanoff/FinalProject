using AutoMapper;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.AppUserDTOs;
using RentalCarFinalProject.Service.DTOs.BrandDTOs;
using RentalCarFinalProject.Service.DTOs.CarDTOs;
using RentalCarFinalProject.Service.DTOs.CategoryDTOs;
using RentalCarFinalProject.Service.DTOs.ColorDTOs;
using RentalCarFinalProject.Service.DTOs.EngineDTOs;
using RentalCarFinalProject.Service.DTOs.FuelDTOs;
using RentalCarFinalProject.Service.DTOs.ModelDTOs;
using RentalCarFinalProject.Service.DTOs.TransmissionDTOs;
using RentalCarFinalProject.Service.DTOs.YearDTOs;
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
            #region Color
            CreateMap<ColorPostDTO,Color>()
                .ForMember(des=>des.CreatedAt,src=>src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Color,ColorGetDTO>();
            CreateMap<Color,ColorListDTO>();
            #endregion
            #region Fuel
            CreateMap<FuelPostDTO, Fuel>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Fuel, FuelListDTO>();
            CreateMap<Fuel, FuelGetDTO>();
            #endregion
            #region Year
            CreateMap<YearPostDTO, Year>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Year,YearListDTO>();
            CreateMap<Year, YearGetDTO>();
            #endregion
            #region Engine
            CreateMap<EnginePostDTO, Engine>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Engine,EngineListDTO>();
            CreateMap<Engine, EngineGetDTO>();
            #endregion
            #region Transmission
            CreateMap<TransmissionPostDTO,Transmission>()
                .ForMember(des=>des.CreatedAt,src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Transmission, TransmissionListDTO>();
            CreateMap<Transmission, TransmissionGetDTO>();
            #endregion
            #region Model
            CreateMap<ModelPostDTO,Model>()
                .ForMember(des=>des.CreatedAt,src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Model,ModelListDTO>();
            CreateMap<Model, ModelGetDTO>();
            #endregion
            #region Car
            CreateMap<CarPostDTO, Car>()
                .ForMember(des=>des.CreatedAt,src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Car, CarListDTO>();
            CreateMap<Car, CarGetDTO>();
            #endregion
            #region AppUser
            CreateMap<RegisterDTO, AppUser>();
            CreateMap<AppUser, UserGetDTO>();
            #endregion
        }
    }
}
