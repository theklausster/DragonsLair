﻿using System;
using System.Net;
using System.Web.Mvc;
using GateWay.Http;
using Newtonsoft.Json;

namespace WebApplicationCleint.Filters
{
    public class HandleApiErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var apiException = filterContext.Exception as ApiException;
            if (!filterContext.ExceptionHandled && apiException != null)
            {
                switch (apiException.StatusCode)
                {
                    case HttpStatusCode.InternalServerError:
                        //Show a YSOD containing the original exception details
                        var errDetails = JsonConvert.DeserializeObject<JsonExceptionMessage>(apiException.JsonData);
                        filterContext.Exception = new Exception(errDetails.ExceptionMessage);
                        break;
                }
            }

            base.OnException(filterContext);
        }

        private class JsonExceptionMessage
        {
            public string Message { get; set; }
            public string ExceptionMessage { get; set; }
        }
    }
}
