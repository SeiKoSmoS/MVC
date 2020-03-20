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
    public class PurchaseOfMaterialsController : Controller
    {
        private readonly EnterpriseContext _context;

        public PurchaseOfMaterialsController(EnterpriseContext context)
        {
            _context = context;
        }

        // GET: PurchaseOfMaterials
        public async Task<IActionResult> Index()
        {
            var enterpriseContext = _context.PurchaseOfMaterials.Include(p => p.MaterialNavigation);
            return View(await enterpriseContext.ToListAsync());
        }

        // GET: PurchaseOfMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOfMaterials = await _context.PurchaseOfMaterials
                .Include(p => p.MaterialNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseOfMaterials == null)
            {
                return NotFound();
            }

            return View(purchaseOfMaterials);
        }

        // GET: PurchaseOfMaterials/Create
        public IActionResult Create()
        {
            ViewData["Material"] = new SelectList(_context.RawMaterials, "Id", "Name");
            return View();
        }

        // POST: PurchaseOfMaterials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Material,Quantity,Amount,Date")] PurchaseOfMaterials purchaseOfMaterials)
        {
            if (ModelState.IsValid)
            {
                if (purchaseOfMaterials.getPermission())
                {
                    _context.Add(purchaseOfMaterials);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Error));
                }
            }
            ViewData["Material"] = new SelectList(_context.RawMaterials, "Id", "Name", purchaseOfMaterials.Material);
            return View(purchaseOfMaterials);
        }

        public IActionResult Error()
        {
            ViewData["Message"] = "Amount is more then budget";
            return View();
        }

        // GET: PurchaseOfMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOfMaterials = await _context.PurchaseOfMaterials.FindAsync(id);
            if (purchaseOfMaterials == null)
            {
                return NotFound();
            }
            ViewData["Material"] = new SelectList(_context.RawMaterials, "Id", "Name", purchaseOfMaterials.Material);
            return View(purchaseOfMaterials);
        }

        // POST: PurchaseOfMaterials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Material,Quantity,Amount,Date")] PurchaseOfMaterials purchaseOfMaterials)
        {
            if (id != purchaseOfMaterials.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOfMaterials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOfMaterialsExists(purchaseOfMaterials.Id))
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
            ViewData["Material"] = new SelectList(_context.RawMaterials, "Id", "Name", purchaseOfMaterials.Material);
            return View(purchaseOfMaterials);
        }

        // GET: PurchaseOfMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOfMaterials = await _context.PurchaseOfMaterials
                .Include(p => p.MaterialNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseOfMaterials == null)
            {
                return NotFound();
            }

            return View(purchaseOfMaterials);
        }

        // POST: PurchaseOfMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseOfMaterials = await _context.PurchaseOfMaterials.FindAsync(id);
            _context.PurchaseOfMaterials.Remove(purchaseOfMaterials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOfMaterialsExists(int id)
        {
            return _context.PurchaseOfMaterials.Any(e => e.Id == id);
        }
    }
}
