using ApplicationLayer.DTOs.Lesson;
using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Helper;
using InfrastructureLayer.Repository;
using InfrastructureLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Lesson
{
    public class LessonsService : ILessonsService
    {
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;

        public LessonsService(ILessonsRepository lessonsRepository, IMapper mapper)
        {
            _lessonsRepository = lessonsRepository;
            _mapper = mapper;
        }
        public async Task<List<LessonDto>> GetAllLessonsAsync()
        {
            var lessons = await _lessonsRepository.ListAsync();
            var lessonsMapper = _mapper.Map<List<LessonDto>>(lessons);
            return lessonsMapper;
        }
        public async Task<LessonDto> GetLessonsByIdAsync(Guid lessonId)
        {
            var lesson = await _lessonsRepository.FindByIdAsync(lessonId);
            var lessonMapper = _mapper.Map<LessonDto>(lesson);
            return lessonMapper;
        }
        public async Task<ResponseDto> CreateLessonsAsync(CreateLessonDto lessonDto)
        {
            var lessonObj = _mapper.Map<Lessons>(lessonDto);
            await _lessonsRepository.CreateAsync(lessonObj);
            var response = new ResponseDto
            {
                IsSucceed = true,
                Message = "Lesson added successfully",
            };
            return response;
        }
        public async Task<ResponseDto> UpdateLessonsAsync(Guid lessonId, UpdateLessonDto updateLessonDto)
        {
            var lessonUpdate = await _lessonsRepository.FindByIdAsync(lessonId);
            if (lessonUpdate != null)
            {
                lessonUpdate = _mapper.Map(updateLessonDto, lessonUpdate);
                await _lessonsRepository.UpdateAsync(lessonUpdate);
                return new ResponseDto
                {
                    IsSucceed = true,
                    Message = "Lesson updated successfully!"
                };
            }
            return new ResponseDto
            {
                IsSucceed = false,
                Message = "Lesson not found!"
            };
        }
        public async Task<ResponseDto> DeleteLessonsAsync(Guid lessonId)
        {
            var lesson = await _lessonsRepository.FindByIdAsync(lessonId);
            if (lesson != null)
            {
                await _lessonsRepository.DeleteAsync(lesson);
                return new ResponseDto
                {
                    IsSucceed = true,
                    Message = "Lesson deleted successfully!"
                };
            }
            return new ResponseDto
            {
                IsSucceed = false,
                Message = "Lesson not found!"
            };
        }
    }
}
