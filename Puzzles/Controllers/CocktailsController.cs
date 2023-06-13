using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Puzzles.Data;
using Puzzles.Data.Services;
using Puzzles.Data.Static;
using Puzzles.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CocktailsController : Controller
    {
        private readonly ICocktailsService _service;
        public CocktailsController(ICocktailsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCocktails = await _service.GetAllAsync();
            return View(allCocktails);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allCocktails = await _service.GetAllAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allCocktails.Where(n => n.Name.ToLower().Contains(searchString.ToLower()));
                return View("Index", filteredResult);
            }

            return View("Index", allCocktails);
        }


        //GET: Cocktails/Details/id
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cocktailDetail = await _service.GetCocktailByIdAsync(id);
            return View(cocktailDetail);
        }

        //GET: Cocktails/Create
        public async Task <IActionResult> Create()
        {
            var cocktailDropdownsData = await _service.GetNewCocktailDropdownsValues();

            ViewBag.Glasses = new SelectList(cocktailDropdownsData.Glasses, "Id", "Name");
            ViewBag.Ingredients = new SelectList(cocktailDropdownsData.Ingredients, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewCocktailVM cocktail)
        {
            if (!ModelState.IsValid)
            {
                var cocktailDropdownsData = await _service.GetNewCocktailDropdownsValues();

                ViewBag.Glasses = new SelectList(cocktailDropdownsData.Glasses, "Id", "Name");
                ViewBag.Ingredients = new SelectList(cocktailDropdownsData.Ingredients, "Id", "Name");

                return View(cocktail);
            }
            await _service.AddNewCocktailAsync(cocktail);

            return RedirectToAction(nameof(Index));
        }

        //GET: Cocktails/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var cocktailDetails = await _service.GetCocktailByIdAsync(id);
            if (cocktailDetails == null) return View("NotFound");

            var response = new NewCocktailVM()
            {
                Id = cocktailDetails.Id,
                Name = cocktailDetails.Name,
                Description = cocktailDetails.Description,
                Price = cocktailDetails.Price,
                ImageUrl = cocktailDetails.ImageUrl,
                DrinkCategory = cocktailDetails.DrinkCategory,
                GlassId = cocktailDetails.GlassId,
                IngredientIds = cocktailDetails.Ingredients_Cocktails.Select(x => x.IngredientId).ToList()
            };

            var cocktailDropdownsData = await _service.GetNewCocktailDropdownsValues();

            ViewBag.Glasses = new SelectList(cocktailDropdownsData.Glasses, "Id", "Name");
            ViewBag.Ingredients = new SelectList(cocktailDropdownsData.Ingredients, "Id", "Name");
            return View(response);  
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewCocktailVM cocktail)
        {
            if (id != cocktail.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var cocktailDropdownsData = await _service.GetNewCocktailDropdownsValues();

                ViewBag.Glasses = new SelectList(cocktailDropdownsData.Glasses, "Id", "Name");
                ViewBag.Ingredients = new SelectList(cocktailDropdownsData.Ingredients, "Id", "Name");

                return View(cocktail);
            }
            await _service.UpdateCocktailAsync(cocktail);

            return RedirectToAction(nameof(Index));
        }
    }
}
