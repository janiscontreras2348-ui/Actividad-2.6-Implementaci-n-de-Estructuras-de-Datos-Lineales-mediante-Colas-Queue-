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
    public partial class FrmPrestamo : FrmBase, IPanelCRUD
    {
        // Instancia que conserva la pila de historial mientras el formulario está abierto
        private Prestamos _prestamoHistorial = new Prestamos();

        public FrmPrestamo()
        {
            InitializeComponent();

            // Enlace de los eventos Click de la pila (el -= evita que se ejecuten doble si el diseñador ya los enlazó)
            btnRegistrarEstado.Click -= btnRegistrarEstado_Click;
            btnRegistrarEstado.Click += btnRegistrarEstado_Click;
            btnDeshacer.Click -= btnDeshacer_Click;
            btnDeshacer.Click += btnDeshacer_Click;

            ActualizarVistaHistorial();   // deja el botón Deshacer desactivado al iniciar
        }

        // ===================== CONTROL VISUAL DE LA PILA =====================

        // Botón "Registrar": apila un PRESTAMO o una DEVOLUCION según el combo de Status
        private void btnRegistrarEstado_Click(object sender, EventArgs e)
        {
            string tipo = "PRESTAMO";
            if (cmb_Status_frmPrest.SelectedItem?.ToString() == "Entregado")
                tipo = "DEVOLUCION";

            _prestamoHistorial.RegistrarAccion(tipo);
            SincronizarStatus();
            ActualizarVistaHistorial();
            txb_Info_frmPrest.Text = "Acción registrada en el historial: " + tipo;
        }

        // Botón "Deshacer Último": desapila y revierte la última acción
        private void btnDeshacer_Click(object sender, EventArgs e)
        {
            string revertido = _prestamoHistorial.DeshacerUltimo();

            if (revertido != null)
            {
                SincronizarStatus();
                txb_Info_frmPrest.Text = "Se revirtió: " + revertido;
            }

            ActualizarVistaHistorial();
        }

        // Refresca ListBox, etiqueta de la cima y habilita/deshabilita Deshacer
        private void ActualizarVistaHistorial()
        {
            lstHistorial.Items.Clear();

            string[] datos = _prestamoHistorial.VolcarAArreglo();
            for (int i = 0; i < datos.Length; i++)
            {
                lstHistorial.Items.Add(datos[i]);
            }

            lblCima.Text = "Cima: " + _prestamoHistorial.VerCima();
            btnDeshacer.Enabled = _prestamoHistorial.ContarHistorial() > 0;
        }

        // Refleja en el combo el Status que dejó el modelo tras registrar o deshacer
        private void SincronizarStatus()
        {
            cmb_Status_frmPrest.SelectedItem = _prestamoHistorial.Status ? "Pendiente" : "Entregado";
        }

        // =====================================================================

        // Implementación obligatoria de los 4 métodos del contrato IPanelCRUD
        public void EjecutarGuardar()
        {
            try
            {
                if (!int.TryParse(txtId.Text.Trim(), out int id))
                {
                    txb_Info_frmPrest.Text = "⚠️ ERROR DE VALIDACIÓN:\r\nPor favor, ingrese un código ID numérico válido.";
                    return;
                }

                // Crear objetos auxiliares para Usuario y Libro a partir del texto ingresado
                Usuarios usr = new Usuarios();
                usr.Nombre = txb_Usuario_frmPrest.Text.Trim();

                Libros libro = new Libros();
                libro.Titulo = txb_Libro_frmPrest.Text.Trim();

                string rutaImagen = txb_RutaIma_frmPrest.Text.Trim();
                bool estado = cmb_Estado_frmPrest.SelectedItem?.ToString() == "Activo";

                // Instancia usando el constructor parametrizado de la clase Prestamos
                Prestamos nuevoPrestamo = new Prestamos(id, usr, libro, rutaImagen, estado);
                nuevoPrestamo.FechaEntrega = dtp_FechaEntr_frmPrest.Value;
                nuevoPrestamo.Status = cmb_Status_frmPrest.SelectedItem?.ToString() == "Pendiente";

                nuevoPrestamo.InsertarRegistro(nuevoPrestamo);

                // Al guardar definitivamente, se vacía el historial de la pila
                _prestamoHistorial.VaciarHistorial();
                ActualizarVistaHistorial();

                txb_Info_frmPrest.Text = "El préstamo ha sido guardado exitosamente.";
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                txb_Info_frmPrest.Text = "Ocurrió un error al guardar: Error de Persistencia";
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.Clear();

            if (string.IsNullOrWhiteSpace(id))
            {
                alerta.SetError(txtId, "Ingrese un ID de préstamo para realizar la búsqueda.");
                return;
            }

            try
            {
                if (!int.TryParse(id.Trim(), out int idBuscado))
                {
                    alerta.SetError(txtId, "El ID debe ser un número entero.");
                    return;
                }

                // Búsqueda del préstamo invocando a ConsultarRegistro
                Prestamos aux = new Prestamos();
                object resultado = aux.ConsultarRegistro(idBuscado.ToString());

                if (resultado is Prestamos prestamoEncontrado)
                {
                    txtId.Text = prestamoEncontrado.Id.ToString();
                    txb_Usuario_frmPrest.Text = prestamoEncontrado.Usuario.Nombre;

                    if (prestamoEncontrado.LibrosPrestados != null && prestamoEncontrado.LibrosPrestados.Length > 0 && prestamoEncontrado.LibrosPrestados[0] != null)
                    {
                        txb_Libro_frmPrest.Text = prestamoEncontrado.LibrosPrestados[0].Titulo;
                    }

                    dtp_FechaEntr_frmPrest.Value = prestamoEncontrado.FechaEntrega;
                    cmb_Status_frmPrest.SelectedItem = prestamoEncontrado.Status ? "Pendiente" : "Entregado";
                    cmb_Estado_frmPrest.SelectedItem = prestamoEncontrado.EsActivo ? "Activo" : "Inactivo";
                    txb_RutaIma_frmPrest.Text = prestamoEncontrado.RutaImagen;
                }
                else
                {
                    txb_Info_frmPrest.Text = "No se encontró ningún préstamo registrado con el ID especificado.";
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
                    txb_Info_frmPrest.Text = "Ingrese un ID numérico válido para actualizar.";
                    return;
                }

                Usuarios usr = new Usuarios();
                usr.Nombre = txb_Usuario_frmPrest.Text.Trim();

                Libros libro = new Libros();
                libro.Titulo = txb_Libro_frmPrest.Text.Trim();

                Prestamos prestamoActualizado = new Prestamos(
                    id,
                    usr,
                    libro,
                    txb_RutaIma_frmPrest.Text.Trim(),
                    cmb_Estado_frmPrest.SelectedItem?.ToString() == "Activo"
                );

                prestamoActualizado.FechaEntrega = dtp_FechaEntr_frmPrest.Value;
                prestamoActualizado.Status = cmb_Status_frmPrest.SelectedItem?.ToString() == "Pendiente";

                prestamoActualizado.ActualizarRegistro(prestamoActualizado);
                LimpiarCampos();
                txb_Info_frmPrest.Text = "Los datos del préstamo han sido actualizados correctamente.";
            }
            catch (Exception ex)
            {
                txb_Info_frmPrest.Text = "Error al actualizar";
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            try
            {
                string idAEliminar = txtId.Text.Trim();

                if (string.IsNullOrWhiteSpace(idAEliminar))
                {
                    txb_Info_frmPrest.Text = "Debe especificar el ID del préstamo a eliminar.";
                    return;
                }

                DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar el préstamo con ID {idAEliminar}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    Prestamos prestamoAuxiliar = new Prestamos();
                    prestamoAuxiliar.EliminarRegistro(idAEliminar);

                    if (barraEstado != null && barraEstado.Items.Count > 0)
                    {
                        barraEstado.Items[0].Text = $"Préstamo con ID [{idAEliminar}] fue eliminado correctamente.";
                    }

                    LimpiarCampos();
                    txb_Info_frmPrest.Text = "El registro ha sido eliminado.";
                }
            }
            catch (Exception ex)
            {
                txb_Info_frmPrest.Text = "Error al eliminar";
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txb_Usuario_frmPrest.Clear();
            txb_Libro_frmPrest.Clear();
            dtp_FechaEntr_frmPrest.Value = DateTime.Now.AddDays(7);
            txb_RutaIma_frmPrest.Clear();

            if (cmb_Status_frmPrest.Items.Count > 0)
                cmb_Status_frmPrest.SelectedIndex = 0;

            if (cmb_Estado_frmPrest.Items.Count > 0)
                cmb_Estado_frmPrest.SelectedIndex = 0;
        }
    }
}