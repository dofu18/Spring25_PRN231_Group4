using ApplicationLayer.DTOs.Category;
using ApplicationLayer.DTOs.Course;
using ApplicationLayer.DTOs.CourseCategory;
using ApplicationLayer.DTOs.Account;
using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.DTOs.Orders;
using ApplicationLayer.DTOs.TutorProfile;
using AutoMapper;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<User, AccountDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.HashedPassword, opt => opt.Ignore())
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status));
            CreateMap<TutorProfile, TutorProfileDto>().ReverseMap();
            CreateMap<Order, OrderCreateDto>().ReverseMap();

            //Course
            CreateMap<CourseCreateDto, Course>().ReverseMap();
            CreateMap<CourseCreateDto, CourseResponseModel>().ReverseMap();
            CreateMap<CourseCreateDto, Course>().ReverseMap();
            CreateMap<CourseResponseModel, Course>().ReverseMap();

            //Category
            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            CreateMap<CategoryCreateDto, CategoryResponseModel>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            CreateMap<CategoryResponseModel, Category>().ReverseMap();

            //CourseCategory
            CreateMap<CourseCategoryCreateDto, CourseCategory>().ReverseMap();
            CreateMap<CourseCategoryCreateDto, CourseCategoryResponseModel>().ReverseMap();
            CreateMap<CourseCategoryCreateDto, CourseCategory>().ReverseMap();
            CreateMap<CourseCategoryResponseModel, CourseCategory>().ReverseMap();
        }
    }
}
