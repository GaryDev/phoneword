﻿using phoneword.rest.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace phoneword.rest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            UnityConfig.RegisterComponents();

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Logger Filter
            config.Filters.Add(new LoggingFilterAttribute());
        }
    }
}
