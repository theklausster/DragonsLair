using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BackendDAL.Context;
using BackendDAL.Facade;
using Entities;
using Match = Entities.Match;

namespace BackendDAL.Repositories
{
    public class MatchRepository : IRepository<Match>
    {
        public Match Create(Match entity)
        {
            Match match = new Match();
            using (var context = new DragonLairContext())
            {
                    context.Matches.Add(entity);
                    context.SaveChanges();
            }
            return entity;
        }

        public List<Match> Create(List<Match> matches)
        {
            List<Team> teams = new List<Team>();
            List<Match> list = new List<Match>();
            using (var context = new DragonLairContext())
            {
                context.Configuration.ProxyCreationEnabled = false;

                teams = context.Teams.ToList();
                foreach (var match in matches)
                {
                    match.HomeTeam = teams.FirstOrDefault(a => a.Name == match.HomeTeam.Name);
                    match.AwayTeam = teams.FirstOrDefault(a => a.Name == match.AwayTeam.Name);
                    context.Matches.Add(match);
                    list.Add(match);

                }
                context.SaveChanges();
                return list; 
            }
            
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

                context.Configuration.ProxyCreationEnabled = false;

                Match match = context.Matches.Find(id);

                context.Entry(match).Reference(a => a.Tournament).Load();
                context.Entry(match.Tournament)
                    .Collection(a => a.Groups)
                    .Query().Include(a => a.Teams).Include(a => a.Teams.Select(b => b.Players)).Load();
                context.Entry(match).Reference(a => a.AwayTeam).Load();
                context.Entry(match).Reference(a => a.HomeTeam).Load();
                context.Entry(match).Reference(a => a.Winner).Load();
                return match;
            }
        }

        public IEnumerable<Match> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                var list = context.Matches.Include(a => a.HomeTeam).Include(a => a.AwayTeam).Include(a => a.Winner).Include(a => a.Tournament).ToList();

                return list;

            }
        }

        public bool Update(Match entity)
        {
           
           using (var context = new DragonLairContext())
            {
                Match match = context.Matches.Find(entity.Id);
                if ((match == null)) return false;
                match.Tournament = context.Tournaments.Find(entity.Tournament.Id);
                match.AwayTeam = context.Teams.Find(entity.AwayTeam.Id);
                match.HomeTeam = context.Teams.Find(entity.HomeTeam.Id);
                match.Round = entity.Round;
                if (entity.Winner != null) match.Winner = context.Teams.Find(entity.Winner.Id);
                context.Entry(match).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }
    }
}

