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
    public partial class FrmGenero : FrmBase, IPanelCRUD
    {
        public FrmGenero()
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
                    txb_Info_frmGene.Text = "⚠️ ERROR DE VALIDACIÓN:\r\nPor favor, ingrese un código ID numérico válido.";
                    return;
                }

                string nombre = txb_Nombre_frmGene.Text.Trim();
                string descripcion = txb_Descripcion_frmGene.Text.Trim();
                string rutaImagen = txb_RutaIma_frmGene.Text.Trim();
                bool estado = ckb_Activo_frmGene.Checked;

                // Instancia del modelo Genero usando su constructor parametrizado
                Genero nuevoGenero = new Genero(id, nombre, descripcion, rutaImagen, estado);
                nuevoGenero.InsertarRegistro(nuevoGenero);

                txb_Info_frmGene.Text = "El género ha sido guardado exitosamente.";
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                txb_Info_frmGene.Text = "Ocurrió un error al guardar: Error de Persistencia";
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.Clear();

            if (string.IsNullOrWhiteSpace(id))
            {
                alerta.SetError(txtId, "Ingrese un ID de género para realizar la búsqueda.");
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
                Genero aux = new Genero();
                object resultado = aux.ConsultarRegistro(idBuscado.ToString());

                if (resultado is Genero generoEncontrado)
                {
                    txtId.Text = generoEncontrado.Id.ToString();
                    txb_Nombre_frmGene.Text = generoEncontrado.Nombre;
                    txb_Descripcion_frmGene.Text = generoEncontrado.Descripcion;
                    txb_RutaIma_frmGene.Text = generoEncontrado.RutaImagen;
                    ckb_Activo_frmGene.Checked = generoEncontrado.EsActivo;
                }
                else
                {
                    txb_Info_frmGene.Text = "No se encontró ningún género registrado con el ID especificado.";
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
                    txb_Info_frmGene.Text = "Ingrese un ID numérico válido para actualizar.";
                    return;
                }

                Genero generoActualizado = new Genero(
                    id,
                    txb_Nombre_frmGene.Text.Trim(),
                    txb_Descripcion_frmGene.Text.Trim(),
                    txb_RutaIma_frmGene.Text.Trim(),
                    ckb_Activo_frmGene.Checked
                );

                generoActualizado.ActualizarRegistro(generoActualizado);
                LimpiarCampos();
                txb_Info_frmGene.Text = "Los datos del género han sido actualizados correctamente.";
            }
            catch (Exception ex)
            {
                txb_Info_frmGene.Text = "Error al actualizar";
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            try
            {
                string idAEliminar = txtId.Text.Trim();

                if (string.IsNullOrWhiteSpace(idAEliminar))
                {
                    txb_Info_frmGene.Text = "Debe especificar el ID del género a eliminar.";
                    return;
                }

                DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar el género con ID {idAEliminar}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    Genero generoAuxiliar = new Genero();
                    generoAuxiliar.EliminarRegistro(idAEliminar);

                    if (barraEstado != null && barraEstado.Items.Count > 0)
                    {
                        barraEstado.Items[0].Text = $"Género con ID [{idAEliminar}] fue eliminado correctamente.";
                    }

                    LimpiarCampos();
                    txb_Info_frmGene.Text = "El registro ha sido eliminado.";
                }
            }
            catch (Exception ex)
            {
                txb_Info_frmGene.Text = "Error al eliminar";
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txb_Nombre_frmGene.Clear();
            txb_Descripcion_frmGene.Clear();
            txb_RutaIma_frmGene.Clear();
            ckb_Activo_frmGene.Checked = true;
        }
    }
}