using System.ComponentModel.DataAnnotations;


namespace MyFirstWebApp.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <summary>
    /// Model logig
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST
    ///     {        
    ///       "email": "admin@mail.ru",
    ///       "password": "123456"    
    ///     }
    /// </remarks>
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        [DataType(DataType.EmailAddress)]
        /// <example>admin@mail.ru</example>
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        /// <example>123456</example>
        public string Password { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
