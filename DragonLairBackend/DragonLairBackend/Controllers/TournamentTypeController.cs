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
    public class TournamentTypeController : ApiController
    {
        private readonly IRepository<TournamentType> tournamentTypeRepository;
        private DTOTournamentTypeConverter DtoTournamentTypeConverter;
        public TournamentTypeController()
        {
            DtoTournamentTypeConverter = new DTOTournamentTypeConverter();
            var facade = new DALFacade();
            tournamentTypeRepository = facade.GetTournamentTypeRepository();


        }
        // GET: api/TournamentType
        public IEnumerable<DTOTournamentType> Get()
        {
            return new List<DTOTournamentType>(DtoTournamentTypeConverter.Convert(tournamentTypeRepository.ReadAll()));
        }

        // GET: api/TournamentType/5
        public IHttpActionResult Get(int id)
        {
            DTOTournamentType dtoTournamentType = DtoTournamentTypeConverter.Convert(tournamentTypeRepository.Read(id));
            if (dtoTournamentType == null) return NotFound();
            return Ok(dtoTournamentType);
        }

        // POST: api/TournamentType
        public HttpResponseMessage Post(TournamentType tournamentType)
        {
            tournamentType = tournamentTypeRepository.Create(tournamentType);
            var response = Request.CreateResponse<TournamentType>(HttpStatusCode.Created, tournamentType);
            string uri = Url.Link("DefaultApi", new { id = tournamentType.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT: api/TournamentType/5
        public void Put(int id, TournamentType tournamentType)
        {
            tournamentType.Id = id;
            if (!tournamentTypeRepository.Update(tournamentType)) throw new HttpResponseException(HttpStatusCode.NotFound);
        }
        // DELETE: api/TournamentType/5
        public void Delete(int id)
        {
            if (tournamentTypeRepository.Read(id) == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            tournamentTypeRepository.Delete(id);
        }
    }
}
