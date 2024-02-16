﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Model.Migrations
{
    [DbContext(typeof(YDbContext))]
    [Migration("20240216121657_InitialCommit")]
    partial class InitialCommit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Model.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("tag_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("tag");
                });

            modelBuilder.Entity("Model.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Model.Entities.UserFollowsUser", b =>
                {
                    b.Property<int>("FollowerId")
                        .HasColumnType("integer")
                        .HasColumnName("follower_id");

                    b.Property<int>("FollowingId")
                        .HasColumnType("integer")
                        .HasColumnName("following_id");

                    b.HasKey("FollowerId", "FollowingId");

                    b.HasIndex("FollowingId");

                    b.ToTable("user_follows_users_jt");
                });

            modelBuilder.Entity("Model.Entities.Yeet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("yeet_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("body");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("Likes")
                        .HasColumnType("integer")
                        .HasColumnName("likes");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("yeets");
                });

            modelBuilder.Entity("Model.Entities.YeetHasTags", b =>
                {
                    b.Property<int>("YeetId")
                        .HasColumnType("integer")
                        .HasColumnName("yeet_id");

                    b.Property<int>("TagId")
                        .HasColumnType("integer")
                        .HasColumnName("tag_id");

                    b.HasKey("YeetId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("yeet_has_tags");
                });

            modelBuilder.Entity("Model.Entities.Yomment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("yomment_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("body");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("Likes")
                        .HasColumnType("integer")
                        .HasColumnName("likes");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("YeetId")
                        .HasColumnType("integer")
                        .HasColumnName("yeet_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("YeetId");

                    b.ToTable("yomments");
                });

            modelBuilder.Entity("Model.Entities.UserFollowsUser", b =>
                {
                    b.HasOne("Model.Entities.User", "Follower")
                        .WithMany()
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.User", "Following")
                        .WithMany()
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("Following");
                });

            modelBuilder.Entity("Model.Entities.Yeet", b =>
                {
                    b.HasOne("Model.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.Entities.YeetHasTags", b =>
                {
                    b.HasOne("Model.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Yeet", "Yeet")
                        .WithMany("Tags")
                        .HasForeignKey("YeetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Yeet");
                });

            modelBuilder.Entity("Model.Entities.Yomment", b =>
                {
                    b.HasOne("Model.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Yeet", "Yeet")
                        .WithMany("Yomments")
                        .HasForeignKey("YeetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Yeet");
                });

            modelBuilder.Entity("Model.Entities.Yeet", b =>
                {
                    b.Navigation("Tags");

                    b.Navigation("Yomments");
                });
#pragma warning restore 612, 618
        }
    }
}
