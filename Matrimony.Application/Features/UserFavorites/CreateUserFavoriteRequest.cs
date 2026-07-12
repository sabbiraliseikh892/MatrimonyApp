using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.UserFavorites
{
    public class CreateUserFavoriteRequest
    {
        [Required]
        public Guid FavoriteUserId { get; set; }
    }
}
