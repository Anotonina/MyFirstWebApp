﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Controllers
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UsersController : BaseController

    {
        private readonly DemoContext _context;

        public UsersController(DemoContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            var u = users[0];
              return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "admin, user")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                User User = user;
                Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");

                if (userRole != null)
                    User.Roles.Add(userRole);

                _context.Add(User);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(User);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                User u = _context.Users.Find(user.Id);
                var c = _context.Users.FirstOrDefault(x => x.Email == user.Email);
                if (c == null)
                {
                    u.Email = user.Email;

                    u.Password = user.Password;

                    // _context.ChangeTracker.Clear();
                    _context.Users.Update(u);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } else ModelState.AddModelError("", "Этот Email уже занят");

            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'DemoContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return _context.Users.Any(e => e.Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
