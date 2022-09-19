using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Models;
using Serilog;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyFirstWebApp.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin")]
    public class CashiersController : BaseController
    {
        private protected readonly DemoContext _context;
        private protected readonly IMapper _mapper;

        public CashiersController(DemoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: Cashiers
        public IActionResult Index(int? id = null)
        {
            CashierViewModel model = new CashierViewModel();  
            model.Cashiers = _context.Cashiers.AsQueryable();
            if (id.HasValue)
            {
                
                model.Cashiers = model.Cashiers.Where(x => x.ShopModelId == id);
            }
            return View(model);
        }


        // GET: Cashiers/Create
        public IActionResult Create(int shopModelId)
        {
            CashierViewModel model = new CashierViewModel();
            model.Cashier = new Cashier();
            model.Cashier.ShopModelId = shopModelId;
            model.Users = _context.Users
                        .Select(user => new SelectListItem { Text = user.Email, Value = user.Id.ToString() });
            if (shopModelId == 0 )
            {
                model.Shops = _context.Shops
                               .Select(shop => new SelectListItem { Text = shop.ShopName, Value = shop.Id.ToString() });
                return View(model);
            }
           
            return View(model);
        }


        // GET: Cashiers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cashiers == null)
            {
                Log.Warning("Details({Id}) NOT FOUND", id);
                return NotFound();
            }
            var cashier = await _context.Cashiers
                .FirstOrDefaultAsync(m => m.CashierId == id);
            CashierViewModel model= new CashierViewModel();
            model.Cashier = cashier;
           // CashierViewModel model = new CashierViewModel() { Age = cashier.Age, CashierName = cashier.CashierName};

            if (cashier == null)
            {
                return NotFound();
            }

            return View(model);
        }


        public IActionResult PartialDetails(int id)
        {
            Cashier cashier = _context.Cashiers.FirstOrDefault(c => c.CashierId == id);
            CashierViewModel model = new CashierViewModel();
            model.Cashier = cashier;

            if (cashier != null)
                return PartialView(model);
            return NotFound();
        }


        // POST: Cashiers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CashierViewModel cashierViewModel)
        {
            var isCashier = _context.Cashiers.FirstOrDefault(c => c.UserId == cashierViewModel.Cashier.UserId);
           
            if (isCashier!= null)
            {
                throw new ArgumentException("Этот пользователь занят");
            }
            
            if (ModelState.IsValid)
            {
                var cashier = _mapper.Map<Cashier>(cashierViewModel.Cashier);
               // var cc = new Cashier() { Age = cashier.Age, CashierName = cashier.CashierName, ShopModelId = cashier.ShopModelId };

                _context.Add(cashier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(cashierViewModel);
        }


        // GET: Cashiers/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cashiers == null)
            {
                Log.Warning("Edit({ Id}) NOT FOUND", id);
                return NotFound();
            }

            var cashier = await _context.Cashiers.FindAsync(id);
            CashierViewModel model = new CashierViewModel();
            model.Cashier = cashier;


            if (cashier == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Cashiers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CashierViewModel cashierViewModel)
        {

            if (id != cashierViewModel.Cashier.CashierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Cashier cashier = _context.Cashiers.Find(cashierViewModel.Cashier.CashierId);
                
                 cashier.User.Email = cashierViewModel.Cashier.User.Email;
                 cashier.User.Password = cashierViewModel.Cashier.User.Password;
                 cashier.Age = cashierViewModel.Cashier.Age;
                 cashier.CashierName = cashierViewModel.Cashier.CashierName;
                 cashier.ShopModelId = cashierViewModel.Cashier.ShopModelId;


               // _context.ChangeTracker.Clear();
                _context.Cashiers.Update(cashier);
                 await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(cashierViewModel);
        }
    
        // GET: Cashiers/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cashiers == null)
            {
                return NotFound();
            }

            var cashier = await _context.Cashiers
                .FirstOrDefaultAsync(m => m.CashierId == id);
            CashierViewModel model = new CashierViewModel();
               model.Cashier =  cashier;

            if (cashier == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Cashiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cashiers == null)
            {
                return Problem("Entity set 'DemoContext.Cashiers'  is null.");
            }
            var cashier = await _context.Cashiers.FindAsync(id);

            if (cashier != null)
            {
                _context.Cashiers.Remove(cashier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashierExists(int id)
        {
            return _context.Cashiers.Any(e => e.CashierId == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
