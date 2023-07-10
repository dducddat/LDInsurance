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
    public class ExpensesController : Controller
    {
        private readonly LDInsuranceContext _context;

        public ExpensesController(LDInsuranceContext context)
        {
            _context = context;
        }

        // GET: Admin/Expenses
        public async Task<IActionResult> Index()
        {
            var lDInsuranceContext = _context.Expenses.Include(e => e.InsuranceRegistration).Include(e => e.Report);
            return View(await lDInsuranceContext.ToListAsync());
        }

        // GET: Admin/Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.InsuranceRegistration)
                .Include(e => e.Report)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Admin/Expenses/Create
        public IActionResult Create()
        {
            ViewData["InsuranceRegistrationID"] = new SelectList(_context.InsuranceRegistrations, "ID", "ID");
            ViewData["ReportID"] = new SelectList(_context.Reports, "ID", "ID");
            return View();
        }

        // POST: Admin/Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,InsuranceRegistrationID,ReportID,Confirm,Date,Amount,Status")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceRegistrationID"] = new SelectList(_context.InsuranceRegistrations, "ID", "ID", expense.InsuranceRegistrationID);
            ViewData["ReportID"] = new SelectList(_context.Reports, "ID", "ID", expense.ReportID);
            return View(expense);
        }

        // GET: Admin/Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["InsuranceRegistrationID"] = new SelectList(_context.InsuranceRegistrations, "ID", "ID", expense.InsuranceRegistrationID);
            ViewData["ReportID"] = new SelectList(_context.Reports, "ID", "ID", expense.ReportID);
            return View(expense);
        }

        // POST: Admin/Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,InsuranceRegistrationID,ReportID,Confirm,Date,Amount,Status")] Expense expense)
        {
            if (id != expense.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.ID))
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
            ViewData["InsuranceRegistrationID"] = new SelectList(_context.InsuranceRegistrations, "ID", "ID", expense.InsuranceRegistrationID);
            ViewData["ReportID"] = new SelectList(_context.Reports, "ID", "ID", expense.ReportID);
            return View(expense);
        }

        // GET: Admin/Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.InsuranceRegistration)
                .Include(e => e.Report)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Admin/Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ID == id);
        }


        public IActionResult Confirm()
        {
            ViewData["InsuranceRegistrationID"] = new SelectList(_context.InsuranceRegistrations, "ID", "ID");
            ViewData["ReportID"] = new SelectList(_context.Reports.Where(r => r.Status == true), "ID", "ID");
            return View();
        }

        [HttpPost]
        public IActionResult Confirm([Bind("ID,InsuranceRegistrationID,ReportID,Confirm,Date,Amount,Status")] Expense expense)
        {
            expense.Date = DateTime.Now;
            expense.Status = true;
            if (ModelState.IsValid)
            {
                    _context.Add(expense);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Expenses");
            }
            ViewData["InsuranceRegistrationID"] = new SelectList(_context.InsuranceRegistrations, "ID", "ID", expense.InsuranceRegistrationID);
            ViewData["ReportID"] = new SelectList(_context.Reports, "ID", "ID", expense.ReportID);
            return View();
        }
    }
}
