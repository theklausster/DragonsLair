using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DragonLairFrontEnd.Controllers;
using Entities;
using ServiceGateway.Http;

namespace DragonLairFrontEnd.Models
{
    public class GameGenreViewModel
    {
        //WebApiService webApiService = new WebApiService();
        public GameGenreViewModel()
        {
           
        }

        public Game Game { get; set; }
        public List<Genre> Genres { get; set; }
    }
}