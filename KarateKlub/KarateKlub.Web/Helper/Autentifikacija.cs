using KarateKlub.Data;
using KarateKlub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarateKlub.Web.Helper
{
    public class Autentifikacija
    {

        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void PokreniNovuSesiju(KorisnickiNalozi korisnickiNalog, HttpContextBase context)
        {
            context.Session.Add(LogiraniKorisnik, korisnickiNalog);

        }

        public static KorisnickiNalozi GetLogiraniKorisnik(HttpContextBase context)
        {
            KorisnickiNalozi korisnickiNalog = (KorisnickiNalozi)context.Session[LogiraniKorisnik];

            if (korisnickiNalog != null)
                return korisnickiNalog;

            HttpCookie cookie = context.Request.Cookies.Get("_mvc_session");

            if (cookie == null)
                return null;

            long userId;
            try
            {
                userId = Int64.Parse(cookie.Value);
            }
            catch
            {
                return null;
            }


            MojContext db = new MojContext();

            KorisnickiNalozi k = db.KorisnickiNalozi
                 .SingleOrDefault(x => x.Id == userId);

            PokreniNovuSesiju(k, context);
            return k;

        }


        public static bool isTakmicar(HttpContextBase context)
        {

        MojContext ctx = new MojContext();
            KorisnickiNalozi korisnickiNalog = (KorisnickiNalozi)context.Session[LogiraniKorisnik];

            List<Takmicari> takmicari = ctx.Takmicari.Where(x => x.isDeleted == false).ToList();
            List<int> takmicariOsobaId = new List<int>();
            for (int i = 0; i < takmicari.Count(); i++)
            {
                takmicariOsobaId.Add(takmicari[i].ClanKluba.OsobaId);
            }
            if (!takmicariOsobaId.Contains(korisnickiNalog.OsobaId))
                return false;
            else
            return true;
          }
    }
}