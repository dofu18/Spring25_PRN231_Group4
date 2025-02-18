using Application.RespType;
using ApplicationLayer.DTOs.Courses;
using AutoMapper;
using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Enums;
using DomainLayer.Exceptions;
using InfrastructureLayer;
using InfrastructureLayer.Repository.IRepository;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Courses
{
    public class CourseService : BaseService, ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        public CourseService(IGenericRepository<Course> courseRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Create(CourseDto dto)
        {
            var course = _mapper.Map<Course>(dto);
            course.CreatedBy = new Guid("11111111-1111-1111-1111-111111111111");
            course.CreatedAt = DateTime.Now;
            course.UpdatedAt = DateTime.Now;
            course.UpdatedBy = new Guid("11111111-1111-1111-1111-111111111111");
            await _courseRepository.CreateAsync(course);

            return SuccessResp.Created("Course created successfully");
        }

        public Task<IActionResult> Delete(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Course>> GetById(Guid courseId, GeneralEnum.IdType idType)
        {
            if(idType == GeneralEnum.IdType.Id)
            {
                List<Course> newCourseList = new List<Course>();
                newCourseList.Add(await _courseRepository.FoundOrThrowAsync(courseId, Constants.Entities.ORDER + Constants.Errors.NOT_EXIST_ERROR));
                return newCourseList;
            }else if(idType == GeneralEnum.IdType.UserId)
            {
                return await _courseRepository.WhereAsync(x => x.CreatedBy == courseId, "User");
            }
            throw new BadRequestException("ID Type not Exist");
        }

        public async Task<ICollection<Course>> List(Guid? courseId = null)
        {
            bool noFiltersApplied = courseId == Guid.Empty;
            if(noFiltersApplied)
            {
                return await _courseRepository.ListAsync();
            }
            return await _courseRepository.WhereAsync(a =>(a.CreatedBy == courseId));
        }

        public Task<IActionResult> Update(Course course)
        {
            throw new NotImplementedException();
        }


        /*private readonly TutoringKidDbContext _context;

        public CourseService(TutoringKidDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Tutor)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(Guid courseId)
        {
            return await _context.Courses
                .Include(c => c.Tutor)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task<bool> AddAsync(Course course)
        {
            _context.Courses.Add(course);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null) return false;

            _context.Courses.Remove(course);
            return await _context.SaveChangesAsync() > 0;
        }*/


    }

}
