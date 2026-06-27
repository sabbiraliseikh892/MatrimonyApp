using Matrimony.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(Guid id);

        Task<ApplicationUser?> GetByEmailAsync(string email);

        Task<ApplicationUser?> GetByPhoneAsync(string phoneNumber);

        Task<bool> EmailExistsAsync(string email);

        Task<bool> PhoneExistsAsync(string phoneNumber);

        Task CreateAsync(ApplicationUser user);

        Task UpdateAsync(ApplicationUser user);
    }
}
