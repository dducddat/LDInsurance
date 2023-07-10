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
    public class TransactionHistoriesController : Controller
    {
        private readonly LDInsuranceContext _context;

        public TransactionHistoriesController(LDInsuranceContext context)
        {
            _context = context;
        }

        // GET: Admin/TransactionHistories
        public async Task<IActionResult> Index()
        {
            var lDInsuranceContext = _context.TransactionHistory.Include(t => t.Account).Include(t => t.Insurance);
            return View(await lDInsuranceContext.ToListAsync());
        }

        // GET: Admin/TransactionHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionHistory = await _context.TransactionHistory
                .Include(t => t.Account)
                .Include(t => t.Insurance)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (transactionHistory == null)
            {
                return NotFound();
            }

            return View(transactionHistory);
        }

        // GET: Admin/TransactionHistories/Create
        public IActionResult Create()
        {
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name");
            ViewData["InsuranceID"] = new SelectList(_context.Insurances, "ID", "ID");
            return View();
        }

        // POST: Admin/TransactionHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccountID,InsuranceID,Card,Date,Price,Status")] TransactionHistory transactionHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transactionHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name", transactionHistory.AccountID);
            ViewData["InsuranceID"] = new SelectList(_context.Insurances, "ID", "ID", transactionHistory.InsuranceID);
            return View(transactionHistory);
        }

        // GET: Admin/TransactionHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionHistory = await _context.TransactionHistory.FindAsync(id);
            if (transactionHistory == null)
            {
                return NotFound();
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name", transactionHistory.AccountID);
            ViewData["InsuranceID"] = new SelectList(_context.Insurances, "ID", "ID", transactionHistory.InsuranceID);
            return View(transactionHistory);
        }

        // POST: Admin/TransactionHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccountID,InsuranceID,Card,Date,Price,Status")] TransactionHistory transactionHistory)
        {
            if (id != transactionHistory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactionHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionHistoryExists(transactionHistory.ID))
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
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name", transactionHistory.AccountID);
            ViewData["InsuranceID"] = new SelectList(_context.Insurances, "ID", "ID", transactionHistory.InsuranceID);
            return View(transactionHistory);
        }

        // GET: Admin/TransactionHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionHistory = await _context.TransactionHistory
                .Include(t => t.Account)
                .Include(t => t.Insurance)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (transactionHistory == null)
            {
                return NotFound();
            }

            return View(transactionHistory);
        }

        // POST: Admin/TransactionHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionHistory = await _context.TransactionHistory.FindAsync(id);
            _context.TransactionHistory.Remove(transactionHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionHistoryExists(int id)
        {
            return _context.TransactionHistory.Any(e => e.ID == id);
        }
    }
}
