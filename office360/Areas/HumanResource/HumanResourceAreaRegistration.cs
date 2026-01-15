using System.Web.Http;
using System.Web.Mvc;

namespace office360.Areas.HumanResource
{
    public class HumanResourceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HumanResource";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HumanResource_default",
                "HumanResource/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
name: "HumanResourceApi",
routeTemplate: "api/HumanResource/{controller}/{action}/{id}",
defaults: new { id = RouteParameter.Optional }
);
        }
    }
}