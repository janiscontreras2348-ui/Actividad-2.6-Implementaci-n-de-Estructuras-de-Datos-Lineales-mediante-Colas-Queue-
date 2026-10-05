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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class FrmAdministrador : FrmBase, IPanelCRUD
    {
        public FrmAdministrador()
        {
            InitializeComponent();
        }

        // Implementación obligatoria de los 4 métodos del contrato IPanelCRUD
        public void EjecutarGuardar()
        {
            try
            {
                if (!int.TryParse(txtId.Text.Trim(), out int id))
                {
                    txb_Info_frmAdmin.Text = "⚠️ ERROR DE VALIDACIÓN:\r\nPor favor, ingrese un código ID numérico válido.";
                    return;
                }

                string nombreCompleto = txb_Nombre_frmAdmin.Text.Trim();
                int edad = int.TryParse(txb_Edad_frmAdmin.Text.Trim(), out int e) ? e : 18;
                string correo = txb_Correo_frmAdmin.Text.Trim();
                string seleccion = cmb_NivAcc_frmAdmin.SelectedItem?.ToString() ?? "";
                int nivelAcceso = int.TryParse(seleccion.Split('-')[0].Trim(), out int n) ? n : 3;
                string departamento = txb_Depa_frmAdmin.Text.Trim();
                DateTime fechaIngreso = dtp_FechaIng_frmAdmin.Value;
                bool estado = cmb_Estado_frmAdmin.SelectedItem?.ToString() == "Activo";
                string rutaImagen = txb_RutaIma_frmAdmin.Text.Trim();

                // Instancia usando el constructor parametrizado del modelo Administrador
                Administrador nuevoAdmin = new Administrador(id, nombreCompleto, edad, correo, nivelAcceso, departamento, fechaIngreso, rutaImagen, estado);
                nuevoAdmin.InsertarRegistro(nuevoAdmin);

                txb_Info_frmAdmin.Text = "El administrador ha sido guardado exitosamente.";
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                txb_Info_frmAdmin.Text = "Ocurrió un error al guardar: Error de Persistencia";
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.Clear();

            if (string.IsNullOrWhiteSpace(id))
            {
                alerta.SetError(txtId, "Ingrese un ID de administrador para realizar la búsqueda.");
                return;
            }

            try
            {
                if (!int.TryParse(id.Trim(), out int idBuscado))
                {
                    alerta.SetError(txtId, "El ID debe ser un número entero.");
                    return;
                }

                // Consulta al modelo
                Administrador aux = new Administrador();
                object resultado = aux.ConsultarRegistro(idBuscado.ToString());

                if (resultado is Administrador adminEncontrado)
                {
                    txtId.Text = adminEncontrado.Id.ToString();
                    txb_Nombre_frmAdmin.Text = adminEncontrado.NombreCompleto;
                    txb_Edad_frmAdmin.Text = adminEncontrado.Edad.ToString();
                    txb_Correo_frmAdmin.Text = adminEncontrado.Correo;
                    cmb_NivAcc_frmAdmin.SelectedItem = adminEncontrado.NivelAcceso.ToString();
                    txb_Depa_frmAdmin.Text = adminEncontrado.Departamento;
                    dtp_FechaIng_frmAdmin.Value = adminEncontrado.FechaIngreso;
                    cmb_Estado_frmAdmin.SelectedItem = adminEncontrado.Estado ? "Activo" : "Inactivo";
                    txb_RutaIma_frmAdmin.Text = adminEncontrado.RutaImagen;
                }
                else
                {
                    txb_Info_frmAdmin.Text = "No se encontró ningún administrador registrado con el ID especificado.";
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
                    txb_Info_frmAdmin.Text = "Ingrese un ID numérico válido para actualizar.";
                    return;
                }

                Administrador adminActualizado = new Administrador(
                    id,
                    txb_Nombre_frmAdmin.Text.Trim(),
                    int.TryParse(txb_Edad_frmAdmin.Text.Trim(), out int e) ? e : 18,
                    txb_Correo_frmAdmin.Text.Trim(),
                    int.TryParse(cmb_NivAcc_frmAdmin.SelectedItem?.ToString(), out int n) ? n : 3,
                    txb_Depa_frmAdmin.Text.Trim(),
                    dtp_FechaIng_frmAdmin.Value,
                    txb_RutaIma_frmAdmin.Text.Trim(),
                    cmb_Estado_frmAdmin.SelectedItem?.ToString() == "Activo"
                );

                adminActualizado.ActualizarRegistro(adminActualizado);
                LimpiarCampos();
                txb_Info_frmAdmin.Text = "Los datos del administrador han sido actualizados correctamente.";
            }
            catch (Exception ex)
            {
                txb_Info_frmAdmin.Text = "Error al actualizar";
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            try
            {
                string idAEliminar = txtId.Text.Trim();

                if (string.IsNullOrWhiteSpace(idAEliminar))
                {
                    txb_Info_frmAdmin.Text = "Debe especificar el ID del administrador a eliminar.";
                    return;
                }

                DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar al administrador con ID {idAEliminar}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    Administrador adminAuxiliar = new Administrador();
                    adminAuxiliar.EliminarRegistro(idAEliminar);

                    if (barraEstado != null && barraEstado.Items.Count > 0)
                    {
                        barraEstado.Items[0].Text = $"Administrador con ID [{idAEliminar}] fue eliminado correctamente.";
                    }

                    LimpiarCampos();
                    txb_Info_frmAdmin.Text = "El registro ha sido eliminado.";
                }
            }
            catch (Exception ex)
            {
                txb_Info_frmAdmin.Text = "Error al eliminar";
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txb_Nombre_frmAdmin.Clear();
            txb_Edad_frmAdmin.Clear();
            txb_Correo_frmAdmin.Clear();
            txb_Depa_frmAdmin.Clear();
            dtp_FechaIng_frmAdmin.Value = DateTime.Now;
            txb_RutaIma_frmAdmin.Clear();

            if (cmb_NivAcc_frmAdmin.Items.Count > 0)
                cmb_NivAcc_frmAdmin.SelectedIndex = 0;

            if (cmb_Estado_frmAdmin.Items.Count > 0)
                cmb_Estado_frmAdmin.SelectedIndex = 0;
        }
    }
}