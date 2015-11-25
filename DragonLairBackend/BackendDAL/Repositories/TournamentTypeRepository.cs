using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;

namespace BackendDAL.Repositories
{
    class TournamentTypeRepository : IRepository<TournamentType>
    {
        public TournamentType Create(TournamentType entity)
        {
            using (var context = new DragonLairContext())
            {
                context.TournamentTypes.Add(entity);
                context.SaveChanges();
            }
            return entity;
        }

        public void Delete(int id)
        {
            using (var context = new DragonLairContext())
            {
                TournamentType tournamentType = context.TournamentTypes.Find(id);
                context.TournamentTypes.Remove(tournamentType);
            }
        }

        public TournamentType Read(int id)
        {
            using (var context = new DragonLairContext())
            {
                return context.TournamentTypes.Find(id);
            }
        }

        public IEnumerable<TournamentType> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                return context.TournamentTypes.ToList();
            }
        }

        public bool Update(TournamentType entity)
        {
            using (var context = new DragonLairContext())
            {
                TournamentType tournamentType = context.TournamentTypes.Find(entity.Id);
                if ((tournamentType == null)) return false;
                tournamentType.Type = entity.Type;
                tournamentType.Tournaments = entity.Tournaments;
                context.SaveChanges();
                return true;
            }
        }
    }
}
