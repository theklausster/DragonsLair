using System;
using System.Web.ModelBinding;
using System.Web.Mvc;
using DragonLairFrontEnd.Models;
using IModelBinder = System.Web.Mvc.IModelBinder;
using ModelBindingContext = System.Web.Mvc.ModelBindingContext;


namespace DragonLairFrontEnd.Binders
{
    public class TeamBinder : IModelBinder
    {

        private const string sessionKey = "TeamViewModel";


        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            TeamViewModel team = null;
            if (controllerContext.HttpContext.Session != null)
            {
                team = (TeamViewModel)controllerContext.HttpContext.Session[sessionKey];
            }

            if (team == null)
            {
                team = new TeamViewModel();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = team;
                }
            }

            return team;
        }

    }

}