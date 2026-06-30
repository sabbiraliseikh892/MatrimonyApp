using Matrimony.Application.Features.Masters.State;
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
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;

        public StateService(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public async Task<List<StateResponse>> GetAllAsync()
        {
            var states = await _stateRepository.GetAllAsync();

            return states.Select(x => new StateResponse
            {
                Id = x.Id,
                CountryId = x.CountryId,
                CountryName = x.Country.Name,
                Name = x.Name,
                StateCode = x.StateCode
            }).ToList();
        }

        public async Task<List<StateResponse>> GetByCountryIdAsync(Guid countryId)
        {
            var states = await _stateRepository.GetByCountryIdAsync(countryId);

            return states.Select(x => new StateResponse
            {
                Id = x.Id,
                CountryId = x.CountryId,
                Name = x.Name,
                StateCode = x.StateCode,
                CountryName = ""
            }).ToList();
        }

        public async Task<StateResponse?> GetByIdAsync(Guid id)
        {
            var state = await _stateRepository.GetByIdAsync(id);

            if (state == null)
                throw new NotFoundException("State not found.");

            return new StateResponse
            {
                Id = state.Id,
                CountryId = state.CountryId,
                CountryName = state.Country.Name,
                Name = state.Name,
                StateCode = state.StateCode
            };
        }

        public async Task AddAsync(CreateStateRequest request)
        {
            if (await _stateRepository.ExistsAsync(request.Name, request.CountryId))
                throw new BusinessException("State already exists.");

            var state = new StateMaster
            {
                CountryId = request.CountryId,
                Name = request.Name,
                StateCode = request.StateCode,
                IsActive = true
            };

            await _stateRepository.AddAsync(state);
        }

        public async Task UpdateAsync(UpdateStateRequest request)
        {
            var state = await _stateRepository.GetByIdAsync(request.Id);

            if (state == null)
                throw new NotFoundException("State not found.");

            state.CountryId = request.CountryId;
            state.Name = request.Name;
            state.StateCode = request.StateCode;

            await _stateRepository.UpdateAsync(state);
        }

        public async Task DeleteAsync(Guid id)
        {
            var state = await _stateRepository.GetByIdAsync(id);

            if (state == null)
                throw new NotFoundException("State not found.");

            await _stateRepository.DeleteAsync(state);
        }
    }
}
