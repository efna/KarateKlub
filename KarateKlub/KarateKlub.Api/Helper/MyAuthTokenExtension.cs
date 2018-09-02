using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace KarateKlub.Api.Helper
{
    public static class MyAuthTokenExtension
    {

        public static KorisnickiNalozi GetKorisnikOfAuthToken(this HttpContext httpContext)
        {
            MyContext db = httpContext.RequestServices.GetService<MyContext>();

            string token = httpContext.GetMyAuthToken();
            KorisnickiNalozi korisnickiNalog = db.AutorizacijskiTokens.Where(x => token != null && x.Vrijednost == token).Select(s => s.KorisnickiNalog).SingleOrDefault();
            return korisnickiNalog;
        }

        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["MyAuthToken"];
            return token;
        }
    }
}
