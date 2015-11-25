using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;

namespace BackendDAL.Initializer
{
    public static class DbInitializer 
    {
        public static void Initialize()
        {
            Database.SetInitializer(new DragonLairInitizalizer());
        }
    }
}
