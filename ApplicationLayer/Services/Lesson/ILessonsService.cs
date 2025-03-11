using ApplicationLayer.DTOs.Lesson;
using DomainLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Lesson
{
    public interface ILessonsService
    {
        Task<List<LessonDto>> GetAllLessonsAsync();
        Task<LessonDto> GetLessonsByIdAsync(Guid lessonId);
        Task<ResponseDto> CreateLessonsAsync(CreateLessonDto lessonDto);
        Task<ResponseDto> UpdateLessonsAsync(Guid lessonId, UpdateLessonDto lessonDto);
        Task<ResponseDto> DeleteLessonsAsync(Guid lessonId);
    }
}
