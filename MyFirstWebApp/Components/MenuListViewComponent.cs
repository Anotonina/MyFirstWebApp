using Microsoft.AspNetCore.Mvc;
using MyFirstWebApp.Models;
using System.Collections.Generic;


namespace MyFirstWebApp.Components
{
    public class MenuListViewComponent : ViewComponent
    {
        
        public IViewComponentResult Invoke()
        {
            var menuItems = new List<MenuItemViewModel>();
           
            if (User.IsInRole("admin"))
            {                
                menuItems.Add(new MenuItemViewModel() { Title = "Добавить кассира", Url = "/Cashiers/Create" });
                menuItems.Add(new MenuItemViewModel() { Title = "Кассиры", Url = "/Cashiers/Index" });
                menuItems.Add(new MenuItemViewModel() { Title = "Пользователи", Url = "/Users/Index" });

            }
            if (User.IsInRole("user"))
            {
                menuItems.Add(new MenuItemViewModel() { Title = "Диаграммы", Url = "/Home/Diagrams" });
                menuItems.Add(new MenuItemViewModel() { Title = "Магазины", Url = "/Home/AddShops" });
                menuItems.Add(new MenuItemViewModel() { Title = "Выйти", Url = "/Account/Logout" });

            }
            return View(menuItems);
        }
    }
}
