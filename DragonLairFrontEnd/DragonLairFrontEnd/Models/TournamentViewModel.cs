using Entities;
using ServiceGateway.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DragonLairFrontEnd.Models
{
    public class TournamentViewModel
    {

        private WebApiService ServiceGateway = new WebApiService();
        public List<Tournament> Tournaments { get; set; }
        public Tournament Tournament { get; set; }

        public async Task PopulateIndexData()
        {
            Tournaments = await ServiceGateway.GetAsync<List<Tournament>>("api/Tournament/");
        }

        public async Task PopulateDetailsData(int id)
        {
            Tournament = await ServiceGateway.GetAsync<Tournament>("api/Tournament/" + id);
        }
    }
}