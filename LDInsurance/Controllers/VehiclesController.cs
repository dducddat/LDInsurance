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
    public class VehiclesController : Controller
    {
        private readonly LDInsuranceContext _context;

        public VehiclesController(LDInsuranceContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public IActionResult Index(int? id)
        {
            HttpContext.Session.SetString("PageBeing", "Vehicles");
            if (HttpContext.Session.GetInt32("ID") == null)
            {

                return RedirectToAction("Login", "Accounts");
            }
            else
            {
                var vehicleContext = _context.Vehicles.Where(p => p.AccountID == id).Include(i => i.VehicleType).ToList();
                return View(vehicleContext);
            }
        }

        // GET: Vehicles/Details/5
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

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "ID");
            ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "ID");
            return View();
        }

        // POST: Vehicles/Create
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

        // GET: Vehicles/Edit/5
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

        // POST: Vehicles/Edit/5
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

        // GET: Vehicles/Delete/5
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

        // POST: Vehicles/Delete/5
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

        public IActionResult Register()
        {
            HttpContext.Session.SetString("PageBeing", "Insurances");
            if (HttpContext.Session.GetInt32("ID") == null)
            {

                return RedirectToAction("Login", "Accounts");
            }
            else
            {
                ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "ID");
                ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "Type");
                return View();   
            }
        }

            [HttpPost]
            public IActionResult Register([Bind("ID,AccountID,OwnerName,Name,Model,Version,Rate,VehicleNumber,VehicleTypeID,Status")] Vehicle vehicle)
            {
                vehicle.AccountID = HttpContext.Session.GetInt32("ID");
                vehicle.Status = true;

                if (ModelState.IsValid)
                {
                    _context.Add(vehicle);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Accounts");
                }
                ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "ID", vehicle.AccountID);
                ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "Types", vehicle.VehicleTypeID);
                return View();
            }
    }
}
