using System.Web.Mvc;

namespace Memorial.Areas.CemeteryLandscapeCompanyConfig
{
    public class CemeteryLandscapeCompanyConfigAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CemeteryLandscapeCompanyConfig";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CemeteryLandscapeCompanyConfig_default",
                "CemeteryLandscapeCompanyConfig/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}