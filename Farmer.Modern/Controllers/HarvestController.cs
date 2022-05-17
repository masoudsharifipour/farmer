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
    public class HarvestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HarvestController(ApplicationDbContext context)
        {
            _context = context;
        }
        public void BindingDate()
        {
            var gardens = _context.Garden.ToList();
            var product = _context.Product.ToList();
            ViewBag.Garden = new SelectList(gardens, "Id", "Name");
            ViewBag.Product = new SelectList(product, "Id", "Name");
        }
        
        public void BindingDateEdit(long gardenId , long productId)
        {
            var gardens = _context.Garden.ToList();
            var product = _context.Product.ToList();
            ViewBag.Garden = new SelectList(gardens, "Id", "Name",gardenId);
            ViewBag.Product = new SelectList(product, "Id", "Name",productId);
        }
        // GET: Harvest
        public async Task<IActionResult> Index()
        {
            return View(await _context.Harvest.Include(x=>x.Garden).Include(x=>x.Product).ToListAsync());
        }

        // GET: Harvest/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var harvest = await _context.Harvest.Include(x=>x.Garden).Include(x=>x.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (harvest == null)
            {
                return NotFound();
            }

            return View(harvest);
        }

        // GET: Harvest/Create
        public IActionResult Create()
        {
            BindingDate();
            return View();
        }

        // POST: Harvest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HarvestDto harvest)
        {
            if (!ModelState.IsValid) return View(harvest);
            var ha = new Harvest
            {
                Description = harvest.Description,
                Size = harvest.Size,
                GardenId = harvest.Garden,
                HarvestDate = harvest.HarvestDate,
                ProductId = harvest.Product
            };

            _context.Add(ha);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Harvest/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var harvest = await _context.Harvest.FindAsync(id);
            HarvestDto hr = new HarvestDto();

            if (harvest != null)
            {
                BindingDateEdit(harvest.GardenId, harvest.ProductId);
                hr.Description = harvest.Description;
                hr.Size = harvest.Size;
                hr.HarvestDate = harvest.HarvestDate;

                if (harvest == null)
                {
                    return NotFound();
                }

            }
            return View(hr);

        }

        // POST: Harvest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, HarvestDto harvest)
        {
            if (id != harvest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ha = new Harvest
                    {
                        Description = harvest.Description,
                        Size = harvest.Size,
                        GardenId = harvest.Garden,
                        HarvestDate = harvest.HarvestDate,
                        ProductId = harvest.Product,
                        Id = id
                        
                    };
                    _context.Update(ha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HarvestExists(harvest.Id))
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
            return View(harvest);
        }

        // GET: Harvest/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var harvest = await _context.Harvest.Include(x=>x.Garden).Include(x=>x.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (harvest == null)
            {
                return NotFound();
            }

            return View(harvest);
        }

        // POST: Harvest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var harvest = await _context.Harvest.FindAsync(id);
            _context.Harvest.Remove(harvest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HarvestExists(long id)
        {
            return _context.Harvest.Any(e => e.Id == id);
        }
    }
}
