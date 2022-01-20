using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain
{
    public partial class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<AlbumPerformer> AlbumPerformers { get; set; }
        public DbSet<SongPerformer> SongPerformers { get; set; }
        public DbSet<UserLibraryAlbum> UserLibraryAlbums { get; set; }
        public DbSet<UserLibrarySong> UserLibrarySongs { get; set; }
        public DbSet<WeeklyChart> WeeklyCharts { get; set; }
        public DbSet<ChartSong> ChartSongs { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "2a3e0934-bf85-4fce-b893-af35699f116b",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "fbc402dc-ea0a-4c10-931e-454dba6f9bed",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "superpassword"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "2a3e0934-bf85-4fce-b893-af35699f116b",
                UserId = "fbc402dc-ea0a-4c10-931e-454dba6f9bed"
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(e => e.CoverPath)
                .IsRequired()
                .HasMaxLength(256);

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(256);
            });

            modelBuilder.Entity<AlbumPerformer>(entity =>
            {
                entity.HasKey(e => new { e.AlbumId, e.PerformerId })
                .HasName("AlbumPerformer_pkey");

                entity.HasOne(d => d.Album)
                .WithMany(p => p.AlbumPerformers)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Album_fkey");

                entity.HasOne(d => d.Performer)
                .WithMany(p => p.AlbumPerformers)
                .HasForeignKey(d => d.PerformerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Performer_fkey");
            });

            modelBuilder.Entity<ChartSong>(entity =>
            {
                entity.HasKey(e => new { e.ChartId, e.SongId })
                .HasName("ChartSong_pkey");

                entity.HasOne(d => d.Chart)
                .WithMany(p => p.ChartSongs)
                .HasForeignKey(d => d.ChartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Chart_fkey");

                entity.HasOne(d => d.Song)
                .WithMany(p => p.ChartSongs)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Song_fkey");
            });

            modelBuilder.Entity<Performer>(entity =>
            {
#pragma warning disable CS0618 // Тип или член устарел
                entity.HasIndex(e => e.PerformerName)
                .HasName("PerformerName_unique")
#pragma warning restore CS0618 // Тип или член устарел
                .IsUnique();

                entity.Property(e => e.AvatarPath)
                .IsRequired()
                .HasMaxLength(256);

                entity.Property(e => e.PerformerName)
                .IsRequired()
                .HasMaxLength(256);
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.AlbumId).IsRequired();

                entity.Property(e => e.ChartListensNumber).HasDefaultValueSql("0");

                entity.Property(e=>e.CoverPath)
                .IsRequired()
                .HasMaxLength(256);

                entity.Property(e => e.ListensNumber).HasDefaultValueSql("0");

                entity.Property(e => e.SongPath)
                .IsRequired()
                .HasMaxLength(256);

                entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(256);

                entity.HasOne(d => d.Album)
                .WithMany(p => p.Songs)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Album_fkey");
            });
            
            modelBuilder.Entity<SongPerformer>(entity =>
            {
                entity.HasKey(e => new { e.SongId, e.PerformerId })
                .HasName("SongPerformer_pkey");

                entity.HasOne(d => d.Performer)
                .WithMany(p => p.SongPerformers)
                .HasForeignKey(d => d.PerformerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Performer_fkey");

                entity.HasOne(d => d.Song)
                .WithMany(p => p.SongPerformers)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Song_fkey");
            });

            modelBuilder.Entity<UserLibraryAlbum>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.AlbumId })
                .HasName("UserLibraryAlbum_pkey");

                entity.Property(e => e.AdditionDate).HasColumnType("date");

                entity.HasOne(d => d.Album)
                .WithMany(p => p.UserLibraryAlbums)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Album_fkey");


                entity.HasOne(d => d.User)
                .WithMany(p => p.UserLibraryAlbums)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_fkey");
            });

            modelBuilder.Entity<UserLibrarySong>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SongId })
                .HasName("UserLibrarySong_pkey");

                entity.Property(e => e.AdditionDate).HasColumnType("date");

                entity.HasOne(d => d.Song)
                .WithMany(p => p.UserLibrarySongs)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Song_fkey");


                entity.HasOne(d => d.User)
                .WithMany(p => p.UserLibrarySongs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_fkey");
            });

            modelBuilder.Entity<WeeklyChart>(entity =>
            {
                entity.Property(e => e.ReleaseDate).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
