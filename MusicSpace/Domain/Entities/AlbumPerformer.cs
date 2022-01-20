using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Entities
{
    public partial class AlbumPerformer
    {
        [Key, Column(Order = 0)]
        public string AlbumId { get; set; }

        [Key, Column(Order = 1)]
        public string PerformerId { get; set; }

        [Display(Name = "Альбом")]
        public virtual Album Album { get; set; }
        [Display(Name = "Исполнитель")]
        public virtual Performer Performer { get; set; }
    }
}
