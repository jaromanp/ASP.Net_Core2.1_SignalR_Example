using Microsoft.AspNetCore.Mvc;
using DemoNetCore2._1.Dominio.Servicios;
using DemoNetCore2._1.Dominio.Contratos.Servicios;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoNetCore2._1.Presentacion.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IProductoServicio _productoServicio;

        //public HomeController(IProductoServicio productoServicio)
        //{
        //    this._productoServicio = productoServicio;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetListadoProductos()
        {
            using (var HttpClient = new HttpClient())
            {
                HttpClient.BaseAddress = new System.Uri("http://localhost:63932/api/values/Productos");
                var responseTask = HttpClient.GetAsync("");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    return Json(readTask);
                } else
                {
                    return Json("");
                }

            }
            //var varRequestURL = "http://localhost:5000/api/Producto/GetListadoProductos";
            //var varResponseData = await Get
            //var data = _productoServicio.Listado().ToList();
            

        }

    }
}