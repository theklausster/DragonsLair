using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                context.Players.Remove(player);
            }
        }

        public Player Read(int id)
        {
            using (var context = new DragonLairContext())
            {
                return context.Players.Find(id);
            }
        }

        public IEnumerable<Player> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                return context.Players.ToList();
            }
        }

        public bool Update(Player entity)
        {
            using (var context = new DragonLairContext())
            {
                Player player = context.Players.Find(entity.Id);
                if ((player == null)) return false;
                player.Name = entity.Name;
                player.Teams = entity.Teams;
                context.SaveChanges();
                return true;
            }
                
        }
    }
}
