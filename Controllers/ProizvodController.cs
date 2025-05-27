using ComputerShop.Data;
using ComputerShop.Models;
using ComputerShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;

namespace ComputerShop.Controllers
{
    [Authorize] // Sve osim što ima AllowAnonymous, zahteva prijavu
    public class ProizvodController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IExternalProductService _externalProductService;

        public ProizvodController(ApplicationDbContext context, IExternalProductService externalProductService)
        {
            _context = context;
            _externalProductService = externalProductService;
        }

        // GET: Proizvod
        [AllowAnonymous] // Dostupno svima, i gostima
        public IActionResult Index(string? searchString, int? page)
        {
            var proizvodi = _context.Proizvod.Include(p => p.Kategorija).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                proizvodi = proizvodi.Where(p => p.Naziv.Contains(searchString));
            }

            int pageSize = 5;
            int pageNumber = page ?? 1;

            var pagedList = proizvodi.OrderBy(p => p.Naziv).ToPagedList(pageNumber, pageSize);

            ViewBag.SearchString = searchString;

            return View(pagedList);
        }

        // GET: Proizvod/Details/5
        [AllowAnonymous] // Dostupno svima, i gostima
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var proizvod = await _context.Proizvod
                .Include(p => p.Kategorija)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (proizvod == null)
                return NotFound();

            return View(proizvod);
        }

        // GET: Proizvod/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["KategorijaId"] = new SelectList(_context.Kategorija, "Id", "Naziv");
            return View();
        }

        // POST: Proizvod/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Opis,Cena,Kolicina,SlikaPath,KategorijaId")] Proizvod proizvod)
        {
            System.Diagnostics.Debug.WriteLine("Create POST called");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(proizvod);
                    await _context.SaveChangesAsync();
                    System.Diagnostics.Debug.WriteLine("SaveChangesAsync succeeded");

                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception on SaveChangesAsync: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "Unexpected error saving data.");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ModelState is invalid:");
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($" - {state.Key}: {error.ErrorMessage}");
                    }
                }
            }

            ViewData["KategorijaId"] = new SelectList(_context.Kategorija, "Id", "Naziv", proizvod.KategorijaId);
            return View(proizvod);
        }

        // GET: Proizvod/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod == null)
                return NotFound();

            ViewData["KategorijaId"] = new SelectList(_context.Kategorija, "Id", "Naziv", proizvod.KategorijaId);
            return View(proizvod);
        }

        // POST: Proizvod/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Opis,Cena,Kolicina,SlikaPath,KategorijaId")] Proizvod proizvod)
        {
            if (id != proizvod.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proizvod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProizvodExists(proizvod.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategorijaId"] = new SelectList(_context.Kategorija, "Id", "Naziv", proizvod.KategorijaId);
            return View(proizvod);
        }

        // GET: Proizvod/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var proizvod = await _context.Proizvod
                .Include(p => p.Kategorija)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (proizvod == null)
                return NotFound();

            return View(proizvod);
        }

        // POST: Proizvod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod != null)
                _context.Proizvod.Remove(proizvod);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProizvodExists(int id)
        {
            return _context.Proizvod.Any(e => e.Id == id);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalIndex()
        {
            var eksterniProizvodi = await _externalProductService.GetExternalProductsAsync();
            return View(eksterniProizvodi);
        }

    }
}
