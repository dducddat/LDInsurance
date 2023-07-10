using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LDInsurance.Data;
using LDInsurance.Models;
using Microsoft.AspNetCore.Http;

namespace LDInsurance.Controllers
{
    public class ReportsController : Controller
    {
        private readonly LDInsuranceContext _context;

        public ReportsController(LDInsuranceContext context)
        {
            _context = context;
        }

        // GET: Reports
        public IActionResult Index(int? id)
        {
            try
            {
                HttpContext.Session.SetString("PageBeing", "Reports");
                if (HttpContext.Session.GetInt32("ID") == null)
                {

                    return RedirectToAction("Login", "Accounts");
                }
                else
                {
                    var reportContext = _context.Reports.Where(p => p.AccountID == id).Include(i => i.Vehicle).ToList();
                    return View(reportContext);
                }
            }
            catch
            {
                return View("IndexNull");
            }
        }

        public IActionResult IndexNull(int? id)
        {

                HttpContext.Session.SetString("PageBeing", "Reports");
                if (HttpContext.Session.GetInt32("ID") == null)
                {

                    return RedirectToAction("Login", "Accounts");
                }
                else
                {
                    return View();
                }

        }

        // GET: Reports/Details/5
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

        // GET: Reports/Create
        public IActionResult Create()
        {
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name");
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID");
            return View();
        }

        // POST: Reports/Create
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
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID", report.VehicleID);
            return View(report);
        }

        // GET: Reports/Edit/5
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
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID", report.VehicleID);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccountID,VehicleID,Location,Date,Rate,ClaimAmount,Status")] Report report)
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
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID", report.VehicleID);
            return View(report);
        }

        // GET: Reports/Delete/5
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

        // POST: Reports/Delete/5
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

        public IActionResult Write()
        {
            HttpContext.Session.SetString("PageBeing", "Reports");
            if (HttpContext.Session.GetInt32("ID") == null)
            {

                return RedirectToAction("Login", "Accounts");
            }
            else
            {
                var accID = HttpContext.Session.GetInt32("ID");

                ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name");
                ViewData["VehicleID"] = new SelectList(_context.Vehicles.Where(v => v.AccountID == accID).ToList(), "ID", "Name");

                return View();
            }
        }

        [HttpPost]
        public IActionResult Write([Bind("ID,AccountID,VehicleID,Location,Date,Rate,ClaimAmount,Status")] Report report)
        {
            report.AccountID = HttpContext.Session.GetInt32("ID");
            report.Date = DateTime.Now;
            report.Status = false;

            if (ModelState.IsValid)
            {
                _context.Add(report);
                _context.SaveChanges();
                return RedirectToAction("Index", "Accounts");
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name", report.AccountID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "Name", report.VehicleID);
            return View();
        }
    }
}
