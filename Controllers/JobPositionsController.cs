using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerHub.Data;
using CareerHub.Models;

namespace CareerHub.Controllers
{
    public class JobPositionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobPositionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobPositions
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobPosition.ToListAsync());
        }

        // GET: JobPositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = await _context.JobPosition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPosition == null)
            {
                return NotFound();
            }

            return View(jobPosition);
        }

        // GET: JobPositions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobPositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Country,City,Type,Level,YearsFrom,YearsTo,IsFullTime,IsRemote,SalaryFrom,SalaryTo,PostedAt")] JobPosition jobPosition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobPosition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobPosition);
        }

        // GET: JobPositions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = await _context.JobPosition.FindAsync(id);
            if (jobPosition == null)
            {
                return NotFound();
            }
            return View(jobPosition);
        }

        // POST: JobPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Country,City,Type,Level,YearsFrom,YearsTo,IsFullTime,IsRemote,SalaryFrom,SalaryTo,PostedAt")] JobPosition jobPosition)
        {
            if (id != jobPosition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPosition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPositionExists(jobPosition.Id))
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
            return View(jobPosition);
        }

        // GET: JobPositions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = await _context.JobPosition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPosition == null)
            {
                return NotFound();
            }

            return View(jobPosition);
        }

        // POST: JobPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPosition = await _context.JobPosition.FindAsync(id);
            if (jobPosition != null)
            {
                _context.JobPosition.Remove(jobPosition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPositionExists(int id)
        {
            return _context.JobPosition.Any(e => e.Id == id);
        }
    }
}
