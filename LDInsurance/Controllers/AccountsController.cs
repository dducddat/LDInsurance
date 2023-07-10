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
using Microsoft.AspNetCore.Authorization;

namespace LDInsurance.Controllers
{
    public class AccountsController : Controller
    {
        private readonly LDInsuranceContext _context;

        public AccountsController(LDInsuranceContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public IActionResult Index()
        {
            HttpContext.Session.SetString("PageBeing", "Accounts");
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return View("Login");
            }
            return View(_context.Accounts.Where(acc => acc.Username == HttpContext.Session.GetString("CurrentUser")).ToList());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Phone,SSN,Username,Password,IsAdmin,Status")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Phone,SSN,Username,Password,IsAdmin,Status")] Account account)
        {
            if (id != account.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.ID))
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
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.ID == id);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            bool result = _context.Accounts.Any(acc => acc.Username == username && acc.Password == password);
            if (result)
            {
                bool isAdmin = _context.Accounts.Where(acc => acc.Username == username).FirstOrDefault().IsAdmin;
                HttpContext.Session.SetString("CurrentUser", username);
                HttpContext.Session.SetInt32("ID", _context.Accounts.Where(acc => acc.Username == username).FirstOrDefault().ID);
                ViewBag.BeLogin = true;
                if (!isAdmin)
                {
                    return RedirectToAction("Index", HttpContext.Session.GetString("PageBeing"));
                }
                else
                {
                    return RedirectToAction("Index", "Accounts", new { area = "Admin" });
                }
            }
            else
            {
                HttpContext.Session.SetInt32("Id", Convert.ToInt32(-1));
                ViewBag.ErrorMess = "Wrong password";
                return View();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Accounts");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([Bind("ID,Name,Phone,SSN,Username,Password,IsAdmin,Status")] Account account)
        {
            account.IsAdmin = false;
            account.Status = true;

            if (ModelState.IsValid)
            {
                var check = _context.Accounts.FirstOrDefault(s => s.Username == account.Username);
                if (check == null)
                {
                    _context.Add(account);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Accounts");
                }
                else
                {
                    ViewBag.ErrorRegister = "Username already exists";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorRegister = "Register Failed!!!";
                return View();
            }
        }

        public IActionResult ChangePass()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePass(string Old, string New, string Newconfirm)
        {
            if (ModelState.IsValid)
            {
                Account user = _context.Accounts.Where(acc => acc.Username == HttpContext.Session.GetString("CurrentUser")).FirstOrDefault();
                if (Old == user.Password)
                {
                    if(Old == New)
                    {
                        ViewBag.Message = "You entered the new password with the old password";
                        return View();
                    }
                    else if(New == Newconfirm)
                    {
                        user.Password = Newconfirm;
                        _context.Accounts.Update(user);
                        _context.SaveChanges();
                        return RedirectToAction("Index", "Accounts");
                    }
                    else
                    {
                        ViewBag.Message = "Confirm new password is not correct";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Old password is not correct";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public IActionResult Sitemap()
        {
            return View();
        }
    }
}
