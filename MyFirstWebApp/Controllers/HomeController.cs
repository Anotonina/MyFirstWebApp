﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFirstWebApp.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstWebApp.Controllers
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoContext _context;
        private readonly IMapper _mapper;

        

        public HomeController(ILogger<HomeController> logger, DemoContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

       
        public  IActionResult Diagrams()
        {
            DiagramsViewModel model = new DiagramsViewModel();

            model.ShopData = _mapper.Map<List<ShopIncome>>(_context.Shops);
            Log.Information("Hello Diagrams");
            return View(model);

        }


        
        public IActionResult AddShops(ShopFiltrViwModel shopFiltrViwModel)
        {
            
            var indexViewModel = GetPaggedShops(shopFiltrViwModel.Page, shopFiltrViwModel);
            indexViewModel.Filter = shopFiltrViwModel;
            return View(indexViewModel);
        }

        [HttpGet]
        public IActionResult GetShopsForm(ShopFiltrViwModel shopFiltrViwModel)
        {
            if (!ModelState.IsValid)
            {
                return View("AddShops", new IndexViewModel(){ PageInfo = new PageInfo() { PageSize = 1 } });
            }
            var indexViewModel = GetPaggedShops(shopFiltrViwModel.Page, shopFiltrViwModel);
            indexViewModel.Filter = shopFiltrViwModel;
            return View("AddShops", indexViewModel);

        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetShops(ShopFiltrViwModel shopFiltrViwModel)
         {
            if(!ModelState.IsValid)
            {
                return Json(shopFiltrViwModel);
            }
            var indexViewModel = GetPaggedShops(shopFiltrViwModel.Page, shopFiltrViwModel );
            return Json(indexViewModel);
            
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult SetShops([FromBody] ShopModel shopModel)
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }

            using (var db = new DemoContext())
            {
                db.Add(shopModel);
                db.SaveChanges();
            }
            return Ok();
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            using (var db = new DemoContext())
            {
                ShopModel shop = db.Shops.Find(id);
                if (shop == null)
                {
                    return HttpNotFound();
                }
                return View(shop);
            }
        }

        private IActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed([FromBody]List<int> selectedItems)
        {
            using (var db = new DemoContext())
            {
                foreach ( int s in selectedItems)
                {
                    ShopModel shop = db.Shops.Find(s);
                    if (shop == null)
                    {
                        return (ActionResult)HttpNotFound();
                    }
                    db.Shops.Remove(shop);
                    db.SaveChanges();

                }
               
            }
            return Ok();
        }

        private IndexViewModel GetPaggedShops(int page,ShopFiltrViwModel shopFiltrViwModel )
        {
            List<ShopModel> shopsPerPages = new List<ShopModel>();
            int pageSize = 5;
            var totalCount = 0;
            using (var db = new DemoContext())
            {
                var query = db.Shops.AsQueryable();

                
                //Expression exp = null;
                //var param = Expression.Parameter(typeof(ShopModel), "x");
                if (shopFiltrViwModel.SaleFrom.HasValue)
                {
                    //var p = Expression.Property(param, "Sale");
                    //exp = Expression.GreaterThanOrEqual(p, Expression.Constant(shopFiltrViwModel.SaleFrom.Value));
                    query = query.Where(x => x.Sale >= shopFiltrViwModel.SaleFrom.Value);
                }
                if (shopFiltrViwModel.SaleTo.HasValue)
                {
                    //var p = Expression.Property(param, "Sale");
                    //var and = Expression.LessThanOrEqual(p, Expression.Constant(shopFiltrViwModel.SaleTo.Value));
                    //exp = exp == null ? and : Expression.And(exp, and);
                    query = query.Where(x => x.Sale <= shopFiltrViwModel.SaleTo.Value);
                }
                if (!string.IsNullOrEmpty(shopFiltrViwModel.ShopNameFiltr))
                {
                    //var p = Expression.Property(param, "ShopName");
                    //var and = Expression.Equal(p, Expression.Constant(shopFiltrViwModel.ShopNameFiltr));
                    //exp = exp == null ? and : Expression.And(exp, and);
                    query = query.Where(x => x.ShopName == shopFiltrViwModel.ShopNameFiltr);
                }
                //if (exp != null)
                // {
                //var tt = Expression.Lambda<Func<ShopModel, bool>>(exp, param);
                //query = query.Where(tt);
                //}
                totalCount = query.Count();
                query = query.OrderBy(x => x.Id)
                       .Skip((page - 1) * pageSize)
                       .Take(pageSize);
                shopsPerPages = query.ToList();
                
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalCount = totalCount };
                IndexViewModel indexViewModel = new IndexViewModel { PageInfo = pageInfo, Shops = shopsPerPages };
                return indexViewModel;

            }

        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
