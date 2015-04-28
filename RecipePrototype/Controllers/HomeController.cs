using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RecipePrototype.Models;
using RecipePrototype.Services.Interfaces;

namespace RecipePrototype.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Recipe> _recipeRepository;

        public HomeController(IRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public ActionResult Index(int pageNumber = 1)
        {
            var recipesList = _recipeRepository.GetPaged(5, pageNumber);
            var vm = new RecipesVM()
            {
                Recipes = recipesList.Payload,
                TotalRecipes = recipesList.TotalItems,
                PagesToShow =  recipesList.TotalPages
            };
            return View(vm);
        }
        
        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recipe = _recipeRepository.Get(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _recipeRepository.Create(recipe);//todo: notify of failure
                return RedirectToAction("Search");
            }

            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var recipe = _recipeRepository.Get(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _recipeRepository.Update(recipe);
                return RedirectToAction("Search");
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recipe = _recipeRepository.Get(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _recipeRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string searchTerm, int skip=0, int take=5)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Search Page.";

            var recipes = _recipeRepository.GetQueryable();

            var recipeList = new List<Recipe>();

            if (!String.IsNullOrEmpty(searchTerm))
            {
                recipeList.AddRange(recipes.Where(s => s.Name.Contains(searchTerm)).ToList());
                recipeList.AddRange(recipes.Where(s => s.MealType.ToString().Equals(searchTerm.ToLower())).ToList());
                recipeList.AddRange(recipes.Where(r => r.Ingredients.Any(i => i.Name.Contains(searchTerm))));
                recipeList.AddRange(recipes.Where(s => s.WeightWatchersPoints.ToString() == searchTerm).ToList());
                recipeList.AddRange(recipes.Where(s => s.HealthyRating.ToString().Equals(searchTerm.ToLower())).ToList());
            }
            else
            {
                recipeList = recipes.ToList();
            }

            var recipesList = recipeList.Distinct().ToList().Skip(skip).Take(take);

            var vm = new RecipesVM()
            {
                Recipes = recipesList,
                TotalRecipes = recipesList.Count(),
                PagesToShow = (int)Math.Ceiling((float)recipesList.Count() / 5)
            };

            return View(vm);
        }
    }
}