using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
            //db.Restaurants.AddRange(new List<Restaurant>()
            //{
            //    new Restaurant{Id = 1, Name ="Scott's Pizza", Location ="MaryLand", Cusinie = CusinieType.Italian},
            //    new Restaurant{Id = 2, Name ="Cinnamon Club", Location ="London", Cusinie = CusinieType.Indian},
            //    new Restaurant{Id = 3, Name ="La Costa", Location ="CA", Cusinie = CusinieType.Mexican}
            //});
        }
        public Restaurant Add(Restaurant restaurant)
        {
            db.Add(restaurant);
            return restaurant;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if(restaurant != null)
                db.Restaurants.Remove(restaurant);
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetrestaurantsByName(string name)
        {
            var query = from r in db.Restaurants
                        where  string.IsNullOrEmpty(name) || r.Name.ToLower().Contains(name.ToLower())
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}
