using System.Web.Mvc;

namespace Memorial.Areas.ColumbariumConfig
{
    public class ColumbariumConfigAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ColumbariumConfig";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ColumbariumConfig_default",
                "ColumbariumConfig/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}