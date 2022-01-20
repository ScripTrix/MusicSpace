using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Entities
{
    public partial class ChartSong
    {
        public string ChartId { get; set; }
        public string SongId { get; set; }
        [Display(Name = "Позиция")]
        public ushort Spot { get; set; }
        
        [Display(Name = "Чарт")]
        public virtual WeeklyChart Chart { get; set; }
        [Display(Name = "Песня")]
        public virtual Song Song { get; set; }
    }
}
