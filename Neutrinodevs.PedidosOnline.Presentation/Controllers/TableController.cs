using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class TableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
