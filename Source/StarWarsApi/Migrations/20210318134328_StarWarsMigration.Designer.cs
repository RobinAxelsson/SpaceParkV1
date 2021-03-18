﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StarWarsApi.Database;

namespace StarWarsApi.Migrations
{
    [DbContext(typeof(StarWarsContext))]
    [Migration("20210318134328_StarWarsMigration")]
    partial class StarWarsMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StarWarsApi.Models.SpaceShip", b =>
                {
                    b.Property<int>("SpaceShipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SizeShipSizeID")
                        .HasColumnType("int");

                    b.HasKey("SpaceShipID");

                    b.HasIndex("SizeShipSizeID");

                    b.ToTable("SpaceShips");
                });

            modelBuilder.Entity("StarWarsApi.Models.SpaceShip+ShipSize", b =>
                {
                    b.Property<int>("ShipSizeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Width")
                        .HasColumnType("bigint");

                    b.HasKey("ShipSizeID");

                    b.ToTable("ShipSize");
                });

            modelBuilder.Entity("StarWarsApi.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StarWarsApi.Models.SpaceShip", b =>
                {
                    b.HasOne("StarWarsApi.Models.SpaceShip+ShipSize", "Size")
                        .WithMany()
                        .HasForeignKey("SizeShipSizeID");

                    b.Navigation("Size");
                });
#pragma warning restore 612, 618
        }
    }
}
