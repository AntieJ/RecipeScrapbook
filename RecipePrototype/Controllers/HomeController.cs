using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RecipePrototype.Models;

namespace RecipePrototype.Controllers
{
    public class HomeController : Controller
    {
        private RecipeDBContext db = new RecipeDBContext();

        public ActionResult Index()
        {
            //http://blog.stevensanderson.com/2008/12/22/editing-a-variable-length-list-of-items-in-aspnet-mvc/
            var recipes = db.Recipes.ToList();
            var ingredients = db.Ingredients.ToList();
            return View(recipes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
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
        public ActionResult Create([Bind(Include = "ID,Name,Method,WeightWatchersPoints,MealType,HealthyRating")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipe);
                db.SaveChanges();
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
            Recipe recipe = db.Recipes.Find(id);
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
        public ActionResult Edit([Bind(Include = "ID,Name,Method,WeightWatchersPoints,MealType,HealthyRating")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
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
            Recipe recipe = db.Recipes.Find(id);
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
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            ViewBag.Message = "Search Page.";
            var recipes = db.Recipes.ToList();
            var ingredients = db.Ingredients.ToList();
            return View(recipes);
        }

        [HttpPost]
        public ActionResult Search(string searchTerm)
        {
            ViewBag.Message = "Search Page.";

            var recipes = from m in db.Recipes
                          select m;
            var ingredients = db.Ingredients.ToList();
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

            return View(recipeList.Distinct());
        }

    }
}