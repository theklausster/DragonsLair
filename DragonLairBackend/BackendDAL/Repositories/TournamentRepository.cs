using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;
using System.Data.Entity;
using BackendDAL.Facade;

namespace BackendDAL.Repositories
{
    class TournamentRepository : IRepository<Tournament>
    {

        DALFacade facade = new DALFacade();
        public Tournament Create(Tournament entity)
        {
            var matches =  entity.Matches;
            entity.Matches = null;
            using (var context = new DragonLairContext())
            {
                foreach (var group in entity.Groups)
                {
                    foreach (var team in group.Teams)
                    {
                        team.Players.ForEach(a => context.Players.Attach(a));            
                    }
                }
                context.Games.Attach(entity.Game);
                context.Genres.Attach(entity.Game.Genre);
                context.TournamentTypes.Attach(entity.TournamentType);
                context.Tournaments.Add(entity);
                context.SaveChanges();
            }

            using (var context = new DragonLairContext())
            {

                List<Group> groups = entity.Groups.ToList();
                List<Team> allTeams = new List<Team>();

                foreach (var group in groups)
                {
                    group.Teams.ForEach(team => allTeams.Add(team));                  
                }
                matches.ForEach(x => x.AwayTeam = allTeams.FirstOrDefault(a => a.Name.Equals(x.AwayTeam.Name)));
                matches.ForEach(x => x.HomeTeam = allTeams.FirstOrDefault(a => a.Name.Equals(x.HomeTeam.Name)));
                matches.ForEach(x => x.Tournament = entity);
                matches.ForEach(x => context.Teams.Attach(x.AwayTeam));
                matches.ForEach(x => context.Teams.Attach(x.HomeTeam));
                matches.ForEach(x => context.Tournaments.Attach(x.Tournament));
                matches.ForEach(x => context.Matches.Add(x));
                context.SaveChanges();
            }

            using (var context = new DragonLairContext())
            {
                Tournament tounament = context.Tournaments.Find(entity.Id);
                tounament.Matches = context.Matches.Where(a => a.Tournament.Id == entity.Id).ToList();
                
                context.SaveChanges();
            }

            return entity;
        }

        public void Delete(int id)
        {
            using (var context = new DragonLairContext())
            {


                var tournament = context.Tournaments.Find(id);
                //tournament.Groups.ForEach(a => a.Teams.ForEach(b => context.Teams.Remove(b)));
                //tournament.Groups.ForEach(a => context.Groups.Remove(a));
                //tournament.Matches.ForEach(a => context.Matches.Remove(a));
                context.Tournaments.Remove(tournament);
                context.SaveChanges();
            }
        }

        public Tournament Read(int id)
        {
            using (var context = new DragonLairContext())
            {
                context.Configuration.ProxyCreationEnabled = false;

                Tournament tournament = context.Tournaments.FirstOrDefault(a => a.Id == id);
                if (tournament != null)
                {
                    context.Entry(tournament).Collection(a => a.Groups).Query().Include(b => b.Teams).Include(c => c.Teams.Select(d => d.Players)).Load();
                    context.Entry(tournament).Reference(a => a.Game).Query().Include(a => a.Genre).Load();
                    context.Entry(tournament).Reference(a => a.TournamentType).Load();
                    context.Entry(tournament).Collection(a => a.Matches).Query().Include(a => a.AwayTeam).Include(a => a.HomeTeam).Load();
                }
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
