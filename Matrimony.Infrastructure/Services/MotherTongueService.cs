using Matrimony.Application.Features.Masters.MotherTongue;
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
    public class MotherTongueService : IMotherTongueService
    {
        private readonly IMotherTongueRepository _motherTongueRepository;

        public MotherTongueService(IMotherTongueRepository motherTongueRepository)
        {
            _motherTongueRepository = motherTongueRepository;
        }

        public async Task<List<MotherTongueResponse>> GetAllAsync()
        {
            var motherTongues = await _motherTongueRepository.GetAllAsync();

            return motherTongues.Select(x => new MotherTongueResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public async Task<MotherTongueResponse?> GetByIdAsync(Guid id)
        {
            var motherTongue = await _motherTongueRepository.GetByIdAsync(id);

            if (motherTongue == null)
                throw new NotFoundException("Mother tongue not found.");

            return new MotherTongueResponse
            {
                Id = motherTongue.Id,
                Name = motherTongue.Name
            };
        }

        public async Task AddAsync(CreateMotherTongueRequest request)
        {
            if (await _motherTongueRepository.ExistsAsync(request.Name))
                throw new BusinessException("Mother tongue already exists.");

            var motherTongue = new MotherTongueMaster
            {
                Name = request.Name,
                IsActive = true
            };

            await _motherTongueRepository.AddAsync(motherTongue);
        }

        public async Task UpdateAsync(UpdateMotherTongueRequest request)
        {
            var motherTongue = await _motherTongueRepository.GetByIdAsync(request.Id);

            if (motherTongue == null)
                throw new NotFoundException("Mother tongue not found.");

            motherTongue.Name = request.Name;

            await _motherTongueRepository.UpdateAsync(motherTongue);
        }

        public async Task DeleteAsync(Guid id)
        {
            var motherTongue = await _motherTongueRepository.GetByIdAsync(id);

            if (motherTongue == null)
                throw new NotFoundException("Mother tongue not found.");

            await _motherTongueRepository.DeleteAsync(motherTongue);
        }
    }
}
