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
    class TournamentRepository : IRepository<Tournament>
    {
        public Tournament Create(Tournament entity)
        {
            using (var context = new DragonLairContext())
            {
                context.Tournaments.Add(entity);
                context.SaveChanges();
            }
            return entity;
        }

        public void Delete(int id)
        {
            using (var context = new DragonLairContext())
            {
                context.Tournaments.Remove(context.Tournaments.Find(id));
                context.SaveChanges();
            }
        }

        public Tournament Read(int id)
        {
            using (var context = new DragonLairContext())
            {
                Tournament tournament =
                    context.Tournaments.Include(a => a.Game)
                        .Include(b => b.Groups)
                        .Include(c => c.TournamentType)
                        .FirstOrDefault(d => d.Id == id);
                return tournament;
            }
        }

        public IEnumerable<Tournament> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                List<Tournament> tournaments = context.Tournaments.Include(a => a.Game)
                        .Include(b => b.Groups)
                        .Include(c => c.TournamentType).ToList();
                return tournaments;
            }
        }

        public bool Update(Tournament entity)
        {
            using (var context = new DragonLairContext())
            {
                Tournament tournament = context.Tournaments.Find(entity.Id);
                if ((tournament == null)) return false;
                tournament.TournamentType = entity.TournamentType;
                tournament.Game = entity.Game;
                tournament.Groups = entity.Groups;
                tournament.Name = entity.Name;
                tournament.StartDate = entity.StartDate;
                context.SaveChanges();
                return true;
            }
        }
    }
}
