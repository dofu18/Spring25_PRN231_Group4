using ApplicationLayer.DTOs.Category;
using ApplicationLayer.DTOs.TutorProfile;
using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Helper;
using InfrastructureLayer.Repository;
using InfrastructureLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> GetAllCategoryAsync()
        {
            var categories = await _categoryRepository.ListAsync();
            var categoryMapper = _mapper.Map<List<CategoryDto>>(categories);
            return categoryMapper;
        }
        public async Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _categoryRepository.FindByIdAsync(categoryId);
            var categoryMapper = _mapper.Map<CategoryDto>(category);
            return categoryMapper;
        }
        public async Task<ResponseDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var categoryObj = _mapper.Map<Category>(createCategoryDto);
            await _categoryRepository.CreateAsync(categoryObj);
            var response = new ResponseDto
            {
                IsSucceed = true,
                Message = "Category added successfully",
            };
            return response;
        }
        public async Task<ResponseDto> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto)
        {
            var categoryUpdate = await _categoryRepository.FindByIdAsync(categoryId);
            if (categoryUpdate != null)
            {
                categoryUpdate = _mapper.Map(updateCategoryDto, categoryUpdate);
                await _categoryRepository.UpdateAsync(categoryUpdate);
                return new ResponseDto
                {
                    IsSucceed = true,
                    Message = "Category updated successfully!"
                };
            }
            return new ResponseDto
            {
                IsSucceed = false,
                Message = "Category not found!"
            };
        }
        public async Task<ResponseDto> DeleteCategoryAsync(Guid categoryId)
        {
            var deleteCategory = await _categoryRepository.FindByIdAsync(categoryId);
            if (deleteCategory != null)
            {
                await _categoryRepository.DeleteAsync(categoryId);

                return new ResponseDto
                {
                    IsSucceed = true,
                    Message = "Category deleted successfully"
                };
            }
            else
            {
                return new ResponseDto
                {
                    IsSucceed = false,
                    Message = $"Category with ID {categoryId} not found"
                };
            }
        }
    }
}
