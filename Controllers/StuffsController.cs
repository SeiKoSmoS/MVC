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
    public class StuffsController : Controller
    {
        private readonly EnterpriseContext _context;

        public StuffsController(EnterpriseContext context)
        {
            _context = context;
        }

        // GET: Stuffs
        public async Task<IActionResult> Index()
        {
            var enterpriseContext = _context.Stuff.Include(s => s.PositionNavigation);
            return View(await enterpriseContext.ToListAsync());
        }

        // GET: Stuffs/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stuff = await _context.Stuff
                .Include(s => s.PositionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stuff == null)
            {
                return NotFound();
            }

            return View(stuff);
        }

        // GET: Stuffs/Create
        public IActionResult Create()
        {
            ViewData["Position"] = new SelectList(_context.Positions, "Id", "Name");
            return View();
        }

        // POST: Stuffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Position,Salary,Address,PhoneNumber")] Stuff stuff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stuff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Position"] = new SelectList(_context.Positions, "Id", "Name", stuff.Position);
            return View(stuff);
        }

        // GET: Stuffs/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stuff = await _context.Stuff.FindAsync(id);
            if (stuff == null)
            {
                return NotFound();
            }
            ViewData["Position"] = new SelectList(_context.Positions, "Id", "Name", stuff.Position);
            return View(stuff);
        }

        // POST: Stuffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name,Position,Salary,Address,PhoneNumber")] Stuff stuff)
        {
            if (id != stuff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stuff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StuffExists(stuff.Id))
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
            ViewData["Position"] = new SelectList(_context.Positions, "Id", "Name", stuff.Position);
            return View(stuff);
        }

        // GET: Stuffs/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stuff = await _context.Stuff
                .Include(s => s.PositionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stuff == null)
            {
                return NotFound();
            }

            return View(stuff);
        }

        // POST: Stuffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var stuff = await _context.Stuff.FindAsync(id);
            _context.Stuff.Remove(stuff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StuffExists(short id)
        {
            return _context.Stuff.Any(e => e.Id == id);
        }
    }
}
