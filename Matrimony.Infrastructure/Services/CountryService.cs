using Matrimony.Application.Features.Masters.Country;
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
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<CountryResponse>> GetAllAsync()
        {
            var countries = await _countryRepository.GetAllAsync();

            return countries.Select(x => new CountryResponse
            {
                Id = x.Id,
                Name = x.Name,
                CountryCode = x.CountryCode
            }).ToList();
        }

        public async Task<CountryResponse?> GetByIdAsync(Guid id)
        {
            var country = await _countryRepository.GetByIdAsync(id);

            if (country == null)
                throw new NotFoundException("Country not found.");

            return new CountryResponse
            {
                Id = country.Id,
                Name = country.Name,
                CountryCode = country.CountryCode
            };
        }

        public async Task AddAsync(CreateCountryRequest request)
        {
            if (await _countryRepository.ExistsAsync(request.Name))
                throw new BusinessException("Country already exists.");

            var country = new CountryMaster
            {
                Name = request.Name,
                CountryCode = request.CountryCode,
                IsActive = true
            };

            await _countryRepository.AddAsync(country);
        }

        public async Task UpdateAsync(UpdateCountryRequest request)
        {
            var country = await _countryRepository.GetByIdAsync(request.Id);

            if (country == null)
                throw new NotFoundException("Country not found.");

            country.Name = request.Name;
            country.CountryCode = request.CountryCode;

            await _countryRepository.UpdateAsync(country);
        }

        public async Task DeleteAsync(Guid id)
        {
            var country = await _countryRepository.GetByIdAsync(id);

            if (country == null)
                throw new NotFoundException("Country not found.");

            await _countryRepository.DeleteAsync(country);
        }
    }
}
