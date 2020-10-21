using Microsoft.AspNetCore.Mvc;
using DemoNetCore2._1.Dominio.Servicios;
using DemoNetCore2._1.Dominio.Contratos.Servicios;
using System.Linq;

namespace DemoNetCore2._1.Presentacion.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductoServicio _productoServicio;

        public HomeController(IProductoServicio productoServicio)
        {
            this._productoServicio = productoServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetListadoProductos()
        {
            var data = _productoServicio.Listado().ToList();
            return Json(data);

        }

    }
}