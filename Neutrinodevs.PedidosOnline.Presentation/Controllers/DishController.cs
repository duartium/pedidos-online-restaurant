using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Dish;
using Neutrinodevs.PedidosOnline.Domain.DTOs.User;
using Neutrinodevs.PedidosOnline.Infraestructure.Repositories;
using Newtonsoft.Json;
using System.IO;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class DishController : Controller
    {
        private readonly DishRepository _rpsDish;
        private readonly ILogger<DishRepository> _logger;
        private readonly IHostingEnvironment _hosting;
        private readonly IImageRepository _rpsImage;

        public DishController(DishRepository dishRepository, ILogger<DishRepository> logger, 
            IHostingEnvironment hostingEnvironment, IImageRepository imageRepository)
        {
            _rpsDish = dishRepository;
            _logger = logger;
            _hosting = hostingEnvironment;
            _rpsImage = imageRepository;
        }
        public IActionResult Index()
        {
            ViewBag.User = new UserAuthenticateDto { IdRole = 4 };
            return View();
        }

        public IActionResult New()
        {
            ViewBag.User = new UserAuthenticateDto { IdRole = 4 };
            return View();
        }

        [HttpPost]
        public JsonResult GetAll()
        {
            try
            {
                var dishes = _rpsDish.GetAll();
                return Json(JsonConvert.SerializeObject(dishes));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json("999");
            }
        }

        [HttpPost]
        public JsonResult GetProducts()
        {
            try
            {
                var products = _rpsDish.GetProducts();
                return Json(JsonConvert.SerializeObject(products));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json("999");
            }
        }

        [HttpPost]
        public JsonResult GetDrinks()
        {
            try
            {
                var drinks = _rpsDish.GetDrinks();
                return Json(JsonConvert.SerializeObject(drinks));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json("999");
            }
        }

        [HttpPost]
        public JsonResult New([FromBody] DishDto dish)
        {
            try
            {
                if (dish == null) return Json("002");

                int resp = _rpsDish.Save(dish);
                if(resp > 0)
                    return Json("000");
                else
                    return Json("001");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json("999");
            }
        }

        [HttpPost]
        public JsonResult SaveResource(IFormFile image)
        {
            try
            {
                if (image == null) return Json("002");

                string path = _hosting.WebRootPath;
                string pathImage = Path.Combine($"{path}\\images\\products\\{image.FileName}");
                
                using (var fileSteam = new FileStream(pathImage, FileMode.Create))
                {
                    image.CopyToAsync(fileSteam);
                }

                return Json("000");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json("001");
            }
        }
    }
}
