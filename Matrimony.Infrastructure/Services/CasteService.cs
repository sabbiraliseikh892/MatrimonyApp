using Matrimony.Application.Features.Masters.Caste;
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
    public class CasteService : ICasteService
    {
        private readonly ICasteRepository _casteRepository;

        public CasteService(ICasteRepository casteRepository)
        {
            _casteRepository = casteRepository;
        }

        public async Task<List<CasteResponse>> GetAllAsync()
        {
            var castes = await _casteRepository.GetAllAsync();

            return castes.Select(x => new CasteResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public async Task<CasteResponse?> GetByIdAsync(Guid id)
        {
            var caste = await _casteRepository.GetByIdAsync(id);

            if (caste == null)
                throw new NotFoundException("Caste not found.");

            return new CasteResponse
            {
                Id = caste.Id,
                Name = caste.Name
            };
        }

        public async Task AddAsync(CreateCasteRequest request)
        {
            if (await _casteRepository.ExistsAsync(request.Name))
                throw new BusinessException("Caste already exists.");

            var caste = new CasteMaster
            {
                Name = request.Name,
                IsActive = true
            };

            await _casteRepository.AddAsync(caste);
        }

        public async Task UpdateAsync(UpdateCasteRequest request)
        {
            var caste = await _casteRepository.GetByIdAsync(request.Id);

            if (caste == null)
                throw new NotFoundException("Caste not found.");

            caste.Name = request.Name;

            await _casteRepository.UpdateAsync(caste);
        }

        public async Task DeleteAsync(Guid id)
        {
            var caste = await _casteRepository.GetByIdAsync(id);

            if (caste == null)
                throw new NotFoundException("Caste not found.");

            await _casteRepository.DeleteAsync(caste);
        }
    }
}
