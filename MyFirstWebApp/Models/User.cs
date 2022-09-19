using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyFirstWebApp.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
        public virtual Cashier Cashier { get; set; }
        

    }
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; } = new List<User>();
       
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
