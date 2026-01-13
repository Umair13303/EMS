using System.Web.Mvc;

namespace office360.Areas.HiringAndRecruitmentModule
{
    public class HiringAndRecruitmentModuleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HiringAndRecruitmentModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HiringAndRecruitmentModule_default",
                "HiringAndRecruitmentModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}