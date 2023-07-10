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
    public class InsurancesController : Controller
    {
        private readonly LDInsuranceContext _context;

        public InsurancesController(LDInsuranceContext context)
        {
            _context = context;
        }

        // GET: Insurances
        public IActionResult Index(int? id)
        {
            HttpContext.Session.SetString("PageBeing", "Insurances");
            //Lấy list ProductType và lưu vào viewbag để đưa qua Dropdown
            var listTypeId = _context.InsuranceTypes;
            ViewBag.listType = listTypeId;


            //Xét trường hợp hiển thị sản phẩm
            if (id != null && id != -1)//Trường hợp sinh ra sản phẩm theo id
            {
                var eshopContext = _context.Insurances.Where(p => p.InsuranceTypeID == id);
                return View(eshopContext.ToList());
            }
            else//Trường hợp sinh ra tất cả sản phẩm
            {
                var eshopContext = _context.Insurances.Include(p => p.InsuranceType);
                return View(eshopContext.ToList());
            }
        }

        // GET: Insurances/Details/5
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

        // GET: Insurances/Create
        public IActionResult Create()
        {
            ViewData["InsuranceTypeID"] = new SelectList(_context.InsuranceTypes, "ID", "Name");
            ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "Type");
            return View();
        }

        // POST: Insurances/Create
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
            ViewData["InsuranceTypeID"] = new SelectList(_context.InsuranceTypes, "ID", "Name", insurance.InsuranceTypeID);
            ViewData["VehicleTypeID"] = new SelectList(_context.VehicleTypes, "ID", "Type", insurance.VehicleTypeID);
            return View(insurance);
        }

        // GET: Insurances/Edit/5
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

        // POST: Insurances/Edit/5
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

        // GET: Insurances/Delete/5
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

        // POST: Insurances/Delete/5
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
