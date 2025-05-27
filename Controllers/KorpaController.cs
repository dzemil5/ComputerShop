using ComputerShop.Data;
using ComputerShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ComputerShop.Controllers
{
    [Authorize(Roles = "User,Admin")] 
    public class KorpaController : Controller
    {
        private const string KorpaSessionKey = "Korpa";
        private readonly ApplicationDbContext _context;

        public KorpaController(ApplicationDbContext context)
        {
            _context = context;
        }

        private Korpa GetKorpa()
        {
            var korpaJson = HttpContext.Session.GetString(KorpaSessionKey);
            if (korpaJson == null)
                return new Korpa();
            return JsonSerializer.Deserialize<Korpa>(korpaJson) ?? new Korpa();
        }

        private void SaveKorpa(Korpa korpa)
        {
            var korpaJson = JsonSerializer.Serialize(korpa);
            HttpContext.Session.SetString(KorpaSessionKey, korpaJson);
        }

        public IActionResult Index()
        {
            var korpa = GetKorpa();
            return View(korpa);
        }

        [HttpPost]
        public async Task<IActionResult> Dodaj(int proizvodId, int kolicina = 1)
        {
            if (kolicina < 1)
            {
                return BadRequest("Količina mora biti bar 1.");
            }

            var proizvod = await _context.Proizvod.FindAsync(proizvodId);
            if (proizvod == null)
            {
                return NotFound();
            }

            var korpa = GetKorpa();
            korpa.DodajProizvod(proizvod, kolicina);
            SaveKorpa(korpa);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Ukloni(int proizvodId)
        {
            var korpa = GetKorpa();
            korpa.UkloniProizvod(proizvodId);
            SaveKorpa(korpa);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Ocisti()
        {
            var korpa = GetKorpa();
            korpa.Ocisti();
            SaveKorpa(korpa);

            return RedirectToAction("Index");
        }
    }
}
