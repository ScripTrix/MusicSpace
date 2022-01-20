using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Entities
{
    public partial class UserLibraryAlbum
    {
        public string UserId { get; set; }
        public string AlbumId { get; set; }
        [Display(Name = "Дата добавления")]
        public DateTime AdditionDate { get; set; }

        [Display(Name = "Альбом")]
        public virtual Album Album { get; set; }
        [Display(Name = "Пользователь")]
        public virtual ApplicationUser User { get; set; }
    }
}
