using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoNetCore2._1.Dominio.Contratos.Servicios;
using DemoNetCore2._1.Infraestructura.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DemoNetCore2._1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IProductoServicio _productoServicio;

        private readonly IHubContext<TablaHub> _hub;

        public ValuesController(IProductoServicio productoServicio, IHubContext<TablaHub> hub)
        {
            this._productoServicio = productoServicio;
            this._hub = hub;
        }

        [HttpGet]
        [Route("Productos")]
        public IActionResult GetListadoProductos()
        {
            var data = _productoServicio.Listado().ToList();
            return new OkObjectResult(data);

        }

        //[HttpGet]
        //[Route("ProductosSignal")]
        //public async Task<IActionResult> GetListadoProductosSignalAsync()
        //{
        //    await _hub.Clients.All.SendAsync("Notify", $"Home page loaded at: {DateTime.Now}");
        //    //return new OkObjectResult(data);

        //}

        public IActionResult Get()
        {
            var data = _hub.Clients.All.SendAsync("ActualizarGrill", _productoServicio.Listado().ToList());
            return Ok(new { Message = "Request Completed" });
        }        

        // GET api/values
        [HttpGet]
        [Route("Prueba")]
        public ActionResult<IEnumerable<string>> Prueba()
        {
            return new string[] { "Prueba", "Prueba" };
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
