using MusicSpace.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicSpace.Areas.User.Models
{
    public class UserHomeViewModel
    {
        public UserHomeViewModel()
        {
            CurrentUser = new ApplicationUser();
            UserAlbums = new List<Album>();
            UserSongs = new List<Song>();
        }
        
        [Display(Name = "Пользователь")]
        public ApplicationUser CurrentUser { get; set; }

        [Display(Name = "Добавленные альбомы")]
        public virtual IEnumerable<Album> UserAlbums { get; set; }

        [Display(Name = "Добавленные композиции")]
        public virtual IEnumerable<Song> UserSongs { get; set; }
    }
}

