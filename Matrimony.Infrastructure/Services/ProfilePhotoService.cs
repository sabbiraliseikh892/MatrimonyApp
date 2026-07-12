using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Matrimony.Application.Features.ProfilePhoto;
using Matrimony.Domain.Entities;
using Matrimony.Shared.Exceptions;

namespace Matrimony.Infrastructure.Services
{
    public class ProfilePhotoService : IProfilePhotoService
    {
        private readonly IProfilePhotoRepository _photoRepository;
        private readonly IUserProfileRepository _profileRepository;
        private readonly IWebHostEnvironment _environment;
        public ProfilePhotoService(
           IProfilePhotoRepository photoRepository,
           IUserProfileRepository profileRepository,
           IWebHostEnvironment environment)
        {
            _photoRepository = photoRepository;
            _profileRepository = profileRepository;
            _environment = environment;
        }
        public async Task UploadAsync(Guid userId, UploadProfilePhotoRequest request)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(userId);

            if (profile == null)
                throw new NotFoundException("Profile not found.");

            if (request.Photo == null || request.Photo.Length == 0)
                throw new BusinessException("Please select a photo.");

            var extension = Path.GetExtension(request.Photo.FileName).ToLower();

            var allowedExtensions = new[]
            {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    };

            if (!allowedExtensions.Contains(extension))
                throw new BusinessException("Invalid image format.");

            if (request.Photo.Length > 5 * 1024 * 1024)
                throw new BusinessException("Maximum file size is 5 MB.");

            var fileName = $"{Guid.NewGuid()}{extension}";

            var folder = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "profile");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Photo.CopyToAsync(stream);
            }

            if (request.IsPrimary)
            {
                var oldPrimary =
                    await _photoRepository.GetPrimaryPhotoAsync(profile.Id);

                if (oldPrimary != null)
                {
                    oldPrimary.IsPrimary = false;

                    await _photoRepository.UpdateAsync(oldPrimary);
                }
            }

            var photo = new ProfilePhoto
            {
                UserProfileId = profile.Id,
                PhotoUrl = $"/uploads/profile/{fileName}",
                IsPrimary = request.IsPrimary,
                IsApproved = false
            };

            await _photoRepository.AddAsync(photo);
        }
        public async Task<List<ProfilePhotoResponse>> GetMyPhotosAsync(Guid userId)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(userId);

            if (profile == null)
                throw new NotFoundException("Profile not found.");

            var photos = await _photoRepository.GetByUserProfileIdAsync(profile.Id);

            return photos.Select(x => new ProfilePhotoResponse
            {
                Id = x.Id,
                PhotoUrl = x.PhotoUrl,
                IsPrimary = x.IsPrimary,
                IsApproved = x.IsApproved,
                CreatedAt = x.CreatedAt
            }).ToList();
        }
        public async Task SetPrimaryAsync(Guid userId, Guid photoId)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(userId);

            if (profile == null)
                throw new NotFoundException("Profile not found.");

            var photo = await _photoRepository.GetByIdAsync(photoId);

            if (photo == null)
                throw new NotFoundException("Photo not found.");

            if (photo.UserProfileId != profile.Id)
                throw new BusinessException("You cannot modify another user's photo.");

            var currentPrimary =
                await _photoRepository.GetPrimaryPhotoAsync(profile.Id);

            if (currentPrimary != null)
            {
                currentPrimary.IsPrimary = false;
                await _photoRepository.UpdateAsync(currentPrimary);
            }

            photo.IsPrimary = true;

            await _photoRepository.UpdateAsync(photo);
        }
        public async Task DeleteAsync(Guid userId, Guid photoId)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(userId);

            if (profile == null)
                throw new NotFoundException("Profile not found.");

            var photo = await _photoRepository.GetByIdAsync(photoId);

            if (photo == null)
                throw new NotFoundException("Photo not found.");

            if (photo.UserProfileId != profile.Id)
                throw new BusinessException("You cannot delete another user's photo.");

            var physicalPath = Path.Combine(
                _environment.WebRootPath,
                photo.PhotoUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }

            await _photoRepository.DeleteAsync(photo);
        }
    }
}
