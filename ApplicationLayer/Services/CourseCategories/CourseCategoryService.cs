using Application.RespType;
using ApplicationLayer.DTOs.Course;
using ApplicationLayer.DTOs.CourseCategory;
using AutoMapper;
using DomainLayer.Entities;
using InfrastructureLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ApplicationLayer.Services.CourseCategories
{

    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly IGenericRepository<CourseCategory> _courseCategoryRepository;
        private readonly IMapper _mapper;

        public async Task<GenericResp<CourseCategoryResponseModel>> CreateCourseCategoryAsync(CourseCategoryCreateDto model)
        {
            try
            {
                var courseCate = _mapper.Map<CourseCategory>(model);
                await _courseCategoryRepository.CreateAsync(courseCate);
                return new GenericResp<CourseCategoryResponseModel>()
                {
                    Code = 201,
                    Message = "Create CourseCategory success",
                    Data = _mapper.Map<CourseCategoryResponseModel>(model)
                };
            }
            catch (Exception ex)
            {
                return new GenericResp<CourseCategoryResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error",
                    Data = null
                };
            }
        }

        public async Task<GenericResp<CourseCategoryResponseModel>> DeleteCourseCategoryAsync(Guid id)
        {
            try
            {
                var courseCate = await _courseCategoryRepository.FindByIdAsync(id);
                if (courseCate == null)
                {
                    return new GenericResp<CourseCategoryResponseModel>()
                    {
                        Code = 404,
                        Message = "Not found CourseCategory",
                        Data = null
                    };
                }
                await _courseCategoryRepository.UpdateAsync(courseCate);
                return new GenericResp<CourseCategoryResponseModel>()
                {
                    Code = 200,
                    Message = " Not found CourseCategory!",
                    Data = _mapper.Map<CourseCategoryResponseModel>(courseCate)
                };
            }
            catch (Exception ex)
            {
                return new GenericResp<CourseCategoryResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<CourseCategoryResponseModel>> GetAllCourseCategoriesAsync(GetAllCategoryCourseRequestModel model)
        {
            try
            {
                var listCourseCate = await _courseCategoryRepository.ListAsync();
                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<CourseCategory> listCourseCateByCourseId = listCourseCate.Where(a => a.CourseId.ToString().Contains(model.keyWord)).ToList();

                    List<CourseCategory> listCourseCateByCategoryId = listCourseCate.Where(a => a.CategoryId.ToString().Contains(model.keyWord)).ToList();

                    listCourseCate = listCourseCateByCourseId
                        .Concat(listCourseCateByCategoryId)
                        .GroupBy(b => b.Id)
                        .Select(g => g.First())
                        .ToList();
                }
                
                var result = _mapper.Map<List<CourseCategoryResponseModel>>(listCourseCate);

                var pageCourseCate = result
                    .OrderBy(c => c.CourseId)
                    .ToPagedList(model.pageNum, model.pageSize);
                return new DynamicResponse<CourseCategoryResponseModel>()
                {
                    Code = 200,
                    Message = null,

                    Data = new MegaData<CourseCategoryResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageCourseCate.PageNumber,
                            Size = pageCourseCate.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageCourseCate.PageCount,
                            TotalItem = pageCourseCate.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageCourseCate.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<CourseCategoryResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<GenericResp<CourseCategoryResponseModel>> GetCourseCategoryByIdAsync(Guid id)
        {
            try
            {
                var courseCate = await _courseCategoryRepository.FindByIdAsync(id);
                if (courseCate == null)
                {
                    return new GenericResp<CourseCategoryResponseModel>()
                    {
                        Code = 404,
                        Message = "Not found Course",
                        Data = null
                    };
                }
                return new GenericResp<CourseCategoryResponseModel>()
                {
                    Code = 200,
                    Message = null,
                    Data = _mapper.Map<CourseCategoryResponseModel>(courseCate)
                };
            }
            catch (Exception ex)
            {
                return new GenericResp<CourseCategoryResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<GenericResp<CourseCategoryResponseModel>> UpdateCourseCategoryAsync(CourseCategoryCreateDto model, Guid id)
        {
            try
            {
                var courseCate = await _courseCategoryRepository.FindByIdAsync(id);
                if (courseCate == null)
                {
                    return new GenericResp<CourseCategoryResponseModel>()
                    {
                        Code = 404,
                        Message = "Not Found CourseCategory!",
                        Data = null
                    };
                }
                await _courseCategoryRepository.UpdateAsync(_mapper.Map(model, courseCate));
                return new GenericResp<CourseCategoryResponseModel>()
                {
                    Code = 200,
                    Message = "Update CourseCategory success!",
                    Data = _mapper.Map<CourseCategoryResponseModel>(courseCate)
                };
            }
            catch (Exception ex)
            {
                return new GenericResp<CourseCategoryResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }
    }


}
