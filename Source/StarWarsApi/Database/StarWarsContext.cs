﻿using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models;

namespace StarWarsApi.Database
{
    public class StarWarsContext : DbContext
    {
        public string ConnectionString; //hard code connectionstring in onconfiguring method when migrating
        public DbSet<SpaceShip> SpaceShips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<User.Homeworld> Homeworlds { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            ConnectionString = @"Server = 90.229.161.68,52578; Database = StarWarsProject2.6; User Id = adminuser; Password = starwars;";
            optionsbuilder.UseSqlServer(ConnectionString);
        }
    }
}
