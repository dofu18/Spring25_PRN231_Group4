using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.RespType;
using ApplicationLayer.DTOs.Orders;
using AutoMapper;
using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Enums;
using DomainLayer.Exceptions;
using InfrastructureLayer;
using InfrastructureLayer.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DomainLayer.Enums.GeneralEnum;

namespace ApplicationLayer.Services.Orders
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(IGenericRepository<Order> orderRepo, IMapper mapper, IHttpContextAccessor httpCtx) : base(mapper, httpCtx)
        {
            _orderRepo = orderRepo;
        }

        public async Task<IActionResult> Create(OrderCreateDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.CreatedBy = new Guid("11111111-1111-1111-1111-111111111111");
            order.CreatedAt = DateTime.Now;
            order.UpdatedAt = DateTime.Now;
            await _orderRepo.CreateAsync(order);

            return SuccessResp.Created("Order created successfully");
        }

        public async Task<ICollection<Order>> List(Guid? userId = null)
        {
            bool noFiltersApplied = userId == Guid.Empty;
            if (noFiltersApplied)
            {
                return await _orderRepo.ListAsync();
            }
            return await _orderRepo.WhereAsync(up =>
                    (up.CreatedBy == userId));
        }

        public async Task<ICollection<Order>> SearchById(Guid id, GeneralEnum.IdType idtype)
        {
            if (idtype == GeneralEnum.IdType.Id)
            {
                List<Order> newOrderList = new List<Order>();
                newOrderList.Add(await _orderRepo.FoundOrThrowAsync(id, Constants.Entities.ORDER + Constants.Errors.NOT_EXIST_ERROR));
                return newOrderList;
            }
            else if (idtype == GeneralEnum.IdType.UserId)
            {
                return await _orderRepo.WhereAsync(x => x.CreatedBy == id, "User");
            }
            throw new BadRequestException("ID Type not Exist");
        }
    }
}
