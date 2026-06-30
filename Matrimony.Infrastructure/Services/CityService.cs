using Matrimony.Application.Features.Masters.City;
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
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<CityResponse>> GetAllAsync()
        {
            var cities = await _cityRepository.GetAllAsync();

            return cities.Select(city => new CityResponse
            {
                Id = city.Id,
                StateId = city.StateId,
                StateName = city.State.Name,
                CountryId = city.State.CountryId,
                CountryName = city.State.Country.Name,
                Name = city.Name,
                CityCode = city.CityCode
            }).ToList();
        }

        public async Task<List<CityResponse>> GetByStateIdAsync(Guid stateId)
        {
            var cities = await _cityRepository.GetByStateIdAsync(stateId);

            return cities.Select(city => new CityResponse
            {
                Id = city.Id,
                StateId = city.StateId,
                StateName = city.State.Name,
                CountryId = city.State.CountryId,
                CountryName = city.State.Country.Name,
                Name = city.Name,
                CityCode = city.CityCode
            }).ToList();
        }

        public async Task<CityResponse?> GetByIdAsync(Guid id)
        {
            var city = await _cityRepository.GetByIdAsync(id);

            if (city == null)
                throw new NotFoundException("City not found.");

            return new CityResponse
            {
                Id = city.Id,
                StateId = city.StateId,
                StateName = city.State.Name,
                CountryId = city.State.CountryId,
                CountryName = city.State.Country.Name,
                Name = city.Name,
                CityCode = city.CityCode
            };
        }

        public async Task AddAsync(CreateCityRequest request)
        {
            if (await _cityRepository.ExistsAsync(request.Name, request.StateId))
                throw new BusinessException("City already exists.");

            var city = new CityMaster
            {
                StateId = request.StateId,
                Name = request.Name,
                CityCode = request.CityCode,
                IsActive = true
            };

            await _cityRepository.AddAsync(city);
        }

        public async Task UpdateAsync(UpdateCityRequest request)
        {
            var city = await _cityRepository.GetByIdAsync(request.Id);

            if (city == null)
                throw new NotFoundException("City not found.");

            city.StateId = request.StateId;
            city.Name = request.Name;
            city.CityCode = request.CityCode;

            await _cityRepository.UpdateAsync(city);
        }

        public async Task DeleteAsync(Guid id)
        {
            var city = await _cityRepository.GetByIdAsync(id);

            if (city == null)
                throw new NotFoundException("City not found.");

            await _cityRepository.DeleteAsync(city);
        }
    }
}
