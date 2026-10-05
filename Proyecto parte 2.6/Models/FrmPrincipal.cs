/*
 * ENCABEZADO DE AUTORÍA
 * Equipo N°: 3
 * Integrantes:
 * 1. CONTRERAS Rodriguez Janis Isabel
 * 2. TORRES Barrera Jose Angel
 * 3. MACÍAS Cruz Meredith Miranda
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class FrmPrincipal : Form
    {
        private IPanelCRUD vistaActiva;
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void CargarVistaHija(Form formularioHijo)
        {
            pnlContenedorVistas.Controls.Clear();// A. Limpiar cualquier vista que estuviera cargada previamente en el panel central

            // B. Anular sus propiedades de ventana externa para que actúe como un control embebido
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;

            // C. Incrustarlo en los controles del pnlContenedorVistas y mostrarlo
            pnlContenedorVistas.Controls.Add(formularioHijo);
            pnlContenedorVistas.Tag = formularioHijo;
            formularioHijo.Show();

            vistaActiva = (IPanelCRUD)formularioHijo;// D. Invocación Abstracta / Polimorfismo: Asignar directamente el formulario a vistaActiva
        }

        //Botones que llaman a las otras clases / ventanas
        private void btnAutores_Click(object sender, EventArgs e)
        {
            CargarVistaHija(new FrmAutor());
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            CargarVistaHija(new FrmUsuario());
        }

        private void btnLibros_Click(object sender, EventArgs e)
        {
            CargarVistaHija(new FrmLibros());
        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            CargarVistaHija(new FrmPrestamo());
        }

        private void btnPersona_Click(object sender, EventArgs e)
        {
            CargarVistaHija(new FrmPersona());
        }

        private void btnGenero_Click(object sender, EventArgs e)
        {
            CargarVistaHija(new FrmGenero());
        }

        private void btnReserva_Click(object sender, EventArgs e)
        {
            CargarVistaHija(new FrmReserva());
        }

        private void btnMulta_Click(object sender, EventArgs e)
        {
            CargarVistaHija(new FrmMulta());
        }

        private void btnEditorial_Click(object sender, EventArgs e)
        {
            CargarVistaHija(new FrmEditorial());
        }

        private void btnAdministrador_Click(object sender, EventArgs e)
        {
            CargarVistaHija(new FrmAdministrador());
        }


        //Botones maestros
        private void btnMasterGuardar_Click(object sender, EventArgs e)
        {
            if (vistaActiva != null)
            {
                vistaActiva.EjecutarGuardar();// Llamada limpia y polimórfica
            }
            else
            {
                MessageBox.Show("Seleccione un módulo antes de guardar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMasterActualizar_Click(object sender, EventArgs e)
        {
            if (vistaActiva != null)
            {
                vistaActiva.EjecutarActualizar();
            }
            else
            {
                MessageBox.Show("Seleccione un módulo antes de actualizar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMasterEliminar_Click(object sender, EventArgs e)
        {
            if (vistaActiva != null)
            {
                vistaActiva.EjecutarEliminar(statusStrip1);
            }
            else
            {
                MessageBox.Show("Seleccione un módulo antes de eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMasterBuscar_Click(object sender, EventArgs e)
        {
            if (vistaActiva != null)
            {
                vistaActiva.EjecutarBuscar(txtIdBusqueda.Text.Trim(), errorProvider1);
            }
            else
            {
                MessageBox.Show("Seleccione un módulo antes de buscar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
