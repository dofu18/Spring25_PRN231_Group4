using Application.RespType;
using ApplicationLayer.DTOs.Category;
using ApplicationLayer.DTOs.Course;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Categories
{
    public interface ICategoryService
    {
        Task<DynamicResponse<CategoryResponseModel>> GetAllCategoriesAsync(GetAllCategoryRequestModel model);
        Task<GenericResp<CategoryResponseModel>> UpdateCategoryAsync(CategoryCreateDto model, Guid id);
        Task<GenericResp<CategoryResponseModel>> DeleteCategoryAsync(Guid id, string status);
        Task<GenericResp<CategoryResponseModel>> GetCategoryByIdAsync(Guid id);
        Task<GenericResp<CategoryResponseModel>> CreateCategoryAsync(CategoryCreateDto model);
    }
}
