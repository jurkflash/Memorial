using System.Web.Mvc;

namespace Memorial.Areas.ApplicantDeceaseds
{
    public class ApplicantDeceasedsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ApplicantDeceaseds";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ApplicantDeceaseds_default",
                "ApplicantDeceaseds/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}