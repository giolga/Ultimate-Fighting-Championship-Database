﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UltimateFightingChampionshipAPI.Data;

#nullable disable

namespace UltimateFightingChampionshipAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FighterWeightClass", b =>
                {
                    b.Property<int>("FightersId")
                        .HasColumnType("int");

                    b.Property<int>("WeightClassesId")
                        .HasColumnType("int");

                    b.HasKey("FightersId", "WeightClassesId");

                    b.HasIndex("WeightClassesId");

                    b.ToTable("FighterWeightClass");
                });

            modelBuilder.Entity("UltimateFightingChampionshipAPI.Models.Fighter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Fighters");
                });

            modelBuilder.Entity("UltimateFightingChampionshipAPI.Models.FighterWeightClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FighterId")
                        .HasColumnType("int");

                    b.Property<int>("WeightClassId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FighterId");

                    b.HasIndex("WeightClassId");

                    b.ToTable("FighterWeightClasses");
                });

            modelBuilder.Entity("UltimateFightingChampionshipAPI.Models.WeightClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("WeightClasses");
                });

            modelBuilder.Entity("FighterWeightClass", b =>
                {
                    b.HasOne("UltimateFightingChampionshipAPI.Models.Fighter", null)
                        .WithMany()
                        .HasForeignKey("FightersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UltimateFightingChampionshipAPI.Models.WeightClass", null)
                        .WithMany()
                        .HasForeignKey("WeightClassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UltimateFightingChampionshipAPI.Models.FighterWeightClass", b =>
                {
                    b.HasOne("UltimateFightingChampionshipAPI.Models.Fighter", "FighterFK")
                        .WithMany()
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UltimateFightingChampionshipAPI.Models.WeightClass", "WeightClassFK")
                        .WithMany()
                        .HasForeignKey("WeightClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FighterFK");

                    b.Navigation("WeightClassFK");
                });
#pragma warning restore 612, 618
        }
    }
}
