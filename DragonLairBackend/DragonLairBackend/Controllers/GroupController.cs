using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BackendDAL.Facade;
using BackendDAL.Repositories;
using DTOConverter.Converter;
using DTOConverter.DTOModel;
using Entities;

namespace DragonLairBackend.Controllers
{
    public class GroupController : ApiController
    {
        private readonly IRepository<Group> groupRepository;
        private readonly IRepository<Team> teamRepository;
        private readonly IRepository<Player> playerRepository;
        private DTOGroupConverter dtoGroupConverter;

        public GroupController()
        {
            dtoGroupConverter = new DTOGroupConverter();
            var facade = new Facade();
            groupRepository = facade.GetGroupRepository();
            teamRepository = facade.GetTeamRepository();
            playerRepository = facade.GetPlayerRepository();
        }
        // GET: api/Group
        public IEnumerable<DTOGroup> Get()
        {
            var groups = groupRepository.ReadAll();
            foreach (var group in groups)
            {
                List<Team> teams = new List<Team>();
                //foreach (var team in group.Teams)
                //{
                //    List<Player> players = new List<Player>();
                //    foreach (var player in team.Players)
                //    {
                //        var playerFromRepo = playerRepository.Read(player.Id);
                //        players.Add(playerFromRepo);
                //    }
                //    team.Players = players;
                //    var teamFromRepo = teamRepository.Read(team.Id);
                //    teams.Add(teamFromRepo);
                //}
                //group.Teams = teams;
            }
            return new List<DTOGroup>(dtoGroupConverter.Convert(groups));
        }

        // GET: api/Group/5
        public IHttpActionResult Get(int id)
        {
            var group = groupRepository.Read(id);
            List<Team> teams = new List<Team>();
            foreach (var team in group.Teams)
            {
                var teamFromRepo = teamRepository.Read(team.Id);
                teams.Add(teamFromRepo);
            }
            group.Teams = teams;
            DTOGroup dtoGroup = dtoGroupConverter.Convert(group);
            if (dtoGroup == null) return NotFound();
            return Ok(dtoGroup);
        }

        // POST: api/Group
        public HttpResponseMessage Post(Group group)
        {
            group = groupRepository.Create(group);
            var response = Request.CreateResponse<Group>(HttpStatusCode.Created, group);
            //string uri = Url.Link("DefaultApi", new { id = group.Id });
            //response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT: api/Group/5
        public void Put(int id, Group group)
        {
            group.Id = id;
            if (!groupRepository.Update(group)) throw new HttpResponseException(HttpStatusCode.NotFound);

        }

        // DELETE: api/Group/5
        public void Delete(int id)
        {
            Group group = groupRepository.Read(id);
            if (group == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            groupRepository.Delete(id);
        }
    }
}
