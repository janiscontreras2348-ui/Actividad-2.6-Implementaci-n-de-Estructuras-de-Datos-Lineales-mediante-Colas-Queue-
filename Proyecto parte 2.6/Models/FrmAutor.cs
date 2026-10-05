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
    public partial class FrmAutor : FrmBase, IPanelCRUD
    {
        public FrmAutor()
        {
            InitializeComponent();
        }
        // Implementación obligatoria de los 4 métodos del contrato
        public void EjecutarGuardar()
        {
            try
            {
                if (!int.TryParse(txtId.Text.Trim(), out int id))
                {
                    textInfoAutor.Text = "⚠️ ERROR DE VALIDACIÓN:\r\nPor favor, ingrese un código ID numérico válido."; 
                    return;
                }

                string nombre = txB_Autor_Nombre.Text.Trim();
                int edad = int.TryParse(txB_Autor_Edad.Text.Trim(), out int e) ? e : 18;
                string correo = txB_Autor_Correo.Text.Trim();
                string nacionalidad = txB_Autor_Nacionalidad.Text.Trim();
                string rutaImagen = txB_Autor_RutaImagen.Text.Trim();
                bool estado = ckB_Estado.Checked;

                // Crear objeto y guardarlo invocando el método de almacenamiento del modelo
                Autores nuevoAutor = new Autores(id, nombre, edad, correo, nacionalidad, rutaImagen, estado);
                nuevoAutor.InsertarRegistro(nuevoAutor);

                textInfoAutor.Text = ("El autor ha sido guardado exitosamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                textInfoAutor.Text = ("Ocurrió un error al guardar: Error de Persistencia");
             }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.Clear();

            if (string.IsNullOrWhiteSpace(id))
            {
                alerta.SetError(txtId, "Ingrese un ID de autor para realizar la búsqueda.");
                return;
            }

            try
            {
                if (!int.TryParse(id.Trim(), out int idBuscado))
                {
                    alerta.SetError(txtId, "El ID debe ser un número entero.");
                    return;
                }

                // Búsqueda directa sobre la colección estática de Autores
                Autores autorEncontrado = Autores.ObtenerTodos().Find(a => a.Id == idBuscado);

                if (autorEncontrado != null)
                {
                    txtId.Text = autorEncontrado.Id.ToString();
                    txB_Autor_Nombre.Text = autorEncontrado.NombreCompleto;
                    txB_Autor_Edad.Text = autorEncontrado.Edad.ToString();
                    txB_Autor_Correo.Text = autorEncontrado.Correo;
                    txB_Autor_Nacionalidad.Text = autorEncontrado.Nacionalidad;
                    txB_Autor_RutaImagen.Text = autorEncontrado.RutaImagen;
                    ckB_Estado.Checked = autorEncontrado.Estado;
                }
                else
                {
                    textInfoAutor.Text = ("No se encontró ningún autor registrado con el ID especificado.");
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
                    textInfoAutor.Text = ("Ingrese un ID numérico válido para actualizar.");
                    return;
                }

                Autores autorActualizado = new Autores(
                    id,
                    txB_Autor_Nombre.Text.Trim(),
                    int.TryParse(txB_Autor_Edad.Text.Trim(), out int e) ? e : 18,
                    txB_Autor_Correo.Text.Trim(),
                    txB_Autor_Nacionalidad.Text.Trim(),
                    txB_Autor_RutaImagen.Text.Trim(),
                    ckB_Estado.Checked
                );

                autorActualizado.ActualizarRegistro(autorActualizado);
                LimpiarCampos();
                textInfoAutor.Text = ("Los datos del autor han sido actualizados correctamente.");
            }
            catch (Exception ex)
            {
                textInfoAutor.Text = ("Error al actualizar");
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            try
            {
                string idAEliminar = txtId.Text.Trim();

                if (string.IsNullOrWhiteSpace(idAEliminar))
                {
                    textInfoAutor.Text = ("Debe especificar el ID del autor a eliminar.");
                    return;
                }

                DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar al autor con ID {idAEliminar}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    Autores autorAuxiliar = new Autores();
                    autorAuxiliar.EliminarRegistro(idAEliminar);

                    if (barraEstado != null && barraEstado.Items.Count > 0)
                    {
                        barraEstado.Items[0].Text = $"Autor con ID [{idAEliminar}] fue eliminado correctamente.";
                    }

                    LimpiarCampos();
                    textInfoAutor.Text = ("El registro ha sido eliminado.");
                }
            }
            catch (Exception ex)
            {
                textInfoAutor.Text = ("Error al eliminar");
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txB_Autor_Nombre.Clear();
            txB_Autor_Edad.Clear();
            txB_Autor_Correo.Clear();
            txB_Autor_Nacionalidad.Clear();
            txB_Autor_RutaImagen.Clear();
            ckB_Estado.Checked = true;
        }
    }
}
