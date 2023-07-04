using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Puzzles.Data;
using Puzzles.Data.Services;
using Puzzles.Data.Static;
using Puzzles.Models;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class GlassesController : Controller
    {
        private readonly IGlassesService _service;
        public GlassesController(IGlassesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allGlasses = await _service.GetAllAsync();
            return View(allGlasses);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allIngredients = await _service.GetAllAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allIngredients.Where(n => n.Name.ToLower().Contains(searchString.ToLower()));
                return View("Index", filteredResult);
            }

            return View("Index", allIngredients);
        }

        //GET : glasses/details/id (1)
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var glassDetails = await _service.GetByIdAsync(id);
            if (glassDetails == null) return View("NotFound");
            return View(glassDetails);
        }

        //GET : glasses/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,ImageUrl")] Glass glass)
        {
            if(!ModelState.IsValid) return View (glass);

            await _service.AddAsync(glass);
            return RedirectToAction(nameof(Index));
        }

        //GET : glasses/edit/id 1
        public async Task <IActionResult> Edit(int id)
        {
            var glassDetails = await _service.GetByIdAsync(id);
            if (glassDetails == null) return View("NotFound");
            return View(glassDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl")] Glass glass)
        {
            if (!ModelState.IsValid) return View(glass);
            if (id == glass.Id)
            {
                await _service.UpdateAsync(id, glass);
                return RedirectToAction(nameof(Index));
            }
            return View(glass);
            
        }

        //GET : glasses/delete/id 1
        public async Task<IActionResult> Delete(int id)
        {
            var glassDetails = await _service.GetByIdAsync(id);
            if (glassDetails == null) return View("NotFound");
            return View(glassDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var glassDetails = await _service.GetByIdAsync(id);
            if (glassDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

      

        }
    }
}
