using Application.RespType;
using ApplicationLayer.DTOs.Course;
using ApplicationLayer.DTOs.CourseCategory;
using AutoMapper;
using DomainLayer.Entities;
using InfrastructureLayer.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ApplicationLayer.Services.Courses
{
    public class CourseService : BaseService, ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        public CourseService( IGenericRepository<Course> courseRepository, IMapper mapper, IHttpContextAccessor httpCtx) : base(mapper, httpCtx) 
        {
            _courseRepository = courseRepository;
        }
        public async Task<GenericResp<CourseResponseModel>> CreateCourseAsync(CourseCreateDto model)
        {
            try
            {
                var course = _mapper.Map<Course>(model);
                course.Id = Guid.NewGuid();
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

        public async Task<DynamicResponse<CourseResponseModel>> GetAllCoursesAsync(GetAllCourseDto model)
        {
            try
            {
                var listCourse = await _courseRepository.ListAsync();
                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<Course> listCourseByName = listCourse.Where(a => a.Name.ToLower().Contains(model.keyWord)).ToList();

                    List<Course> listCourseByDiscription = listCourse.Where(a => a.Description.ToLower().Contains(model.keyWord)).ToList();

                    listCourse = listCourseByName
                        .Concat(listCourseByDiscription)
                        .GroupBy(b => b.Id)
                        .Select(g => g.First())
                        .ToList();
                }
                if (model.Status != null)
                {
                    listCourse = listCourse.Where(c => c.Status == model.Status).ToList();
                }
                var result = _mapper.Map<List<CourseResponseModel>>(listCourse);

                var pageCourse = result
                    .OrderBy(c => c.Id)
                    .ToPagedList(model.pageNum, model.pageSize);
                return new DynamicResponse<CourseResponseModel>()
                {
                    Code = 200,
                    Message = null,

                    Data = new MegaData<CourseResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageCourse.PageNumber,
                            Size = pageCourse.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageCourse.PageCount,
                            TotalItem = pageCourse.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageCourse.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<CourseResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error!",
                    Data = null
                };
            }
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

        public async Task<ICollection<Course>> List(Guid? userId = null)
        {
            bool noFiltersApplied = userId == Guid.Empty;
            if (noFiltersApplied)
            {
                return await _courseRepository.ListAsync();
            }
            return await _courseRepository.WhereAsync(up =>
                    (up.Tutorid == userId));
        }
    }

}
