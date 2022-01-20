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
    public partial class Song
    {
        public Song()
        {
            ChartSongs = new HashSet<ChartSong>();
            SongPerformers = new HashSet<SongPerformer>();
            UserLibrarySongs = new HashSet<UserLibrarySong>();
        }

        public string Id { get; set; }
        public string AlbumId { get; set; }
        [Display(Name = "Название песни")]
        public string Title { get; set; }
        [Display(Name = "Длительность")]
        public ushort DurationSec { get; set; }
        [Display(Name = "Количество прослушиваний")]
        public int? ListensNumber { get; set; }
        [Display(Name = "Количество прослушиваний текущей недели")]
        public int? ChartListensNumber { get; set; }

        [Display(Name = "Дата релиза")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Обложка")]
        public string CoverPath { get; set; }
        [Display(Name = "Песня")]
        public string SongPath { get; set; }

        [Display(Name = "Альбом")]
        public Album Album { get; set; }
        [Display(Name = "Чарты, в которых участвовала песня")]
        public virtual ICollection<ChartSong> ChartSongs { get; set; }
        [Display(Name = "Исполнители песни")]
        public virtual ICollection<SongPerformer> SongPerformers { get; set; }
        [Display(Name = "Пользовательские библиотеки")]
        public virtual ICollection<UserLibrarySong> UserLibrarySongs { get; set; }


        [NotMapped]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [Required(ErrorMessage = "Загрузите корректный файл!")]
        [Display(Name = "Файл")]
        public IFormFile CoverFile { get; set; }

        [NotMapped]
        [AllowedExtensions(new string[] { ".mp3" })]
        [Required(ErrorMessage = "Загрузите корректный файл!")]
        [Display(Name = "Обложка")]
        public IFormFile SongFile { get; set; }
    }
}
