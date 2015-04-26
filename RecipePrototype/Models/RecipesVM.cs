using System.Collections.Generic;

namespace RecipePrototype.Models
{
    public class RecipesVM
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public int TotalRecipes { get; set; }
        public int PagesToShow { get; set; }
    }
}