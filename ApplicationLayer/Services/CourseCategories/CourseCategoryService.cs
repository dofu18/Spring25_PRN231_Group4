using ApplicationLayer.DTOs.CourseCategory;
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

namespace ApplicationLayer.Services.CourseCategories
{
    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CourseCategoryService(ICourseCategoryRepository courseCategoryRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _courseCategoryRepository = courseCategoryRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CourseCategoryDto>> GetAllCourseCategoryAsync()
        {
            var courseCategories = await _courseCategoryRepository.ListAsync();
            var courseCategoryMapper = _mapper.Map<List<CourseCategoryDto>>(courseCategories);
            return courseCategoryMapper;
        }
        public async Task<CourseCategoryDto> GetCourseCategoryByIdAsync(Guid courseCategoryId)
        {
            var courseCategory = await _courseCategoryRepository.FindByIdAsync(courseCategoryId);
            var courseCategoryMapper = _mapper.Map<CourseCategoryDto>(courseCategory);
            return courseCategoryMapper;
        }
        public async Task<ResponseDto> CreateCourseCategoryAsync(CreateCourseCategoryDto createCourseCategoryDto)
        {
            var category = await _categoryRepository.FindByIdAsync(createCourseCategoryDto.CategoryId);
            if (category == null)
            {
                return new ResponseDto
                {
                    IsSucceed = false,
                    Message = "Category not found",
                };
            }

            var courseCategory = _mapper.Map<CourseCategory>(createCourseCategoryDto);
            courseCategory.Category = category;

            await _courseCategoryRepository.CreateAsync(courseCategory);

            var response = new ResponseDto
            {
                IsSucceed = true,
                Message = "CourseCategory added successfully",
            };

            return response;
        }
        public async Task<ResponseDto>DeleteCourseCategoryAsync(Guid courseCategoryId)
        {
            var courseCategory = await _courseCategoryRepository.FindByIdAsync(courseCategoryId);
            if (courseCategory != null)
            {
                await _courseCategoryRepository.DeleteAsync(courseCategory);
                return new ResponseDto
                {
                    IsSucceed = true,
                    Message = "CourseCategory deleted successfully!"
                };
            }
            return new ResponseDto
            {
                IsSucceed = false,
                Message = "CourseCategory not found!"
            };
        }
    }
}
