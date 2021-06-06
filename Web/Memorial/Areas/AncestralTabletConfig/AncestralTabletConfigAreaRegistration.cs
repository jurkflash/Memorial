using System.Web.Mvc;

namespace Memorial.Areas.AncestralTabletConfig
{
    public class AncestralTabletConfigAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AncestralTabletConfig";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AncestralTabletConfig_default",
                "AncestralTabletConfig/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}