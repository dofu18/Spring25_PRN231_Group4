using DomainLayer.Entities;
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
            private readonly IGenericRepository<CourseCategory> _courseCategoryRepository;

            public CourseCategoryService(IGenericRepository<CourseCategory> courseCategoryRepository)
            {
                _courseCategoryRepository = courseCategoryRepository;
            }

            public async Task<IEnumerable<CourseCategory>> GetAllCourseCategoriesAsync()
            {
                return await _courseCategoryRepository.ListAsync();
            }

            public async Task<CourseCategory> GetCourseCategoryByIdAsync(Guid id)
            {
                return await _courseCategoryRepository.FindByIdAsync(id);
            }

            public async Task CreateCourseCategoryAsync(CourseCategory courseCategory)
            {
                await _courseCategoryRepository.CreateAsync(courseCategory);
            }

            public async Task UpdateCourseCategoryAsync(CourseCategory courseCategory)
            {
                await _courseCategoryRepository.UpdateAsync(courseCategory);
            }

            public async Task DeleteCourseCategoryAsync(Guid id)
            {
                await _courseCategoryRepository.DeleteAsync(id);
            }
        }

    
}
