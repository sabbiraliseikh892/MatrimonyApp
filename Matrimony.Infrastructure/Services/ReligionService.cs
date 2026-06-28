using Matrimony.Application.Features.Masters.Religion;
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
    public class ReligionService : IReligionService
    {
        private readonly IReligionRepository _religionRepository;

        public ReligionService(IReligionRepository religionRepository)
        {
            _religionRepository = religionRepository;
        }

        public async Task<List<ReligionResponse>> GetAllAsync()
        {
            var religions = await _religionRepository.GetAllAsync();

            return religions.Select(x => new ReligionResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public async Task<ReligionResponse?> GetByIdAsync(Guid id)
        {
            var religion = await _religionRepository.GetByIdAsync(id);

            if (religion == null)
                throw new NotFoundException("Religion not found.");

            return new ReligionResponse
            {
                Id = religion.Id,
                Name = religion.Name
            };
        }

        public async Task AddAsync(CreateReligionRequest request)
        {
            if (await _religionRepository.ExistsAsync(request.Name))
                throw new BusinessException("Religion already exists.");

            var religion = new ReligionMaster
            {
                Name = request.Name,
                IsActive = true
            };

            await _religionRepository.AddAsync(religion);
        }

        public async Task UpdateAsync(UpdateReligionRequest request)
        {
            var religion = await _religionRepository.GetByIdAsync(request.Id);

            if (religion == null)
                throw new NotFoundException("Religion not found.");

            religion.Name = request.Name;

            await _religionRepository.UpdateAsync(religion);
        }

        public async Task DeleteAsync(Guid id)
        {
            var religion = await _religionRepository.GetByIdAsync(id);

            if (religion == null)
                throw new NotFoundException("Religion not found.");

            await _religionRepository.DeleteAsync(religion);
        }
    }
}
