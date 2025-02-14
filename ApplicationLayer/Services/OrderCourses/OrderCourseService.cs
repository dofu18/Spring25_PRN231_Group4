using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Entities;
using InfrastructureLayer.Repository;
using Microsoft.AspNetCore.Http;

namespace ApplicationLayer.Services.OrderCourses
{
    public class OrderCourseService : BaseService, IOrderCourseService
    {
        private readonly IGenericRepository<OrderCourse> _orderCourseRepo;

        public OrderCourseService(IGenericRepository<OrderCourse> orderCourseRepo, IMapper mapper, IHttpContextAccessor httpCtx) : base(mapper, httpCtx)
        {
            _orderCourseRepo = orderCourseRepo;
        }   
        public async Task<ICollection<OrderCourse>> List(Guid? userId = null)
        {
            //bool noFiltersApplied = userId == Guid.Empty;
            //if (noFiltersApplied)
            //{
            //    return await _orderCourseRepo.ListAsync();
            //}
            //return await _orderCourseRepo.WhereAsync(up =>
            //        (up.CreatedBy == userId));

            if (userId.HasValue)
            {
                return await _orderCourseRepo.WhereAsync(oc => oc.CreatedBy == userId, "Course", "Order");
            }

            return await _orderCourseRepo.ListAsync("Course", "Order");
        }
    }
}
