using System.Web.Http;
using System.Web.Mvc;

namespace office360.Areas.Account
{
    public class AccountAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Account_default",
                "Account/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
        name: "AccountApi",
        routeTemplate: "api/account/{controller}/{action}/{id}",
        defaults: new { id = RouteParameter.Optional }
    );
        }

    }
}