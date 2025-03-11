using ApplicationLayer.DTOs.Category;
using DomainLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoryAsync();
        Task<CategoryDto> GetCategoryByIdAsync(Guid CategoryId);
        Task<ResponseDto> CreateCategoryAsync(CreateCategoryDto categoryDto);
        Task<ResponseDto> UpdateCategoryAsync(Guid CategoryId, UpdateCategoryDto categoryDto);
        Task<ResponseDto> DeleteCategoryAsync(Guid CategoryId);
    }
}
