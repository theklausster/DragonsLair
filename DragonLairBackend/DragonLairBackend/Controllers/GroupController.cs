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
        private DTOGroupConverter dtoGroupConverter;

        public GroupController()
        {
            dtoGroupConverter = new DTOGroupConverter();
            var facade = new Facade();
            groupRepository = facade.GetGroupRepository();
        }
        // GET: api/Group
        public IEnumerable<DTOGroup> Get()
        {
            var groups = groupRepository.ReadAll();
            return new List<DTOGroup>(dtoGroupConverter.Convert(groups));
        }

        // GET: api/Group/5
        public IHttpActionResult Get(int id)
        {
            DTOGroup dtoGroup = dtoGroupConverter.Convert(groupRepository.Read(id));
            if (dtoGroup == null) return NotFound();
            return Ok(dtoGroup);
        }

        // POST: api/Group
        public HttpResponseMessage Post(Group group)
        {
            group = groupRepository.Create(group);
            var response = Request.CreateResponse<Group>(HttpStatusCode.Created, group);
            string uri = Url.Link("DefaultApi", new { id = group.Id });
            response.Headers.Location = new Uri(uri);
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
