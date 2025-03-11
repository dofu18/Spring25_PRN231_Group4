using ApplicationLayer.DTOs.CourseCategory;
using DomainLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.CourseCategories
{
    public interface ICourseCategoryService
    {
        Task<List<CourseCategoryDto>> GetAllCourseCategoryAsync();
        Task<CourseCategoryDto> GetCourseCategoryByIdAsync(Guid courseCategoryId);
        Task<ResponseDto> CreateCourseCategoryAsync(CreateCourseCategoryDto courseCategoryDto);
        Task<ResponseDto> DeleteCourseCategoryAsync(Guid courseCategoryId);
    }
}
