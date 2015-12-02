using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;
using System.Data.Entity;

namespace BackendDAL.Repositories
{

    public class GameRepository : IRepository<Game>
    {
        public Game Create(Game entity)
        {
            using (var context = new DragonLairContext())
            {
                context.Genres.Attach(entity.Genre);
                context.Games.Add(entity);
                context.SaveChanges();
            }
            return entity;
        }

        public void Delete(int id)
        {
            using (var context = new DragonLairContext())
            {
                context.Games.Remove(context.Games.Find(id));
                context.SaveChanges();
            }
        }

        public Game Read(int id)
        {
            using (var context = new DragonLairContext())
            {
                return context.Games.Include(b => b.Tournaments).Include(b => b.Genre).FirstOrDefault(a => a.Id == id);
            }
        }

        public IEnumerable<Game> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                return context.Games.Include(b => b.Tournaments).Include(b => b.Genre).ToList();
            }
        }

        public bool Update(Game entity)
        {
            using (var context = new DragonLairContext())
            {
                Game game = context.Games.Find(entity.Id);
                if ((game == null)) return false;
                game.Name = entity.Name;
                game.Genre = entity.Genre;
                game.Tournaments = entity.Tournaments;
                context.SaveChanges();
                return true;
            }
        }
    }
}

