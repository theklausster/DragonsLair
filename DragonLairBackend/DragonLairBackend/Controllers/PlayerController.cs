using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Design;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BackendDAL.Facade;
using BackendDAL.Repositories;
using DTOConverter;
using DTOConverter.DTOModel;
using Entities;

namespace DragonLairBackend.Controllers
{
    public class PlayerController : ApiController
    {
        private readonly IRepository<Player> playerRepository;
        private DTOPlayerConverter DtoPlayerConverter;
        public PlayerController()
        {
            DtoPlayerConverter = new DTOPlayerConverter();
            var facade = new Facade();
            playerRepository = facade.GetPlayerRepository();
        }

        // GET: api/Player
        public List<DTOPlayer> Get()
        {
            var players = new List<DTOPlayer>(DtoPlayerConverter.Convert(playerRepository.ReadAll()));
            return players;
        }

        // GET: api/Player/5
        public IHttpActionResult Get(int id)
        {
            DTOPlayer dtoPlayer = DtoPlayerConverter.Convert(playerRepository.Read(id));
            if (dtoPlayer == null) return NotFound();
            return Ok(dtoPlayer);
        }

        // POST: api/Player
        public HttpResponseMessage Post(Player player)
        {
            player = playerRepository.Create(player);
            var response = Request.CreateResponse<Player>(HttpStatusCode.Created, player);
            string uri = Url.Link("DefaultApi", new { id = player.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT: api/Player/5
        public void Put(int id, Player player)
        {
            player.Id = id;
            if (!playerRepository.Update(player)) throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // DELETE: api/Player/5
        public void Delete(int id)
        {
            if (playerRepository.Read(id) == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            playerRepository.Delete(id);
        }
    }
}
