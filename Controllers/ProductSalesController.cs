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
    public class ProductSalesController : Controller
    {
        private readonly EnterpriseContext _context;

        public ProductSalesController(EnterpriseContext context)
        {
            _context = context;
        }

        // GET: ProductSales
        public async Task<IActionResult> Index()
        {
            var enterpriseContext = _context.ProductSales.Include(p => p.EmployeeNavigation).Include(p => p.ProductNavigation);
            return View(await enterpriseContext.ToListAsync());
        }

        // GET: ProductSales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSales = await _context.ProductSales
                .Include(p => p.EmployeeNavigation)
                .Include(p => p.ProductNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSales == null)
            {
                return NotFound();
            }

            return View(productSales);
        }

        // GET: ProductSales/Create
        public IActionResult Create()
        {
            ViewData["Employee"] = new SelectList(_context.Stuff, "Id", "Name");
            ViewData["Product"] = new SelectList(_context.FinishedProducts, "Id", "Name");
            return View();
        }

        // POST: ProductSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Product,Quantity,Amount,Employee,Date")] ProductSales productSales)
        {
            if (ModelState.IsValid)
            {
                if (productSales.getPermission())
                {
                    _context.Add(productSales);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Error));
                }
            }
            ViewData["Employee"] = new SelectList(_context.Stuff, "Id", "Name", productSales.Employee);
            ViewData["Product"] = new SelectList(_context.FinishedProducts, "Id", "Name", productSales.Product);
            return View(productSales);
        }

        public IActionResult Error()
        {
            ViewData["Message"] = "Product is not enough";
            return View();
        }

        // GET: ProductSales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSales = await _context.ProductSales.FindAsync(id);
            if (productSales == null)
            {
                return NotFound();
            }
            ViewData["Employee"] = new SelectList(_context.Stuff, "Id", "Name", productSales.Employee);
            ViewData["Product"] = new SelectList(_context.FinishedProducts, "Id", "Name", productSales.Product);
            return View(productSales);
        }

        // POST: ProductSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Product,Quantity,Amount,Employee,Date")] ProductSales productSales)
        {
            if (id != productSales.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productSales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSalesExists(productSales.Id))
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
            ViewData["Employee"] = new SelectList(_context.Stuff, "Id", "Name", productSales.Employee);
            ViewData["Product"] = new SelectList(_context.FinishedProducts, "Id", "Name", productSales.Product);
            return View(productSales);
        }

        // GET: ProductSales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSales = await _context.ProductSales
                .Include(p => p.EmployeeNavigation)
                .Include(p => p.ProductNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSales == null)
            {
                return NotFound();
            }

            return View(productSales);
        }

        // POST: ProductSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productSales = await _context.ProductSales.FindAsync(id);
            _context.ProductSales.Remove(productSales);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSalesExists(int id)
        {
            return _context.ProductSales.Any(e => e.Id == id);
        }
    }
}
