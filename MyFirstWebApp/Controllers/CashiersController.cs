using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Models;


namespace MyFirstWebApp.Controllers
{
    public class CashiersController : Controller
    {
        private readonly DemoContext _context;

        public CashiersController(DemoContext context)
        {
            _context = context;
        }

        // GET: Cashiers
        public async Task<IActionResult> Index(int? id = null)
        {
            var q = _context.Cashiers.AsQueryable();
            if (id.HasValue)
            {
                q = q.Where(x => x.ShopModelId == id);
            }
            var result = await q.ToListAsync();
            return View(result);
        }



        // GET: Cashiers/Create
        public IActionResult Create(int shopModelId)
        {

            CreateCashierViewModel model = new CreateCashierViewModel();

            model.ShopmodelId = shopModelId;

            if (shopModelId == 0)
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
                return NotFound();
            }

            var cashier = await _context.Cashiers
                .FirstOrDefaultAsync(m => m.CashierId == id);
            
            if (cashier == null)
            {
                return NotFound();
            }

            return View(cashier);
        }
        public ActionResult PartialDetails(int id)
        {
            Cashier c = _context.Cashiers.FirstOrDefault(c => c.CashierId == id);
            if (c != null)
                return PartialView(c);
            return NotFound();
        }


        // POST: Cashiers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCashierViewModel cashier)
        {
            if (ModelState.IsValid)
            {
                var cc = new Cashier() { Age = cashier.Age, CashierName = cashier.CashierName, ShopModelId = cashier.ShopmodelId };

                _context.Add(cc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(cashier);
        }


        // GET: Cashiers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cashiers == null)
            {
                return NotFound();
            }

            var cashier = await _context.Cashiers.FindAsync(id);
            if (cashier == null)
            {
                return NotFound();
            }
            return View(cashier);
        }

        // POST: Cashiers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCashierViewModel cashierViewModel)
        {

            if (id != cashierViewModel.CashierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dbc = _context.Cashiers.Find(cashierViewModel.CashierId);
                dbc.Age = cashierViewModel.Age;
                dbc.CashierName = cashierViewModel.CashierName;
                dbc.ShopModelId = cashierViewModel.ShopmodelId;

                _context.Cashiers.Update(dbc);
                 await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(cashierViewModel);
        }

        // GET: Cashiers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cashiers == null)
            {
                return NotFound();
            }

            var cashier = await _context.Cashiers
                .FirstOrDefaultAsync(m => m.CashierId == id);
            if (cashier == null)
            {
                return NotFound();
            }

            return View(cashier);
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
}
