using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Entities
{
    public partial class WeeklyChart
    {
        public WeeklyChart()
        {
            ChartSongs = new HashSet<ChartSong>();
        }
        
        public string Id { get; set; }
        [Display(Name = "Дата релиза")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Песни")]
        public virtual ICollection<ChartSong> ChartSongs { get; set; }
    }
}
