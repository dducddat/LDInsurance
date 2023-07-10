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
    public class InsuranceRegistrationsController : Controller
    {
        private readonly LDInsuranceContext _context;

        public InsuranceRegistrationsController(LDInsuranceContext context)
        {
            _context = context;
        }

        // GET: Admin/InsuranceRegistrations
        public async Task<IActionResult> Index()
        {
            var lDInsuranceContext = _context.InsuranceRegistrations.Include(i => i.Account).Include(i => i.Insurance).Include(i => i.Vehicle);
            return View(await lDInsuranceContext.ToListAsync());
        }

        public async Task<IActionResult> InMonth(DateTime month)
        {
            month = DateTime.Now;
            var lDInsuranceContext = _context.InsuranceRegistrations.Where(m => m.StartDate.Month == month.Month).Include(i => i.Account).Include(i => i.Insurance).Include(i => i.Vehicle);
            return View(await lDInsuranceContext.ToListAsync());
        }

        public async Task<IActionResult> End(DateTime month)
        {
            month = DateTime.Now;
            var lDInsuranceContext = _context.InsuranceRegistrations.Where(m => m.EndDate <= month).Include(i => i.Account).Include(i => i.Insurance).Include(i => i.Vehicle);
            return View(await lDInsuranceContext.ToListAsync());
        }


        // GET: Admin/InsuranceRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceRegistration = await _context.InsuranceRegistrations
                .Include(i => i.Account)
                .Include(i => i.Insurance)
                .Include(i => i.Vehicle)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (insuranceRegistration == null)
            {
                return NotFound();
            }

            return View(insuranceRegistration);
        }

        // GET: Admin/InsuranceRegistrations/Create
        public IActionResult Create()
        {
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name");
            ViewData["InsuranceID"] = new SelectList(_context.Insurances, "ID", "ID");
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID");
            return View();
        }

        // POST: Admin/InsuranceRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccountID,VehicleID,InsuranceID,StartDate,EndDate,Price,Status")] InsuranceRegistration insuranceRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name", insuranceRegistration.AccountID);
            ViewData["InsuranceID"] = new SelectList(_context.Insurances, "ID", "ID", insuranceRegistration.InsuranceID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID", insuranceRegistration.VehicleID);
            return View(insuranceRegistration);
        }

        // GET: Admin/InsuranceRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceRegistration = await _context.InsuranceRegistrations.FindAsync(id);
            if (insuranceRegistration == null)
            {
                return NotFound();
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name", insuranceRegistration.AccountID);
            ViewData["InsuranceID"] = new SelectList(_context.Insurances, "ID", "ID", insuranceRegistration.InsuranceID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID", insuranceRegistration.VehicleID);
            return View(insuranceRegistration);
        }

        // POST: Admin/InsuranceRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccountID,VehicleID,InsuranceID,StartDate,EndDate,Price,Status")] InsuranceRegistration insuranceRegistration)
        {
            if (id != insuranceRegistration.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceRegistrationExists(insuranceRegistration.ID))
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
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name", insuranceRegistration.AccountID);
            ViewData["InsuranceID"] = new SelectList(_context.Insurances, "ID", "ID", insuranceRegistration.InsuranceID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID", insuranceRegistration.VehicleID);
            return View(insuranceRegistration);
        }

        // GET: Admin/InsuranceRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceRegistration = await _context.InsuranceRegistrations
                .Include(i => i.Account)
                .Include(i => i.Insurance)
                .Include(i => i.Vehicle)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (insuranceRegistration == null)
            {
                return NotFound();
            }

            return View(insuranceRegistration);
        }

        // POST: Admin/InsuranceRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuranceRegistration = await _context.InsuranceRegistrations.FindAsync(id);
            _context.InsuranceRegistrations.Remove(insuranceRegistration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceRegistrationExists(int id)
        {
            return _context.InsuranceRegistrations.Any(e => e.ID == id);
        }
    }
}
