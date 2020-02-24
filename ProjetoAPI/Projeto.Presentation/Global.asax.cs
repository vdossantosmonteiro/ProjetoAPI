using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Projeto.Presentation.App_Start;

namespace Projeto.Presentation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //registrar a classe AutoMapperConfig.cs
            Mapper.Initialize(m => { m.AddProfile<AutoMapperConfig>(); });
        }
    }
}
