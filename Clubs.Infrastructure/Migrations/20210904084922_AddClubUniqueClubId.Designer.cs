﻿// <auto-generated />
using Clubs.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Clubs.Infrastructure.Migrations
{
    [DbContext(typeof(ClubDbContext))]
    [Migration("20210904084922_AddClubUniqueClubId")]
    partial class AddClubUniqueClubId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Clubs.Domain.AggregateModels.ClubAggregate.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClubId")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClubId")
                        .IsUnique();

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("Clubs.Domain.AggregateModels.ClubAggregate.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClubId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Clubs.Domain.AggregateModels.ClubAggregate.Player", b =>
                {
                    b.HasOne("Clubs.Domain.AggregateModels.ClubAggregate.Club", null)
                        .WithMany("Players")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Clubs.Domain.AggregateModels.ClubAggregate.Club", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
