using Microsoft.AspNetCore.Mvc;
using WebApplicationMVChw.Models;

namespace WebApplicationMVChw.Controllers
{
    public class RecipesController : Controller
    {
        private static List<Recipe> recipes = new();
        private static int _id = 1;

        public IActionResult Index()
        {
            return View(recipes);
        }

        public IActionResult Create()
        {
            var recipe = new Recipe();
            recipe.Ingredients.Add(new Ingredient());
            return View("Form", recipe);
        }

        [HttpPost]
        public IActionResult Create(Recipe recipe, string actionType)
        {
            if (actionType == "add")
            {
                recipe.Ingredients.Add(new Ingredient());
                return View("Form", recipe);
            }

            if (actionType == "remove")
            {
                if (recipe.Ingredients.Count > 0)
                    recipe.Ingredients.RemoveAt(recipe.Ingredients.Count - 1);
                return View("Form", recipe);
            }

            recipe.Id = _id++;
            recipe.CreatedAt = DateTime.Now;
            recipes.Add(recipe);

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var recipe = recipes.FirstOrDefault(r => r.Id == id);
            if (recipe == null) return NotFound();

            if (recipe.Ingredients.Count == 0)
                recipe.Ingredients.Add(new Ingredient());

            return View("Form", recipe);
        }

        [HttpPost]
        public IActionResult Edit(Recipe recipe, string actionType)
        {
            var existing = recipes.FirstOrDefault(r => r.Id == recipe.Id);
            if (existing == null) return NotFound();

            if (actionType == "add")
            {
                recipe.Ingredients.Add(new Ingredient());
                return View("Form", recipe);
            }

            if (actionType == "remove")
            {
                if (recipe.Ingredients.Count > 0)
                    recipe.Ingredients.RemoveAt(recipe.Ingredients.Count - 1);
                return View("Form", recipe);
            }

            existing.Title = recipe.Title;
            existing.Instructions = recipe.Instructions;
            existing.IsVerifiedByAdmin = recipe.IsVerifiedByAdmin;
            existing.Ingredients = recipe.Ingredients;

            return RedirectToAction("Index");
        }
    }
}
