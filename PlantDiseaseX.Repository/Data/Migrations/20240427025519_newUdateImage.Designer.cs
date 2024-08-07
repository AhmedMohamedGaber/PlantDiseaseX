﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlantDiseaseX.Repository.Data;

#nullable disable

namespace PlantDiseaseX.Repository.Data.Migrations
{
    [DbContext(typeof(PlantContext))]
    [Migration("20240427025519_newUdateImage")]
    partial class newUdateImage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PlantDiseaseX.Core.Entities.Plant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GeneralUse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MedicalUse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlantCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("PlantSeasonId")
                        .HasColumnType("int");

                    b.Property<string>("Properties")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Warnings")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("diseases")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("season")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("treatment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlantCategoryId");

                    b.HasIndex("PlantSeasonId");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("PlantDiseaseX.Core.Entities.Plantcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PlantCategories");
                });

            modelBuilder.Entity("PlantDiseaseX.Core.Entities.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PlantSeasons");
                });

            modelBuilder.Entity("PlantDiseaseX.Core.Entities.Plant", b =>
                {
                    b.HasOne("PlantDiseaseX.Core.Entities.Plantcategory", "PlantCategory")
                        .WithMany()
                        .HasForeignKey("PlantCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlantDiseaseX.Core.Entities.Season", "PlantSeason")
                        .WithMany()
                        .HasForeignKey("PlantSeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlantCategory");

                    b.Navigation("PlantSeason");
                });
#pragma warning restore 612, 618
        }
    }
}
