using System;
using System.Collections.Generic;
using DemoNetCore2._1.Dominio.Contratos.Repositorios;
using DemoNetCore2._1.Dominio.Entidades;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using DemoNetCore2._1.Infraestructura.Hubs;

namespace DemoNetCore2._1.Infraestructura.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly IConfiguration _configuration;
        private readonly IHubContext<TablaHub> _tablaHub;

        public ProductoRepositorio(IConfiguration configuration, IHubContext<TablaHub> tablaHub)
        {
            this._configuration = configuration;
            this._tablaHub = tablaHub;
        }

        /// <summary>
        /// Se puede utilizar ASYN y Await para mejorar performance
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Producto> Get()
        {
            List<Producto> listProducto = null;
            Producto pProducto = null;
            SqlDependency dependency = null;

            var connectionString = _configuration.GetConnectionString("dbconnection");

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspGetProductos", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    #region SqlDependency

                    cmd.Notification = null;
                    dependency = new SqlDependency(cmd);
                    dependency.OnChange += DetectarCambios;
                    SqlDependency.Start(connectionString);

                    #endregion

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        listProducto = new List<Producto>();
                        while (dr.Read())
                        {
                            pProducto = new Producto()
                            {
                                ID = dr.IsDBNull(dr.GetOrdinal("ProductID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ProductID")),
                                Nombre = dr.IsDBNull(dr.GetOrdinal("Nombre")) ? "noData" : dr.GetString(dr.GetOrdinal("Nombre")),
                                Precio = dr.IsDBNull(dr.GetOrdinal("Precio")) ? Convert.ToDecimal(0) : dr.GetDecimal(dr.GetOrdinal("Precio")),
                                Stock = dr.IsDBNull(dr.GetOrdinal("Stock")) ? -1 : dr.GetInt32(dr.GetOrdinal("Stock")),
                            };
                            listProducto.Add(pProducto);
                        }
                    }
                    return listProducto;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// enviar a clientes web para que se reflejen los cambios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetectarCambios(object sender, SqlNotificationEventArgs e)
        {
            if(e.Type == SqlNotificationType.Change)
            {
                _tablaHub.Clients.All.SendAsync("ActualizarGrill");
            }
            Get();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
