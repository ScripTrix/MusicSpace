using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.Abstract
{
    public interface ISongPerformersRepository
    {
        IQueryable<SongPerformer> GetSongPerformersBySongId(string id);
        IQueryable<SongPerformer> GetSongPerformers();
        SongPerformer GetSongPerformerById(string songId, string performerId);
        void SaveSongPerformer(SongPerformer entity);
        void DeleteSongPerformer(string songId, string performerId);
    }
}
