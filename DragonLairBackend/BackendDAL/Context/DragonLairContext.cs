using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Initializer;
using Entities;

namespace BackendDAL.Context
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class DragonLairContext : DbContext
    {
        public DragonLairContext() : base("DragonLair")
        {
            Database.SetInitializer(new DragonLairInitizalizer());
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentType> TournamentTypes { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Match> Matches { get; set; } 
    }
}
