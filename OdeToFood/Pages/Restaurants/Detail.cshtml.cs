using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData _restaurant;

        public DetailModel(IRestaurantData restaurant)
        {
            _restaurant = restaurant;
        }

        public Restaurant Restaurant { get; set; }

        [TempData]
        public string Message { get; set; }

        public IActionResult OnGet(int restaurantId)
        {            
            Restaurant = _restaurant.GetRestaurantById(restaurantId);            

            if (Restaurant is null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }
    }
}
