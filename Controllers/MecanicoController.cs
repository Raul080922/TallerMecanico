using TallerMecanico.Models;
using System;
using System.Collections.Generic;

namespace TallerMecanico.Controllers
{
    public class MecanicoController
    {
        // Método para insertar un mecánico
        public Mecanico InsertarMecanico(Mecanico mecanico)
        {
            return Mecanico.InsertarMecanico(mecanico);
        }

        // Método para actualizar un mecánico
        public string ActualizarMecanico(Mecanico mecanico)
        {
            return Mecanico.ActualizarMecanico(mecanico);
        }

        // Método para eliminar un mecánico
        public string EliminarMecanico(string idMecanico)
        {
            return Mecanico.EliminarMecanico(idMecanico);
        }

        // Método para obtener un mecánico por ID
        public Mecanico ObtenerMecanicoPorId(string idMecanico)
        {
            return Mecanico.ObtenerMecanicoPorId(idMecanico);
        }

        // Método para obtener la lista de todos los mecánicos
        public List<Mecanico> ListarMecanicos()
        {
            return Mecanico.ListarMecanicos();
        }
    }
}
