using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;

namespace BackendDAL.Repositories
{
    class GroupRepository : IRepository<Group>
    {
        public Group Create(Group entity)
        {
            using (var context = new DragonLairContext())
            {
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
                return context.Groups.Find(id);
            }
        }

        public IEnumerable<Group> ReadAll()
        {
            using (var context = new DragonLairContext())
            {
                return context.Groups.ToList();
            }
        }

        public bool Update(Group entity)
        {
            using (var context = new DragonLairContext())
            {
                Group group = context.Groups.Find(entity.Id);
                if ((group == null)) return false;
                group.Name = entity.Name;
                group.Teams = entity.Teams;
                group.Tournament = entity.Tournament;
                context.SaveChanges();
                return true;
            }
        }
    }
}
