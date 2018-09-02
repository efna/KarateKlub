using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KarateKlub.Api.Helper;
using KarateKlub.Api.Models;
using KarateKlub.Data;
using KarateKlub.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace KarateKlub.Api.Controllers
{
    public class AutentifikacijaController : MyWebApiBaseController
    {
        public AutentifikacijaController(MyContext db) : base(db)
        {
        }

        [HttpPost]
        public ActionResult<AutentifikacijaResultVM> LoginCheck([FromBody] AutentifikacijaLoginPostVM input)
        {
           
            string token = Guid.NewGuid().ToString();

            AutentifikacijaResultVM model = _db.KorisnickiNalozis
                .Where(w => w.KorisnickoIme == input.Username && w.Lozinka == input.Password && w.isDeleted == false && w.isAktivanNalog == true)
                .Select(s => new AutentifikacijaResultVM
                {
                    osobaId = s.OsobaId,
                    ime = _db.Osobas.Where(x => x.Id == s.OsobaId).First().Ime,
                    korisnickiNalogId = s.Id,
                    prezime = _db.Osobas.Where(x => x.Id == s.OsobaId).First().Prezime,
                    username = s.KorisnickoIme,
                    token = token,
                    isAdministrator = _db.Osobas.Where(x => x.Id == s.OsobaId).First().isAdministrator,
                    isBlagajnik= _db.Osobas.Where(x => x.Id == s.OsobaId).First().isBlagajnik,
                    isClanKluba = _db.Osobas.Where(x => x.Id == s.OsobaId).First().isClanKluba,
                    isSekretar = _db.Osobas.Where(x => x.Id == s.OsobaId).First().isSekretar,
                    isTrener = _db.Osobas.Where(x => x.Id == s.OsobaId).First().isTrener,
                    isKnjigovodja = _db.Osobas.Where(x => x.Id == s.OsobaId).First().isKnjigovodja,
                    isClanUpravnogOdbora = _db.Osobas.Where(x => x.Id == s.OsobaId).First().isClanUpravnogOdbora,
                    isAktivanNalog=s.isAktivanNalog,

                    isAktivnaOsoba = _db.Osobas.Where(x => x.Id == s.OsobaId).First().isAktivnaOsoba,


                }).SingleOrDefault();

            
            if (model != null  && (model.isClanKluba == true || model.isTrener == true || model.isBlagajnik == true))
            {
                    _db.AutorizacijskiTokens.Add(new AutorizacijskiToken
                    {
                        Vrijednost = model.token,
                        KorisnickiNalogId = model.korisnickiNalogId.Value,
                        VrijemeEvidentiranja = DateTime.Now,
                        deviceInfo = "Mobile app - " + input.deviceInfo,
                        IpAdresa = HttpContext.Connection.RemoteIpAddress + ":" + HttpContext.Connection.RemotePort
                    });
                    _db.SaveChanges();
                int tokenId = _db.AutorizacijskiTokens.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                model.tokenId = tokenId;


            }

            return model;
        }

        [HttpDelete("{tokenId}")]
        public ActionResult Logout(int tokenId)
        {

            AutorizacijskiToken autorizacijskiToken = _db.AutorizacijskiTokens.Find(tokenId);
            if (autorizacijskiToken != null)
            {
                _db.Remove(autorizacijskiToken);
                _db.SaveChanges();
            }
            return Ok();
        }
    }
}