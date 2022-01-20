using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Entities
{
    public partial class ApplicationUser : IdentityUser
    {
        [ForeignKey("UserLibraryAlbumId")]
        public virtual ICollection<UserLibraryAlbum> UserLibraryAlbums { get; set; }
        [ForeignKey("UserLibrarySongId")]
        public virtual ICollection<UserLibrarySong> UserLibrarySongs { get; set; }
    }
}
