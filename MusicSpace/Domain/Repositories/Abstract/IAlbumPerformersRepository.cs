using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.Abstract
{
    public interface IAlbumPerformersRepository
    {
        IQueryable<AlbumPerformer> GetAlbumPerformersByAlbumId(string id);
        IQueryable<AlbumPerformer> GetAlbumPerformers();
        AlbumPerformer GetAlbumPerformerById(string albumId, string performerId);
        void SaveAlbumPerformer(AlbumPerformer entity);
        void DeleteAlbumPerformer(string albumId, string performerId);
    }
}
