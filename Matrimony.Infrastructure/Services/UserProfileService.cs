using Matrimony.Application.Features.Profile.CreateProfile;
using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Domain.Entities;
using Matrimony.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _profileRepository;
        public UserProfileService(IUserProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<List<ProfileResponse>> GetAllAsync()
        {
            var profiles = await _profileRepository.GetAllAsync();

            return profiles.Select(MapProfileResponse).ToList();
        }
        public async Task<ProfileResponse?> GetProfileByIdAsync(Guid id)
        {
            var profile = await _profileRepository.GetProfileByIdAsync(id);

            if (profile == null)
                throw new NotFoundException("Profile not found.");

            return MapProfileResponse(profile);
        }
        public async Task<ProfileResponse?> GetProfileByUserIdAsync(Guid userId)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(userId);

            if (profile == null)
                throw new NotFoundException("Profile not found.");

            return MapProfileResponse(profile);
        }
        public async Task CreateAsync(Guid userId, CreateProfileRequest request)
        {
            if (await _profileRepository.ExistsAsync(userId))
                throw new BusinessException("Profile already exists.");

            var profileId = await _profileRepository.GenerateProfileIdAsync();

            var profile = new UserProfile
            {
                UserId = userId,
                ProfileId = profileId,
                DateOfBirth = request.DateOfBirth,
                HeightFeet = request.HeightFeet,
                HeightInches = request.HeightInches,
                Weight = request.Weight,
                ReligionId = request.ReligionId,
                CasteId = request.CasteId,
                MotherTongueId = request.MotherTongueId,
                EducationId = request.EducationId,
                OccupationId = request.OccupationId,
                CountryId = request.CountryId,
                StateId = request.StateId,
                CityId = request.CityId,
                AnnualIncome = request.AnnualIncome,
                AboutMe = request.AboutMe
            };

            await _profileRepository.AddAsync(profile);
        }
        public async Task UpdateAsync(UpdateProfileRequest request)
        {
            var profile = await _profileRepository.GetProfileByIdAsync(request.Id);

            if (profile == null)
                throw new NotFoundException("Profile not found.");

            profile.DateOfBirth = request.DateOfBirth;
            profile.HeightFeet = request.HeightFeet;
            profile.HeightInches = request.HeightInches;
            profile.Weight = request.Weight;

            profile.ReligionId = request.ReligionId;
            profile.CasteId = request.CasteId;
            profile.MotherTongueId = request.MotherTongueId;
            profile.EducationId = request.EducationId;
            profile.OccupationId = request.OccupationId;

            profile.CountryId = request.CountryId;
            profile.StateId = request.StateId;
            profile.CityId = request.CityId;

            profile.AnnualIncome = request.AnnualIncome;
            profile.AboutMe = request.AboutMe;

            await _profileRepository.UpdateAsync(profile);
        }
        public async Task DeleteAsync(Guid id)
        {
            var profile = await _profileRepository.GetProfileByIdAsync(id);

            if (profile == null)
                throw new NotFoundException("Profile not found.");

            await _profileRepository.DeleteAsync(profile);
        }
        private static ProfileResponse MapProfileResponse(UserProfile profile)
        {
            return new ProfileResponse
            {
                Id = profile.Id,
                UserId = profile.UserId,
                ProfileId = profile.ProfileId,

                DateOfBirth = profile.DateOfBirth,

                HeightFeet = profile.HeightFeet,
                HeightInches = profile.HeightInches,
                Weight = profile.Weight,

                ReligionId = profile.ReligionId,
                Religion = profile.Religion.Name,

                CasteId = profile.CasteId,
                Caste = profile.Caste.Name,

                MotherTongueId = profile.MotherTongueId,
                MotherTongue = profile.MotherTongue.Name,

                EducationId = profile.EducationId,
                Education = profile.Education.Name,

                OccupationId = profile.OccupationId,
                Occupation = profile.Occupation.Name,

                CountryId = profile.CountryId,
                Country = profile.Country.Name,

                StateId = profile.StateId,
                State = profile.State.Name,

                CityId = profile.CityId,
                City = profile.City.Name,

                AnnualIncome = profile.AnnualIncome,

                AboutMe = profile.AboutMe
            };
        }
    }
}
