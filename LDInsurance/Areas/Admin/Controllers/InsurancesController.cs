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
    public class InsurancesController : Controller
    {
        private readonly LDInsuranceContext _context;

        public InsurancesController(LDInsuranceContext context)
        {
            _context = context;
        }

        // GET: Admin/Insurances
        public async Task<IActionResult> Index()
        {
            var lDInsuranceContext = _context.Insurances.Include(i => i.InsuranceType).Include(i => i.VehicleType);
            return View(await lDInsuranceContext.ToListAsync());
        }

        // GET: Admin/Insurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurances
                .Include(i => i.InsuranceType)
                .Include(i => i.VehicleType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // GET: Admin/Insurances/Create
        public IActionResult Create()
        {
            ViewData["InsuranceTypeID"] = new SelectList(_context.InsuranceTypes, "ID", "ID");
            ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "ID");
            return View();
        }

        // POST: Admin/Insurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,InsuranceTypeID,VehicleTypeID,Name,Price,Period,Status")] Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceTypeID"] = new SelectList(_context.InsuranceTypes, "ID", "ID", insurance.InsuranceTypeID);
            ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "ID", insurance.VehicleTypeID);
            return View(insurance);
        }

        // GET: Admin/Insurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurances.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }
            ViewData["InsuranceTypeID"] = new SelectList(_context.InsuranceTypes, "ID", "ID", insurance.InsuranceTypeID);
            ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "ID", insurance.VehicleTypeID);
            return View(insurance);
        }

        // POST: Admin/Insurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,InsuranceTypeID,VehicleTypeID,Name,Price,Period,Status")] Insurance insurance)
        {
            if (id != insurance.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insurance.ID))
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
            ViewData["InsuranceTypeID"] = new SelectList(_context.InsuranceTypes, "ID", "ID", insurance.InsuranceTypeID);
            ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "ID", insurance.VehicleTypeID);
            return View(insurance);
        }

        // GET: Admin/Insurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurances
                .Include(i => i.InsuranceType)
                .Include(i => i.VehicleType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // POST: Admin/Insurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insurance = await _context.Insurances.FindAsync(id);
            _context.Insurances.Remove(insurance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceExists(int id)
        {
            return _context.Insurances.Any(e => e.ID == id);
        }
    }
}
