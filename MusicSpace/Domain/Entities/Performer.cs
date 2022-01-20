using Microsoft.AspNetCore.Http;
using MusicSpace.Service.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicSpace.Domain.Entities
{
    public partial class Performer
    {
        public Performer()
        {
            AlbumPerformers = new HashSet<AlbumPerformer>();
            SongPerformers = new HashSet<SongPerformer>();
        }

        public string Id { get; set; }
        [Display(Name = "Имя исполнителя")]
        public string PerformerName { get; set; }
        [Display(Name = "Аватар")]
        public string AvatarPath { get; set; }

        [Display(Name = "Альбомы исполнителя")]
        public virtual ICollection<AlbumPerformer> AlbumPerformers { get; set; }
        [Display(Name = "Песни исполнителя")]
        public virtual ICollection<SongPerformer> SongPerformers { get; set; }



        [NotMapped]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [Required(ErrorMessage = "Загрузите корректный файл!")]
        [Display(Name = "Файл")]
        public IFormFile AvatarFile { get; set; }
    }
}
