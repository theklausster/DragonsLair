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
                context.Configuration.ProxyCreationEnabled = false;

                Tournament tournament = context.Tournaments.Find(id);

                context.Entry(tournament).Collection(a => a.Groups).Query().Include(b => b.Teams).Include(c => c.Teams.Select(d => d.Players)).Load();
                context.Entry(tournament).Reference(a => a.Game).Query().Include(a => a.Genre).Load();
                context.Entry(tournament).Reference(a => a.TournamentType).Load();
                context.Entry(tournament).Collection(a => a.Matches).Load();
                return tournament;
            }
        }

        public IEnumerable<Tournament> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                context.Configuration.ProxyCreationEnabled = false;

                List<Tournament> tournaments = context.Tournaments.ToList();
                List<Tournament> ts = ts = new List<Tournament>();

                foreach (var tournament in tournaments)
                {

                    var t = Read(tournament.Id);
                    ts.Add(t);
                }
                return ts;
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
