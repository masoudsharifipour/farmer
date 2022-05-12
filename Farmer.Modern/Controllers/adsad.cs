// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using Farmer.Modern.Dto;
// using Farmer.Modern.Models.DbContext;
//
// namespace Farmer.Modern.Controllers
// {
//     public class adsad : Controller
//     {
//         private readonly ApplicationDbContext _context;
//
//         public adsad(ApplicationDbContext context)
//         {
//             _context = context;
//         }
//
//         // GET: adsad
//         public async Task<IActionResult> Index()
//         {
//             return View(await _context.ApplicationUserInputDto.ToListAsync());
//         }
//
//         // GET: adsad/Details/5
//         public async Task<IActionResult> Details(string id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var applicationUserInputDto = await _context.ApplicationUserInputDto
//                 .FirstOrDefaultAsync(m => m.Name == id);
//             if (applicationUserInputDto == null)
//             {
//                 return NotFound();
//             }
//
//             return View(applicationUserInputDto);
//         }
//
//         // GET: adsad/Create
//         public IActionResult Create()
//         {
//             return View();
//         }
//
//         // POST: adsad/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("Name,LastName,UserName,Email,Password,Address,PhoneNumber")] ApplicationUserInputDto applicationUserInputDto)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Add(applicationUserInputDto);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(applicationUserInputDto);
//         }
//
//         // GET: adsad/Edit/5
//         public async Task<IActionResult> Edit(string id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var applicationUserInputDto = await _context.ApplicationUserInputDto.FindAsync(id);
//             if (applicationUserInputDto == null)
//             {
//                 return NotFound();
//             }
//             return View(applicationUserInputDto);
//         }
//
//         // POST: adsad/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(string id, [Bind("Name,LastName,UserName,Email,Password,Address,PhoneNumber")] ApplicationUserInputDto applicationUserInputDto)
//         {
//             if (id != applicationUserInputDto.Name)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(applicationUserInputDto);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!ApplicationUserInputDtoExists(applicationUserInputDto.Name))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(applicationUserInputDto);
//         }
//
//         // GET: adsad/Delete/5
//         public async Task<IActionResult> Delete(string id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var applicationUserInputDto = await _context.ApplicationUserInputDto
//                 .FirstOrDefaultAsync(m => m.Name == id);
//             if (applicationUserInputDto == null)
//             {
//                 return NotFound();
//             }
//
//             return View(applicationUserInputDto);
//         }
//
//         // POST: adsad/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(string id)
//         {
//             var applicationUserInputDto = await _context.ApplicationUserInputDto.FindAsync(id);
//             _context.ApplicationUserInputDto.Remove(applicationUserInputDto);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//
//         private bool ApplicationUserInputDtoExists(string id)
//         {
//             return _context.ApplicationUserInputDto.Any(e => e.Name == id);
//         }
//     }
// }
