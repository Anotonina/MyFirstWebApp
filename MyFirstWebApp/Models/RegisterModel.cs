using System.ComponentModel.DataAnnotations;

namespace MyFirstWebApp.Models
{
    public class RegisterModel
    {
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Required(ErrorMessage ="Не указан Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage ="Не указан пароль")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
