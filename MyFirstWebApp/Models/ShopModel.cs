using System;
using System.ComponentModel.DataAnnotations;



namespace MyFirstWebApp.Models
{
    public class ShopModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Введите название магазина")]
        public string ShopName { get; set; }
        [Required(ErrorMessage = "Введите скидку")]        
        public string Sale { get; set; }
    }
}
