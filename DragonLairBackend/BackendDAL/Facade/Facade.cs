using BackendDAL.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDAL.Facade
{
    public class Facade
    {
        public IRepository<Player> GetPlayerRepository()
        {
            return new PlayerRepository();
        }

        public IRepository<Team> GetTeamRepository()
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

        public IRepository<Game> GetGameRepository()
        {
            return new GameRepository();
        }

        public IRepository<Genre> GetGenreRepository()
        {
            return new GenreRepository();
        }



    }
}
