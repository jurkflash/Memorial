using System.Web.Mvc;

namespace Memorial.Areas.FuneralCompanyConfig
{
    public class FuneralCompanyConfigAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FuneralCompanyConfig";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FuneralCompanyConfig_default",
                "FuneralCompanyConfig/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}