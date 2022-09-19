
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyFirstWebApp.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountApiController : ControllerBase
    {
        private readonly DemoContext db;
        private IConfiguration _config;


        public AccountApiController(DemoContext context, IConfiguration config)
        {
            db = context;
            _config = config;
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public IActionResult Login([FromBody]LoginModel loginModel)
        {
            var user = Authenticate(loginModel);
             if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

             return NotFound("User not found");
        }
        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
            };
            user.Roles.ToList().ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x.Name)));

            var token = new JwtSecurityToken(_config["Jwt:Isuer "],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

             return new JwtSecurityTokenHandler().WriteToken(token);   
        }

        private User Authenticate(LoginModel loginModel)
        {
            var currentUser = db.Users.FirstOrDefault(o =>o.Email==loginModel.Email && o.Password==loginModel.Password );
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }






    }
}
