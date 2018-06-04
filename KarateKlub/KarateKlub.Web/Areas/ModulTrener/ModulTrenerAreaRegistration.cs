using System.Web.Mvc;

namespace KarateKlub.Web.Areas.ModulTrener
{
    public class ModulTrenerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulTrener";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulTrener_default",
                "ModulTrener/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}