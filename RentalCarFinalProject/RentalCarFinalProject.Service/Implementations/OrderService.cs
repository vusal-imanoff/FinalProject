﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Core.Enums;
using RentalCarFinalProject.Service.DTOs.OrderDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Extentions;
using RentalCarFinalProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class OrderService:IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;


        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task AcceptAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Order order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id);
            if (order == null)
            {
                throw new NotFoundException("order not found");
            }

            order.OrderStatus = OrderStatus.Accepted;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Order order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id);
            if (order == null)
            {
                throw new NotFoundException("order not found");

            }
            if (order.IsDeleted)
            {
                order.IsDeleted = false;
                order.DeletedAt = null;
            }
            else
            {
                order.IsDeleted = true;
                order.DeletedAt = CustomDateTime.currentDate;
            }

            await _unitOfWork.CommitAsync();

        }

        public async Task EndAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Order order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id);
            if (order == null)
            {
                throw new NotFoundException("order not found");

            }

            order.OrderStatus = OrderStatus.End;

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<OrderListDTO>> GetAllAsync(string user)
        {
            AppUser appUser = await _userManager.FindByNameAsync(user);

            if (appUser == null)
            {
                throw new NotFoundException("User Not Found");
            }

            List<OrderListDTO> orderListDTOs = _mapper.Map<List<OrderListDTO>>(await _unitOfWork.OrderRepository.GetAllAsync(o => !o.IsDeleted && o.AppUserId == appUser.Id));
            return orderListDTOs;
        }

        public async Task<OrderGetDTO> GetByIdAsync(int? id, string user)
        {
            if (id == null)
            {
                throw new BadRequestException("Id is required");
            }

            AppUser appUser = await _userManager.FindByNameAsync(user);
            if (appUser == null)
            {
                throw new NotFoundException("User Not Found");
            }

            OrderGetDTO orderGetDTO = _mapper.Map<OrderGetDTO>(await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id && o.AppUserId == appUser.Id));
            return orderGetDTO;
        }

        public async Task PostAsync(OrderPostDTO orderPostDTO)
        {
            Order order = _mapper.Map<Order>(orderPostDTO);
            order.OrderStatus = OrderStatus.Pending;

            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.CommitAsync();
        }

        public async Task ProcessAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Order order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id);
            if (order == null)
            {
                throw new NotFoundException("order not found");

            }

            order.OrderStatus = OrderStatus.Processing;

            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, OrderPutDTO orderPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            if (orderPutDTO.Id != id)
            {
                throw new BadRequestException("id is not matched");
            }

            Order order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id && !o.IsDeleted);
            if (order == null)
            {
                throw new NotFoundException("order not found");
            }

            order.Price = orderPutDTO.Price;
            order.CarId = orderPutDTO.CarId;
            order.UpdatedAt = CustomDateTime.currentDate;

            await _unitOfWork.CommitAsync();
        }

        public async Task RejectAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Order order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id);
            if (order == null)
            {
                throw new NotFoundException("order not found");

            }

            order.OrderStatus = OrderStatus.Rejected;

            await _unitOfWork.CommitAsync();
        }
    }
}
