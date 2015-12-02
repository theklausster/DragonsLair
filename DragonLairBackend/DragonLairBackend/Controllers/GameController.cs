using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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
    public class GameController : ApiController
    {
        private readonly IRepository<Game> gameRepository;
        private DTOGameConverter dtoGameConverter;

        public GameController()
        {
            dtoGameConverter = new DTOGameConverter();
            var facade = new Facade();
            gameRepository = facade.GetGameRepository();
        }

        // GET: api/Game
        public IEnumerable<DTOGame> Get()
        {
            var games = gameRepository.ReadAll();
            return new List<DTOGame>(dtoGameConverter.Convert(games));
        }

        // GET: api/Game/5
        public IHttpActionResult Get(int id)
        {
            Game game = gameRepository.Read(id);
            DTOGame dtoGame = dtoGameConverter.Convert(game);
            if (dtoGame == null) return NotFound();
            return Ok(dtoGame);
        }

        // POST: api/Game
        public HttpResponseMessage Post(Game game)
        {         
            game = gameRepository.Create(game);
            DTOGame dtoGame = dtoGameConverter.Convert(game);
            var response = Request.CreateResponse<DTOGame>(HttpStatusCode.Created, dtoGame);
            string uri = Url.Link("DefaultApi", new { id = dtoGame.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT: api/Game/5
        public void Put(int id, Game game)
        {
            game.Id = id;
            if (!gameRepository.Update(game)) throw new HttpResponseException(HttpStatusCode.NotFound);

        }

        // DELETE: api/Game/5
        public void Delete(int id)
        {
            Game game = gameRepository.Read(id);
            if (game == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            gameRepository.Delete(id);
        }
    }
}
