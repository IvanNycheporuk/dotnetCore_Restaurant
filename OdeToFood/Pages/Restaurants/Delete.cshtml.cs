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
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        public Restaurant Restaurant { get; set; }

        public DeleteModel(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = _restaurantData.GetRestaurantById(restaurantId);

            if (Restaurant is null)
            {
                RedirectToPage("/NotFound");
            }

            return Page();
        }

        public IActionResult OnPost(int restaurantId)
        {
            var targetRestaurant = _restaurantData.Delete(restaurantId);
            _restaurantData.Commit();

            if (targetRestaurant is null)
            {
                return RedirectToPage("/NotFound");
            }

            TempData["Message"] = $"{targetRestaurant.Name} deleted";

            return RedirectToPage("./List");            
        }
    }
}
