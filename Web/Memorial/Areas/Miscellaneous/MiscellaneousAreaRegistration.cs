using System.Web.Mvc;

namespace Memorial.Areas.Miscellaneous
{
    public class MiscellaneousAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Miscellaneous";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Miscellaneous_default",
                "Miscellaneous/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}