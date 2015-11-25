using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BackendDAL.Facade;
using BackendDAL.Repositories;
using DTOConverter;
using DTOConverter.Converter;
using Entities;

namespace DragonLairBackend.Controllers
{
    public class TeamController : ApiController
    {
        private readonly IRepository<Team> teamRepository;
        private DTOTeamConverter DtoTeamConverter;
        public TeamController()
        {
            DtoTeamConverter = new DTOTeamConverter();
            var facade = new Facade();
            teamRepository = facade.GetTeamRepository();
        }

        // GET: api/Team/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Team
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Team/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Team/5
        public void Delete(int id)
        {
        }
    }
}
