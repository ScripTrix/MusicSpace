using Microsoft.AspNetCore.Http;
using MusicSpace.Service.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Entities
{
    public partial class Album
    {
        public Album()
        {
            AlbumPerformers = new HashSet<AlbumPerformer>();
            Songs = new HashSet<Song>();
            UserLibraryAlbums = new HashSet<UserLibraryAlbum>();
        }   

        public string Id { get; set; }
        [Display(Name = "Название альбома")]
        public string Title { get; set; }

        [Display(Name = "Дата релиза")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Обложка")]
        public string CoverPath { get; set; }

        [Display(Name = "Исполнители")]
        public virtual ICollection<AlbumPerformer> AlbumPerformers { get; set; }
        [Display(Name = "Песни")]
        public virtual ICollection<Song> Songs { get; set; }
        [Display(Name = "Пользовательские библиотеки")]
        public virtual ICollection<UserLibraryAlbum> UserLibraryAlbums { get; set; }


        [NotMapped]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [Required(ErrorMessage = "Загрузите корректный файл!")]
        [Display(Name = "Обложка")]
        public IFormFile CoverFile { get; set; }
    }
}
