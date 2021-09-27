using Microsoft.AspNetCore.Mvc;
using Neutrinodevs.PedidosOnline.Infraestructure.Repositories;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly DishRepository _rpsDish;
        public HomeController(DishRepository dishRepository)
        {
            _rpsDish = dishRepository;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "LA PESCA";
            var dishes = _rpsDish.GetAll();
            return View(dishes);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult EndSession()
        {
            return View();
        }
    }

}
