using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.CarDTOs;
using RentalCarFinalProject.Service.DTOs.CompanyDTOs;
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
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public CompanyService(IWebHostEnvironment env, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _env = env;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Company company = await _unitOfWork.CompanyRepository.GetAsync(o => o.Id == id);
            if (company == null)
            {
                throw new NotFoundException("company not found");
            }

            if (company.IsDeleted)
            {
                company.IsDeleted = false;
                company.DeletedAt = null;
            }
            else
            {
                company.IsDeleted = true;
                company.DeletedAt = CustomDateTime.currentDate;
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<CompanyListDTO>> GetAllAsync()
        {
            List<CompanyListDTO> companyListDTOs = _mapper.Map<List<CompanyListDTO>>(await _unitOfWork.CompanyRepository.GetAllAsync());
            return companyListDTOs;
        }

        public async Task<List<CompanyListDTO>> GetAllForUsersAsync()
        {
            List<CompanyListDTO> companyListDTOs = _mapper.Map<List<CompanyListDTO>>(await _unitOfWork.CompanyRepository.GetAllForAdminAsync(o => !o.IsDeleted && o.CompanyStatus));
            return companyListDTOs;
        }

        public async Task<PaginatedCompanyListDTO<CompanyListDTO>> GetAllPageIndexAsync(int pageIndex)
        {
            List<CompanyListDTO> companyListDTOs = _mapper.Map<List<CompanyListDTO>>(await _unitOfWork.CompanyRepository.GetAllForAdminAsync(o => !o.IsDeleted && o.CompanyStatus));

            PaginatedCompanyListDTO<CompanyListDTO> pagenetedListDTO = new PaginatedCompanyListDTO<CompanyListDTO>(companyListDTOs, pageIndex, 9);
            return pagenetedListDTO;

        }

        public async Task<CompanyGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }

            CompanyGetDTO companyGetDTO = _mapper.Map<CompanyGetDTO>(await _unitOfWork.CompanyRepository.GetAsync(o => o.Id == id && o.CompanyStatus,"Cars"));
            
            return companyGetDTO;
        }

        public async Task PostAsync(CompanyPostDTO companyPostDTO)
        {
            if (await _unitOfWork.CompanyRepository.IsExistsAsync(c=>c.Name==companyPostDTO.Name))
            {
                throw new AlreadyExistsException($"{companyPostDTO.Name} is already exists");
            }

            Company company = _mapper.Map<Company>(companyPostDTO);

            if (companyPostDTO.File != null)
            {
                company.Image = await companyPostDTO.File.CreateFileAsync(_env, "companies");
            }
            company.CompanyStatus = true;

            await _unitOfWork.CompanyRepository.AddAsync(company);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, CompanyPutDTO companyPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            if (companyPutDTO.Id!=id)
            {
                throw new BadRequestException("id is not matched");
            }

            Company company = await _unitOfWork.CompanyRepository.GetAsync(o => o.Id == id && !o.IsDeleted);
            if (company==null)
            {
                throw new NotFoundException("company not found");
            }

            if (await _unitOfWork.CompanyRepository.IsExistsAsync(o=>o.Id!=companyPutDTO.Id && o.Name==companyPutDTO.Name))
            {
                throw new AlreadyExistsException($"{companyPutDTO.Name} is already exists");
            }


            if (companyPutDTO.File != null)
            {
                if (company.Image != null)
                {
                    string fullpath = Path.Combine(_env.WebRootPath, "companies", company.Image);
                    if (System.IO.File.Exists(fullpath))
                    {
                        System.IO.File.Delete(fullpath);
                    }
                }

                company.Image = await companyPutDTO.File.CreateFileAsync(_env, "companies");

            }

            company.Name = companyPutDTO.Name;
            company.Description = companyPutDTO.Description;
            company.PhoneNumber = companyPutDTO.PhoneNumber;
            company.Address = companyPutDTO.Address;
            company.WorkTime = companyPutDTO.WorkTime;
            company.UpdatedAt = CustomDateTime.currentDate;

            await _unitOfWork.CommitAsync();
        }
    }
}
