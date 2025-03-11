using ApplicationLayer.DTOs.Account;
using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.DTOs.Category;
using ApplicationLayer.DTOs.CourseCategory;
using ApplicationLayer.DTOs.Lesson;
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
            //Account Mapping
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<User, AccountDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.HashedPassword, opt => opt.Ignore())
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status));

            // TutorProfile Mapping
            CreateMap<TutorProfile, TutorProfileDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Meta, opt => opt.MapFrom(src => src.Meta));

            CreateMap<CreateTutorProfileDto, TutorProfile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
            CreateMap<UpdateTutorProfileDto, TutorProfile>();

            // Order Mapping
            CreateMap<Order, OrderCreateDto>().ReverseMap();

            // Lesson Mapping
            CreateMap<Lessons, LessonDto>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.OrderIndex, opt => opt.MapFrom(src => src.OrderIndex));
            CreateMap<CreateLessonDto, Lessons>().ReverseMap();
            CreateMap<UpdateLessonDto, Lessons>().ReverseMap();
        }
    }
}
