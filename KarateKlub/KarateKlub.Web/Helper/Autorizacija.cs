using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateKlub.Web.Helper
{
    public class Autorizacija : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool IsAdministrator;
        private readonly bool IsSekretar;
        private readonly bool IsClanKluba;
        private readonly bool IsTrener;
        private readonly bool IsBlagajnik;





        public Autorizacija(bool isAdministrator,bool isSekretar,bool isClanKluba,bool isTrener,bool isBlagajnik)
        {
            IsAdministrator = isAdministrator;
            IsSekretar = isSekretar;
            IsClanKluba = isClanKluba;
            IsTrener = isTrener;
            IsBlagajnik = isBlagajnik;
        }




        public void OnAuthorization(AuthorizationContext filter)
        {
            KorisnickiNalozi PrijavljeniKorisnik = Autentifikacija.GetLogiraniKorisnik(filter.HttpContext);

            bool pristupOdobren = false;

            if (PrijavljeniKorisnik != null)
            {
                if (PrijavljeniKorisnik.Osoba.isAdministrator == true)
                    pristupOdobren = true;

                else if (PrijavljeniKorisnik.Osoba.isSekretar == true)
                    pristupOdobren = true;
                else if (PrijavljeniKorisnik.Osoba.isClanKluba == true)
                    pristupOdobren = true;
                else if (PrijavljeniKorisnik.Osoba.isTrener == true)
                    pristupOdobren = true;
                else if (PrijavljeniKorisnik.Osoba.isBlagajnik == true)
                    pristupOdobren = true;
            }

            if (!pristupOdobren)
                filter.HttpContext.Response.Redirect("/Home/Index");


        }
    }
}