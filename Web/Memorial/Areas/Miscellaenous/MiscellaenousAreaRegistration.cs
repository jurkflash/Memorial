using System.Web.Mvc;

namespace Memorial.Areas.Miscellaenous
{
    public class MiscellaenousAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Miscellaenous";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Miscellaenous_default",
                "Miscellaenous/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}