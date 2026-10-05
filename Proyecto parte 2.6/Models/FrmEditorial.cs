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
    public partial class FrmEditorial : FrmBase, IPanelCRUD
    {
        public FrmEditorial()
        {
            InitializeComponent();
        }

        public void EjecutarGuardar()
        {
            try
            {
                if (!int.TryParse(txtId.Text.Trim(), out int id))
                {
                    txb_Info_frmEdi.Text = "⚠️ ERROR DE VALIDACIÓN:\r\nPor favor, ingrese un código ID numérico válido.";
                    return;
                }

                string nombre = txb_Nombre_frmEdi.Text.Trim();
                string pais = txb_Pais_frmEdi.Text.Trim();
                int anioFundacion = dtp_AñoFunda_frmEdi.Value.Year;
                string correo = txb_Correo_frmEdi.Text.Trim();
                string rutaImagen = txb_RutaIma_frmEdi.Text.Trim();
                bool estado = cmb_Estado_frmEdi.SelectedItem?.ToString() == "Activo";

                Editorial nuevaEditorial = new Editorial(id, nombre, pais, anioFundacion, correo, rutaImagen, estado);
                nuevaEditorial.InsertarRegistro(nuevaEditorial);

                txb_Info_frmEdi.Text = "La editorial ha sido guardada exitosamente.";
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                txb_Info_frmEdi.Text = "Ocurrió un error al guardar: Error de Persistencia";
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.Clear();

            if (string.IsNullOrWhiteSpace(id))
            {
                alerta.SetError(txtId, "Ingrese un ID de editorial para realizar la búsqueda.");
                return;
            }

            try
            {
                if (!int.TryParse(id.Trim(), out int idBuscado))
                {
                    alerta.SetError(txtId, "El ID debe ser un número entero.");
                    return;
                }

                Editorial editorialEncontrada = (Editorial)new Editorial().ConsultarRegistro(id);

                if (editorialEncontrada != null)
                {
                    txtId.Text = editorialEncontrada.Id.ToString();
                    txb_Nombre_frmEdi.Text = editorialEncontrada.Nombre;
                    txb_Pais_frmEdi.Text = editorialEncontrada.Pais;
                    dtp_AñoFunda_frmEdi.Value = new DateTime(editorialEncontrada.AnioFundacion, 1, 1);
                    txb_Correo_frmEdi.Text = editorialEncontrada.CorreoContacto;
                    txb_RutaIma_frmEdi.Text = editorialEncontrada.RutaImagen;
                    cmb_Estado_frmEdi.SelectedItem = editorialEncontrada.EsActivo ? "Activo" : "Inactivo";
                }
                else
                {
                    txb_Info_frmEdi.Text = "No se encontró ninguna editorial registrada con el ID especificado.";
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
                    txb_Info_frmEdi.Text = "Ingrese un ID numérico válido para actualizar.";
                    return;
                }

                Editorial editorialActualizada = new Editorial(
                    id,
                    txb_Nombre_frmEdi.Text.Trim(),
                    txb_Pais_frmEdi.Text.Trim(),
                    dtp_AñoFunda_frmEdi.Value.Year,
                    txb_Correo_frmEdi.Text.Trim(),
                    txb_RutaIma_frmEdi.Text.Trim(),
                    cmb_Estado_frmEdi.SelectedItem?.ToString() == "Activo"
                );

                editorialActualizada.ActualizarRegistro(editorialActualizada);
                LimpiarCampos();
                txb_Info_frmEdi.Text = "Los datos de la editorial han sido actualizados correctamente.";
            }
            catch (Exception ex)
            {
                txb_Info_frmEdi.Text = "Error al actualizar";
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            try
            {
                string idAEliminar = txtId.Text.Trim();

                if (string.IsNullOrWhiteSpace(idAEliminar))
                {
                    txb_Info_frmEdi.Text = "Debe especificar el ID de la editorial a eliminar.";
                    return;
                }

                DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar la editorial con ID {idAEliminar}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    Editorial editorialAuxiliar = new Editorial();
                    editorialAuxiliar.EliminarRegistro(idAEliminar);

                    if (barraEstado != null && barraEstado.Items.Count > 0)
                    {
                        barraEstado.Items[0].Text = $"Editorial con ID [{idAEliminar}] fue eliminada correctamente.";
                    }

                    LimpiarCampos();
                    txb_Info_frmEdi.Text = "El registro ha sido eliminado.";
                }
            }
            catch (Exception ex)
            {
                txb_Info_frmEdi.Text = "Error al eliminar";
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txb_Nombre_frmEdi.Clear();
            txb_Pais_frmEdi.Clear();
            dtp_AñoFunda_frmEdi.Value = DateTime.Now;
            txb_Correo_frmEdi.Clear();
            txb_RutaIma_frmEdi.Clear();
            cmb_Estado_frmEdi.SelectedIndex = 0;
        }
    }
} //Final