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

        public List<Team> Create(List<Team> teams)
        {
            List<Team> list = new List<Team>();
            using (var context = new DragonLairContext())
            {               
                foreach (var team in teams)
                {
                    team.Players.ForEach(a => context.Players.Attach(a));
                    context.Teams.Add(team);
                    list.Add(team);
                }
                context.SaveChanges();

            }
            return list; 
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
                return teams;
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

                }
                context.Entry(team).State = EntityState.Modified;
                context.SaveChanges();
                return true;

            }
        }
    }
}
