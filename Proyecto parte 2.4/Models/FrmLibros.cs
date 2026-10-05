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
    public partial class FrmLibros : FrmBase, IPanelCRUD
    {
        public FrmLibros()
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
                    txb_Info_frmLibros.Text = "⚠️ ERROR DE VALIDACIÓN:\r\nPor favor, ingrese un código ID numérico válido.";
                    return;
                }

                string titulo = txb_Titulo_frmLibros.Text.Trim();
                decimal costo = decimal.TryParse(txb_Costo_frmLibros.Text.Trim(), out decimal c) ? c : 20.0m;
                bool esNovedad = cmb_Novedad_frmLibros.SelectedItem?.ToString() == "Si";
                bool estado = cmb_Estado_frmLibros.SelectedItem?.ToString() == "Disponible";
                string rutaImagen = txb_RutaIma_frmLibros.Text.Trim();

                // Atributos no presentes en el formulario con valores por defecto
                string isbn = "000-0000000000";
                int copiasDisponibles = 1;

                // Instancia usando el constructor completo de la clase Libros
                Libros nuevoLibro = new Libros(id, isbn, titulo, costo, copiasDisponibles, esNovedad, rutaImagen, estado);
                nuevoLibro.InsertarRegistro(nuevoLibro);

                txb_Info_frmLibros.Text = "El libro ha sido guardado exitosamente.";
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                txb_Info_frmLibros.Text = "Ocurrió un error al guardar: Error de Persistencia";
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.Clear();

            if (string.IsNullOrWhiteSpace(id))
            {
                alerta.SetError(txtId, "Ingrese un ID de libro para realizar la búsqueda.");
                return;
            }

            try
            {
                if (!int.TryParse(id.Trim(), out int idBuscado))
                {
                    alerta.SetError(txtId, "El ID debe ser un número entero.");
                    return;
                }

                // Búsqueda directa sobre la colección estática de Libros
                Libros libroEncontrado = Libros.ObtenerTodos().Find(l => l.Id == idBuscado);

                if (libroEncontrado != null)
                {
                    txtId.Text = libroEncontrado.Id.ToString();
                    txb_Titulo_frmLibros.Text = libroEncontrado.Titulo;
                    txb_Costo_frmLibros.Text = libroEncontrado.PrecioRentaDiaria.ToString();
                    cmb_Novedad_frmLibros.SelectedItem = libroEncontrado.EsNovedad ? "Sí" : "No";
                    cmb_Estado_frmLibros.SelectedItem = libroEncontrado.EsActivo ? "Activo" : "Inactivo";
                    txb_RutaIma_frmLibros.Text = libroEncontrado.RutaImagen;
                }
                else
                {
                    txb_Info_frmLibros.Text = "No se encontró ningún libro registrado con el ID especificado.";
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
                    txb_Info_frmLibros.Text = "Ingrese un ID numérico válido para actualizar.";
                    return;
                }

                Libros libroActualizado = new Libros(
                    id,
                    "000-0000000000",
                    txb_Titulo_frmLibros.Text.Trim(),
                    decimal.TryParse(txb_Costo_frmLibros.Text.Trim(), out decimal c) ? c : 20.0m,
                    1,
                    cmb_Novedad_frmLibros.SelectedItem?.ToString() == "Si",
                    txb_RutaIma_frmLibros.Text.Trim(),
                    cmb_Estado_frmLibros.SelectedItem?.ToString() == "Activo"
                );

                libroActualizado.ActualizarRegistro(libroActualizado);
                LimpiarCampos();
                txb_Info_frmLibros.Text = "Los datos del libro han sido actualizados correctamente.";
            }
            catch (Exception ex)
            {
                txb_Info_frmLibros.Text = "Error al actualizar";
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            try
            {
                string idAEliminar = txtId.Text.Trim();

                if (string.IsNullOrWhiteSpace(idAEliminar))
                {
                    txb_Info_frmLibros.Text = "Debe especificar el ID del libro a eliminar.";
                    return;
                }

                DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar el libro con ID {idAEliminar}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    Libros libroAuxiliar = new Libros();
                    libroAuxiliar.EliminarRegistro(idAEliminar);

                    if (barraEstado != null && barraEstado.Items.Count > 0)
                    {
                        barraEstado.Items[0].Text = $"Libro con ID [{idAEliminar}] fue eliminado correctamente.";
                    }

                    LimpiarCampos();
                    txb_Info_frmLibros.Text = "El registro ha sido eliminado.";
                }
            }
            catch (Exception ex)
            {
                txb_Info_frmLibros.Text = "Error al eliminar";
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txb_Titulo_frmLibros.Clear();
            txb_Costo_frmLibros.Clear();
            txb_RutaIma_frmLibros.Clear();

            if (cmb_Novedad_frmLibros.Items.Count > 0)
                cmb_Novedad_frmLibros.SelectedIndex = 0;

            if (cmb_Estado_frmLibros.Items.Count > 0)
                cmb_Estado_frmLibros.SelectedIndex = 0;
        }
    }
}