using Microsoft.AspNetCore.Mvc;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class DishController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
