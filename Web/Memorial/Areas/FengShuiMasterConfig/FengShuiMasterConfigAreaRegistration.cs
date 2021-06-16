using System.Web.Mvc;

namespace Memorial.Areas.FengShuiMasterConfig
{
    public class FengShuiMasterConfigAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FengShuiMasterConfig";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FengShuiMasterConfig_default",
                "FengShuiMasterConfig/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}