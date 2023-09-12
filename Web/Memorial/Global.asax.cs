using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Memorial.App_Start;
using System.Web.Http;
using Serilog;

namespace Memorial
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(c => {
                c.AddProfile<DomainToDomain>();
                c.AddProfile<DomainToDto>();
                c.AddProfile<DtoToDomain>();
            });
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var log = new LoggerConfiguration()
            .WriteTo.File(System.Web.Hosting.HostingEnvironment.MapPath("~/bin/Logs/log.txt"))
            .CreateLogger();
            Log.Logger = log;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                Log.Logger.Error(exception, exception.Message.ToString());
            }
        }
    }
}
