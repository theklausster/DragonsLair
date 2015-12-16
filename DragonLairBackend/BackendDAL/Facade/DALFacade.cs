using BackendDAL.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDAL.Facade
{
    public class DALFacade
    {
        ////Hacks to ensure DLL is copies to the database!
        private SqlProviderServices ensureDLLIsInUse = SqlProviderServices.Instance;
        private string Ensure = MySql.Data.Entity.MySqlProviderInvariantName.ProviderName;

        public IRepository<Player> GetPlayerRepository()
        {
            return new PlayerRepository();
        }
        public MatchRepository GetMatchRepository()
        {
            return new MatchRepository();
        }
        public TeamRepository GetTeamRepository()
        {
            return new TeamRepository();
        }

        public IRepository<Group> GetGroupRepository()
        {
            return new GroupRepository();
        }

        public IRepository<Tournament> GetTournamentRepository()
        {
            return new TournamentRepository();
        }

        public IRepository<TournamentType> GetTournamentTypeRepository()
        {
            return new TournamentTypeRepository();
        }

        public GameRepository GetGameRepository()
        {
            return new GameRepository();
        }

        public IRepository<Genre> GetGenreRepository()
        {
            return new GenreRepository();
        }



    }
}
