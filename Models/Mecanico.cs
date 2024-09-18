using TallerMecanico.config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MVC.config;

namespace TallerMecanico.Models
{
    internal class Mecanico
    {
        public string IdMecanico { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string Telefono { get; set; }

        // Constructor vacío
        public Mecanico() { }

        // Método para insertar un nuevo mecánico y retornar el registro insertado
        public static Mecanico InsertarMecanico(Mecanico mecanico)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "INSERT INTO mecanicos (nombre, especialidad, telefono) " +
                                   "VALUES (@Nombre, @Especialidad, @Telefono)";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@Nombre", mecanico.Nombre);
                        comando.Parameters.AddWithValue("@Especialidad", mecanico.Especialidad);
                        comando.Parameters.AddWithValue("@Telefono", mecanico.Telefono);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new Mecanico
                                {
                                    Nombre = lector["nombre"].ToString(),
                                    Especialidad = lector["especialidad"].ToString(),
                                    Telefono = lector["telefono"].ToString(),
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al insertar el mecánico.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al insertar el mecánico.");
            }
            return null;
        }

        // Método para actualizar un mecánico existente y retornar "OK"
        public static string ActualizarMecanico(Mecanico mecanico)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "UPDATE mecanicos SET nombre = @Nombre, especialidad = @Especialidad, telefono = @Telefono " +
                                   "WHERE mecanico_id = @IdMecanico";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Clear();
                        comando.Parameters.AddWithValue("@IdMecanico", mecanico.IdMecanico);
                        comando.Parameters.AddWithValue("@Nombre", mecanico.Nombre);
                        comando.Parameters.AddWithValue("@Especialidad", mecanico.Especialidad);
                        comando.Parameters.AddWithValue("@Telefono", mecanico.Telefono);

                        comando.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al actualizar el mecánico.");
                return "Error SQL";
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al actualizar el mecánico.");
                return "Error";
            }
        }

        // Método para eliminar un mecánico y retornar "OK"
        public static string EliminarMecanico(string idMecanico)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "DELETE FROM mecanicos WHERE mecanico_id = @IdMecanico";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdMecanico", idMecanico);
                        comando.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al eliminar el mecánico.");
                return "Error SQL";
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al eliminar el mecánico.");
                return "Error";
            }
        }

        // Método para obtener un mecánico por ID
        public static Mecanico ObtenerMecanicoPorId(string idMecanico)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "SELECT * FROM mecanicos WHERE mecanico_id = @IdMecanico";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdMecanico", idMecanico);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new Mecanico
                                {
                                    IdMecanico = lector["mecanico_id"].ToString(),
                                    Nombre = lector["nombre"].ToString(),
                                    Especialidad = lector["especialidad"].ToString(),
                                    Telefono = lector["telefono"].ToString(),
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al obtener el mecánico.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al obtener el mecánico.");
            }
            return null;
        }

        // Método para obtener una lista de mecánicos
        public static List<Mecanico> ListarMecanicos()
        {
            var mecanicos = new List<Mecanico>();

            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "SELECT * FROM mecanicos";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                mecanicos.Add(new Mecanico
                                {
                                    IdMecanico = lector["mecanico_id"].ToString(),
                                    Nombre = lector["nombre"].ToString(),
                                    Especialidad = lector["especialidad"].ToString(),
                                    Telefono = lector["telefono"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al obtener la lista de mecánicos.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al obtener la lista de mecánicos.");
            }

            return mecanicos;
        }
    }
}
