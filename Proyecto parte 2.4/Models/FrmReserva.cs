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
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class FrmReserva : FrmBase, IPanelCRUD
    {
        // static para que la cola no se pierda al cambiar de módulo y regresar
        private static Reserva _modelo = new Reserva();

        public FrmReserva()
        {
            InitializeComponent();

            // Conecta los botones con sus métodos (el -= evita que se ejecuten dos veces)
            btnRegistrarTurno.Click -= btnRegistrarTurno_Click;
            btnRegistrarTurno.Click += btnRegistrarTurno_Click;
            btnAtender.Click -= btnAtender_Click;
            btnAtender.Click += btnAtender_Click;
            btnVaciar.Click -= btnVaciar_Click;
            btnVaciar.Click += btnVaciar_Click;

            ActualizarVistaCola();
        }

        public void EjecutarGuardar()
        {
            try
            {
                if (!int.TryParse(txtId.Text.Trim(), out int id))
                {
                    txb_Info_frmReser.Text = "⚠️ ERROR DE VALIDACIÓN:\r\nPor favor, ingrese un código ID numérico válido.";
                    return;
                }

                if (!int.TryParse(txb_Usuario_frmReser.Text.Trim(), out int usuarioId))
                {
                    txb_Info_frmReser.Text = "⚠️ Ingrese un ID de usuario numérico válido.";
                    return;
                }

                if (!int.TryParse(txb_LibReser_frmReser.Text.Trim(), out int libroId))
                {
                    txb_Info_frmReser.Text = "⚠️ Ingrese un ID de libro numérico válido.";
                    return;
                }

                Usuarios usuarioEncontrado = Usuarios.ObtenerTodos().Find(u => u.Id == usuarioId);
                Libros libroEncontrado = Libros.ObtenerTodos().Find(l => l.Id == libroId);

                if (usuarioEncontrado == null)
                {
                    txb_Info_frmReser.Text = "No se encontró ningún usuario con ese ID.";
                    return;
                }

                if (libroEncontrado == null)
                {
                    txb_Info_frmReser.Text = "No se encontró ningún libro con ese ID.";
                    return;
                }

                DateTime fechaReserva = dtp_FechaReser_frmReser.Value;
                DateTime fechaLimite = dtp_FechaEntr_frmReser.Value;
                string rutaImagen = txb_RutaIma_frmReser.Text.Trim();
                bool estado = ckb_Activo_frmReser.Checked;

                Reserva nuevaReserva = new Reserva(id, usuarioEncontrado, libroEncontrado, fechaReserva, fechaLimite, rutaImagen, estado);
                nuevaReserva.InsertarRegistro(nuevaReserva);

                txb_Info_frmReser.Text = "La reserva ha sido guardada exitosamente.";
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                txb_Info_frmReser.Text = "Ocurrió un error al guardar: Error de Persistencia";
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.Clear();

            if (string.IsNullOrWhiteSpace(id))
            {
                alerta.SetError(txtId, "Ingrese un ID de reserva para realizar la búsqueda.");
                return;
            }

            try
            {
                if (!int.TryParse(id.Trim(), out int idBuscado))
                {
                    alerta.SetError(txtId, "El ID debe ser un número entero.");
                    return;
                }

                Reserva reservaEncontrada = Reserva.ObtenerTodos().Find(r => r.Id == idBuscado);

                if (reservaEncontrada != null)
                {
                    txtId.Text = reservaEncontrada.Id.ToString();
                    txb_Usuario_frmReser.Text = reservaEncontrada.Usuario.Id.ToString();
                    txb_LibReser_frmReser.Text = reservaEncontrada.LibroReservado.Id.ToString();
                    dtp_FechaReser_frmReser.Value = reservaEncontrada.FechaReserva;
                    dtp_FechaEntr_frmReser.Value = reservaEncontrada.FechaLimite;
                    txb_RutaIma_frmReser.Text = reservaEncontrada.RutaImagen;
                    ckb_Activo_frmReser.Checked = reservaEncontrada.Estado;
                }
                else
                {
                    txb_Info_frmReser.Text = "No se encontró ninguna reserva registrada con el ID especificado.";
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
                    txb_Info_frmReser.Text = "Ingrese un ID numérico válido para actualizar.";
                    return;
                }

                if (!int.TryParse(txb_Usuario_frmReser.Text.Trim(), out int usuarioId) ||
                    !int.TryParse(txb_LibReser_frmReser.Text.Trim(), out int libroId))
                {
                    txb_Info_frmReser.Text = "Ingrese IDs numéricos válidos de usuario y libro.";
                    return;
                }

                Usuarios usuarioEncontrado = Usuarios.ObtenerTodos().Find(u => u.Id == usuarioId);
                Libros libroEncontrado = Libros.ObtenerTodos().Find(l => l.Id == libroId);

                if (usuarioEncontrado == null || libroEncontrado == null)
                {
                    txb_Info_frmReser.Text = "No se encontró el usuario o el libro especificado.";
                    return;
                }

                Reserva reservaActualizada = new Reserva(
                    id,
                    usuarioEncontrado,
                    libroEncontrado,
                    dtp_FechaReser_frmReser.Value,
                    dtp_FechaEntr_frmReser.Value,
                    txb_RutaIma_frmReser.Text.Trim(),
                    ckb_Activo_frmReser.Checked
                );

                reservaActualizada.ActualizarRegistro(reservaActualizada);
                LimpiarCampos();
                txb_Info_frmReser.Text = "Los datos de la reserva han sido actualizados correctamente.";
            }
            catch (Exception ex)
            {
                txb_Info_frmReser.Text = "Error al actualizar";
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            try
            {
                string idAEliminar = txtId.Text.Trim();

                if (string.IsNullOrWhiteSpace(idAEliminar))
                {
                    txb_Info_frmReser.Text = "Debe especificar el ID de la reserva a eliminar.";
                    return;
                }

                DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar la reserva con ID {idAEliminar}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    Reserva reservaAuxiliar = new Reserva();
                    reservaAuxiliar.EliminarRegistro(idAEliminar);

                    if (barraEstado != null && barraEstado.Items.Count > 0)
                    {
                        barraEstado.Items[0].Text = $"Reserva con ID [{idAEliminar}] fue eliminada correctamente.";
                    }

                    LimpiarCampos();
                    txb_Info_frmReser.Text = "El registro ha sido eliminado.";
                }
            }
            catch (Exception ex)
            {
                txb_Info_frmReser.Text = "Error al eliminar";
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txb_Usuario_frmReser.Clear();
            txb_LibReser_frmReser.Clear();
            dtp_FechaReser_frmReser.Value = DateTime.Now;
            dtp_FechaEntr_frmReser.Value = DateTime.Now.AddDays(3);
            txb_RutaIma_frmReser.Clear();
            ckb_Activo_frmReser.Checked = true;
        }

        // ---- Cola de reservas ----

        // Quita acentos y mayúsculas para comparar textos (Carreon = Carreón)
        private string Normalizar(string texto)
        {
            string descompuesto = texto.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in descompuesto)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString();
        }

        // Busca el usuario por ID o por nombre
        private Usuarios? BuscarUsuario(string texto)
        {
            string buscado = Normalizar(texto);
            bool esNumero = int.TryParse(texto.Trim(), out int id);
            foreach (Usuarios u in Usuarios.ObtenerTodos())
            {
                if (esNumero && u.Id == id) return u;
                if (Normalizar(u.NombreCompleto).Contains(buscado)) return u;
            }
            return null;
        }

        // Busca el libro por ID o por título
        private Libros? BuscarLibro(string texto)
        {
            string buscado = Normalizar(texto);
            bool esNumero = int.TryParse(texto.Trim(), out int id);
            foreach (Libros l in Libros.ObtenerTodos())
            {
                if (esNumero && l.Id == id) return l;
                if (Normalizar(l.Titulo).Contains(buscado)) return l;
            }
            return null;
        }

        // Llena el ListBox con lo que hay en la cola y activa o desactiva el botón Atender
        private void ActualizarVistaCola()
        {
            Reserva[] copia = _modelo.VolcarCola();

            lstCola.Items.Clear();
            foreach (Reserva r in copia)
            {
                lstCola.Items.Add(r.ToString());
            }

            // Muestra quién sigue sin sacarlo de la cola
            Reserva? siguiente = _modelo.InspeccionarFrente();
            if (siguiente != null)
                lblSiguiente.Text = "Siguiente en ser atendido: " + siguiente.Usuario.NombreCompleto + " - " + siguiente.LibroReservado.Titulo;
            else
                lblSiguiente.Text = "Siguiente en ser atendido: (cola vacía)";

            btnAtender.Enabled = _modelo.ContarElementos() > 0;
            btnVaciar.Enabled = _modelo.ContarElementos() > 0;
        }

        private void btnRegistrarTurno_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtId.Text.Trim(), out int id))
                {
                    txb_Info_frmReser.Text = "Ingrese un ID de reserva numérico.";
                    return;
                }

                if (string.IsNullOrWhiteSpace(txb_Usuario_frmReser.Text) ||
                    string.IsNullOrWhiteSpace(txb_LibReser_frmReser.Text))
                {
                    txb_Info_frmReser.Text = "Escriba el usuario y el libro.";
                    return;
                }

                if (_modelo.ExisteEnCola(id))
                {
                    txb_Info_frmReser.Text = "Esa reserva ya está en la cola.";
                    return;
                }

                Usuarios? usuario = BuscarUsuario(txb_Usuario_frmReser.Text);
                if (usuario == null)
                {
                    txb_Info_frmReser.Text = "No se encontró ese usuario. Regístrelo primero en el módulo Usuarios.";
                    return;
                }

                Libros? libro = BuscarLibro(txb_LibReser_frmReser.Text);
                if (libro == null)
                {
                    txb_Info_frmReser.Text = "No se encontró ese libro. Regístrelo primero en el módulo Libros.";
                    return;
                }

                Reserva nueva = new Reserva(id, usuario, libro,
                    dtp_FechaReser_frmReser.Value, dtp_FechaEntr_frmReser.Value,
                    txb_RutaIma_frmReser.Text.Trim(), ckb_Activo_frmReser.Checked);

                _modelo.Encolar(nueva);
                txb_Info_frmReser.Text = "Turno registrado. En espera: " + _modelo.ContarElementos();
                LimpiarCampos();
                ActualizarVistaCola();
            }
            catch (Exception ex)
            {
                txb_Info_frmReser.Text = "Error al registrar el turno: " + ex.Message;
            }
        }

        private void btnVaciar_Click(object sender, EventArgs e)
        {
            _modelo.VaciarCola();
            txb_Info_frmReser.Text = "Se canceló toda la cola de espera.";
            ActualizarVistaCola();
        }

        private void btnAtender_Click(object sender, EventArgs e)
        {
            Reserva? atendida = _modelo.Desencolar();

            if (atendida != null)
                txb_Info_frmReser.Text = "Atendiendo: " + atendida.ToString();

            ActualizarVistaCola();
        }
    }
} //Final