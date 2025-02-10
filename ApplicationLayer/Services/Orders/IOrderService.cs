using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer.DTOs;
using DomainLayer.Entities;
using DomainLayer.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Services.Orders
{
    public interface IOrderService
    {
        Task<ICollection<Order>> List(Guid? userId = null);
        Task<ICollection<Order>> SearchById(Guid id, GeneralEnum.IdType idtype);
        Task<IActionResult> Create(OrderCreateDto order);
        
    }
}
