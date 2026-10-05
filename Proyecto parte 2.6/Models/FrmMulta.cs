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
    public partial class FrmMulta : FrmBase, IPanelCRUD
    {
        public FrmMulta()
        {
            InitializeComponent();
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            cmb_Usuario_frmMlta.DataSource = Usuarios.ObtenerTodos();
            cmb_Usuario_frmMlta.DisplayMember = "NombreCompleto";
            cmb_Usuario_frmMlta.ValueMember = "Id";
        }

        public void EjecutarGuardar()
        {
            try
            {
                if (!int.TryParse(txtId.Text.Trim(), out int id))
                {
                    txb_Info_frmMlta.Text = "⚠️ ERROR DE VALIDACIÓN:\r\nPor favor, ingrese un código ID numérico válido.";
                    return;
                }

                if (!(cmb_Usuario_frmMlta.SelectedItem is Usuarios usuarioSeleccionado))
                {
                    txb_Info_frmMlta.Text = "⚠️ Seleccione un usuario válido.";
                    return;
                }

                string motivo = txb_Motivo_frmMlta.Text.Trim();
                decimal montoBase = nup_MonBas_frmMlta.Value;
                DateTime fechaEmision = dtp_FechaEmi_frmMlta.Value;
                bool pagada = ckb_Pagada_frmMlta.Checked;
                string rutaImagen = txb_RutaIma_frmMlta.Text.Trim();
                bool estado = ckb_ActEst_frmMlta.Checked;

                Multa nuevaMulta = new Multa(id, usuarioSeleccionado, motivo, montoBase, fechaEmision, pagada, rutaImagen, estado);
                nuevaMulta.InsertarRegistro(nuevaMulta);

                txb_Info_frmMlta.Text = "La multa ha sido guardada exitosamente.";
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                txb_Info_frmMlta.Text = "Ocurrió un error al guardar: Error de Persistencia";
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.Clear();

            if (string.IsNullOrWhiteSpace(id))
            {
                alerta.SetError(txtId, "Ingrese un ID de multa para realizar la búsqueda.");
                return;
            }

            try
            {
                if (!int.TryParse(id.Trim(), out int idBuscado))
                {
                    alerta.SetError(txtId, "El ID debe ser un número entero.");
                    return;
                }

                Multa multaEncontrada = (Multa)new Multa().ConsultarRegistro(id);

                if (multaEncontrada != null)
                {
                    txtId.Text = multaEncontrada.Id.ToString();
                    cmb_Usuario_frmMlta.SelectedValue = multaEncontrada.Usuario.Id;
                    txb_Motivo_frmMlta.Text = multaEncontrada.Motivo;
                    nup_MonBas_frmMlta.Value = multaEncontrada.MontoBase;
                    dtp_FechaEmi_frmMlta.Value = multaEncontrada.FechaEmision;
                    ckb_Pagada_frmMlta.Checked = multaEncontrada.Pagada;
                    txb_RutaIma_frmMlta.Text = multaEncontrada.RutaImagen;
                    ckb_ActEst_frmMlta.Checked = multaEncontrada.EsActivo;
                }
                else
                {
                    txb_Info_frmMlta.Text = "No se encontró ninguna multa registrada con el ID especificado.";
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
                    txb_Info_frmMlta.Text = "Ingrese un ID numérico válido para actualizar.";
                    return;
                }

                if (!(cmb_Usuario_frmMlta.SelectedItem is Usuarios usuarioSeleccionado))
                {
                    txb_Info_frmMlta.Text = "Seleccione un usuario válido.";
                    return;
                }

                Multa multaActualizada = new Multa(
                    id,
                    usuarioSeleccionado,
                    txb_Motivo_frmMlta.Text.Trim(),
                    nup_MonBas_frmMlta.Value,
                    dtp_FechaEmi_frmMlta.Value,
                    ckb_Pagada_frmMlta.Checked,
                    txb_RutaIma_frmMlta.Text.Trim(),
                    ckb_ActEst_frmMlta.Checked
                );

                multaActualizada.ActualizarRegistro(multaActualizada);
                LimpiarCampos();
                txb_Info_frmMlta.Text = "Los datos de la multa han sido actualizados correctamente.";
            }
            catch (Exception ex)
            {
                txb_Info_frmMlta.Text = "Error al actualizar";
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            try
            {
                string idAEliminar = txtId.Text.Trim();

                if (string.IsNullOrWhiteSpace(idAEliminar))
                {
                    txb_Info_frmMlta.Text = "Debe especificar el ID de la multa a eliminar.";
                    return;
                }

                DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar la multa con ID {idAEliminar}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    Multa multaAuxiliar = new Multa();
                    multaAuxiliar.EliminarRegistro(idAEliminar);

                    if (barraEstado != null && barraEstado.Items.Count > 0)
                    {
                        barraEstado.Items[0].Text = $"Multa con ID [{idAEliminar}] fue eliminada correctamente.";
                    }

                    LimpiarCampos();
                    txb_Info_frmMlta.Text = "El registro ha sido eliminado.";
                }
            }
            catch (Exception ex)
            {
                txb_Info_frmMlta.Text = "Error al eliminar";
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            if (cmb_Usuario_frmMlta.Items.Count > 0)
                cmb_Usuario_frmMlta.SelectedIndex = 0;
            txb_Motivo_frmMlta.Clear();
            nup_MonBas_frmMlta.Value = 0;
            dtp_FechaEmi_frmMlta.Value = DateTime.Now;
            ckb_Pagada_frmMlta.Checked = false;
            txb_RutaIma_frmMlta.Clear();
            ckb_ActEst_frmMlta.Checked = true;
        }
    }
}  //Final