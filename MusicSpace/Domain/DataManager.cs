using MusicSpace.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain
{
    public class DataManager
    {
        public IAlbumPerformersRepository AlbumPerformers { get; set; }
        public IAlbumsRepository Albums { get; set; }
        public IApplicationUsersRepository ApplicationUsers { get; set; }
        public IChartSongsRepository ChartSongs { get; set; }
        public IPerformersRepository Performers { get; set; }
        public ISongPerformersRepository SongPerformers { get; set; }
        public ISongsRepository Songs { get; set; }
        public IUserLibraryAlbumsRepository UserLibraryAlbums { get; set; }
        public IUserLibrarySongsRepository UserLibrarySongs { get; set; }
        public IWeeklyChartsRepository WeeklyCharts { get; set; }

        public DataManager(IAlbumPerformersRepository albumPerformersRepository,
                           IAlbumsRepository albumsRepository,
                           IApplicationUsersRepository applicationUsersRepository,
                           IChartSongsRepository chartSongsRepository,
                           IPerformersRepository performersRepository,
                           ISongPerformersRepository songPerformersRepository,
                           ISongsRepository songsRepository,
                           IUserLibraryAlbumsRepository userLibraryAlbumsRepository,
                           IUserLibrarySongsRepository userLibrarySongsRepository,
                           IWeeklyChartsRepository weeklyChartsRepository)
        {
            AlbumPerformers = albumPerformersRepository;
            Albums = albumsRepository;
            ApplicationUsers = applicationUsersRepository;
            ChartSongs = chartSongsRepository;
            Performers = performersRepository;
            SongPerformers = songPerformersRepository;
            Songs = songsRepository;
            UserLibraryAlbums = userLibraryAlbumsRepository;
            UserLibrarySongs = userLibrarySongsRepository;
            WeeklyCharts = weeklyChartsRepository;
        }
    }
}
