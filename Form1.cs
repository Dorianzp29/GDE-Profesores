using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Profesores.Controllers;
using Profesores.Models;

namespace Profesores
{
    public partial class frm_Profesores : Form
    {
        ProfesoresController profesoresController = new ProfesoresController();
        string profesor_id;
        public frm_Profesores()
        {
            InitializeComponent();
        }

        private void frm_Profesores_Load(object sender, EventArgs e)
        {
            cargarLista();
        }

        public void cargarLista()
        {
            lstProfes.DataSource = profesoresController.todos();
            lstProfes.DisplayMember = "DisplayName";
            lstProfes.ValueMember = "profesor_id";
        }

        private void btn_Editar_Click(object sender, EventArgs e)
        {
            if (lstProfes.SelectedItem != null)
            {
                ProfesoresModel profesorSeleccionado = (ProfesoresModel)lstProfes.SelectedItem;
                profesorSeleccionado.nombreprof = txt_Nombre.Text;
                profesorSeleccionado.apellidoprof = txt_Apellido.Text;
                profesorSeleccionado.especialidad = txt_Especialidad.Text;
                profesorSeleccionado.email = txt_Email.Text;

                
                try
                {
                    profesoresController.actualizar(profesorSeleccionado);
                    MessageBox.Show("Profesor actualizado correctamente");
                    cargarLista();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un profesor de la lista");
            }
        }

        private void btn_Grabar_Click(object sender, EventArgs e)
        {
            ProfesoresModel Profesor = new ProfesoresModel
            {
                profesor_id = Convert.ToInt32(lstProfes.SelectedValue),
                nombreprof = txt_Nombre.Text,
                apellidoprof = txt_Apellido.Text,
                especialidad = txt_Especialidad.Text,
                email = txt_Email.Text
            };
            ProfesoresController profesoresController1 = new ProfesoresController();
            try
            {
                profesoresController.agregar(Profesor);
                MessageBox.Show("Profesor actualizado");
                cargarLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar profesor: " + ex.Message);
                
            }
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (lstProfes.SelectedItem != null)
            {
                ProfesoresModel profesorSeleccionado = (ProfesoresModel)lstProfes.SelectedItem;
                ProfesoresController profesorController = new ProfesoresController();
                try
                {
                    profesorController.eliminar(profesorSeleccionado.profesor_id);
                    MessageBox.Show("Profesor eliminado correctamente");
                    cargarLista();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el profesor" + ex.Message);
                   
                }
            }
            else
            {
                MessageBox.Show("Seleccione un profesor de la lista");
            }
        }

        private void lstProfes_DoubleClick(object sender, EventArgs e)
        {
            if (lstProfes.SelectedItem != null)
            {
                ProfesoresModel profesorSeleccionado = (ProfesoresModel)lstProfes.SelectedItem;
                profesor_id = profesorSeleccionado.profesor_id.ToString();
                txt_Nombre.Text = profesorSeleccionado.nombreprof;
                txt_Apellido.Text = profesorSeleccionado.apellidoprof;
                txt_Especialidad.Text = profesorSeleccionado.especialidad;
                txt_Email.Text = profesorSeleccionado.email;
            }
            else
            {
                MessageBox.Show("Seleccione un profesor de la lista");
            }
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            txt_Nombre.Text = string.Empty;
            txt_Apellido.Text = string.Empty;
            txt_Especialidad.Text = string.Empty;
            txt_Email.Text = string.Empty;
        }

        private void btn_Regresar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
