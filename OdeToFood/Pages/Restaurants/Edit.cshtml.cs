using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurant;
        private readonly IHtmlHelper _htmlHelper;

        public EditModel(IRestaurantData restaurant, IHtmlHelper htmlHelper)
        {
            _restaurant = restaurant;
            _htmlHelper = htmlHelper;
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisine { get; set; }

        public IActionResult OnGet(int? restaurantId)
        {
            Cuisine = _htmlHelper.GetEnumSelectList<CuisineType>();

            if (restaurantId.HasValue)
            {
                Restaurant = _restaurant.GetRestaurantById(restaurantId.Value);
            } else
            {
                Restaurant = new Restaurant();
            }

            if (Restaurant is null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                Cuisine = _htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id > 0)
            {
                _restaurant.Update(Restaurant);

            } else
            {
                _restaurant.Add(Restaurant);
            }

            TempData["Message"] = "Restaurant info saved";
            _restaurant.Commit();
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}
