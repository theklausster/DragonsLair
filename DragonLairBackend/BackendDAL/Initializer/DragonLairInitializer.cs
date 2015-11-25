using System.Collections.Generic;
using BackendDAL.Context;
using System.Data.Entity;
using Entities;

namespace BackendDAL.Initializer
{
    public class DragonLairInitializer : DropCreateDatabaseIfModelChanges<DBContext>
    {
        private Player player1 = new Player() {Id = 1, Name = "Hans"};
        private Player player2 = new Player() { Id = 2, Name = "Peter" };

        private List<Player> players = new List<Player>(); 

        protected override void Seed(DBContext context)
        {
            
            context.Players.Add(player1);
            context.Players.Add(player2);
            base.Seed(context);
        }


    }
}