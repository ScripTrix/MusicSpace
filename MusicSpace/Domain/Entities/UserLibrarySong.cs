using System;
using System.ComponentModel.DataAnnotations;

namespace MusicSpace.Domain.Entities
{
    public partial class UserLibrarySong
    {
        public string UserId { get; set; }
        public string SongId { get; set; }
        [Display(Name = "Дата добавления")]
        public DateTime AdditionDate { get; set; }

        [Display(Name = "Песня")]
        public virtual Song Song { get; set; }
        [Display(Name = "Пользователь")]
        public virtual ApplicationUser User { get; set; }
    }
}
