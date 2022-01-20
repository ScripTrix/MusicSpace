using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Entities
{
    public partial class SongPerformer
    {
        public string SongId { get; set; }
        public string PerformerId { get; set; }

        [Display(Name = "Исполнитель")]
        public virtual Performer Performer { get; set; }
        [Display(Name = "Песня")]
        public virtual Song Song { get; set; }
    }
}
