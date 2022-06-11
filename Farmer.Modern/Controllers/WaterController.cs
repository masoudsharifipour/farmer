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
    [Authorize("Admin,SuperAdmin")]
    public class WaterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WaterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Water
        public async Task<IActionResult> Index()
        {
            return View(await _context.WaterMotor.ToListAsync());
        }

        // GET: Water/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterMotor = await _context.WaterMotor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waterMotor == null)
            {
                return NotFound();
            }

            return View(waterMotor);
        }

        // GET: Water/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Water/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] WaterMotor waterMotor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(waterMotor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(waterMotor);
        }

        // GET: Water/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterMotor = await _context.WaterMotor.FindAsync(id);
            if (waterMotor == null)
            {
                return NotFound();
            }
            return View(waterMotor);
        }

        // POST: Water/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Description")] WaterMotor waterMotor)
        {
            if (id != waterMotor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(waterMotor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaterMotorExists(waterMotor.Id))
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
            return View(waterMotor);
        }

        // GET: Water/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterMotor = await _context.WaterMotor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waterMotor == null)
            {
                return NotFound();
            }

            return View(waterMotor);
        }

        // POST: Water/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var waterMotor = await _context.WaterMotor.FindAsync(id);
            _context.WaterMotor.Remove(waterMotor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WaterMotorExists(long id)
        {
            return _context.WaterMotor.Any(e => e.Id == id);
        }
    }
}
