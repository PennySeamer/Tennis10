using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tennis10.Data;
using Tennis10.Models;

namespace Tennis10.Controllers
{
    public class ScheduleMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScheduleMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ScheduleMembers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ScheduleMembers.Include(s => s.Schedule);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ScheduleMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleMembers = await _context.ScheduleMembers
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduleMembers == null)
            {
                return NotFound();
            }

            return View(scheduleMembers);
        }

        // GET: ScheduleMembers/Create
        public IActionResult Create()
        {
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Id");
            return View();
        }

        // POST: ScheduleMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ScheduleId,MemberEmail")] ScheduleMembers scheduleMembers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduleMembers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Id", scheduleMembers.ScheduleId);
            return View(scheduleMembers);
        }

        // GET: ScheduleMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleMembers = await _context.ScheduleMembers.FindAsync(id);
            if (scheduleMembers == null)
            {
                return NotFound();
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Id", scheduleMembers.ScheduleId);
            return View(scheduleMembers);
        }

        // POST: ScheduleMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ScheduleId,MemberEmail")] ScheduleMembers scheduleMembers)
        {
            if (id != scheduleMembers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduleMembers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleMembersExists(scheduleMembers.Id))
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
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Id", scheduleMembers.ScheduleId);
            return View(scheduleMembers);
        }

        // GET: ScheduleMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleMembers = await _context.ScheduleMembers
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduleMembers == null)
            {
                return NotFound();
            }

            return View(scheduleMembers);
        }

        // POST: ScheduleMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduleMembers = await _context.ScheduleMembers.FindAsync(id);
            _context.ScheduleMembers.Remove(scheduleMembers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleMembersExists(int id)
        {
            return _context.ScheduleMembers.Any(e => e.Id == id);
        }
    }
}
