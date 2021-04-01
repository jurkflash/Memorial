using System.Web.Mvc;

namespace Memorial.Areas.Urn
{
    public class UrnAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Urn";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Urn_default",
                "Urn/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}