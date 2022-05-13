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

        public async Task DropDownBinding()
        {
            var gardens = await _context.Garden.ToListAsync();
            var products = await _context.Product.ToListAsync();
            var categories = await _context.Category.ToListAsync();
            var motors = await _context.WaterMotor.ToListAsync();
            var agents = await _context.Users.ToListAsync();

            ViewBag.GardenId = new SelectList(gardens, "Id", "Name");
            ViewBag.ProductId = new SelectList(products, "Id", "Name");
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            ViewBag.WaterMotorId = new SelectList(motors, "Id", "Name");
            ViewBag.Agent = new SelectList(agents, "Id", "Name");
        }

        public async Task DropDownBindingEdit(long gardenId, long? productId, long categoryId, long? motorId,
            Guid? agentId)
        {
            var gardens = await _context.Garden.ToListAsync();
            var products = await _context.Product.ToListAsync();
            var categories = await _context.Category.ToListAsync();
            var motors = await _context.WaterMotor.ToListAsync();
            var agents = await _context.Users.ToListAsync();

            ViewBag.GardenId = new SelectList(gardens, "Id", "Name", gardenId);
            ViewBag.ProductId = new SelectList(products, "Id", "Name", productId);
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", categoryId);
            ViewBag.WaterMotorId = new SelectList(motors, "Id", "Name", motorId);
            ViewBag.Agent = new SelectList(agents, "Id", "Name", agentId);
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
        public async Task<IActionResult> Create()
        {
            await this.DropDownBinding();
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
                var w = new Work
                {
                    Agent = work.Agent,
                    Description = work.Description,
                    Size = work.Size,
                    CategoryId = work.CategoryId,
                    GardenId = work.GardenId,
                    Status = work.Status,
                    Type = work.Type,
                    ActionDatetime = work.ActionDatetime,
                    WaterMotorId = work.WaterMotorId,
                    EndActionDateTime = work.EndActionDateTime,
                    ProductId = work.ProductId

                };
                _context.Add(w);
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
            var w = new WorkDto
            {
                Agent = work.Agent,
                Description = work.Description,
                Size = work.Size,
                CategoryId = work.CategoryId,
                GardenId = work.GardenId,
                Status = work.Status,
                Type = work.Type,
                ActionDatetime = work.ActionDatetime,
                WaterMotorId = work.WaterMotorId,
                EndActionDateTime = work.EndActionDateTime,
                ProductId = work.ProductId

            };
            if (work != null)
            {
                await this.DropDownBindingEdit(work.GardenId, work.ProductId, work.CategoryId, work.WaterMotorId,
                    work.Agent);
                

                if (work == null)
                {
                    return NotFound();
                }
            }

            return View(w);
        }

        // POST: Work/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id,
            WorkDto work)
        {
            if (id != work.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var w = new Work
                    {
                        Id = id,
                        Agent = work.Agent,
                        Description = work.Description,
                        Size = work.Size,
                        CategoryId = work.CategoryId,
                        GardenId = work.GardenId,
                        Status = work.Status,
                        Type = work.Type,
                        ActionDatetime = work.ActionDatetime,
                        WaterMotorId = work.WaterMotorId,
                        EndActionDateTime = work.EndActionDateTime,
                        ProductId = work.ProductId

                    };
                    _context.Update(w);
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