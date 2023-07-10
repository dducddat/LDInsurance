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
    public class InsuranceTypesController : Controller
    {
        private readonly LDInsuranceContext _context;

        public InsuranceTypesController(LDInsuranceContext context)
        {
            _context = context;
        }

        // GET: Admin/InsuranceTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.InsuranceTypes.ToListAsync());
        }

        // GET: Admin/InsuranceTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceType = await _context.InsuranceTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (insuranceType == null)
            {
                return NotFound();
            }

            return View(insuranceType);
        }

        // GET: Admin/InsuranceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/InsuranceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Status")] InsuranceType insuranceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceType);
        }

        // GET: Admin/InsuranceTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceType = await _context.InsuranceTypes.FindAsync(id);
            if (insuranceType == null)
            {
                return NotFound();
            }
            return View(insuranceType);
        }

        // POST: Admin/InsuranceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Status")] InsuranceType insuranceType)
        {
            if (id != insuranceType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceTypeExists(insuranceType.ID))
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
            return View(insuranceType);
        }

        // GET: Admin/InsuranceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceType = await _context.InsuranceTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (insuranceType == null)
            {
                return NotFound();
            }

            return View(insuranceType);
        }

        // POST: Admin/InsuranceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuranceType = await _context.InsuranceTypes.FindAsync(id);
            _context.InsuranceTypes.Remove(insuranceType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceTypeExists(int id)
        {
            return _context.InsuranceTypes.Any(e => e.ID == id);
        }
    }
}
