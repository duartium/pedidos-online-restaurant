using Microsoft.AspNetCore.Mvc;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Product;
using Neutrinodevs.PedidosOnline.Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private DishRepository _rpsDish;
        public HomeController()
        {
            _rpsDish = new DishRepository();
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "LA PESCA";
            var dishes = _rpsDish.GetAll();
            return View(dishes);
        }
    }
}
