using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebApp.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class RegisterModel
    {
        
        [Required(ErrorMessage ="Не указан Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required(ErrorMessage ="Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
