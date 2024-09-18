using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TallerMecanico;

namespace TallerMecanico.Controllers
{
    public class PropietarioController
    {
        // Método para insertar un propietario
        public Propietario InsertarPropietario(Propietario propietario)
        {
            return Propietario.Insertar(propietario);
        }

        // Método para actualizar un propietario
        public string ActualizarPropietario(Propietario propietario)
        {
            return Propietario.Actualizar(propietario);
        }

        // Método para eliminar un propietario
        public string EliminarPropietario(string idPropietario)
        {
            return Propietario.Eliminar(idPropietario);
        }

        // Método para obtener un propietario por ID
        public Propietario ObtenerPropietarioPorId(string idPropietario)
        {
            return Propietario.ObtenerPorId(idPropietario);
        }

        // Método para obtener todos los propietarios
        public List<Propietario> ObtenerTodosLosPropietarios()
        {
            return Propietario.ObtenerTodos();
        }
    }
}
