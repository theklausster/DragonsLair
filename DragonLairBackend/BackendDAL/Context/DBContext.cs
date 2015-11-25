using System;
using System.Collections.Generic;
using System.Data.Entity;
using BackendDAL.Initializer;
using Entities;

namespace BackendDAL.Context
{
    public class DBContext : DbContext
    {

        public DBContext() : base("DragonLair")
        {
            Database.SetInitializer(new DragonLairInitializer());
            
        }
        public DbSet<Player> Players { get; set; }
    }
}