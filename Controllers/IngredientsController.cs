using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppEnterprise;

namespace WebAppEnterprise.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly EnterpriseContext _context;

        public IngredientsController(EnterpriseContext context)
        {
            _context = context;
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
            ViewData["Product"] = new SelectList(_context.FinishedProducts.Where(p => _context.Ingredients.Any(s => s.Product == p.Id)), "Id", "Name");
            var enterpriseContext = _context.FinishedProducts.Where(p => _context.Ingredients.Any(s => s.Product == p.Id));
            return View(await enterpriseContext.ToListAsync());
        }

        public async Task<IActionResult> List(short? id)
        {
            var enterpriseContext = _context.Ingredients.Include(i => i.MaterialNavigation).Include(i => i.ProductNavigation).Where(p => p.Product == id);
            return View(await enterpriseContext.ToListAsync());
        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients
                .Include(i => i.MaterialNavigation)
                .Include(i => i.ProductNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredients == null)
            {
                return NotFound();
            }

            return View(ingredients);
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            ViewData["Material"] = new SelectList(_context.RawMaterials, "Id", "Name");
            ViewData["Product"] = new SelectList(_context.FinishedProducts, "Id", "Name");
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Product,Material,Quantity")] Ingredients ingredients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Material"] = new SelectList(_context.RawMaterials, "Id", "Name", ingredients.Material);
            ViewData["Product"] = new SelectList(_context.FinishedProducts, "Id", "Name", ingredients.Product);
            return View(ingredients);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients.FindAsync(id);
            if (ingredients == null)
            {
                return NotFound();
            }
            ViewData["Material"] = new SelectList(_context.RawMaterials, "Id", "Name", ingredients.Material);
            ViewData["Product"] = new SelectList(_context.FinishedProducts, "Id", "Name", ingredients.Product);
            return View(ingredients);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Product,Material,Quantity")] Ingredients ingredients)
        {
            if (id != ingredients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientsExists(ingredients.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Material"] = new SelectList(_context.RawMaterials, "Id", "Name", ingredients.Material);
            ViewData["Product"] = new SelectList(_context.FinishedProducts, "Id", "Name", ingredients.Product);
            return View(ingredients);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients
                .Include(i => i.MaterialNavigation)
                .Include(i => i.ProductNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredients == null)
            {
                return NotFound();
            }

            return View(ingredients);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var ingredients = await _context.Ingredients.FindAsync(id);
            _context.Ingredients.Remove(ingredients);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientsExists(short id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}
