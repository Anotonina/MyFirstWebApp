using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyFirstWebApp.Models
{

    public class ShopModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название магазина")]
        public string ShopName { get; set; }

        [Required(ErrorMessage = "Введите скидку")]
        [Range(1, 100, ErrorMessage = "Недопустимая скидка")]
        public int Sale { get; set; }
        public int ShopIncome { get; set; }
        public virtual List<Cashier> Cashiers { get; set; }
    }

    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalCount / PageSize); }
        }
    }
    public class IndexViewModel
    {

        public IEnumerable<ShopModel> Shops { get; set; }
        public PageInfo PageInfo { get; set; }
        public ShopFiltrViwModel Filter { get; set; }

        public IndexViewModel()
        {
            PageInfo = new PageInfo();
            Shops = new List<ShopModel>();
            Filter = new ShopFiltrViwModel();

        }
    }

    public class ShopFiltrViwModel : IValidatableObject
    {
        public int Page { get; set; }
        public string ShopNameFiltr { get; set; }

        [Range(1, 100, ErrorMessage = "Недопустимое значение")]
        public decimal? SaleTo { get; set; }

        [Range(1, 100, ErrorMessage = "Недопустимое значение")]
        public decimal? SaleFrom { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();
            if (SaleFrom > SaleTo)
            {
                result.Add(new ValidationResult("SaleFrom can't be great than SaleTo", new[] { nameof(SaleFrom), nameof(SaleTo) }));
            }
            return result;
        }
    }
    
  
    public class Cashier
    {
        [Key]
        public int CashierId { get; set; }
        public string CashierName { get; set; }
        public int Age { get; set; }
        public int ShopModelId { get; set; }
        public virtual ShopModel ShopModel { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }

    }

    public class CashierViewModel
    {   
        public IEnumerable<SelectListItem> Shops { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        [JsonIgnore]
        public IEnumerable<Cashier> Cashiers { get; set; }
        public string CashierName { get; set; }
        public int Age { get; set; }
        public int CashierId { get; set; }
        public  int ShopModelId { get; set; }
        public virtual ShopModel ShopModel { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public CashierViewModel()
        {
            Cashiers = new List<Cashier>();

        }
    }
  

    public class DiagramsViewModel
    {
        //public Dictionary<string, int> ShopData { get; set; }
        //public List<string> ShopNames { get; set; }
        //public List<int> Income { get; set; }
        public List<ShopIncome> ShopData{ get; set; }
}
    public class ShopIncome
    {
        public string Name { get; set; }
        public int Income { get; set; }
    }


    }





