using MVC.config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TallerMecanico.Models
{
    internal class Vehiculo
    {
        public string IdVehiculo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public string Color { get; set; }

        // Constructor vacío
        public Vehiculo() { }

        // Método para insertar un nuevo vehículo y retornar el registro insertado
        public static Vehiculo InsertarVehiculo(Vehiculo vehiculo)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "INSERT INTO Vehiculos (IdVehiculo, Marca, Modelo, Año, Color) VALUES (@IdVehiculo, @Marca, @Modelo, @Año, @Color)";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdVehiculo", vehiculo.IdVehiculo);
                        comando.Parameters.AddWithValue("@Marca", vehiculo.Marca);
                        comando.Parameters.AddWithValue("@Modelo", vehiculo.Modelo);
                        comando.Parameters.AddWithValue("@Año", vehiculo.Año);
                        comando.Parameters.AddWithValue("@Color", vehiculo.Color);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new Vehiculo
                                {
                                    IdVehiculo = lector["IdVehiculo"].ToString(),
                                    Marca = lector["Marca"].ToString(),
                                    Modelo = lector["Modelo"].ToString(),
                                    Año = Convert.ToInt32(lector["Año"]),
                                    Color = lector["Color"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al insertar el vehículo.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al insertar el vehículo.");
            }
            return null;
        }

        // Método para actualizar un vehículo existente y retornar "OK"
        public static string ActualizarVehiculo(Vehiculo vehiculo)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "UPDATE Vehiculos SET Marca = @Marca, Modelo = @Modelo, Año = @Año, Color = @Color WHERE IdVehiculo = @IdVehiculo";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdVehiculo", vehiculo.IdVehiculo);
                        comando.Parameters.AddWithValue("@Marca", vehiculo.Marca);
                        comando.Parameters.AddWithValue("@Modelo", vehiculo.Modelo);
                        comando.Parameters.AddWithValue("@Año", vehiculo.Año);
                        comando.Parameters.AddWithValue("@Color", vehiculo.Color);

                        comando.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al actualizar el vehículo.");
                return "Error SQL";
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al actualizar el vehículo.");
                return "Error";
            }
        }

        // Método para eliminar un vehículo y retornar "OK"
        public static string EliminarVehiculo(string idVehiculo)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "DELETE FROM Vehiculos WHERE IdVehiculo = @IdVehiculo";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdVehiculo", idVehiculo);
                        comando.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al eliminar el vehículo.");
                return "Error SQL";
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al eliminar el vehículo.");
                return "Error";
            }
        }

        // Método para obtener un vehículo por ID
        public static Vehiculo ObtenerVehiculoPorId(string idVehiculo)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "SELECT * FROM Vehiculos WHERE IdVehiculo = @IdVehiculo";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdVehiculo", idVehiculo);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new Vehiculo
                                {
                                    IdVehiculo = lector["IdVehiculo"].ToString(),
                                    Marca = lector["Marca"].ToString(),
                                    Modelo = lector["Modelo"].ToString(),
                                    Año = Convert.ToInt32(lector["Año"]),
                                    Color = lector["Color"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al obtener el vehículo.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al obtener el vehículo.");
            }
            return null;
        }

        // Método para listar todos los vehículos
        public static List<Vehiculo> ListarVehiculos()
        {
            var vehiculos = new List<Vehiculo>();

            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "SELECT * FROM Vehiculos";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                vehiculos.Add(new Vehiculo
                                {
                                    IdVehiculo = lector["IdVehiculo"].ToString(),
                                    Marca = lector["Marca"].ToString(),
                                    Modelo = lector["Modelo"].ToString(),
                                    Año = Convert.ToInt32(lector["Año"]),
                                    Color = lector["Color"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al obtener la lista de vehículos.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al obtener la lista de vehículos.");
            }
            return vehiculos;
        }
    }
}
