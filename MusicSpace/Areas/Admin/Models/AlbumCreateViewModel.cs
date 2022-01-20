using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Areas.Admin.Models
{
    public class AlbumCreateViewModel
    {
        public AlbumCreateViewModel()
        {
            Performers = new List<Performer>();
            Songs = new List<Song>();
        }

        public Album Album { get; set; }

        [Display(Name = "Список исполнителей")]
        public virtual IEnumerable<Performer> Performers { get; set; }
        [Display(Name = "Список песен")]
        public virtual IEnumerable<Song> Songs { get; set; }
    }
}

