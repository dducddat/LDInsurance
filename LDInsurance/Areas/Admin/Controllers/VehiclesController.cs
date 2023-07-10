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
    public class VehiclesController : Controller
    {
        private readonly LDInsuranceContext _context;

        public VehiclesController(LDInsuranceContext context)
        {
            _context = context;
        }

        // GET: Admin/Vehicles
        public async Task<IActionResult> Index()
        {
            var lDInsuranceContext = _context.Vehicles.Include(v => v.Account).Include(v => v.VehicleType);
            return View(await lDInsuranceContext.ToListAsync());
        }

        // GET: Admin/Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Account)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Admin/Vehicles/Create
        public IActionResult Create()
        {
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "ID");
            ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "ID");
            return View();
        }

        // POST: Admin/Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccountID,OwnerName,Name,Model,Version,Rate,VehicleNumber,VehicleTypeID,Status")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "ID", vehicle.AccountID);
            ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "ID", vehicle.VehicleTypeID);
            return View(vehicle);
        }

        // GET: Admin/Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "ID", vehicle.AccountID);
            ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "ID", vehicle.VehicleTypeID);
            return View(vehicle);
        }

        // POST: Admin/Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccountID,OwnerName,Name,Model,Version,Rate,VehicleNumber,VehicleTypeID,Status")] Vehicle vehicle)
        {
            if (id != vehicle.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.ID))
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
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "ID", vehicle.AccountID);
            ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "ID", vehicle.VehicleTypeID);
            return View(vehicle);
        }

        // GET: Admin/Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Account)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Admin/Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.ID == id);
        }
    }
}
