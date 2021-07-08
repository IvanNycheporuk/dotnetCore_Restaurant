using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options)
            :base(options)
        {

        }
        
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
