using DemoNetCore2._1.Dominio.Contratos.Repositorios;
using DemoNetCore2._1.Dominio.Contratos.Servicios;
using DemoNetCore2._1.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace DemoNetCore2._1.Dominio.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IProductoRepositorio _productoRepositorio;

        public ProductoServicio(IProductoRepositorio productoRepositorio)
        {
            this._productoRepositorio = productoRepositorio;
        }

        public IEnumerable<Producto> Listado()
        {
            return _productoRepositorio.Get();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
