using System.Collections.Generic;
using RecipePrototype.Models;

namespace RecipePrototype.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RecipePrototype.Models.RecipeDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RecipePrototype.Models.RecipeDBContext context)
        {
            context.Recipes.AddOrUpdate(i => i.Name, new Recipe()
            {
                Name = "Banana Pancakes",
                Method = "Chop some Bananas. Add some eggs and flour. Cook on frying pan",
                MealType = MealType.Breakfast,
                WeightWatchersPoints = 5,
                Ingredients = new List<RecipeIngredient>()
                {
                    new RecipeIngredient()
                    {
                        Name = "Banana",
                        Quantity = "2"
                    },
                    new RecipeIngredient()
                    {
                        Name = "Egg",
                        Quantity = "2"
                    },
                    new RecipeIngredient()
                    {
                        Name = "Flour",
                        Quantity = "200g"
                    }
                }
            },
            new Recipe()
            {
                Name = "Creamy Chicken",
                Method = "Fry chicken for 4 minutes on each side. Add stock and cream cheese. Simmer for 20 mins.",
                MealType = MealType.Dinner,
                WeightWatchersPoints = 6,
                Ingredients = new List<RecipeIngredient>()
                {
                    new RecipeIngredient()
                    {
                        Name = "Chicken breast",
                        Quantity = "2"
                    },
                    new RecipeIngredient()
                    {
                        Name = "Stock",
                        Quantity = "240ml"
                    },
                    new RecipeIngredient()
                    {
                        Name = "Cream cheese",
                        Quantity = "100g"
                    }
                }
            },
            new Recipe()
            {
                Name = "Beef Stew",
                Method = "Brown beef in frying pan. Add to slow cooker with red wine and onions. Slow cook for 8 hours.",
                MealType = MealType.Dinner,
                WeightWatchersPoints = 5,
                Ingredients = new List<RecipeIngredient>()
                {
                    new RecipeIngredient()
                    {
                        Name = "Beef Chunks",
                        Quantity = "1kg"
                    },
                    new RecipeIngredient()
                    {
                        Name = "Wine",
                        Quantity = "1 glass"
                    },
                    new RecipeIngredient()
                    {
                        Name = "Onions",
                        Quantity = "2"
                    },
                    new RecipeIngredient()
                    {
                        Name = "Stock",
                        Quantity = "200ml"
                    }
                }
            });
        }
    }
}
