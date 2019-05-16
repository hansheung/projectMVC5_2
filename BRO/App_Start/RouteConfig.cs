using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BRO
{
    public class RouteConfig
    {   
        
        public static void RegisterRoutes(RouteCollection routes)
        {
           
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //   "CurrencyDet",                                              // Route name
            //   "{controller}/{action}/{id}/{tableName}",                           // URL with parameters
            //   new { controller = "IC", action = "CurrencyDet", id = UrlParameter.Optional, tableName = UrlParameter.Optional }  // Parameter defaults
            //);

            //routes.MapRoute(
            //    name: "Password",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Password", action = "Password", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
