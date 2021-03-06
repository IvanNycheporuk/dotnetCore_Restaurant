using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            _db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _db.Restaurants.Add(newRestaurant);

            return newRestaurant;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var targetRestaurant = GetRestaurantById(id);
            if (targetRestaurant != null)
            {
                _db.Restaurants.Remove(targetRestaurant);
            }

            return targetRestaurant;
        }

        public int GetCountOfRestaurants()
        {
            return _db.Restaurants.Count();
        }

        public Restaurant GetRestaurantById(int id)
        {
            return _db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return _db.Restaurants
                .Where(r => r.Name.StartsWith(name) || string.IsNullOrEmpty(name))
                .OrderBy(r => r.Name);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = _db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;

            return updatedRestaurant;
        }
    }
}
