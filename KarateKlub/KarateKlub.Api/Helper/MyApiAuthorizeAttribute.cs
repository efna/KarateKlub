using System.Threading.Tasks;
using KarateKlub.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
namespace KarateKlub.Api.Helper
{
    public class MyApiAuthorizeAttribute : TypeFilterAttribute
    {
        public MyApiAuthorizeAttribute()
            : base(typeof(MyApiAuthorizeImpl))
        {
            Arguments = new object[] { };
        }
    }


    public class MyApiAuthorizeImpl : IAsyncActionFilter
    {
        public MyApiAuthorizeImpl()
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            KorisnickiNalozi korisnickiNalog = context.HttpContext.GetKorisnikOfAuthToken();

            if (korisnickiNalog != null)
            {
                await next(); 
                return;
            }
            
            context.Result = new UnauthorizedResult();
        }
    }
}
