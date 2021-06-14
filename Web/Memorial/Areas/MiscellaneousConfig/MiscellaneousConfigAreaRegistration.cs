using System.Web.Mvc;

namespace Memorial.Areas.MiscellaneousConfig
{
    public class MiscellaneousConfigAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MiscellaneousConfig";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MiscellaneousConfig_default",
                "MiscellaneousConfig/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}