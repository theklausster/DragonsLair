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
                context.Genres.Remove(context.Genres.Find(id));
                context.SaveChanges();
            }
        }

        public Genre Read(int id)
        {
            using (var context = new DragonLairContext())
            {
                Genre genre = context.Genres.Include(a => a.Games).FirstOrDefault(b => b.Id == id);
                return genre;
            }
        }

        public IEnumerable<Genre> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                List<Genre> genres= context.Genres.Include(a => a.Games).ToList();
                return genres;
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
