using Matrimony.Application.Features.Masters.Occupation;
using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Domain.Entities.Masters;
using Matrimony.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Services
{
    public class OccupationService : IOccupationService
    {
        private readonly IOccupationRepository _occupationRepository;

        public OccupationService(IOccupationRepository occupationRepository)
        {
            _occupationRepository = occupationRepository;
        }

        public async Task<List<OccupationResponse>> GetAllAsync()
        {
            var occupations = await _occupationRepository.GetAllAsync();

            return occupations.Select(x => new OccupationResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public async Task<OccupationResponse?> GetByIdAsync(Guid id)
        {
            var occupation = await _occupationRepository.GetByIdAsync(id);

            if (occupation == null)
                throw new NotFoundException("Occupation not found.");

            return new OccupationResponse
            {
                Id = occupation.Id,
                Name = occupation.Name
            };
        }

        public async Task AddAsync(CreateOccupationRequest request)
        {
            if (await _occupationRepository.ExistsAsync(request.Name))
                throw new BusinessException("Occupation already exists.");

            var occupation = new OccupationMaster
            {
                Name = request.Name,
                IsActive = true
            };

            await _occupationRepository.AddAsync(occupation);
        }

        public async Task UpdateAsync(UpdateOccupationRequest request)
        {
            var occupation = await _occupationRepository.GetByIdAsync(request.Id);

            if (occupation == null)
                throw new NotFoundException("Occupation not found.");

            occupation.Name = request.Name;

            await _occupationRepository.UpdateAsync(occupation);
        }

        public async Task DeleteAsync(Guid id)
        {
            var occupation = await _occupationRepository.GetByIdAsync(id);

            if (occupation == null)
                throw new NotFoundException("Occupation not found.");

            await _occupationRepository.DeleteAsync(occupation);
        }
    }
}
