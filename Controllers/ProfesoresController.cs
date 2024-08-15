using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Profesores.Models;
namespace Profesores.Controllers
{
    internal class ProfesoresController
    {
        private ProfesoresModel profesorModel = new ProfesoresModel();

        public List<ProfesoresModel> todos()
        {
            List<ProfesoresModel> listaProfesores = new List<ProfesoresModel>();
            listaProfesores = profesorModel.Todos();
            return listaProfesores;
        }

        public void agregar(ProfesoresModel Profesor)
        {
            Profesor.agregar();
        }

        public void actualizar(ProfesoresModel Profesor)
        {
            Profesor.Actualizar();
        }

        public void eliminar(int id)
        {
            profesorModel.Eliminar(id);
        }
    }
}
