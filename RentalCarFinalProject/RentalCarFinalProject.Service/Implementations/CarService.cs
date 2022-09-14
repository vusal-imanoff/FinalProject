using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.CarDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Extentions;
using RentalCarFinalProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CarService(IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }
            
            Car car = await _unitOfWork.CarRepository.GetAsync(c => c.Id == id);

            if (car == null)
            {
                throw new NotFoundException("car not found");
            }

            if (!car.IsDeleted)
            {
                car.IsDeleted = true;
                car.DeletedAt = CustomDateTime.currentDate;
            }
            else
            {
                car.IsDeleted = false;
                car.DeletedAt = null;
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<CarGetDTO>> GetAllAsync()
        {
            
            List<CarGetDTO> carlistDTO = new List<CarGetDTO>();
            foreach (var car in await _unitOfWork.CarRepository.GetAllForAdminAsync(c => !c.IsDeleted, "Brand", "Model", "Fuel", "Year", "Transmission", "Category", "Engine", "Color", "Company"))
            {
                //var dto = _mapper.Map<CarGetDTO>(car);
                var dto = new CarGetDTO();
                dto.Id = car.Id;
                dto.Plate=car.Plate;
                dto.BrandId = car.Brand.Id;
                dto.CategoryId = car.Category.Id;
                dto.CompanyId = car.Company.Id;
                dto.Description = car.Description;
                dto.TransmissionName = car.Transmission.Name;
                dto.Price = car.Price;
                dto.DiscountPrice = car.DiscountPrice;
                dto.Image = car.Image;
                dto.IsFree=car.IsFree;
                dto.BrandName = car.Brand.Name;
                dto.ColorName = car.Color.Name;
                dto.ModelName = car.Model.Name;
                dto.EngineName = car.Engine.Name;
                dto.Year = car.Year.ProductionYear;
                dto.FuelName = car.Fuel.Name;
                dto.CategoryName = car.Category.Name;
                dto.CompanyName = car.Company.Name;
                carlistDTO.Add(dto);
            }
            return carlistDTO;
        }


        public async Task<List<CarListDTO>> GetAllForAdminAsync(int pageIndex)
        {
            List<CarListDTO> carListDTOs = _mapper.Map<List<CarListDTO>>(await _unitOfWork.CarRepository.GetAllAsync());


            return carListDTOs;
        }

        public async Task<CarGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }
            var car = await _unitOfWork.CarRepository.GetAsync(c => c.Id == id, "Brand",
                "Model", "Fuel", "Year", "Transmission", "Category", "Engine", "Color", "Company", "CarImages", "CarTags");
            var dto = new CarGetDTO();
            dto.Id = car.Id;
            dto.BrandId = car.Brand.Id;
            dto.CategoryId = car.Category.Id;
            dto.CompanyId = car.Company.Id;
            dto.Plate = car.Plate;
            dto.Description = car.Description;
            dto.TransmissionName = car.Transmission.Name;
            dto.Price = car.Price;
            dto.DiscountPrice = car.DiscountPrice;
            dto.Image = car.Image;
            dto.IsFree = car.IsFree;
            dto.BrandName = car.Brand.Name;
            dto.ColorName = car.Color.Name;
            dto.ModelName = car.Model.Name;
            dto.EngineName = car.Engine.Name;
            dto.Year = car.Year.ProductionYear;
            dto.FuelName = car.Fuel.Name;
            dto.CategoryName = car.Category.Name;
            dto.CompanyName = car.Company.Name;
            //List<CarImages> carImages = new List<CarImages>();
            //foreach (var images in car.CarImages)
            //{
            //   carImages.Add(images);
            //}
            //List<CarTags> carTags = new List<CarTags>();
            //foreach (var tags in car.CarTags)
            //{
            //    carTags.Add(tags);
            //}
            return dto;
        }

        public async Task PostAsync(CarPostDTO carPostDTO)
        {
            if (await _unitOfWork.CarRepository.IsExistsAsync(c => c.Plate == carPostDTO.Plate))
            {
                throw new AlreadyExistsException($"{carPostDTO.Plate} Plate already exists");
            }

            Car car = _mapper.Map<Car>(carPostDTO);

            if (carPostDTO.File != null)
            {
                car.Image = await carPostDTO.File.CreateFileAsync(_env, "uploads");
            }

            if (carPostDTO.Files != null && carPostDTO.Files.Count > 0)
            {
                if (carPostDTO.Files.Count > 5)
                {
                    throw new BadRequestException("Can You Select Maximum 5 Images");
                }
                List<CarImages> carImages = new List<CarImages>();
                foreach (IFormFile file in carPostDTO.Files)
                {
                    if (file.CheckFileContextType("image/jpeg"))
                    {
                        throw new BadRequestException("Please Select A Correct Image type. Example Jpeg Or Jpg");
                    }
                    if (file.CheckFileSize(2000))
                    {
                        throw new BadRequestException("Please Select A Correct Image Size. Maximum 2 MB");
                    }
                    CarImages images = new CarImages
                    {
                        Image = await file.CreateFileAsync(_env, "listImages")
                    };
                    carImages.Add(images);
                }
                car.CarImages = carImages;
            }


            if (carPostDTO.TagIds != null && carPostDTO.TagIds.Count > 0)
            {
                List<CarTags> carTags = new List<CarTags>();

                foreach (int tagId in carPostDTO.TagIds)
                {
                    if (!await _unitOfWork.TagRepository.IsExistsAsync(t => !t.IsDeleted && t.Id == tagId))
                    {
                        throw new BadRequestException("tag is incorrect");
                    }
                    CarTags carTag = new CarTags
                    {
                        TagId = tagId
                    };

                    carTags.Add(carTag);
                }

                car.CarTags  = carTags;
            }

            await _unitOfWork.CarRepository.AddAsync(car);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, CarPutDTO carPutDTO)
        {

            if (id == null)
            {
                throw new BadRequestException("id is required");
            }
            if (carPutDTO.Id != id)
            {
                throw new BadRequestException("id is not matched");
            }
            Car car = await _unitOfWork.CarRepository.GetAsync(c => c.Id == id && !c.IsDeleted, "CarImages", "CarTags");
            if (car == null)
            {
                throw new NotFoundException("car not found");
            }

            if (await _unitOfWork.CarRepository.IsExistsAsync(c => c.Id != carPutDTO.Id && c.Plate == carPutDTO.Plate))
            {
                throw new AlreadyExistsException($"{carPutDTO.Plate} Plate already exists");
            }
            if (carPutDTO.File != null)
            {
                if (car.Image != null)
                {
                    string fullpath = Path.Combine(_env.WebRootPath, "uploads", car.Image);
                    if (System.IO.File.Exists(fullpath))
                    {
                        System.IO.File.Delete(fullpath);
                    }
                }

                car.Image = await carPutDTO.File.CreateFileAsync(_env, "uploads");
            }

            if (carPutDTO.Files != null && carPutDTO.Files.Count > 0)
            {
                int selectedimage = 5 - car.CarImages.Count;
                if (selectedimage == 0)
                {
                    throw new BadRequestException($" Selected Max Images");
                }
                if (carPutDTO.Files != null && carPutDTO.Files.Count > 0 && selectedimage < car.CarImages.Count)
                {
                    throw new BadRequestException($" You Can Select {selectedimage} Image");
                }
                List<CarImages> carImages = new List<CarImages>();
                foreach (IFormFile file in carPutDTO.Files)
                {
                    if (file.CheckFileContextType("image/jpeg"))
                    {
                        throw new BadRequestException("Please Select A Correct Image type. Example Jpeg Or Jpg");
                    }
                    if (file.CheckFileSize(2000))
                    {
                        throw new BadRequestException("Please Select A Correct Image Size. Maximum 2 MB");
                    }
                    CarImages images = new CarImages
                    {
                        Image = await file.CreateFileAsync(_env, "listImages")
                    };
                    carImages.Add(images);
                }
                if (car.CarImages != null && carPutDTO.Files.Count >= 0)
                {
                    car.CarImages.AddRange(carImages);
                }
                else
                {
                    car.CarImages = carImages;
                }
            }

            if (carPutDTO.TagIds != null && carPutDTO.TagIds.Count > 0)
            {
                List<CarTags> carTags = new List<CarTags>();

                foreach (int tagId in carPutDTO.TagIds)
                {
                    if (!await _unitOfWork.TagRepository.IsExistsAsync(t => !t.IsDeleted && t.Id == tagId))
                    {
                        throw new BadRequestException("tag is incorrect");
                    }
                    //if (product.ProductTags != null && product.ProductTags.Count > 0)
                    //{
                    //    product.ProductTags.RemoveRange(tagId,productTags.Count);
                    //}

                    CarTags carTag = new CarTags
                    {
                        TagId = tagId
                    };

                    carTags.Add(carTag);
                }
                car.CarTags = carTags;
            }


            car.Plate = carPutDTO.Plate;
            car.Description = carPutDTO.Description;
            car.Price = carPutDTO.Price;
            car.DiscountPrice = carPutDTO.DiscouuntPrice;
            car.IsFree = carPutDTO.IsFree;
            car.BrandId = carPutDTO.BrandId;
            car.ModelId = carPutDTO.ModelId;
            car.CategoryId = carPutDTO.CategoryId;
            car.FuelId = carPutDTO.FuelId;
            car.EngineId = carPutDTO.EngineId;
            car.TransmissionId = carPutDTO.TransmissionId;
            car.YearId = carPutDTO.YearId;
            car.ColorId = carPutDTO.ColorId;
            car.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();

        }
    }
}
