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
    public class RawMaterialsController : Controller
    {
        private readonly EnterpriseContext _context;

        public RawMaterialsController(EnterpriseContext context)
        {
            _context = context;
        }

        // GET: RawMaterials
        public async Task<IActionResult> Index()
        {
            var enterpriseContext = _context.RawMaterials.Include(r => r.UnitNavigation);
            return View(await enterpriseContext.ToListAsync());
        }

        // GET: RawMaterials/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterials = await _context.RawMaterials
                .Include(r => r.UnitNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rawMaterials == null)
            {
                return NotFound();
            }

            return View(rawMaterials);
        }

        // GET: RawMaterials/Create
        public IActionResult Create()
        {
            ViewData["Unit"] = new SelectList(_context.Units, "Id", "Name");
            return View();
        }

        // POST: RawMaterials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Unit,Quantity,Amount")] RawMaterials rawMaterials)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rawMaterials);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Unit"] = new SelectList(_context.Units, "Id", "Name", rawMaterials.Unit);
            return View(rawMaterials);
        }

        // GET: RawMaterials/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterials = await _context.RawMaterials.FindAsync(id);
            if (rawMaterials == null)
            {
                return NotFound();
            }
            ViewData["Unit"] = new SelectList(_context.Units, "Id", "Name", rawMaterials.Unit);
            return View(rawMaterials);
        }

        // POST: RawMaterials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Name,Unit,Quantity,Amount")] RawMaterials rawMaterials)
        {
            if (id != rawMaterials.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rawMaterials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RawMaterialsExists(rawMaterials.Id))
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
            ViewData["Unit"] = new SelectList(_context.Units, "Id", "Name", rawMaterials.Unit);
            return View(rawMaterials);
        }

        // GET: RawMaterials/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterials = await _context.RawMaterials
                .Include(r => r.UnitNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rawMaterials == null)
            {
                return NotFound();
            }

            return View(rawMaterials);
        }

        // POST: RawMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var rawMaterials = await _context.RawMaterials.FindAsync(id);
            _context.RawMaterials.Remove(rawMaterials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RawMaterialsExists(byte id)
        {
            return _context.RawMaterials.Any(e => e.Id == id);
        }
    }
}
