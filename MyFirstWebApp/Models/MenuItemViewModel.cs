using System.Collections.Generic;

namespace MyFirstWebApp.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class MenuItemViewModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public List<MenuItemViewModel> Submenu { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
