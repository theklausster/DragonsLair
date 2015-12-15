using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BackendDAL.Context;
using Entities;
using Match = Entities.Match;

namespace BackendDAL.Repositories
{
    public class MatchRepository : IRepository<Match>
    {
        public Match Create(Match entity)
        {
            using (var context = new DragonLairContext())
            {
                context.Tournaments.Attach(entity.Tournament);
                context.Matches.Add(entity);
                context.SaveChanges();
            }
            return entity;
        }

        public void Delete(int id)
        {
            using (var context = new DragonLairContext())
            {
                context.Matches.Remove(context.Matches.Find(id));
                context.SaveChanges();
            }
        }

        public Match Read(int id)
        {
            using (var context = new DragonLairContext())
            {
                Match match = context.Matches.Include(b => b.Tournament).Include(b => b.Winner).FirstOrDefault(a => a.Id == id);
                return match;
            }
        }

        public IEnumerable<Match> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                List<Match> matches =  context.Matches.Include(b => b.Tournament).ToList();
                return matches;
            }
        }

        public bool Update(Match entity)
        {
            using (var context = new DragonLairContext())
            {
                Match match = context.Matches.Find(entity.Id);
                if ((match == null)) return false;
                match.Tournament = entity.Tournament;
                if (entity.Winner != null) match.Winner = entity.Winner;
                context.Entry(match).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }
    }
}

