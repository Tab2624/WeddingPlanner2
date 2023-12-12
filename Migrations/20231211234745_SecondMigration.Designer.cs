﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeddingPlanner2.Models;

#nullable disable

namespace WeddingPlanner2.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20231211234745_SecondMigration")]
    partial class SecondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WeddingPlanner2.Models.RSVP", b =>
                {
                    b.Property<int>("RSVPId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WeddingId")
                        .HasColumnType("int");

                    b.HasKey("RSVPId");

                    b.HasIndex("UserId");

                    b.HasIndex("WeddingId");

                    b.ToTable("RSVP");
                });

            modelBuilder.Entity("WeddingPlanner2.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WeddingPlanner2.Models.Wedding", b =>
                {
                    b.Property<int>("WeddingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Wedder1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Wedder2")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("WeddingId");

                    b.HasIndex("UserId");

                    b.ToTable("Wedding");
                });

            modelBuilder.Entity("WeddingPlanner2.Models.RSVP", b =>
                {
                    b.HasOne("WeddingPlanner2.Models.User", "RSVPUser")
                        .WithMany("RSVPs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WeddingPlanner2.Models.Wedding", "RSVPWedding")
                        .WithMany("UserRSVPs")
                        .HasForeignKey("WeddingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RSVPUser");

                    b.Navigation("RSVPWedding");
                });

            modelBuilder.Entity("WeddingPlanner2.Models.Wedding", b =>
                {
                    b.HasOne("WeddingPlanner2.Models.User", "Creator")
                        .WithMany("AllWeddings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("WeddingPlanner2.Models.User", b =>
                {
                    b.Navigation("AllWeddings");

                    b.Navigation("RSVPs");
                });

            modelBuilder.Entity("WeddingPlanner2.Models.Wedding", b =>
                {
                    b.Navigation("UserRSVPs");
                });
#pragma warning restore 612, 618
        }
    }
}
