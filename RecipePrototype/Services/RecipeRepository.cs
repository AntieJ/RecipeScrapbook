using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RecipePrototype.Models;
using RecipePrototype.Services.Interfaces;

namespace RecipePrototype.Services
{
    public class RecipeRepository : IRepository<Recipe>
    {
        private RecipeDBContext db = new RecipeDBContext();

        public Recipe Create(Recipe item)
        {
            db.Recipes.Add(item);
            db.SaveChanges();
            return item;
        }

        public IEnumerable<Recipe> Get()
        {
            var recipes = GetRecipeDBSet();
            return recipes;
        }

        public IEnumerable<Recipe> Get(int skip, int take)
        {
            var recipes = GetRecipeDBSet();
            var recipesList = recipes.ToList().Skip(skip).Take(take);
            return recipesList;
        }

        public PagedItem<Recipe> GetPaged(int pageSize, int pageNumber)
        {
            var recipes = GetRecipeDBSet();
            var skip = (pageNumber-1)*pageSize;
            var recipesList = recipes.ToList().Skip(skip).Take(pageSize);
            return new PagedItem<Recipe>()
            {
                Payload = recipesList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = recipes.Count(),
                TotalPages = (int)Math.Ceiling((float)recipes.Count()/pageSize)
            };
        }

        public IQueryable<Recipe> GetQueryable()
        {
            var recipes = from m in db.Recipes select m;
            var ingredients = db.Ingredients.ToList();
            return recipes;
        }

        public Recipe Get(int? id)
        {
            return db.Recipes.Find(id);
        }

        public Recipe Update(Recipe item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return item;
        }

        public int Delete(int id)
        {
            var recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
            db.SaveChanges();

            return id;
        }

        private IEnumerable<Recipe> GetRecipeDBSet()
        {
            var ingredients = db.Ingredients.ToList();
            return db.Recipes;
        }
    }
}