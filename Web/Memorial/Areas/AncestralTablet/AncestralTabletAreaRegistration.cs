using System.Web.Mvc;

namespace Memorial.Areas.AncestralTablet
{
    public class AncestralTabletAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AncestralTablet";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AncestralTablet_default",
                "AncestralTablet/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}