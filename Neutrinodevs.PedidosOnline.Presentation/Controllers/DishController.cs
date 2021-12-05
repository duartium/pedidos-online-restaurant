using Microsoft.AspNetCore.Mvc;
using Neutrinodevs.PedidosOnline.Domain.DTOs.User;
using Neutrinodevs.PedidosOnline.Infraestructure.Repositories;
using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class DishController : Controller
    {
        private readonly DishRepository _rpsDish;
        public DishController(DishRepository dishRepository)
        {
            _rpsDish = dishRepository;
        }
        public IActionResult Index()
        {
            ViewBag.User = new UserAuthenticateDto { IdRole = 4 };
            return View();
        }

        [HttpPost]
        public JsonResult GetAll()
        {
            var dishes = _rpsDish.GetAll();
            return Json(JsonConvert.SerializeObject(dishes));
        }

        [HttpPost]
        public JsonResult GetDrinks()
        {
            var drinks = _rpsDish.GetDrinks();
            return Json(JsonConvert.SerializeObject(drinks));
        }
    }
}
