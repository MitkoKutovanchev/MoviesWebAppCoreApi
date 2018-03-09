using Data.DB.Helpers;
using Data.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DB
{
    public class MoviesWebAppDbContext : DbContext
    {
        private string connection;

        public MoviesWebAppDbContext()
        {
            this.connection = DbConnections.GetAppHarborConnection();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ImgUrls> Urls { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>()
                .HasKey(t => new { t.ActorId, t.MovieId });

            modelBuilder.Entity<Movie>()
       .HasMany(c => c.aditionalImgUrls)
       .WithOne(e => e.Movie)
       .OnDelete(DeleteBehavior.SetNull);
        }
    }
}