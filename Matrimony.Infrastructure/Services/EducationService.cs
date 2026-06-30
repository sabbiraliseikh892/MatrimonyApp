using Matrimony.Application.Features.Masters.Education;
using Matrimony.Application.Features.Masters.Religion;
using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Domain.Entities.Masters;
using Matrimony.Infrastructure.Repositories;
using Matrimony.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;
        public EducationService(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public async Task AddAsync(CreateEducationRequest request)
        {
            if (await _educationRepository.ExistsAsync(request.Name))
                throw new BusinessException("Education already exists.");

            var education = new EducationMaster
            {
                Name = request.Name,
                IsActive = true
            };

            await _educationRepository.AddAsync(education);
        }

        public async Task DeleteAsync(Guid id)
        {
            var education = await _educationRepository.GetByIdAsync(id);

            if (education == null)
                throw new NotFoundException("Education not found.");

            await _educationRepository.DeleteAsync(education);
        }

        public async Task<List<EducationResponse>> GetAllAsync()
        {
            var education = await _educationRepository.GetAllAsync();

            return education.Select(x => new EducationResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public async Task<EducationResponse?> GetByIdAsync(Guid id)
        {
            var religion = await _educationRepository.GetByIdAsync(id);

            if (religion == null)
                throw new NotFoundException("Education not found.");

            return new EducationResponse
            {
                Id = religion.Id,
                Name = religion.Name
            };
        }

        public async Task UpdateAsync(UpdateEducationRequest request)
        {
            var education = await _educationRepository.GetByIdAsync(request.Id);

            if (education == null)
                throw new NotFoundException("Education not found.");

            education.Name = request.Name;

            await _educationRepository.UpdateAsync(education);
        }
    }
}
