using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MyFirstWebApp.Controllers
{
    /// <summary>
    /// Discription for apiController
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(Roles = "admin")]
    public class CashiersApiController : ControllerBase
    {
        private readonly DemoContext _context;
        private readonly IMapper _mapper;


        /// <summary>
        /// Discription for constructor of apiController
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
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
        public async Task<ActionResult<CashierViewModel>> GetCashiers()
        {
            var cashier = new CashierViewModel();
             cashier.Cashiers =  await _context.Cashiers.ToListAsync();
            return cashier;
        }
       
            /// <summary>
            /// Gets cashier from database by id
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            // GET: api/CashiersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CashierViewModel>> GetCashier(int id)
        {
            var cashier = await _context.Cashiers
               .FirstOrDefaultAsync(m => m.CashierId == id);
            CashierViewModel model = new CashierViewModel();
                model.Cashier = cashier;
            if (cashier == null)
            {
                return NotFound();
            }

            return model;
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
        public async Task<IActionResult> PutCashier(int id, CashierViewModel cashier)
        {
            if (id != cashier.Cashier.CashierId)
            {
                return BadRequest();
            }

            _context.Entry(cashier.Cashier).State = EntityState.Modified;

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
        /// <returns>A newly created cashier</returns>
        /// <response code="201">Returns the newly created cashier</response>
        /// <response code="400">If the cashier is null</response>     
        // POST: api/CashiersApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cashier>> PostCashier(Cashier cashier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
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
            var cashier = new CashierViewModel();
               cashier.Cashier = await _context.Cashiers.FindAsync(id);
            if (cashier == null)
            {
                return NotFound();
            }

            _context.Cashiers.Remove(cashier.Cashier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CashierExists(int id)
        {
            return _context.Cashiers.Any(e => e.CashierId == id);
        }
    }
    
}
