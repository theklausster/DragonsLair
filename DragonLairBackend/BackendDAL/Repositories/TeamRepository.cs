using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;
using System.Data.Entity;
using System.Threading;
using BackendDAL.Facade;

namespace BackendDAL.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        public Team Create(Team entity)
        {
            using (var context = new DragonLairContext())
            {
                entity.Players.ForEach(a => context.Players.Attach(a));
                context.Teams.Add(entity);
                context.SaveChanges();
            }
            return entity;
        }

       public void Delete(int id)
        {
            using (var context = new DragonLairContext())
            {
                context.Teams.Remove(context.Teams.Find(id));
                context.SaveChanges();
            }
        }

        public Team Read(int id)
        {
            using (var context = new DragonLairContext())
            {
                Team team = context.Teams.Include(p => p.Players).Include(g => g.Groups).FirstOrDefault(a => a.Id == id);
                return team;
            }
        }

        public IEnumerable<Team> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                List<Team> teams = context.Teams.Include(p => p.Players).Include(g => g.Groups).ToList();
                UpdateTeams(teams);
                List<Team> updatedTeams = context.Teams.Include(p => p.Players).Include(g => g.Groups).ToList();
                return updatedTeams;
            }
        }

        private void UpdateTeams(List<Team> teams)
        {
            var facade = new DALFacade();
            List<Match> matches = (List<Match>)facade.GetMatchRepository().ReadAll();
            List<Match> matchesWithWinners = new List<Match>();
            foreach (var match in matches)
            {
                if (match.Winner != null)
                {
                    match.HomeTeam.Win = 0;
                    match.AwayTeam.Win = 0;
                    match.HomeTeam.Loss = 0;
                    match.AwayTeam.Loss = 0;
                    matchesWithWinners.Add(match);
                }
            }
          
            foreach (var team in teams)
            {
                int winCount = 0;
                int lossCount = 0;
                team.Win = 0;
                team.Loss = 0;
                foreach (var match in matchesWithWinners)
                {
                    
                     
                    if (team.Id == match.Winner.Id)
                    {
                        winCount++;
                        if (team.Win != winCount)
                        {
                            team.Win = winCount;                           
                        }
                    }
                    if (team.Id == match.AwayTeam.Id && team.Id != match.Winner.Id || team.Id == match.HomeTeam.Id && team.Id != match.Winner.Id)
                    {
                       lossCount++;
                        if (team.Loss != lossCount)
                        {
                            team.Loss = lossCount;                       
                        }
                    }
                    Update(team);
                }
                
            }

        }

        public bool Update(Team entity)
        {
            using (var context = new DragonLairContext())
            {

                Team team = context.Teams.Include(a => a.Players).FirstOrDefault(b => b.Id == entity.Id);
                if ((team == null)) return false;
                team.Name = entity.Name;
                team.Win = entity.Win;
                team.Loss = entity.Loss;
                team.Draw = entity.Draw;

                if (entity.Players == null) entity.Players = new List<Player>();
                team.Players.Clear();
                foreach (var player in entity.Players)
                {
                    team.Players.Add(context.Players.Find(player.Id));
                    context.Players.Attach(context.Players.Find(player.Id));
                }
                
                context.Entry(team).State = EntityState.Modified;
                context.SaveChanges();
                return true;

            }
        }
    }
}
