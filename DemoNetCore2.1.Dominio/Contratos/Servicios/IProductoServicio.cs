using DemoNetCore2._1.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoNetCore2._1.Dominio.Contratos.Servicios
{
    public interface IProductoServicio : IDisposable
    {
        IEnumerable<Producto> Listado();
    }
}
