using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulBlagajnik
{
    public class ModulBlagajnikAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulBlagajnik";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulBlagajnik_default",
                "ModulBlagajnik/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}