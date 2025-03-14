using Microsoft.AspNetCore.Mvc;
using progetto_settimanale_S17.Services;

namespace progetto_settimanale_S17.Controllers
{
    public class QueryController : Controller
    {
        private readonly QueryService _queryService;

        public QueryController(QueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _queryService.GetTotalVerbalByPerson();

            return View(list);
        }

        public async Task<IActionResult> Index2()
        {
            var list = await _queryService.GetTotalPointsByPerson();

            return View(list);
        }
    }
}
