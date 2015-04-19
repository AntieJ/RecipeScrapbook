using System.Data.Entity;

namespace RecipePrototype.Models
{
    public class RecipeDBContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> Ingredients { get; set; }
    }
}
