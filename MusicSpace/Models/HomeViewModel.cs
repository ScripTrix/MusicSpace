using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Albums = new List<Album>();
            Songs = new List<Song>();
        }

        [Display(Name = "Недавно добавленные альбомы")]
        public virtual IEnumerable<Album> Albums { get; set; }

        [Display(Name = "Недавно добавленные композиции")]
        public virtual IEnumerable<Song> Songs { get; set; }
    }
}
