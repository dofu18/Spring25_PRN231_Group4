using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.RespType;
using ApplicationLayer.DTOs.Courses;
using ApplicationLayer.Shared;
using AutoMapper;
using DomainLayer.Entities;
using InfrastructureLayer.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DomainLayer.Enums.GeneralEnum;

namespace ApplicationLayer.Services.Courses
{
    public class CourseService : BaseService, ICourseService
    {
        private readonly IGenericRepository<Course> _repo;

        public CourseService(IGenericRepository<Course> Repo, IMapper mapper, IHttpContextAccessor httpCtx) : base(mapper, httpCtx)
        {
            _repo = Repo;
        }

        public async Task<IActionResult> GetAllCourseAsync(CourseQuery query, CourseStatusEnum? status)
        {
            string searchKeyword = query.SearchKeyword ?? "";
            int page = query.Page < 0 ? 0 : query.Page;
            int pageSize = query.PageSize <= 0 ? 10 : query.PageSize;

            var filter = PredicateBuilder.True<Course>();

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                filter = filter.And(u => u.Name.Contains(searchKeyword) ||
                                         u.Description.Contains(searchKeyword));
            }

            if (status != null)
            {
                filter = filter.And(u => u.Status == status);
            }

            var courses = await _repo.WhereAsync(
                filter,
                orderBy: q => q.OrderByDescending(u => u.CreatedAt),
                page: page,
                pageSize: pageSize
            );

            var totalCourses = await _repo.CountAsync(filter);

            var result = new
            {
                Data = _mapper.Map<IEnumerable<Course>>(courses),
                Total = totalCourses,
                Page = query.Page,
                PageSize = query.PageSize
            };

            return SuccessResp.Ok(result);

        }

        public async Task<IActionResult> GetCourseActive(CourseQuery query)
        {
            string searchKeyword = query.SearchKeyword ?? "";
            int page = query.Page < 0 ? 0 : query.Page;
            int pageSize = query.PageSize <= 0 ? 10 : query.PageSize;

            //var courses = await _repo.WhereAsync(c => c.Status == CourseStatusEnum.Publish);

            //if (!courses.Any())
            //{
            //    return ErrorResp.NotFound("No course found");
            //}
            //var result = _mapper.Map<List<CourseDto>>(courses);

            //return SuccessResp.Ok(courses);
            var filter = PredicateBuilder.True<Course>();

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                filter = filter.And(u => u.Name.Contains(searchKeyword) ||
                                         u.Description.Contains(searchKeyword));
            }

            filter = filter.And(u => u.Status == CourseStatusEnum.Publish);


            var courses = await _repo.WhereAsync(
                filter,
                orderBy: q => q.OrderByDescending(u => u.CreatedAt),
                page: page,
                pageSize: pageSize
            );

            var totalCourses = await _repo.CountAsync(filter);

            var result = new
            {
                Data = _mapper.Map<IEnumerable<Course>>(courses),
                Total = totalCourses,
                Page = query.Page,
                PageSize = query.PageSize
            };

            return SuccessResp.Ok(result);
        }

        public async Task<IActionResult> HandleCreateCourse(CourseCreateDto dto, Guid userId)
        {
            var course = _mapper.Map<Course>(dto);
            course.Discount = 0;
            course.Status = CourseStatusEnum.Draft;
            course.Tutorid = userId;
            course.CreatedAt = DateTime.Now;
            course.UpdatedAt = DateTime.Now;
            await _repo.CreateAsync(course);

            return SuccessResp.Created("Course created successfully");
        }

        public async Task<IActionResult> HandleDeleteAsync(Guid courseId)
        {
            var course = await _repo.FindByIdAsync(courseId);

            if (course == null)
            {
                return ErrorResp.NotFound("Course not found");
            }

            course.Status = CourseStatusEnum.Disable;

            await _repo.UpdateAsync(course);

            return SuccessResp.Ok($"Deleted Course {courseId}");
        }

        public async Task<IActionResult> HandleGetByIdAsync(Guid courseId)
        {
            var course = await _repo.FoundOrThrowAsync(courseId);

            return SuccessResp.Ok(course);
        }

        public async Task<IActionResult> HandleStatusAsync(Guid courseId, CourseStatusEnum status)
        {
            var course = await _repo.FindByIdAsync(courseId);

            if (course == null)
            {
                return ErrorResp.NotFound("Course not found");
            }

            course.Status = status;

            await _repo.UpdateAsync(course);

            return SuccessResp.Ok($"Updated course with status {status}");
        }

        public async Task<IActionResult> HandleUpdateAsync(Guid courseId, CourseUpdateDto dto)
        {
            var course = await _repo.FoundOrThrowAsync(courseId);

            _mapper.Map(dto, course);

            await _repo.UpdateAsync(course);

            return SuccessResp.Ok(course);
        }
    }
}
