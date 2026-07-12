using Matrimony.Application.Features.PartnerPreference;
using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Domain.Entities;
using Matrimony.Infrastructure.Repositories;
using Matrimony.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Services
{
    public class PartnerPreferenceService : IPartnerPreferenceService
    {
        private readonly IPartnerPreferenceRepository _repository;

        public PartnerPreferenceService(
            IPartnerPreferenceRepository repository)
        {
            _repository = repository;
        }
        public async Task CreateAsync(
    Guid userId,
    CreatePartnerPreferenceRequest request)
        {
            var existing = await _repository.GetByUserIdAsync(userId);

            if (existing != null)
                throw new BusinessException("Partner preference already exists.");

            var preference = new PartnerPreference
            {
                UserId = userId,

                MinAge = request.MinAge,
                MaxAge = request.MaxAge,

                MinHeightFeet = request.MinHeightFeet,
                MinHeightInches = request.MinHeightInches,

                MaxHeightFeet = request.MaxHeightFeet,
                MaxHeightInches = request.MaxHeightInches,

                MinAnnualIncome = request.MinAnnualIncome,
                MaxAnnualIncome = request.MaxAnnualIncome,

                ReligionId = request.ReligionId,
                CasteId = request.CasteId,
                MotherTongueId = request.MotherTongueId,
                EducationId = request.EducationId,
                OccupationId = request.OccupationId,
                CountryId = request.CountryId,
                StateId = request.StateId,
                CityId = request.CityId,

                AcceptDivorced = request.AcceptDivorced,
                AcceptWidowed = request.AcceptWidowed,
                AcceptWithChildren = request.AcceptWithChildren
            };

            await _repository.AddAsync(preference);
        }
        public async Task<PartnerPreferenceResponse?> GetMyPreferenceAsync(Guid userId)
        {
            var preference = await _repository.GetByUserIdAsync(userId);

            if (preference == null)
                return null;

            return new PartnerPreferenceResponse
            {
                Id = preference.Id,

                MinAge = preference.MinAge,
                MaxAge = preference.MaxAge,

                MinHeightFeet = preference.MinHeightFeet,
                MinHeightInches = preference.MinHeightInches,

                MaxHeightFeet = preference.MaxHeightFeet,
                MaxHeightInches = preference.MaxHeightInches,

                MinAnnualIncome = preference.MinAnnualIncome,
                MaxAnnualIncome = preference.MaxAnnualIncome,

                ReligionId = preference.ReligionId,
                CasteId = preference.CasteId,
                MotherTongueId = preference.MotherTongueId,
                EducationId = preference.EducationId,
                OccupationId = preference.OccupationId,
                CountryId = preference.CountryId,
                StateId = preference.StateId,
                CityId = preference.CityId,

                AcceptDivorced = preference.AcceptDivorced,
                AcceptWidowed = preference.AcceptWidowed,
                AcceptWithChildren = preference.AcceptWithChildren
            };
        }
        public async Task UpdateAsync(
    Guid userId,
    UpdatePartnerPreferenceRequest request)
        {
            var preference = await _repository.GetByUserIdAsync(userId);

            if (preference == null)
                throw new NotFoundException("Partner preference not found.");

            preference.MinAge = request.MinAge;
            preference.MaxAge = request.MaxAge;

            preference.MinHeightFeet = request.MinHeightFeet;
            preference.MinHeightInches = request.MinHeightInches;

            preference.MaxHeightFeet = request.MaxHeightFeet;
            preference.MaxHeightInches = request.MaxHeightInches;

            preference.MinAnnualIncome = request.MinAnnualIncome;
            preference.MaxAnnualIncome = request.MaxAnnualIncome;

            preference.ReligionId = request.ReligionId;
            preference.CasteId = request.CasteId;
            preference.MotherTongueId = request.MotherTongueId;
            preference.EducationId = request.EducationId;
            preference.OccupationId = request.OccupationId;
            preference.CountryId = request.CountryId;
            preference.StateId = request.StateId;
            preference.CityId = request.CityId;

            preference.AcceptDivorced = request.AcceptDivorced;
            preference.AcceptWidowed = request.AcceptWidowed;
            preference.AcceptWithChildren = request.AcceptWithChildren;

            await _repository.UpdateAsync(preference);
        }
        public async Task DeleteAsync(Guid userId)
        {
            var preference = await _repository.GetByUserIdAsync(userId);

            if (preference == null)
                throw new NotFoundException("Partner preference not found.");

            await _repository.DeleteAsync(preference);
        }
       
    }
}
