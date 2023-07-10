using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LDInsurance.Data;
using LDInsurance.Models;

namespace LDInsurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportsController : Controller
    {
        private readonly LDInsuranceContext _context;

        public ReportsController(LDInsuranceContext context)
        {
            _context = context;
        }

        // GET: Admin/Reports
        public async Task<IActionResult> Index(bool check)
        {
            var checkContext = _context.Reports.Where(c => c.Status == false).Include(i => i.Account);
            return View(checkContext.ToList());
        }

        public async Task<IActionResult> Checked(bool check)
        {
            var checkContext = _context.Reports.Where(c => c.Status == true).Include(i => i.Account);
            return View(checkContext.ToList());
        }

        // GET: Admin/Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Account)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Admin/Reports/Create
        public IActionResult Create()
        {
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name");
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID");
            return View();
        }

        // POST: Admin/Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccountID,VehicleID,Location,Date,Rate,ClaimAmount,Status")] Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name", report.AccountID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "Name", report.VehicleID);
            return View(report);
        }

        // GET: Admin/Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name", report.AccountID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "Name", report.VehicleID);
            return View(report);
        }

        // POST: Admin/Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccountID,VehicleID,Location,Date,Rate,ClaimAmount,Checker,Status")] Report report)
        {
            if (id != report.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.ID))
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
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name", report.AccountID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "Name", report.VehicleID);
            return View(report);
        }

        // GET: Admin/Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Account)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Admin/Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ID == id);
        }

    }
}
