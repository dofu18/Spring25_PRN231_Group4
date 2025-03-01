using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.CourseCategories
{
    public interface ICourseCategoryService
    {
        Task<IEnumerable<CourseCategory>> GetAllCourseCategoriesAsync();
        Task<CourseCategory> GetCourseCategoryByIdAsync(Guid id);
        Task CreateCourseCategoryAsync(CourseCategory courseCategory);
        Task UpdateCourseCategoryAsync(CourseCategory courseCategory);
        Task DeleteCourseCategoryAsync(Guid id);
    }
}
