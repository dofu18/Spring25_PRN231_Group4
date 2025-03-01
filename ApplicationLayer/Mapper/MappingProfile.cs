using ApplicationLayer.DTOs.Category;
using ApplicationLayer.DTOs.Course;
using ApplicationLayer.DTOs.CourseCategory;
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
            CreateMap<TutorProfile, TutorProfileDto>().ReverseMap();
            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<CourseCategory, CourseCategoryCreateDto>().ReverseMap();
        }
    }
}
