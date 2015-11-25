using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;

namespace BackendDAL.Repositories
{
    class GenreRepository : IRepository<Genre>
    {
        public Genre Create(Genre entity)
        {
            using (var context = new DragonLairContext())
            {
                context.Genres.Add(entity);
                context.SaveChanges();
            }
            return entity;
        }

        public void Delete(int id)
        {
            using (var context = new DragonLairContext())
            {
                Genre genre = context.Genres.Find(id);
                context.Genres.Remove(genre);
            }
        }

        public Genre Read(int id)
        {
            using (var context = new DragonLairContext())
            {
                return context.Genres.Find(id);
            }
        }

        public IEnumerable<Genre> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                return context.Genres.ToList();
            }
        }

        public bool Update(Genre entity)
        {
            using (var context = new DragonLairContext())
            {
                Genre genre = context.Genres.Find(entity.Id);
                if ((genre == null)) return false;
                genre.Name = entity.Name;
                genre.Games = entity.Games;
                context.SaveChanges();
                return true;
            }
        }
    }
}
