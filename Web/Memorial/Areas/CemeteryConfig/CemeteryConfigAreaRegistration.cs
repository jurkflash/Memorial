using System.Web.Mvc;

namespace Memorial.Areas.CemeteryConfig
{
    public class CemeteryConfigAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CemeteryConfig";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CemeteryConfig_default",
                "CemeteryConfig/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}