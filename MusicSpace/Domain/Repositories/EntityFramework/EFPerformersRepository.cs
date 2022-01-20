using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain.Entities;
using MusicSpace.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.EntityFramework
{
    public class EFPerformersRepository : IPerformersRepository
    {
        private readonly AppDbContext context;
        public EFPerformersRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void DeletePerformer(string id)
        {
            context.Performers.Remove(new Performer() { Id = id });
            context.SaveChanges();
        }

        public IQueryable<Performer> GetPerformers()
        {
            return context.Performers;
        }

        public async Task<Performer> GetPerformerById(string id)
        {
            return await context.Performers.FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<Performer> GetPerformersByAlbumId(string id)
        {
            return context.Performers.Where(e => e.AlbumPerformers.Contains(new AlbumPerformer() { AlbumId = id }));
        }

        public IQueryable<Performer> GetPerformersBySongId(string id)
        {
            return context.Performers.Where(e => e.SongPerformers.Contains(new SongPerformer() { SongId = id }));
        }

        public void SavePerformer(Performer entity)
        {
            if (!context.Performers.Any(e => e.Id == entity.Id))
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
