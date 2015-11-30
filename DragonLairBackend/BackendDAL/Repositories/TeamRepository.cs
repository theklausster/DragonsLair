using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;

namespace BackendDAL.Repositories
{
    class TeamRepository : IRepository<Team>
    {
        public Team Create(Team entity)
        {
            using (var context = new DragonLairContext())
            {
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
                return context.Teams.Find(id);
            }
        }

        public IEnumerable<Team> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                return context.Teams.ToList();
            }
        }

        public bool Update(Team entity)
        {
            using (var context = new DragonLairContext())
            {
                Team team = context.Teams.Find(entity.Id);
                if ((team == null)) return false;
                team.Draw = entity.Draw;
                team.Loss = entity.Loss;
                team.Win = entity.Win;
                team.Name = entity.Name;
                team.Groups = entity.Groups;
                team.Players = entity.Players;
                context.SaveChanges();
                return true;
            }
        }
    }
}
