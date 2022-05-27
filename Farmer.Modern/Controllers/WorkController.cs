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

            var status = (from object e in Enum.GetValues(typeof(ActionStatus))
                select new KeyValuePair<string, int>(e.ToString(), (int) e)).ToList();


            ViewBag.GardenId = new SelectList(gardens, "Id", "Name");
            ViewBag.ProductId = new SelectList(products, "Id", "Name");
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            ViewBag.WaterMotorId = new SelectList(motors, "Id", "Name");
            ViewBag.Agent = new SelectList(agents, "Id", "Name");
            ViewBag.Status = new SelectList(status, "Value", "Key");
        }

        public async Task DropDownBindingEdit(long gardenId, long? productId, long categoryId, long? motorId,
            Guid? agentId, ActionStatus status)
        {
            var gardens = await _context.Garden.ToListAsync();
            var products = await _context.Product.ToListAsync();
            var categories = await _context.Category.ToListAsync();
            var motors = await _context.WaterMotor.ToListAsync();
            var agents = await _context.Users.ToListAsync();
            var statusResult = (from object e in Enum.GetValues(typeof(ActionStatus))
                select new KeyValuePair<string, int>(e.ToString(), (int) e)).ToList();

            ViewBag.GardenId = new SelectList(gardens, "Id", "Name", gardenId);
            ViewBag.ProductId = new SelectList(products, "Id", "Name", productId);
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", categoryId);
            ViewBag.WaterMotorId = new SelectList(motors, "Id", "Name", motorId);
            ViewBag.Agent = new SelectList(agents, "Id", "Name", agentId);
            ViewBag.Status = new SelectList(statusResult, "Value", "Key", (int) status);
        }

        // GET: Work
        public async Task<IActionResult> Index()
        {
            var workDtos = new List<WorkDto>();
            var work = await _context.Work.Include(x => x.Category)
                .Include(x => x.Garden)
                .Include(x => x.Product)
                .Include(x => x.WaterMotor).ToListAsync();

            if (!work.Any()) return View(workDtos);
            foreach (var item in work)
            {
                var agentFullName = await _context.Users.FirstOrDefaultAsync(x => x.Id == item.AgentId.ToString());
                var creatorUser =
                    await _context.Users.FirstOrDefaultAsync(x => x.Id == item.CreatorUserId.ToString());

                workDtos.Add(new WorkDto
                {
                    Id = item.Id,
                    ActionDateTime = item.ActionDatetime,
                    Agent = item.AgentId,
                    CategoryName = item.Category.Name,
                    CategoryId = item.CategoryId,
                    CreatorFullName = $"{creatorUser?.Name} {creatorUser?.LastName}",
                    FullNameAgent = $"{agentFullName?.Name} {agentFullName?.LastName}",
                    Status = item.Status,
                    EndActionDateTime = item.EndActionDateTime,
                    GardenName = item.Garden.Name,
                    ProductName = item.Product.Name,
                    WaterMotorName = item.WaterMotor?.Name,
                    Description = item.Description,
                    Type = item.Type,
                    Size = item.Size,
                });
            }

            return View(workDtos);
        }

        // GET: Work/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDtos = new WorkDto();

            var work = await _context.Work.Include(x => x.Category)
                .Include(x => x.Garden)
                .Include(x => x.Product)
                .Include(x => x.WaterMotor)
                .FirstOrDefaultAsync(m => m.Id == id);

            var agentFullName = await _context.Users.FirstOrDefaultAsync(x => x.Id == work.AgentId.ToString());
            var creatorUser =
                await _context.Users.FirstOrDefaultAsync(x => x.Id == work.CreatorUserId.ToString());


            workDtos.Id = work.Id;
            workDtos.ActionDateTime = work.ActionDatetime;
            workDtos.Agent = work.AgentId;
            workDtos.CategoryName = work.Category.Name;
            workDtos.CategoryId = work.CategoryId;
            workDtos.CreatorFullName = $"{creatorUser?.Name} {creatorUser?.LastName}";
            workDtos.FullNameAgent = $"{agentFullName?.Name} {agentFullName?.LastName}";
            workDtos.Status = work.Status;
            workDtos.EndActionDateTime = work.EndActionDateTime;
            workDtos.GardenName = work.Garden.Name;
            workDtos.ProductName = work.Product.Name;
            workDtos.WaterMotorName = work.WaterMotor?.Name;
            workDtos.Description = work.Description;
            workDtos.Type = work.Type;
            workDtos.Size = work.Size;


            if (work == null)
            {
                return NotFound();
            }

            return View(workDtos);
        }

        // GET: Work/Create
        public async Task<IActionResult> Create()
        {
            var workInputDto = new WorkInputDto();
            await this.DropDownBinding();
            var categories = await _context.Category.ToListAsync();
            var category = categories.Select(item => new CategoryDto
                {
                    CategoryId = item.Id,
                    CategoryName = item.Name,
                    Selected = false
                })
                .ToList();
            workInputDto.Category = category;
            return View(workInputDto);
        }

        // POST: Work/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkInputDto work)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in work.Category.Where(x => x.Selected))
                {
                    var w = new Work
                    {
                        AgentId = work.Agent,
                        Description = work.Description,
                        Size = work.Size,
                        CategoryId = item.CategoryId,
                        GardenId = work.GardenId,
                        Status = work.Status,
                        Type = work.Type,
                        ActionDatetime = work.ActionDateTime,
                        WaterMotorId = work.WaterMotorId,
                        EndActionDateTime = work.EndActionDateTime,
                        ProductId = work.ProductId,
                    };
                    _context.Add(w);
                    await _context.SaveChangesAsync();
                }

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
            if (work == null) throw new ArgumentNullException(nameof(work));
            var w = new WorkDto
            {
                Agent = work.AgentId,
                Description = work.Description,
                Size = work.Size,
                CategoryId = work.CategoryId,
                GardenId = work.GardenId,
                Type = work.Type,
                ActionDateTime = work.ActionDatetime,
                WaterMotorId = work.WaterMotorId,
                EndActionDateTime = work.EndActionDateTime,
                ProductId = work.ProductId
            };
            await this.DropDownBindingEdit(work.GardenId, work.ProductId, work.CategoryId, work.WaterMotorId,
                work.AgentId, work.Status);


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
                        AgentId = work.Agent,
                        Description = work.Description,
                        Size = work.Size,
                        CategoryId = work.CategoryId,
                        GardenId = work.GardenId,
                        Status = work.Status,
                        Type = work.Type,
                        ActionDatetime = work.ActionDateTime,
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

            WorkDto workDto = new WorkDto();
            var work = await _context.Work.Include(x => x.Category)
                .Include(x => x.Garden)
                .Include(x => x.Product)
                .Include(x => x.WaterMotor)
                .FirstOrDefaultAsync(m => m.Id == id);

            var agentFullName = await _context.Users.FirstOrDefaultAsync(x => x.Id == work.AgentId.ToString());
            var creatorUser =
                await _context.Users.FirstOrDefaultAsync(x => x.Id == work.CreatorUserId.ToString());


            workDto.Id = work.Id;
            workDto.ActionDateTime = work.ActionDatetime;
            workDto.Agent = work.AgentId;
            workDto.CategoryName = work.Category.Name;
            workDto.CategoryId = work.CategoryId;
            workDto.CreatorFullName = $"{creatorUser?.Name} {creatorUser?.LastName}";
            workDto.FullNameAgent = $"{agentFullName?.Name} {agentFullName?.LastName}";
            workDto.Status = work.Status;
            workDto.EndActionDateTime = work.EndActionDateTime;
            workDto.GardenName = work.Garden.Name;
            workDto.ProductName = work.Product.Name;
            workDto.WaterMotorName = work.WaterMotor?.Name;
            workDto.Description = work.Description;
            workDto.Type = work.Type;
            workDto.Size = work.Size;
            if (work == null)
            {
                return NotFound();
            }

            return View(workDto);
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


        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate, long? gardenId)
        {
            await DropDownBinding();
            var workDtos = new List<WorkDto>();
            var works = _context.Work.Include(x => x.Category)
                .Include(x => x.Garden)
                .Include(x => x.Product)
                .Include(x => x.WaterMotor).AsQueryable();

            if (startDate != null)
                works = works.Where(x => x.CreationDatetime >= startDate);

            if (endDate != null)
                works = works.Where(x => x.CreationDatetime <= endDate);

            if (gardenId != null)
                works = works.Where(x => x.GardenId == gardenId);

            var workfilter = await works.ToListAsync();
            foreach (var item in workfilter)
            {
                var agentFullName = await _context.Users.FirstOrDefaultAsync(x => x.Id == item.AgentId.ToString());
                var creatorUser =
                    await _context.Users.FirstOrDefaultAsync(x => x.Id == item.CreatorUserId.ToString());

                workDtos.Add(new WorkDto
                {
                    Id = item.Id,
                    ActionDateTime = item.ActionDatetime,
                    Agent = item.AgentId,
                    CategoryName = item.Category.Name,
                    CategoryId = item.CategoryId,
                    CreatorFullName = $"{creatorUser?.Name} {creatorUser?.LastName}",
                    FullNameAgent = $"{agentFullName?.Name} {agentFullName?.LastName}",
                    Status = item.Status,
                    EndActionDateTime = item.EndActionDateTime,
                    GardenName = item.Garden.Name,
                    ProductName = item.Product.Name,
                    WaterMotorName = item.WaterMotor?.Name,
                    Description = item.Description,
                    Type = item.Type,
                    Size = item.Size,
                });
            }


            return View(workDtos);
        }

        public async Task<IActionResult> Action(long? id)
        {
            ActionDto actionDto = new ActionDto();
            var work = await _context.Work.FirstOrDefaultAsync(x => x.Id == id);
            var statusResult = (from object e in Enum.GetValues(typeof(ActionStatus))
                select new KeyValuePair<string, int>(e.ToString(), (int) e)).ToList();
            
            ViewBag.Status = new SelectList(statusResult, "Value", "Key", (int) work.Status);
            return View(actionDto);

        }

        [HttpPost, ActionName("Action")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Action(long id , ActionDto actionDto)
        {
            var work = await _context.Work.FirstOrDefaultAsync(x => x.Id == id);
            work.EndActionDateTime = actionDto.FinishDataTime;
            work.Status = actionDto.Status;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}