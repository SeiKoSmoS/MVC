using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAppEnterprise;

namespace WebAppEnterprise.Controllers
{
    public class ProductionsController : Controller
    {
        private readonly EnterpriseContext _context;

        public ProductionsController(EnterpriseContext context)
        {
            _context = context;
        }

        // GET: Productions
        public async Task<IActionResult> Index()
        {
            var enterpriseContext = _context.Production.Include(p => p.EmployeeNavigation).Include(p => p.ProductNavigation);
            return View(await enterpriseContext.ToListAsync());
        }

        // GET: Productions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Production
                .Include(p => p.EmployeeNavigation)
                .Include(p => p.ProductNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // GET: Productions/Create
        public IActionResult Create()
        {
            ViewData["Employee"] = new SelectList(_context.Stuff, "Id", "Name");
            ViewData["Product"] = new SelectList(_context.FinishedProducts, "Id", "Name");
            return View();
        }

        // POST: Productions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Product,Quantity,Employee,Date")] Production production)
        {
            if (ModelState.IsValid)
            {
                if (production.getPermission()) {
                    _context.Add(production);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {                   
                    return RedirectToAction(nameof(Error));
                }
            }
            ViewData["Employee"] = new SelectList(_context.Stuff, "Id", "Name", production.Employee);
            ViewData["Product"] = new SelectList(_context.FinishedProducts, "Id", "Name", production.Product);
            return View(production);
        }

        public IActionResult Error()
        {
            ViewData["Message"] = "Material is not enough";
            return View();
        }
        // GET: Productions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Production.FindAsync(id);
            if (production == null)
            {
                return NotFound();
            }
            ViewData["Employee"] = new SelectList(_context.Stuff, "Id", "Name", production.Employee);
            ViewData["Product"] = new SelectList(_context.FinishedProducts, "Id", "Name", production.Product);
            return View(production);
        }

        // POST: Productions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Product,Quantity,Employee,Date")] Production production)
        {
            if (id != production.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(production);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionExists(production.Id))
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
            ViewData["Employee"] = new SelectList(_context.Stuff, "Id", "Name", production.Employee);
            ViewData["Product"] = new SelectList(_context.FinishedProducts, "Id", "Name", production.Product);
            return View(production);
        }

        // GET: Productions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Production
                .Include(p => p.EmployeeNavigation)
                .Include(p => p.ProductNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var production = await _context.Production.FindAsync(id);
            _context.Production.Remove(production);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionExists(int id)
        {
            return _context.Production.Any(e => e.Id == id);
        }
    }
}
