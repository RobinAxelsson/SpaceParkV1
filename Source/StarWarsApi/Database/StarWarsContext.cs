﻿using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsApi.Database
{
    public class StarWarsContext : DbContext
    {
        public DbSet<SpaceShip> SpaceShips { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            optionsbuilder.UseSqlServer(@"Data Source=90.229.161.68,1433;Initial Catalog=StarWarsDataBase2.0;user id=adminuser;password=starwars");
        }
    }
}
