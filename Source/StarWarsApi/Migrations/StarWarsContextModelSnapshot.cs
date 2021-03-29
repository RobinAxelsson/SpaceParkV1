﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StarWarsApi.Database;

namespace StarWarsApi.Migrations
{
    [DbContext(typeof(StarWarsContext))]
    partial class StarWarsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StarWarsApi.Models.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpaceShipID")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AccountID");

                    b.HasIndex("SpaceShipID");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("StarWarsApi.Models.Receipt", b =>
                {
                    b.Property<int>("ReceiptID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountID")
                        .HasColumnType("int");

                    b.Property<string>("EndTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("StartTime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReceiptID");

                    b.HasIndex("AccountID");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("StarWarsApi.Models.SpaceShip", b =>
                {
                    b.Property<int>("SpaceShipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Classification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipLength")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpaceShipID");

                    b.ToTable("SpaceShips");
                });

            modelBuilder.Entity("StarWarsApi.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BirthYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EyeColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HairColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Height")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HomeplanetHomeworldID")
                        .HasColumnType("int");

                    b.Property<string>("Mass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkinColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("HomeplanetHomeworldID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StarWarsApi.Models.User+Homeworld", b =>
                {
                    b.Property<int>("HomeworldID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Climate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diameter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrbitalPeriod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Population")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RotationPeriod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Terrain")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HomeworldID");

                    b.ToTable("Homeworlds");
                });

            modelBuilder.Entity("StarWarsApi.Models.Account", b =>
                {
                    b.HasOne("StarWarsApi.Models.SpaceShip", "SpaceShip")
                        .WithMany()
                        .HasForeignKey("SpaceShipID");

                    b.HasOne("StarWarsApi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("SpaceShip");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StarWarsApi.Models.Receipt", b =>
                {
                    b.HasOne("StarWarsApi.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("StarWarsApi.Models.User", b =>
                {
                    b.HasOne("StarWarsApi.Models.User+Homeworld", "Homeplanet")
                        .WithMany()
                        .HasForeignKey("HomeplanetHomeworldID");

                    b.Navigation("Homeplanet");
                });
#pragma warning restore 612, 618
        }
    }
}
