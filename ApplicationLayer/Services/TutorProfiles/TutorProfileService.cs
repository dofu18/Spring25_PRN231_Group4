using ApplicationLayer.DTOs.TutorProfile;
using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Helper;
using InfrastructureLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.TutorProfiles
{
    public class TutorProfileService : ITutorProfileService
    {
        private readonly ITutorProfileRepository _tutorProfileRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public TutorProfileService(ITutorProfileRepository tutorProfileRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _tutorProfileRepository = tutorProfileRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task<List<TutorProfileDto>> GetAllTutorProfileAsync()
        {
            var tutorProfiles = await _tutorProfileRepository.ListAsync();
            var tutorProfileMapper = _mapper.Map<List<TutorProfileDto>>(tutorProfiles);
            return tutorProfileMapper;
        }
        public async Task<TutorProfileDto> GetTutorProfileByIdAsync(Guid tutorId)
        {
            var tutorProfile = await _tutorProfileRepository.FindByIdAsync(tutorId);
            var tutorProfileMapper = _mapper.Map<TutorProfileDto>(tutorProfile);
            return tutorProfileMapper;
        }
        public async Task<ResponseDto> CreateTutorProfileAsync(CreateTutorProfileDto createTutorProfileDto)
        {
            var account = await _accountRepository.FindByIdAsync(createTutorProfileDto.UserId);
            if (account == null)
            {
                return new ResponseDto
                {
                    IsSucceed = false,
                    Message = "Account not found",
                };
            }

            var tutorProfileObj = _mapper.Map<TutorProfile>(createTutorProfileDto);
            tutorProfileObj.User = account;

            await _tutorProfileRepository.CreateAsync(tutorProfileObj);

            var response = new ResponseDto
            {
                IsSucceed = true,
                Message = "Tutor added successfully",
            };

            return response;
        }
        public async Task<ResponseDto> UpdateTutorProfileAsync(Guid tutorId, UpdateTutorProfileDto updateTutorProfileDTO)
        {
            var tutorProfileUpdate = await _tutorProfileRepository.FindByIdAsync(tutorId);
            if (tutorProfileUpdate != null)
            {
                tutorProfileUpdate = _mapper.Map(updateTutorProfileDTO, tutorProfileUpdate);
                await _tutorProfileRepository.UpdateAsync(tutorProfileUpdate);
                return new ResponseDto
                {
                    IsSucceed = true,
                    Message = "TutorProfile updated successfully!"
                };
            }
            return new ResponseDto
            {
                IsSucceed = false,
                Message = "TutorProfile not found!"
            };
        }
        public async Task<ResponseDto> DeleteTutorProfileAsync(Guid tutorId)
        {
            var deletetutorProfile = await _tutorProfileRepository.FindByIdAsync(tutorId);
            if (deletetutorProfile != null)
            {
                await _tutorProfileRepository.DeleteAsync(tutorId);

                return new ResponseDto
                {
                    IsSucceed = true,
                    Message = "TutorProfile deleted successfully"
                };
            }
            else
            {
                return new ResponseDto
                {
                    IsSucceed = false,
                    Message = $"TutorProfile with ID {tutorId} not found"
                };
            }
        }
    }
}
