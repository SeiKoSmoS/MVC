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
    public class FinishedProductsController : Controller
    {
        private readonly EnterpriseContext _context;

        public FinishedProductsController(EnterpriseContext context)
        {
            _context = context;
        }

        // GET: FinishedProducts
        public async Task<IActionResult> Index()
        {
            var enterpriseContext = _context.FinishedProducts.Include(f => f.UnitNavigation);
            return View(await enterpriseContext.ToListAsync());
        }

        // GET: FinishedProducts/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finishedProducts = await _context.FinishedProducts
                .Include(f => f.UnitNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (finishedProducts == null)
            {
                return NotFound();
            }

            return View(finishedProducts);
        }

        // GET: FinishedProducts/Create
        public IActionResult Create()
        {
            ViewData["Unit"] = new SelectList(_context.Units, "Id", "Name");
            return View();
        }

        // POST: FinishedProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Unit,Quantity,Amount")] FinishedProducts finishedProducts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(finishedProducts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Unit"] = new SelectList(_context.Units, "Id", "Name", finishedProducts.Unit);
            return View(finishedProducts);
        }

        // GET: FinishedProducts/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finishedProducts = await _context.FinishedProducts.FindAsync(id);
            if (finishedProducts == null)
            {
                return NotFound();
            }
            ViewData["Unit"] = new SelectList(_context.Units, "Id", "Name", finishedProducts.Unit);
            return View(finishedProducts);
        }

        // POST: FinishedProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Name,Unit,Quantity,Amount")] FinishedProducts finishedProducts)
        {
            if (id != finishedProducts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(finishedProducts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinishedProductsExists(finishedProducts.Id))
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
            ViewData["Unit"] = new SelectList(_context.Units, "Id", "Name", finishedProducts.Unit);
            return View(finishedProducts);
        }

        // GET: FinishedProducts/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finishedProducts = await _context.FinishedProducts
                .Include(f => f.UnitNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (finishedProducts == null)
            {
                return NotFound();
            }

            return View(finishedProducts);
        }

        // POST: FinishedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var finishedProducts = await _context.FinishedProducts.FindAsync(id);
            _context.FinishedProducts.Remove(finishedProducts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinishedProductsExists(byte id)
        {
            return _context.FinishedProducts.Any(e => e.Id == id);
        }
    }
}
