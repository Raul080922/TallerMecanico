using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using MVC.config;

namespace TallerMecanico
{
    public class Propietario
    {
        public string IdPropietario { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        // solo para mostrar información
        public string NombreCompleto { get; set; }

        // Constructor vacío
        public Propietario() { }

        // Método para insertar un nuevo propietario y retornar el registro insertado
        public static Propietario Insertar(Propietario propietario)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "INSERT INTO propietarios (propietario_id, apellido, nombre, telefono, direccion, ciudad, estado, codigo_postal) " +
                                   "OUTPUT INSERTED.propietario_id, INSERTED.apellido, INSERTED.nombre, INSERTED.telefono, " +
                                   "INSERTED.direccion, INSERTED.ciudad, INSERTED.estado, INSERTED.codigo_postal " +
                                   "VALUES (@IdPropietario, @Apellido, @Nombre, @Telefono, @Direccion, @Ciudad, @Estado, @CodigoPostal)";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdPropietario", propietario.IdPropietario);
                        comando.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                        comando.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                        comando.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                        comando.Parameters.AddWithValue("@Direccion", propietario.Direccion);
                        comando.Parameters.AddWithValue("@Ciudad", propietario.Ciudad);
                        comando.Parameters.AddWithValue("@Estado", propietario.Estado);
                        comando.Parameters.AddWithValue("@CodigoPostal", propietario.CodigoPostal);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new Propietario
                                {
                                    IdPropietario = lector["propietario_id"].ToString(),
                                    Apellido = lector["apellido"].ToString(),
                                    Nombre = lector["nombre"].ToString(),
                                    Telefono = lector["telefono"].ToString(),
                                    Direccion = lector["direccion"].ToString(),
                                    Ciudad = lector["ciudad"].ToString(),
                                    Estado = lector["estado"].ToString(),
                                    CodigoPostal = lector["codigo_postal"].ToString(),
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al insertar el propietario.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al insertar el propietario.");
            }
            return null;
        }

        // Método para actualizar un propietario existente y retornar "OK"
        public static string Actualizar(Propietario propietario)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "UPDATE propietarios SET apellido = @Apellido, nombre = @Nombre, telefono = @Telefono, " +
                                   "direccion = @Direccion, ciudad = @Ciudad, estado = @Estado, codigo_postal = @CodigoPostal " +
                                   "WHERE propietario_id = @IdPropietario";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdPropietario", propietario.IdPropietario);
                        comando.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                        comando.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                        comando.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                        comando.Parameters.AddWithValue("@Direccion", propietario.Direccion);
                        comando.Parameters.AddWithValue("@Ciudad", propietario.Ciudad);
                        comando.Parameters.AddWithValue("@Estado", propietario.Estado);
                        comando.Parameters.AddWithValue("@CodigoPostal", propietario.CodigoPostal);

                        comando.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al actualizar el propietario.");
                return "Error SQL";
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al actualizar el propietario.");
                return "Error";
            }
        }

        // Método para eliminar un propietario y retornar "OK"
        public static string Eliminar(string idPropietario)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "DELETE FROM propietarios WHERE propietario_id = @IdPropietario";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdPropietario", idPropietario);
                        comando.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al eliminar el propietario.");
                return "Error SQL";
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al eliminar el propietario.");
                return "Error";
            }
        }

        // Método para obtener un propietario por ID
        public static Propietario ObtenerPorId(string idPropietario)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "SELECT * FROM propietarios WHERE propietario_id = @IdPropietario";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdPropietario", idPropietario);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new Propietario
                                {
                                    IdPropietario = lector["propietario_id"].ToString(),
                                    Apellido = lector["apellido"].ToString(),
                                    Nombre = lector["nombre"].ToString(),
                                    Telefono = lector["telefono"].ToString(),
                                    Direccion = lector["direccion"].ToString(),
                                    Ciudad = lector["ciudad"].ToString(),
                                    Estado = lector["estado"].ToString(),
                                    CodigoPostal = lector["codigo_postal"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al obtener el propietario.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al obtener el propietario.");
            }
            return null;
        }

        // Método para obtener todos los propietarios
        public static List<Propietario> ObtenerTodos()
        {
            var propietarios = new List<Propietario>();

            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "SELECT * FROM propietarios";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                propietarios.Add(new Propietario
                                {
                                    IdPropietario = lector["propietario_id"].ToString(),
                                    Apellido = lector["apellido"].ToString(),
                                    Nombre = lector["nombre"].ToString(),
                                    Telefono = lector["telefono"].ToString(),
                                    Direccion = lector["direccion"].ToString(),
                                    Ciudad = lector["ciudad"].ToString(),
                                    Estado = lector["estado"].ToString(),
                                    CodigoPostal = lector["codigo_postal"].ToString(),
                                    NombreCompleto = lector["nombre"].ToString() + " " + lector["apellido"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al obtener la lista de propietarios.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al obtener la lista de propietarios.");
            }

            return propietarios;
        }
    }
}
