using ApplicationLayer.DTOs.TutorProfile;
using DomainLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.TutorProfiles
{
    public interface ITutorProfileService
    {
        Task<List<TutorProfileDto>> GetAllTutorProfileAsync();
        Task<TutorProfileDto> GetTutorProfileByIdAsync(Guid tutorId);
        Task<ResponseDto> CreateTutorProfileAsync(CreateTutorProfileDto tutorProfileDTO);
        Task<ResponseDto> UpdateTutorProfileAsync(Guid tutorId, UpdateTutorProfileDto tutorProfileDTO);
        Task<ResponseDto> DeleteTutorProfileAsync(Guid tutorId);
    }
}
