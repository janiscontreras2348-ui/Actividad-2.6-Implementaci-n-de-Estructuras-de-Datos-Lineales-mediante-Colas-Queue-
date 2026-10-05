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
    public partial class FrmUsuario : FrmBase, IPanelCRUD
    {
        public FrmUsuario()
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
                    txb_Info_frmUsua.Text = "⚠️ ERROR DE VALIDACIÓN:\r\nPor favor, ingrese un código ID numérico válido.";
                    return;
                }

                string nombre = txb_Nombre_frmUsua.Text.Trim();
                int edad = int.TryParse(txb_Edad_frmUsua.Text.Trim(), out int e) ? e : 18;
                string correo = txb_Correo_frmUsua.Text.Trim();
                string nacionalidad = txb_Nacionalidad_frmUsua.Text.Trim();
                int librosPrestados = int.TryParse(txb_LibPres_frmUsua.Text.Trim(), out int lp) ? lp : 0;
                bool esProfesor = ckb_EsProfe_frmUsua.Checked;
                bool estado = ckb_Activo_frmUsua.Checked;

                // Valores por defecto para atributos no presentes en la interfaz
                decimal multaAcumulada = 0m;
                string rutaImagen = "usuario_default.png";

                // Instancia usando el constructor completo de la clase Usuarios
                Usuarios nuevoUsuario = new Usuarios(id, nombre, edad, correo, librosPrestados, multaAcumulada, esProfesor, rutaImagen, estado);
                nuevoUsuario.InsertarRegistro(nuevoUsuario);

                txb_Info_frmUsua.Text = "El usuario ha sido guardado exitosamente.";
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                txb_Info_frmUsua.Text = "Ocurrió un error al guardar";
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.Clear();

            if (string.IsNullOrWhiteSpace(id))
            {
                alerta.SetError(txtId, "Ingrese un ID de usuario para realizar la búsqueda.");
                return;
            }

            try
            {
                if (!int.TryParse(id.Trim(), out int idBuscado))
                {
                    alerta.SetError(txtId, "El ID debe ser un número entero.");
                    return;
                }

                // Búsqueda directa sobre la colección estática de Usuarios
                Usuarios usuarioEncontrado = Usuarios.ObtenerTodos().Find(u => u.Id == idBuscado);

                if (usuarioEncontrado != null)
                {
                    txtId.Text = usuarioEncontrado.Id.ToString();
                    txb_Nombre_frmUsua.Text = usuarioEncontrado.NombreCompleto;
                    txb_Edad_frmUsua.Text = usuarioEncontrado.Edad.ToString();
                    txb_Correo_frmUsua.Text = usuarioEncontrado.Correo;
                    txb_LibPres_frmUsua.Text = usuarioEncontrado.LibrosPrestados.ToString();
                    ckb_EsProfe_frmUsua.Checked = usuarioEncontrado.EsProfesor;
                    ckb_Activo_frmUsua.Checked = usuarioEncontrado.EsActivo;
                }
                else
                {
                    txb_Info_frmUsua.Text = "No se encontró ningún usuario registrado con el ID especificado.";
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
                    txb_Info_frmUsua.Text = "Ingrese un ID numérico válido para actualizar.";
                    return;
                }

                Usuarios usuarioActualizado = new Usuarios(
                    id,
                    txb_Nombre_frmUsua.Text.Trim(),
                    int.TryParse(txb_Edad_frmUsua.Text.Trim(), out int e) ? e : 18,
                    txb_Correo_frmUsua.Text.Trim(),
                    int.TryParse(txb_LibPres_frmUsua.Text.Trim(), out int lp) ? lp : 0,
                    0m, // Multa acumulada
                    ckb_EsProfe_frmUsua.Checked,
                    "usuario_default.png",
                    ckb_Activo_frmUsua.Checked
                );

                usuarioActualizado.ActualizarRegistro(usuarioActualizado);
                LimpiarCampos();
                txb_Info_frmUsua.Text = "Los datos del usuario han sido actualizados correctamente.";
            }
            catch (Exception ex)
            {
                txb_Info_frmUsua.Text = "Error al actualizar";
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            try
            {
                string idAEliminar = txtId.Text.Trim();

                if (string.IsNullOrWhiteSpace(idAEliminar))
                {
                    txb_Info_frmUsua.Text = "Debe especificar el ID del usuario a eliminar.";
                    return;
                }

                DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar al usuario con ID {idAEliminar}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    Usuarios usuarioAuxiliar = new Usuarios();
                    usuarioAuxiliar.EliminarRegistro(idAEliminar);

                    if (barraEstado != null && barraEstado.Items.Count > 0)
                    {
                        barraEstado.Items[0].Text = $"Usuario con ID [{idAEliminar}] fue eliminado correctamente.";
                    }

                    LimpiarCampos();
                    txb_Info_frmUsua.Text = "El registro ha sido eliminado.";
                }
            }
            catch (Exception ex)
            {
                txb_Info_frmUsua.Text = "Error al eliminar";
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txb_Nombre_frmUsua.Clear();
            txb_Edad_frmUsua.Clear();
            txb_Correo_frmUsua.Clear();
            txb_Nacionalidad_frmUsua.Clear();
            txb_LibPres_frmUsua.Clear();
            ckb_EsProfe_frmUsua.Checked = false;
            ckb_Activo_frmUsua.Checked = true;
        }
    }
}