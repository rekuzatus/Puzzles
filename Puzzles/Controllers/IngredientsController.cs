using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puzzles.Data;
using Puzzles.Data.Services;
using Puzzles.Data.Static;
using Puzzles.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class IngredientsController : Controller
    {
        private readonly IIngredientsService _service;
        public IngredientsController(IIngredientsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }


        //Get request: Ingredients/Create
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create([Bind("Name,ImageUrl,Brand")] Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return View(ingredient);
            }
            await _service.AddAsync(ingredient);
            return RedirectToAction(nameof(Index));
        }

        //Get: Ingredients/Details/Id(1)
        [AllowAnonymous]
        public async Task<IActionResult> Details (int id)
        {
            var ingredientDetails = await _service.GetByIdAsync(id);

            if (ingredientDetails == null) return View("NotFound");
            return View(ingredientDetails);
        }

        //Get request: Ingredients/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var ingredientDetails = await _service.GetByIdAsync(id);

            if (ingredientDetails == null) return View("NotFound");

            return View(ingredientDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,Name,ImageUrl,Brand")] Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return View(ingredient);
            }
            await _service.UpdateAsync(id,ingredient);
            return RedirectToAction(nameof(Index));
        }

        //Get request: Ingredients/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var ingredientDetails = await _service.GetByIdAsync(id);

            if (ingredientDetails == null) return View("NotFound");

            return View(ingredientDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
               
                var ingredientDetails = await _service.GetByIdAsync(id);

                if (ingredientDetails == null) return View("NotFound");

                await _service.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
        }
    }
}
