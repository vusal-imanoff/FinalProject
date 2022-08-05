using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
            
            Car car = await _unitOfWork.CarRepository.GetAsync(c => c.Id == id && !c.IsDeleted);

            if (car == null)
            {
                throw new NotFoundException("car not found");
            }
            car.IsDeleted = true;
            car.DeletedAt = DateTime.UtcNow.AddHours(4);

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
            if (carPostDTO.File != null)
            {

                carPostDTO.Image = await carPostDTO.File.CreateFileAsync(_env, "uploads");

            }

            Car car = _mapper.Map<Car>(carPostDTO);

            await _unitOfWork.CarRepository.AddAsync(car);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, CarPutDTO carPutDTO)
        {

            if (id == null)
            {
                throw new BadRequestException("id is required");
            }
            if (carPutDTO.Id!=id)
            {
                throw new BadRequestException("id is not matched");
            }
            Car car = await _unitOfWork.CarRepository.GetAsync(c => c.Id == id && !c.IsDeleted);
            if (car==null)
            {
                throw new NotFoundException("car not found");
            }

            if (await _unitOfWork.CarRepository.IsExistsAsync(c=>c.Id!=carPutDTO.Id && c.Plate==carPutDTO.Plate))
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

                carPutDTO.Image = await carPutDTO.File.CreateFileAsync(_env, "uploads");

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

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Car car = await _unitOfWork.CarRepository.GetAsync(c => c.Id == id && c.IsDeleted);

            if (car == null)
            {
                throw new NotFoundException("car not found");
            }
            car.IsDeleted = false;
            car.DeletedAt = null;

            await _unitOfWork.CommitAsync();
        }
    }
}
