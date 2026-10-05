/*
 * ENCABEZADO DE AUTORÍA
 * Equipo N°: 3
 * Integrantes:
 * 1. CONTRERAS Rodriguez Janis Isabel
 * 2. TORRES Barrera Jose Angel
 * 3. MACÍAS Cruz Meredith Miranda
 */
using Biblioteca.Models;
using System;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class FrmPersona : FrmBase, IPanelCRUD
    {
        public FrmPersona()
        {
            InitializeComponent();
        }

        public void EjecutarGuardar()
        {
            try
            {
                if (!int.TryParse(txtId.Text.Trim(), out int id))
                {
                    txb_Info_frmPerso.Text = "⚠️ ERROR DE VALIDACIÓN:\r\nPor favor, ingrese un código ID numérico válido.";
                    return;
                }

                string nombre = txb_Nombre_frmPerso.Text.Trim();
                int edad = int.TryParse(txb_Edad_frmPerso.Text.Trim(), out int e) ? e : 18;
                string correo = txb_Correo_frmPerso.Text.Trim();

                // NOTA: RutaImagen y Estado (cmb_Estado_frmPerso) no se guardan porque
                // la clase Persona no tiene esas propiedades. Ver aclaración pendiente.

                Persona nuevaPersona = new Persona(id, nombre, edad, correo);
                nuevaPersona.InsertarRegistro(nuevaPersona);

                txb_Info_frmPerso.Text = "La persona ha sido guardada exitosamente.";
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                txb_Info_frmPerso.Text = "Ocurrió un error al guardar: Error de Persistencia";
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.Clear();

            if (string.IsNullOrWhiteSpace(id))
            {
                alerta.SetError(txtId, "Ingrese un ID de persona para realizar la búsqueda.");
                return;
            }

            try
            {
                if (!int.TryParse(id.Trim(), out int idBuscado))
                {
                    alerta.SetError(txtId, "El ID debe ser un número entero.");
                    return;
                }

                Persona personaEncontrada = (Persona)new Persona().ConsultarRegistro(id);

                if (personaEncontrada != null)
                {
                    txtId.Text = personaEncontrada.Id.ToString();
                    txb_Nombre_frmPerso.Text = personaEncontrada.NombreCompleto;
                    txb_Edad_frmPerso.Text = personaEncontrada.Edad.ToString();
                    txb_Correo_frmPerso.Text = personaEncontrada.Correo;
                    // cmb_Estado_frmPerso y txb_RutaIma_frmPerso no se pueden llenar:
                    // el modelo no expone esos datos.
                }
                else
                {
                    txb_Info_frmPerso.Text = "No se encontró ninguna persona registrada con el ID especificado.";
                }
            }
            catch (Exception ex)
            {
                alerta.SetError(txtId, $"Error de búsqueda: {ex.Message}");
            }
        }

        public void EjecutarActualizar()
        {
            try
            {
                if (!int.TryParse(txtId.Text.Trim(), out int id))
                {
                    txb_Info_frmPerso.Text = "Ingrese un ID numérico válido para actualizar.";
                    return;
                }

                Persona personaActualizada = new Persona(
                    id,
                    txb_Nombre_frmPerso.Text.Trim(),
                    int.TryParse(txb_Edad_frmPerso.Text.Trim(), out int e) ? e : 18,
                    txb_Correo_frmPerso.Text.Trim()
                );

                personaActualizada.ActualizarRegistro(personaActualizada);
                LimpiarCampos();
                txb_Info_frmPerso.Text = "Los datos de la persona han sido actualizados correctamente.";
            }
            catch (Exception ex)
            {
                txb_Info_frmPerso.Text = "Error al actualizar";
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            try
            {
                string idAEliminar = txtId.Text.Trim();

                if (string.IsNullOrWhiteSpace(idAEliminar))
                {
                    txb_Info_frmPerso.Text = "Debe especificar el ID de la persona a eliminar.";
                    return;
                }

                DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar a la persona con ID {idAEliminar}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    Persona personaAuxiliar = new Persona();
                    personaAuxiliar.EliminarRegistro(idAEliminar);

                    if (barraEstado != null && barraEstado.Items.Count > 0)
                    {
                        barraEstado.Items[0].Text = $"Persona con ID [{idAEliminar}] fue eliminada correctamente.";
                    }

                    LimpiarCampos();
                    txb_Info_frmPerso.Text = "El registro ha sido eliminado.";
                }
            }
            catch (Exception ex)
            {
                txb_Info_frmPerso.Text = "Error al eliminar";
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txb_Nombre_frmPerso.Clear();
            txb_Edad_frmPerso.Clear();
            txb_Correo_frmPerso.Clear();
            if (cmb_Estado_frmPerso.Items.Count > 0)
                cmb_Estado_frmPerso.SelectedIndex = 0;
            txb_RutaIma_frmPerso.Clear();
        }
    }
}