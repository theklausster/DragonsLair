using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;

namespace BackendDAL.Repositories
{

    public class GameRepository : AbstractRepository<Game>
    {
        public override bool Update(Game entity)
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


        //public Game Create(Game entity)
        //{
        //    using (var context = new DragonLairContext())
        //    {
        //        context.Games.Add(entity);
        //        context.SaveChanges();
        //    }
        //    return entity;
        //}

        //public void Delete(int id)
        //{
        //    using (var context = new DragonLairContext())
        //    {
        //        context.Games.Remove(context.Games.Find(id));
        //        context.SaveChanges();
        //    }
        //}

        //public Game Read(int id)
        //{
        //    using (var context = new DragonLairContext())
        //    {
        //        return context.Games.Find(id);
        //    }
        //}

        //public IEnumerable<Game> ReadAll()
        //{
        //    using (var context = new DragonLairContext())
        //    {
        //        return context.Games.ToList();
        //    }
        //}

        //    public bool Update(Game entity)
        //    {
        //        using (var context = new DragonLairContext())
        //        {
        //            Game game = context.Games.Find(entity.Id);
        //            if ((game == null)) return false;
        //            game.Name = entity.Name;
        //            game.Genre = entity.Genre;
        //            game.Tournaments = entity.Tournaments;
        //            context.SaveChanges();
        //            return true;
        //        }
        //    }
        //}
    }
}
