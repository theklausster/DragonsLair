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
    public class TournamentController : ApiController
    {
        private readonly IRepository<Tournament> tournamentRepository;
        private DTOTournamentConverter dtoTournamentConverter;

        public TournamentController()
        {
            dtoTournamentConverter = new DTOTournamentConverter();
            var facade = new DALFacade();
            tournamentRepository = facade.GetTournamentRepository();
        }
        // GET: api/Tournament
        public IEnumerable<Tournament> Get()
        {
            //List<DTOTournament> dtoTournaments = new List<DTOTournament>(dtoTournamentConverter.Convert(tournamentRepository.ReadAll()));
            return tournamentRepository.ReadAll();

        }

        // GET: api/Tournament/5
        public IHttpActionResult Get(int id)
        {
            DTOTournament dtoTournament = dtoTournamentConverter.Convert(tournamentRepository.Read(id));
            if (dtoTournament == null) return NotFound();
            return Ok(dtoTournament);
        }

        // POST: api/Tournament
        public HttpResponseMessage Post(Tournament tournament)
        {
            tournament = tournamentRepository.Create(tournament);
            var response = Request.CreateResponse<DTOTournament>(HttpStatusCode.Created, dtoTournamentConverter.Convert(tournament));
            string uri = Url.Link("DefaultApi", new { id = tournament.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT: api/Tournament/5
        public void Put(int id, Tournament tournament)
        {
            tournament.Id = id;
            if (!tournamentRepository.Update(tournament)) throw new HttpResponseException(HttpStatusCode.NotFound);

        }

        // DELETE: api/Tournament/5
        public void Delete(int id)
        {
            Tournament tournament = tournamentRepository.Read(id);
            if (tournament == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            tournamentRepository.Delete(id);
        }
    }
}
