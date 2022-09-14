using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Controllers
{
    /// <summary>
    /// Discription for apiController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CashiersApiController : ControllerBase
    {
        private readonly DemoContext _context;
        private readonly IMapper _mapper;


        /// <summary>
        /// Discription for constructor of apiController
        /// </summary>
        /// <param name="context"></param>
        public CashiersApiController(DemoContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Gets All cashiers from database
        /// </summary>
        /// <returns>All cashiers</returns>
        // GET: api/CashiersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cashier>>> GetCashiers()
        {
            return await _context.Cashiers.ToListAsync();
        }


        /// <summary>
        /// Gets cashier from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status">Status can be active or notactive</param>
        /// <returns></returns>
        // GET: api/CashiersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cashier>> GetCashier(int id, string status)
        {
            var cashier = await _context.Cashiers.FindAsync(id);

            if (cashier == null)
            {
                return NotFound();
            }

            return cashier;
        }
        /// <summary>
        /// Update cashier by Id in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cashier"></param>
        /// <returns></returns>
        // PUT: api/CashiersApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCashier(int id, Cashier cashier)
        {
            if (id != cashier.CashierId)
            {
                return BadRequest();
            }

            _context.Entry(cashier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CashierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// <summary>
        /// Add new cashier to database
        /// </summary>
        /// <param name="cashier"></param>
        /// <returns></returns>
        // POST: api/CashiersApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cashier>> PostCashier(Cashier cashier)
        {
            _context.Cashiers.Add(cashier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCashier", new { id = cashier.CashierId }, cashier);
        }
        /// <summary>
        /// Deletes cashier by id
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/CashiersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCashier(int id)
        {
            var cashier = await _context.Cashiers.FindAsync(id);
            if (cashier == null)
            {
                return NotFound();
            }

            _context.Cashiers.Remove(cashier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CashierExists(int id)
        {
            return _context.Cashiers.Any(e => e.CashierId == id);
        }
    }
}
