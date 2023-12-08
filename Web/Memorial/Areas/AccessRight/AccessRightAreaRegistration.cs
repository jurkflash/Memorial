using System.Web.Mvc;

namespace Memorial.Areas.AccessRight
{
    public class AccessRightAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AccessRight";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AccessRight_default",
                "AccessRight/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}