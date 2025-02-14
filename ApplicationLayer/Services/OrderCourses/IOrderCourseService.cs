using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities;

namespace ApplicationLayer.Services.OrderCourses
{
    public interface IOrderCourseService
    {
        Task<ICollection<OrderCourse>> List(Guid? userId = null);

    }
}
