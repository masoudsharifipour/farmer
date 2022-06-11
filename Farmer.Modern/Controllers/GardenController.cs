using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Farmer.Modern.Models;
using Farmer.Modern.Models.DbContext;
using Microsoft.AspNetCore.Authorization;

namespace Farmer.Modern.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class GardenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GardenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Garden
        public async Task<IActionResult> Index()
        {
            return View(await _context.Garden.ToListAsync());
        }

        // GET: Garden/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garden = await _context.Garden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garden == null)
            {
                return NotFound();
            }

            return View(garden);
        }

        // GET: Garden/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Garden/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,Size,Cultivation,Gps,CreatorUserId,CreationDateTime,Description,IsActive")] Garden garden)
        {
            if (ModelState.IsValid)
            {
                garden.CreationDateTime= DateTime.Now;
                garden.CreatorUserId = 0;
                _context.Add(garden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(garden);
        }

        // GET: Garden/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garden = await _context.Garden.FindAsync(id);
            if (garden == null)
            {
                return NotFound();
            }
            return View(garden);
        }

        // POST: Garden/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Code,Size,Cultivation,Gps,CreatorUserId,CreationDateTime,Description,IsActive")] Garden garden)
        {
            if (id != garden.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(garden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GardenExists(garden.Id))
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
            return View(garden);
        }

        // GET: Garden/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garden = await _context.Garden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garden == null)
            {
                return NotFound();
            }

            return View(garden);
        }

        // POST: Garden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var garden = await _context.Garden.FindAsync(id);
            _context.Garden.Remove(garden);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GardenExists(long id)
        {
            return _context.Garden.Any(e => e.Id == id);
        }
    }
}
