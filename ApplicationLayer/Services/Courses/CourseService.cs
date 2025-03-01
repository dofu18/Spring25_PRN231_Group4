using DomainLayer.Entities;
using InfrastructureLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;

        public CourseService(IGenericRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.ListAsync();
        }

        public async Task<Course> GetCourseByIdAsync(Guid id)
        {
            return await _courseRepository.FindByIdAsync(id);
        }

        public async Task CreateCourseAsync(Course course)
        {
            await _courseRepository.CreateAsync(course);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            await _courseRepository.UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            await _courseRepository.DeleteAsync(id);
        }

        
    }

}
