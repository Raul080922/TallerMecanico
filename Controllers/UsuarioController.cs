using TallerMecanico.Models;
using System.Collections.Generic;

namespace TallerMecanico.Controllers
{
    internal class UsuarioController
    {
        // Método para insertar un usuario
        public static UsuarioModel CrearUsuario(string nombreUsuario, string password, string rol)
        {
            var nuevoUsuario = new UsuarioModel
            {
                NombreUsuario = nombreUsuario,
                Password = password,
                Rol = rol
            };

            return UsuarioModel.Insertar(nuevoUsuario);
        }

        // Método para actualizar un usuario
        public static string ActualizarUsuario(int id, string nombreUsuario, string password, string rol)
        {
            var usuario = new UsuarioModel
            {
                ID = id,
                NombreUsuario = nombreUsuario,
                Password = password,
                Rol = rol
            };

            return UsuarioModel.Actualizar(usuario);
        }

        // Método para eliminar un usuario
        public static string EliminarUsuario(int id)
        {
            return UsuarioModel.Eliminar(id);
        }

        // Método para obtener un usuario por ID
        public static UsuarioModel ObtenerUsuarioPorId(int id)
        {
            return UsuarioModel.ObtenerPorId(id);
        }

        // Método para listar todos los usuarios
        public static List<UsuarioModel> ObtenerTodosLosUsuarios()
        {
            return UsuarioModel.ObtenerTodos();
        }

        // Método para autenticar un usuario
        public static UsuarioModel AutenticarUsuario(string nombreUsuario, string password)
        {
            return UsuarioModel.Autenticar(nombreUsuario, password);
        }
    }
}
