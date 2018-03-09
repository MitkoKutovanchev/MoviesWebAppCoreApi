﻿// <auto-generated />
using Data.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Data.DB.Migrations
{
    [DbContext(typeof(MoviesWebAppDbContext))]
    partial class MoviesWebAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Entity.Entities.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Data.Entity.Entities.ActorMovie", b =>
                {
                    b.Property<int>("ActorId");

                    b.Property<int>("MovieId");

                    b.Property<int>("Id");

                    b.HasKey("ActorId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("ActorMovies");
                });

            modelBuilder.Entity("Data.Entity.Entities.ImgUrls", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MovieId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Urls");
                });

            modelBuilder.Entity("Data.Entity.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MovieDesc");

                    b.Property<double>("MovieIMDBScore");

                    b.Property<string>("MovieIMDBUrl");

                    b.Property<double>("MovieRottenTomatoesScore");

                    b.Property<string>("MovieRottenTomatoesUrl");

                    b.Property<string>("Name");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<int?>("UserId");

                    b.Property<string>("imgUrl");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Data.Entity.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvatarUrl");

                    b.Property<string>("EMail");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.Property<string>("apiIdPlaceholder");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.Entity.Entities.ActorMovie", b =>
                {
                    b.HasOne("Data.Entity.Entities.Actor", "Actor")
                        .WithMany("Movies")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Entity.Entities.Movie", "Movie")
                        .WithMany("Actors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Entity.Entities.ImgUrls", b =>
                {
                    b.HasOne("Data.Entity.Entities.Movie", "Movie")
                        .WithMany("aditionalImgUrls")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Data.Entity.Entities.Movie", b =>
                {
                    b.HasOne("Data.Entity.Entities.User")
                        .WithMany("WatchedMovies")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
