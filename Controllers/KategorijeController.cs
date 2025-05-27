using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerShop.Data;
using ComputerShop.Models;

namespace ComputerShop.Controllers
{
    public class KategorijeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KategorijeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kategorijas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kategorija.ToListAsync());
        }

        // GET: Kategorijas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var kategorija = await _context.Kategorija.FirstOrDefaultAsync(m => m.Id == id);
            if (kategorija == null) return NotFound();

            return View(kategorija);
        }

        // GET: Kategorijas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategorijas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv")] Kategorija kategorija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategorija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategorija);
        }

        // GET: Kategorijas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var kategorija = await _context.Kategorija.FindAsync(id);
            if (kategorija == null) return NotFound();

            return View(kategorija);
        }

        // POST: Kategorijas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv")] Kategorija kategorija)
        {
            if (id != kategorija.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategorija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategorijaExists(kategorija.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kategorija);
        }

        // GET: Kategorijas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var kategorija = await _context.Kategorija.FirstOrDefaultAsync(m => m.Id == id);
            if (kategorija == null) return NotFound();

            return View(kategorija);
        }

        // POST: Kategorijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategorija = await _context.Kategorija.FindAsync(id);
            if (kategorija != null)
            {
                _context.Kategorija.Remove(kategorija);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool KategorijaExists(int id)
        {
            return _context.Kategorija.Any(e => e.Id == id);
        }
    }
}
