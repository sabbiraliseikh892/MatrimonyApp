using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.ProfilePhoto
{
    public class UploadProfilePhotoRequest
    {
        [Required]
        public IFormFile Photo { get; set; } = null!;

        public bool IsPrimary { get; set; }
    }
}
