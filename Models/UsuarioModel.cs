using MVC.config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TallerMecanico.config;

namespace TallerMecanico.Models
{
    internal class UsuarioModel
    {
        public int ID { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }

        public UsuarioModel() { }

        // Método para insertar un nuevo usuario y retornar el usuario insertado
        public static UsuarioModel Insertar(UsuarioModel usuario)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "INSERT INTO Usuarios (nombre_usuario, password, rol) " +
                                   "OUTPUT INSERTED.ID, INSERTED.nombre_usuario, INSERTED.password, INSERTED.rol " +
                                   "VALUES (@NombreUsuario, @Password, @Rol)";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                        comando.Parameters.AddWithValue("@Password", usuario.Password);
                        comando.Parameters.AddWithValue("@Rol", usuario.Rol);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new UsuarioModel
                                {
                                    ID = Convert.ToInt32(lector["ID"]),
                                    NombreUsuario = lector["nombre_usuario"].ToString(),
                                    Password = lector["password"].ToString(),
                                    Rol = lector["rol"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al insertar el usuario.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al insertar el usuario.");
            }
            return null;
        }

        // Método para actualizar un usuario existente
        public static string Actualizar(UsuarioModel usuario)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "UPDATE Usuarios SET nombre_usuario = @NombreUsuario, password = @Password, " +
                                   "rol = @Rol WHERE ID = @ID";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@ID", usuario.ID);
                        comando.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                        comando.Parameters.AddWithValue("@Password", usuario.Password);
                        comando.Parameters.AddWithValue("@Rol", usuario.Rol);
                        comando.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al actualizar el usuario.");
                return "Error SQL";
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al actualizar el usuario.");
                return "Error";
            }
        }

        // Método para eliminar un usuario
        public static string Eliminar(int idUsuario)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "DELETE FROM Usuarios WHERE ID = @ID";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@ID", idUsuario);
                        comando.ExecuteNonQuery();
                    }
                }
                return "OK";
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al eliminar el usuario.");
                return "Error SQL";
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al eliminar el usuario.");
                return "Error";
            }
        }

        // Método para obtener un usuario por ID
        public static UsuarioModel ObtenerPorId(int idUsuario)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "SELECT * FROM Usuarios WHERE ID = @ID";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@ID", idUsuario);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new UsuarioModel
                                {
                                    ID = Convert.ToInt32(lector["ID"]),
                                    NombreUsuario = lector["nombre_usuario"].ToString(),
                                    Password = lector["password"].ToString(),
                                    Rol = lector["rol"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al obtener el usuario.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al obtener el usuario.");
            }
            return null;
        }

        // Método para obtener todos los usuarios
        public static List<UsuarioModel> ObtenerTodos()
        {
            var usuarios = new List<UsuarioModel>();

            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    var consulta = "SELECT * FROM Usuarios";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                usuarios.Add(new UsuarioModel
                                {
                                    ID = Convert.ToInt32(lector["ID"]),
                                    NombreUsuario = lector["nombre_usuario"].ToString(),
                                    Password = lector["password"].ToString(),
                                    Rol = lector["rol"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorHandler.ManejarErrorSql(ex, "Error al obtener la lista de usuarios.");
            }
            catch (Exception ex)
            {
                ErrorHandler.ManejarErrorGeneral(ex, "Error al obtener la lista de usuarios.");
            }

            return usuarios;
        }

        // Método para autenticar un usuario
        public static UsuarioModel Autenticar(string nombreUsuario, string password)
        {
            try
            {
                using (var conexion = Conexion.GetConnection())
                {
                    string consulta = "SELECT * FROM Usuarios WHERE nombre_usuario = @NombreUsuario AND password = @Password";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                        comando.Parameters.AddWithValue("@Password", password);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new UsuarioModel
                                {
                                    ID = Convert.ToInt32(lector["ID"]),
                                    NombreUsuario = lector["nombre_usuario"].ToString(),
                                    Password = lector["password"].ToString(),
                                    Rol = lector["rol"].ToString()
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de SQL al autenticar el usuario: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autenticar el usuario: " + ex.Message);
            }
        }
    }
}
