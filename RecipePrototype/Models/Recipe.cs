using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipePrototype.Models
{
    public class Recipe
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Method { get; set; }
        public List<RecipeIngredient> Ingredients{ get; set; }
        public int WeightWatchersPoints { get; set; }
        public MealType MealType { get; set; }
        public List<string> Tags { get; set; }
        public HealthyRating HealthyRating { get; set; }
        public bool FillingAndHealthy { get; set; }
    }
}
