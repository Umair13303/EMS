using System.Web.Mvc;

namespace office360.Areas.CRMModuleAEO
{
    public class CRMModuleAEOAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CRMModuleAEO";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CRMModuleAEO_default",
                "CRMModuleAEO/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}