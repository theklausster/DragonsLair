using BackendDAL.Facade;
using BackendDAL.Repositories;
using DTOConverter.Converter;
using DTOConverter.DTOModel;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DragonLairBackend.Controllers
{
    public class GenreController : ApiController
    {
        private IRepository<Genre> GenreRepository;
        private DTOGenreConverter DtoGenreConverter;


        public GenreController()
        {
            DtoGenreConverter = new DTOGenreConverter();
            GenreRepository = new Facade().GetGenreRepository();
        }
        // GET: api/Genre
        public List<DTOGenre> Get()
        {      
            return DtoGenreConverter.Convert(GenreRepository.ReadAll()).ToList();
        }

        // GET: api/Genre/5
        public IHttpActionResult Get(int id)
        {
            var dtoGenre = DtoGenreConverter.Convert(GenreRepository.Read(id));
            if (dtoGenre == null) return NotFound();
            return Ok(dtoGenre);
        }

        // POST: api/Genre
        public HttpResponseMessage Post(Genre genre)
        {
            genre = GenreRepository.Create(genre);
            var response = Request.CreateResponse<Genre>(HttpStatusCode.Created, genre);
            string uri = Url.Link("DefaultApi", new { id = genre.Id });
            response.Headers.Location = new Uri(uri);
            return response;

        }

        // PUT: api/Genre/5
        public void Put(int id, Genre genre)
        {
            genre.Id = id;
            if (!GenreRepository.Update(genre)) throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // DELETE: api/Genre/5
        public void Delete(int id)
        {
            var genre = GenreRepository.Read(id);
            if (genre == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            GenreRepository.Delete(id);
        }
    }
}
