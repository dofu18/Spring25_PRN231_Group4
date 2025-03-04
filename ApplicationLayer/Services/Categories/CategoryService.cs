using Application.RespType;
using ApplicationLayer.DTOs.Category;
using ApplicationLayer.DTOs.Course;
using AutoMapper;
using DomainLayer.Entities;
using InfrastructureLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ApplicationLayer.Services.Categories
{
    public class CategoryService : ICategoryService
    {

        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<GenericResp<CategoryResponseModel>> CreateCategoryAsync(CategoryCreateDto model)
        {
            try
            {
                var category = _mapper.Map<Category>(model);
                category.Active = true;
                await _categoryRepository.CreateAsync(category);
                return new GenericResp<CategoryResponseModel>()
                {
                    Code = 201,
                    Message = "Create Category success",
                    Data = _mapper.Map<CategoryResponseModel>(model)
                };
            }
            catch (Exception ex)
            {
                return new GenericResp<CategoryResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error",
                    Data = null
                };
            }
        }

        public async Task<GenericResp<CategoryResponseModel>> DeleteCategoryAsync(Guid id, bool status)
        {
            try
            {
                var category = await _categoryRepository.FindByIdAsync(id);
                if (category == null)
                {
                    return new GenericResp<CategoryResponseModel>()
                    {
                        Code = 404,
                        Message = "Not found category",
                        Data = null
                    };
                }
                category.Active = status;
                await _categoryRepository.UpdateAsync(category);
                return new GenericResp<CategoryResponseModel>()
                {
                    Code = 200,
                    Message = " Not found Course!",
                    Data = _mapper.Map<CategoryResponseModel>(category)
                };
            }
            catch (Exception ex)
            {
                return new GenericResp<CategoryResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<CategoryResponseModel>> GetAllCategoriesAsync(GetAllCategoryRequestModel model)
        {
            try
            {
                var listCategory = await _categoryRepository.ListAsync();
                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<Category> listCategoryByName = listCategory.Where(a => a.Name.ToLower().Contains(model.keyWord)).ToList();

                    List<Category> listCategoryByDiscription = listCategory.Where(a => a.Description.ToLower().Contains(model.keyWord)).ToList();

                    listCategory = listCategoryByName
                        .Concat(listCategoryByDiscription)
                        .GroupBy(b => b.Id)
                        .Select(g => g.First())
                        .ToList();
                }
                if (model.Status != null)
                {
                    listCategory = listCategory.Where(c => c.Active == model.Status).ToList();
                }
                var result = _mapper.Map<List<CategoryResponseModel>>(listCategory);

                var pageCategory = result
                    .OrderBy(c => c.Id)
                    .ToPagedList(model.pageNum, model.pageSize);
                return new DynamicResponse<CategoryResponseModel>()
                {
                    Code = 200,
                    Message = null,

                    Data = new MegaData<CategoryResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageCategory.PageNumber,
                            Size = pageCategory.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageCategory.PageCount,
                            TotalItem = pageCategory.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageCategory.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<CategoryResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<GenericResp<CategoryResponseModel>> GetCategoryByIdAsync(Guid id)
        {
            try
            {
                var category = await _categoryRepository.FindByIdAsync(id);
                if (category == null)
                {
                    return new GenericResp<CategoryResponseModel>()
                    {
                        Code = 404,
                        Message = "Not found Category",
                        Data = null
                    };
                }
                return new GenericResp<CategoryResponseModel>()
                {
                    Code = 200,
                    Message = null,
                    Data = _mapper.Map<CategoryResponseModel>(category)
                };
            }
            catch (Exception ex)
            {
                return new GenericResp<CategoryResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<GenericResp<CategoryResponseModel>> UpdateCategoryAsync(CategoryCreateDto model, Guid id)
        {
            try
            {
                var category = await _categoryRepository.FindByIdAsync(id);
                if (category == null)
                {
                    return new GenericResp<CategoryResponseModel>()
                    {
                        Code = 404,
                        Message = "Not Found Category!",
                        Data = null
                    };
                }
                await _categoryRepository.UpdateAsync(_mapper.Map(model, category));
                return new GenericResp<CategoryResponseModel>()
                {
                    Code = 200,
                    Message = "Update Category success!",
                    Data = _mapper.Map<CategoryResponseModel>(category)
                };
            }
            catch (Exception ex)
            {
                return new GenericResp<CategoryResponseModel>()
                {
                    Code = 500,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }
    }


}
