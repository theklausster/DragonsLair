using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;
using System.Data.Entity;

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
                context.Players.Remove(context.Players.Find(id));         
                context.SaveChanges();
            }
        }

        public Player Read(int id)
        {
            using (var context = new DragonLairContext())
            {
                return context.Players.Include(a => a.Teams).FirstOrDefault(v => v.Id == id);

            }
        }

        public IEnumerable<Player> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                return context.Players.Include(a => a.Teams).ToList();
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
