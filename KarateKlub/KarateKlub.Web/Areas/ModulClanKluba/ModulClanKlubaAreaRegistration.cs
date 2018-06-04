using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulClanKluba
{
    public class ModulClanKlubaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulClanKluba";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulClanKluba_default",
                "ModulClanKluba/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}