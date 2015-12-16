using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;


namespace BackendDAL.Repositories
{
    class PlayerRepository : IRepository<Player>
    {
        
        public Player Create(Player entity)
        {
            using (var context = new DragonLairContext())
            {
                if (entity.Teams != null)
                {
                    entity.Teams.ForEach(team => context.Teams.Attach(team));
                }
                context.Players.Add(entity);
                context.SaveChanges();
            }
            return entity;
        }

        public void Delete(int id)
        {
            using (var context = new DragonLairContext())

            {
                Player player = context.Players.Find(id);
                player.Teams.ForEach(team =>context.Teams.Attach(team));
                context.Players.Attach(player);              
                context.Players.Remove(player);         
                context.SaveChanges();
            }
        }

        public Player Read(int id)
        {
            using (var context = new DragonLairContext())
            {
                Player player = context.Players.Include(a => a.Teams).FirstOrDefault(v => v.Id == id);
                return player;
            }
        }

        public IEnumerable<Player> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
               
                List<Player> players = context.Players.Include(a => a.Teams).ToList();
                return players;
            }
        }

        public bool Update(Player entity)
        {
            using (var context = new DragonLairContext())
            {
                Player player = context.Players.Include(a => a.Teams).FirstOrDefault(b => b.Id == entity.Id);
                if ((player == null)) return false;
                player.Name = entity.Name;
                if(entity.Teams == null) entity.Teams = new List<Team>();
                player.Teams.Clear();
                foreach (var team in entity.Teams)
                {
                    player.Teams.Add(context.Teams.Find(team.Id));
                }
                context.Entry(player).State = EntityState.Modified;           
                context.SaveChanges();
                return true;
            }             
        }
    }
}
