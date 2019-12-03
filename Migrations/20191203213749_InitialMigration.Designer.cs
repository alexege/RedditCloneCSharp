﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reddit2.Models;

namespace Reddit2.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20191203213749_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Reddit2.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Reddit2.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("DOB")
                        .HasColumnName("dob");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("VARCHAR(45)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasColumnType("VARCHAR(45)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasColumnType("VARCHAR(45)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Reddit2.Models.Vote", b =>
                {
                    b.Property<int>("VoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<bool>("IsUpvote")
                        .HasColumnName("is_upvote");

                    b.Property<int>("PostId")
                        .HasColumnName("post_id");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("VoteId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Reddit2.Models.Post", b =>
                {
                    b.HasOne("Reddit2.Models.User", "Creator")
                        .WithMany("CreatedPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reddit2.Models.Vote", b =>
                {
                    b.HasOne("Reddit2.Models.Post", "Voted")
                        .WithMany("Votes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reddit2.Models.User", "Voter")
                        .WithMany("VotesIssued")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}