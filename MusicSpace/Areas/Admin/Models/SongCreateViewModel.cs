using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Areas.Admin.Models
{
    public class SongCreateViewModel
    {
        public SongCreateViewModel()
        {
            Performers = new List<Performer>();
            Albums = new List<Album>();
        }
       
        public Song Song { get; set; }

        [Display(Name = "Список исполнителей")]
        public virtual IEnumerable<Performer> Performers { get; set; }
        [Display(Name = "Список альбомов")]
        public virtual IEnumerable<Album> Albums { get; set; }
    }
}
