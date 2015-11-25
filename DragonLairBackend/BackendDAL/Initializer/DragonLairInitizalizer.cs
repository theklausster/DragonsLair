using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;
using Entities;

namespace BackendDAL.Initializer
{
    class DragonLairInitizalizer : DropCreateDatabaseAlways<DragonLairContext>
    {
        private Player player1;
        private Player player2;
        private Player player3;
        private TournamentType tournamentType;
        private Genre genre;
        private Game game;
        public DragonLairInitizalizer()
        {
            player1 = new Player() { Name = "Søren"};
            player2 = new Player() { Name = "Mark"};
            player3 = new Player() { Name = "René"};
        }

        protected override void Seed(DragonLairContext context)
        {
            context.Players.Add(player1);
            context.Players.Add(player2);
            context.Players.Add(player3);
            base.Seed(context);
        }
    }
}
