using System.ComponentModel.DataAnnotations;


namespace MyFirstWebApp.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <summary>
    /// Model logig
    /// </summary>
   
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        [DataType(DataType.EmailAddress)]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
       
        public string Password { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
