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
    class GroupRepository : IRepository<Group>
    {
        public Group Create(Group entity)
        {
            using (var context = new DragonLairContext())
            {
              foreach (var team in entity.Teams)
                {
                    team.Players = null;
                    context.Teams.Attach(team);
                }
                context.Groups.Add(entity);
                context.SaveChanges();
            }
            return entity;
        }

        public void Delete(int id)
        {
            using (var context = new DragonLairContext())
            {
                context.Groups.Remove(context.Groups.Find(id));
                context.SaveChanges();
            }
        }

        public Group Read(int id)
        {
            using (var context = new DragonLairContext())
            {
                
                Group group = context.Groups.Include(a => a.Teams).Include(a => a.Tournament).FirstOrDefault(b => b.Id == id);
                return group;
            }
        }

        public IEnumerable<Group> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                List<Group> groups = context.Groups.Include(a => a.Teams).Include(a => a.Tournament).ToList();
                return groups;
            }
        }

        public bool Update(Group entity)
        {
            using (var context = new DragonLairContext())
            {
                Group group = context.Groups.Include(a => a.Teams).FirstOrDefault(b => b.Id == entity.Id);
                if ((group == null)) return false;
                group.Name = entity.Name;
                group.Teams.Clear();
                foreach (var team in entity.Teams)
                {
                    team.Players = null;
                    group.Teams.Add(context.Teams.Find(team.Id));
                }
                context.Entry(group).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }
    }
}
