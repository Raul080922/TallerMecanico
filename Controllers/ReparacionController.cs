using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.config;

namespace TallerMecanico.Controllers
{
    internal class ReparacionController
    {
        public string IdReparacion { get; set; }
        public string Descripcion { get; set; }
        public string Vehiculo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal Costo { get; set; }

        // Constructor vacío
        public ReparacionController() { }

        // Método para insertar una nueva reparación y retornar el registro insertado
        public static ReparacionController InsertarReparacion(ReparacionController reparacion)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "INSERT INTO Reparaciones (id_reparacion, descripcion, vehiculo, fecha_ingreso, fecha_entrega, costo) " +
                                   "VALUES (@IdReparacion, @Descripcion, @Vehiculo, @FechaIngreso, @FechaEntrega, @Costo)";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdReparacion", reparacion.IdReparacion);
                        comando.Parameters.AddWithValue("@Descripcion", reparacion.Descripcion);
                        comando.Parameters.AddWithValue("@Vehiculo", reparacion.Vehiculo);
                        comando.Parameters.AddWithValue("@FechaIngreso", reparacion.FechaIngreso);
                        comando.Parameters.AddWithValue("@FechaEntrega", reparacion.FechaEntrega);
                        comando.Parameters.AddWithValue("@Costo", reparacion.Costo);

                        comando.ExecuteNonQuery();
                    }
                }
                return reparacion;
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al insertar la reparación.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al insertar la reparación.");
            }
            return null;
        }

        // Método para actualizar una reparación existente y retornar "OK"
        public static string ActualizarReparacion(ReparacionController reparacion)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "UPDATE Reparaciones SET descripcion = @Descripcion, vehiculo = @Vehiculo, " +
                                   "fecha_ingreso = @FechaIngreso, fecha_entrega = @FechaEntrega, costo = @Costo " +
                                   "WHERE id_reparacion = @IdReparacion";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdReparacion", reparacion.IdReparacion);
                        comando.Parameters.AddWithValue("@Descripcion", reparacion.Descripcion);
                        comando.Parameters.AddWithValue("@Vehiculo", reparacion.Vehiculo);
                        comando.Parameters.AddWithValue("@FechaIngreso", reparacion.FechaIngreso);
                        comando.Parameters.AddWithValue("@FechaEntrega", reparacion.FechaEntrega);
                        comando.Parameters.AddWithValue("@Costo", reparacion.Costo);

                        comando.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al actualizar la reparación.");
                return "Error SQL";
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al actualizar la reparación.");
                return "Error";
            }
        }

        // Método para eliminar una reparación y retornar "OK"
        public static string EliminarReparacion(string idReparacion)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "DELETE FROM Reparaciones WHERE id_reparacion = @IdReparacion";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdReparacion", idReparacion);
                        comando.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al eliminar la reparación.");
                return "Error SQL";
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al eliminar la reparación.");
                return "Error";
            }
        }

        // Método para obtener una reparación por ID
        public static ReparacionController ObtenerReparacionPorId(string idReparacion)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "SELECT * FROM Reparaciones WHERE id_reparacion = @IdReparacion";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdReparacion", idReparacion);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new ReparacionController
                                {
                                    IdReparacion = lector["id_reparacion"].ToString(),
                                    Descripcion = lector["descripcion"].ToString(),
                                    Vehiculo = lector["vehiculo"].ToString(),
                                    FechaIngreso = Convert.ToDateTime(lector["fecha_ingreso"]),
                                    FechaEntrega = Convert.ToDateTime(lector["fecha_entrega"]),
                                    Costo = Convert.ToDecimal(lector["costo"])
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al obtener la reparación.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al obtener la reparación.");
            }
            return null;
        }

        // Método para listar todas las reparaciones
        public static List<ReparacionController> ListarReparaciones()
        {
            var reparaciones = new List<ReparacionController>();

            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "SELECT * FROM Reparaciones";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                reparaciones.Add(new ReparacionController
                                {
                                    IdReparacion = lector["id_reparacion"].ToString(),
                                    Descripcion = lector["descripcion"].ToString(),
                                    Vehiculo = lector["vehiculo"].ToString(),
                                    FechaIngreso = Convert.ToDateTime(lector["fecha_ingreso"]),
                                    FechaEntrega = Convert.ToDateTime(lector["fecha_entrega"]),
                                    Costo = Convert.ToDecimal(lector["costo"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al obtener la lista de reparaciones.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al obtener la lista de reparaciones.");
            }
            return reparaciones;
        }
    }
}
