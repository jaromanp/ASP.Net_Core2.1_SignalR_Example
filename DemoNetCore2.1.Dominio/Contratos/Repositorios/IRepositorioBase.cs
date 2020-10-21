using System;
using System.Collections.Generic;

namespace DemoNetCore2._1.Dominio.Contratos.Repositorios
{
    public interface IRepositorioBase<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> Get();
    }
}
