using System.Collections.Generic;

namespace MyFirstWebApp.Models
{
    public class MenuItemViewModel 
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public List<MenuItemViewModel> Submenu { get; set; }

    }
}
  