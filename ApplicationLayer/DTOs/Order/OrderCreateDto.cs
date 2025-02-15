using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Entities;

namespace ApplicationLayer.DTOs.Orders
{
    public class OrderCreateDto
    {
        public float TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderCreateDto>().ReverseMap();
        }
    }
}
