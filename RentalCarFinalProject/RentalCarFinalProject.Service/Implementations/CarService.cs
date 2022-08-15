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

        public async Task<List<CarListDTO>> GetAllAsync()
        {
            List<CarListDTO> carListDTOs = _mapper.Map<List<CarListDTO>>(await _unitOfWork.CarRepository.GetAllAsync(c => !c.IsDeleted));
            return carListDTOs;
        }

        public async Task<CarGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }
            CarGetDTO carGetDTO = _mapper.Map<CarGetDTO>(await _unitOfWork.CarRepository.GetAsync(c => c.Id == id));
            return carGetDTO;
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
                    if (file.CheckFileSize(200))
                    {
                        throw new BadRequestException("Please Select A Correct Image Size. Maximum 200 KB");
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
                    if (file.CheckFileSize(50))
                    {
                        throw new BadRequestException("Please Select A Correct Image Size. Maximum 200 KB");
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
