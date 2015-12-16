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
using DTOConverter.DTOModel;
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
            var facade = new DALFacade();
            teamRepository = facade.GetTeamRepository();

            
        }

        public List<DTOTeam> Get()
        {
           var teams = new List<DTOTeam>(DtoTeamConverter.Convert(teamRepository.ReadAll()));
            return teams;
        }

        // GET: api/Team/5
        public IHttpActionResult Get(int id)
        {
            DTOTeam dtoTeam = DtoTeamConverter.Convert(teamRepository.Read(id));
            if (dtoTeam == null) return NotFound();
            return Ok(dtoTeam);
        }

        // POST: api/Team
        public HttpResponseMessage Post(Team team)
        {
            team = teamRepository.Create(team);
            var response = Request.CreateResponse<DTOTeam>(HttpStatusCode.Created, DtoTeamConverter.Convert(team));
            //string uri = Url.Link("DefaultApi", new { id = team.Id });
            //response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT: api/Team/5
        public void Put(int id, Team team)
        {
            team.Id = id;
            if (!teamRepository.Update(team)) throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // DELETE: api/Team/5
        public void Delete(int id)
        {
            if (teamRepository.Read(id) == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            teamRepository.Delete(id);
        }
    }
}
