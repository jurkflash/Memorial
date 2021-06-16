using System.Web.Mvc;

namespace Memorial.Areas.ConfigLink
{
    public class ConfigLinkAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ConfigLink";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ConfigLink_default",
                "ConfigLink/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}