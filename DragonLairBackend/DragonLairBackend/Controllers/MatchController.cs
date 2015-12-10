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
    public class MatchController : ApiController
    {
        private readonly IRepository<Group> groupRepository;
        private readonly IRepository<Tournament> tournamentRepository;
        private readonly IRepository<Team> teamRepository;
        private readonly IRepository<Match> matchRepository;
        private DTOMatchConverter DtoMatcbConverter;
        public MatchController()
        {
            DtoMatcbConverter = new DTOMatchConverter();
            var facade = new Facade();
            matchRepository = facade.GetMatchRepository();
            teamRepository = facade.GetTeamRepository();
            groupRepository = facade.GetGroupRepository();
            tournamentRepository = facade.GetTournamentRepository();
        }

        // GET: api/Player
        public List<DTOMatch> Get()
        {
            var matches = new List<DTOMatch>(DtoMatcbConverter.Convert(matchRepository.ReadAll()));
            return matches;
        }

        // GET: api/Player/5
        public IHttpActionResult Get(int id)
        {
            DTOMatch dtoMatch = DtoMatcbConverter.Convert(matchRepository.Read(id));
            if (dtoMatch == null) return NotFound();
            return Ok(dtoMatch);
        }

        // POST: api/Player
        public HttpResponseMessage Post(Match match)
        {
            match = matchRepository.Create(match);
            var response = Request.CreateResponse<Match>(HttpStatusCode.Created, match);
            string uri = Url.Link("DefaultApi", new { id = match.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT: api/Player/5
        public void Put(int id, Match match)
        {
            //Player player;
            //entity.ToString();
            match.Id = id;
            if (!matchRepository.Update(match)) throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // DELETE: api/Player/5
        public void Delete(int id)
        {
            if (matchRepository.Read(id) == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            matchRepository.Delete(id);
        }
    }
}
