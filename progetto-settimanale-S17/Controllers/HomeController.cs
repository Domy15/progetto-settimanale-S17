using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using progetto_settimanale_S17.Models;
using progetto_settimanale_S17.Services;
using progetto_settimanale_S17.ViewModels;

namespace progetto_settimanale_S17.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _homeService;

        public HomeController(HomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index(int selectedFilter)
        {
            var verbalViewModel = new HomeViewModel();

            switch (selectedFilter)
            {
                case 1:
                    verbalViewModel.Verbali = await _homeService.GetContestableVerbals();
                    break;

                case 2:
                    verbalViewModel.Verbali = await _homeService.GetVerbalsWithHighPoints();
                    break;

                case 3:
                    verbalViewModel.Verbali = await _homeService.GetVerbalsWithHighImport();
                    break;

                default:
                    verbalViewModel.Verbali = await _homeService.GetVerbals();
                    break;
            }
            return View(verbalViewModel);
        }

        public async Task<IActionResult> AddVerbal()
        {
            var violations = await _homeService.GetViolations();

            var violationsViewModel = new AddVerbalViewModel()
            {
                violazioni = violations
            };

            return View(violationsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVerbal(AddVerbalViewModel addVerbalViewModel)
        {
            ModelState.Remove("Violazioni");

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Errore nel salvataggio dei dati";
                return RedirectToAction("AddVerbal");
            }

            var result = await _homeService.CreateVerbalAsync(addVerbalViewModel);

            if (!result)
            {
                TempData["Error"] = "Errore nel salvataggio dei dati";
                return RedirectToAction("AddVerbal");
            }

            return RedirectToAction("Index");
        }
    }
}
