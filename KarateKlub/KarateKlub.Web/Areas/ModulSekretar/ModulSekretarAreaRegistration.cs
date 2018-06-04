using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulSekretar
{
    public class ModulSekretarAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulSekretar";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulSekretar_default",
                "ModulSekretar/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}