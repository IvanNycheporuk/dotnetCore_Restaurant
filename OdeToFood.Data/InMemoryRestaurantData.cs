using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>() 
            { 
                new Restaurant() { Id = 1, Cuisine = CuisineType.Indian, Location = "America", Name = "Indian cuisine" },
                new Restaurant() { Id = 2, Cuisine = CuisineType.Italian, Location = "Italia", Name = "Italian cuisine" },
                new Restaurant() { Id = 3, Cuisine = CuisineType.Mexican, Location = "Mexica", Name = "Mexican cuisine" }
            };
        } 

        public IEnumerable<Restaurant> GetRestaurantsByName(string name=null)
        {
            return restaurants
                .Where(x => string.IsNullOrEmpty(name) || x.Name.StartsWith(name))
                .OrderBy(r => r.Name);
        }

        public Restaurant GetRestaurantById(int id)
        {
            return restaurants
                .SingleOrDefault(x => x.Id == id);                
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var targetRestaurant = restaurants.SingleOrDefault(x => x.Id == updatedRestaurant.Id);

            targetRestaurant.Name = updatedRestaurant.Name;
            targetRestaurant.Location = updatedRestaurant.Location;
            targetRestaurant.Cuisine = updatedRestaurant.Cuisine;

            return targetRestaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);

            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;

            return newRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var targetRestaurant = restaurants.SingleOrDefault(x => x.Id == id);

            if (targetRestaurant != null)
            {
                restaurants.Remove(targetRestaurant);
            }

            return targetRestaurant;
        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count();
        }
    }
}
