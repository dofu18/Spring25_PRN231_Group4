using Application.RespType;
using ApplicationLayer.DTOs.Course;
using ApplicationLayer.DTOs.CourseCategory;
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
        Task<DynamicResponse<CourseCategoryResponseModel>> GetAllCourseCategoriesAsync(GetAllCategoryCourseRequestModel model);
        Task<GenericResp<CourseCategoryResponseModel>> UpdateCourseCategoryAsync(CourseCategoryCreateDto model, Guid id);
        Task<GenericResp<CourseCategoryResponseModel>> DeleteCourseCategoryAsync(Guid id, string status);
        Task<GenericResp<CourseCategoryResponseModel>> GetCourseCategoryByIdAsync(Guid id);
        Task<GenericResp<CourseCategoryResponseModel>> CreateCourseCategoryAsync(CourseCategoryCreateDto model);
    }
}
