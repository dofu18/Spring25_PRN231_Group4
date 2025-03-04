using Application.RespType;
using ApplicationLayer.DTOs.Course;
using AutoMapper;
using DomainLayer.Entities;
using InfrastructureLayer.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Course> _courseRepository;
        public CourseService(IMapper mapper, IGenericRepository<Course> courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }
        public async Task<GenericResp<CourseResponseModel>> CreateCourseAsync(CourseCreateDto model)
        {
            try
            {
                var course = _mapper.Map<Course>(model);
                course.Status = true;
                await _courseRepository.CreateAsync(course);
                return new GenericResp<CourseResponseModel>()
                {
                    Code = 201,
                    Message = "Create Course success",
                    Data = _mapper.Map<CourseResponseModel>(model)
                };
            }
            catch (Exception ex)
            {
                return new GenericResp<CourseResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error",
                    Data = null
                };
            }
        }

        public async Task<GenericResp<CourseResponseModel>> DeleteCourseAsync(Guid id, bool status)
        {
            try
            {
                var course = await _courseRepository.FindByIdAsync(id);
                if(course == null)
                {
                    return new GenericResp<CourseResponseModel>()
                    {
                        Code = 404,
                        Message = "Not found Course",
                        Data = null
                    };
                }
                course.Status = status;
                await _courseRepository.UpdateAsync(course);
                return new GenericResp<CourseResponseModel>()
                {
                    Code = 200,
                    Message = " Not found Course!",
                    Data = _mapper.Map<CourseResponseModel>(course)
                };
            }
            catch(Exception ex)
            {
                return new GenericResp<CourseResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error",
                    Data = null
                };
            }
        }

        public Task<DynamicResponse<CourseResponseModel>> GetAllCoursesAsync(GetAllCourseDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResp<CourseResponseModel>> GetCourseByIdAsync(Guid id)
        {
            try
            {
                var course = await _courseRepository.FindByIdAsync(id);
                if(course == null)
                {
                    return new GenericResp<CourseResponseModel>()
                    {
                        Code = 404,
                        Message = "Not found Course",
                        Data = null
                    };
                }
                return new GenericResp<CourseResponseModel>()
                {
                    Code = 200,
                    Message = null,
                    Data = _mapper.Map<CourseResponseModel>(course)
                };
            }
            catch (Exception ex)
            {
                return new GenericResp<CourseResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<GenericResp<CourseResponseModel>> UpdateCourseAsync(CourseCreateDto model, Guid id)
        {
            try
            {
                var course = await _courseRepository.FindByIdAsync(id);
                if(course == null)
                {
                    return new GenericResp<CourseResponseModel>()
                    {
                        Code = 404,
                        Message = "Not Found Course!",
                        Data = null
                    };
                }
                await _courseRepository.UpdateAsync(_mapper.Map(model, course));
                return new GenericResp<CourseResponseModel>()
                {
                    Code = 200,
                    Message = "Update Course success!",
                    Data = _mapper.Map<CourseResponseModel>(course)
                };
            }
            catch(Exception ex)
            {
                return new GenericResp<CourseResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }
    }

}
