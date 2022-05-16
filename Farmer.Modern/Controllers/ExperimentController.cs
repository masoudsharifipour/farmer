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
    public class ExperimentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExperimentController(ApplicationDbContext context)
        {
            _context = context;
        }


        public void BindingDate()
        {
            var gardens = _context.Garden.ToList();
            var motors = _context.WaterMotor.ToList();
            ViewBag.Garden = new SelectList(gardens, "Id", "Name");
            ViewBag.WaterMotor = new SelectList(motors, "Id", "Name");
        }

        public void BindingDateEdit(long gardenId, long motorsId)
        {
            var Garden = _context.Garden.ToList();
            var WaterMotor = _context.WaterMotor.ToList();
            ViewBag.Garden = new SelectList(Garden, "Id", "Name", gardenId);
            ViewBag.WaterMotor = new SelectList(WaterMotor, "Id", "Name", motorsId);
        }

        // GET: Experiment
        public async Task<IActionResult> Index()
        {
            return View(await _context.Experiment.Include(x => x.Garden).Include(x => x.WaterMotor).ToListAsync());
        }

        // GET: Experiment/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiment.Include(x => x.Garden).Include(x => x.WaterMotor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experiment == null)
            {
                return NotFound();
            }

            return View(experiment);
        }

        // GET: Experiment/Create
        public IActionResult Create()
        {
            BindingDate();
            return View();
        }

        // POST: Experiment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExperimentDto experiment)
        {
            if (!ModelState.IsValid) return View(experiment);
            var ex = new Experiment
            {
                Description = experiment.Description,
                GardenId = experiment.Garden,
                WaterMotorId = experiment.WaterMotor,
                Result = experiment.Result
            };
            _context.Add(ex);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Experiment/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiment.FindAsync(id);
            ExperimentDto ex = new ExperimentDto();

            if (experiment != null)
            {
                BindingDateEdit(experiment.GardenId, experiment.WaterMotorId);
                ex.Description = experiment.Description;
                ex.Result = experiment.Result;

                if (experiment == null)
                {
                    return NotFound();
                }
            }

            return View(ex);
        }

        // POST: Experiment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, ExperimentDto experiment)
        {
            if (id != experiment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ex = new Experiment
                    {
                        Id = id,
                        Description = experiment.Description,
                        Result = experiment.Result,
                        GardenId = experiment.Garden,
                        WaterMotorId = experiment.WaterMotor
                    };
                    _context.Update(ex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperimentExists(experiment.Id))
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

            return View(experiment);
        }

        // GET: Experiment/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiment.Include(x => x.Garden).Include(x => x.WaterMotor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experiment == null)
            {
                return NotFound();
            }

            return View(experiment);
        }

        // POST: Experiment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var experiment = await _context.Experiment.FindAsync(id);
            _context.Experiment.Remove(experiment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperimentExists(long id)
        {
            return _context.Experiment.Any(e => e.Id == id);
        }
    }
}