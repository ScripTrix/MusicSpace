using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.Abstract
{
    public interface IPerformersRepository
    {
        IQueryable<Performer> GetPerformersByAlbumId(string id);
        IQueryable<Performer> GetPerformersBySongId(string id);
        IQueryable<Performer> GetPerformers();
        //ApplicationUser GetApplicationUserByLogin(string login);
        Task<Performer> GetPerformerById(string id);
        void SavePerformer(Performer entity);
        void DeletePerformer(string id);
    }
}
