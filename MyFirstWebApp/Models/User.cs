using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual Cashier Cashier { get; set; }
        public User()
        {
            Roles = new List<Role>();
        }

    }
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }

}
