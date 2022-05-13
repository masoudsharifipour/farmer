using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmer.Modern.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Farmer.Modern.Models;
using Farmer.Modern.Models.DbContext;

namespace Farmer.Modern.Controllers
{
    public class WorkController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Work
        public async Task<IActionResult> Index()
        {
            return View(await _context.Work.ToListAsync());
        }

        // GET: Work/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var work = await _context.Work
                .FirstOrDefaultAsync(m => m.Id == id);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        // GET: Work/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Work/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkDto work)
        {
            if (ModelState.IsValid)
            {
                Work w = new Work();
                _context.Add(work);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(work);
        }

        // GET: Work/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var work = await _context.Work.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }
            return View(work);
        }

        // POST: Work/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ActionDatetime,Size,Type,Description,CreatorUserId,Agent,EndActionDateTime,Status")] Work work)
        {
            if (id != work.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(work);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkExists(work.Id))
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
            return View(work);
        }

        // GET: Work/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var work = await _context.Work
                .FirstOrDefaultAsync(m => m.Id == id);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        // POST: Work/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var work = await _context.Work.FindAsync(id);
            _context.Work.Remove(work);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkExists(long id)
        {
            return _context.Work.Any(e => e.Id == id);
        }
    }
}
