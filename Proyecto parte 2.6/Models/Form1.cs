/*
 * ENCABEZADO DE AUTORÍA
 * Equipo N°: 3
 * Integrantes:
 * 1. CONTRERAS Rodriguez Janis Isabel
 * 2. TORRES Barrera Jose Angel
 * 3. MACÍAS Cruz Meredith Miranda
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Biblioteca.Models;

namespace Biblioteca
{
    public partial class Form1 : Form
    {
        private static int _contadorIdLibros = 1;
        private static readonly List<Editorial> _editorialesUI = new List<Editorial>();
        private static readonly List<Multa> _multasUI = new List<Multa>();
        private static readonly List<Genero> _generosUI = new List<Genero>();
        private static readonly List<Prestamos> _prestamosUI = new List<Prestamos>();
        private readonly Dictionary<int, string> _estadoEditorialUI = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _estadoAdministradorUI = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _estadoPersonaUI = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _motivoBloqueoPersonaUI = new Dictionary<int, string>();
        private readonly HashSet<int> _usuariosBloqueadosPorMultaUI = new HashSet<int>();
        private const int DiasLimitePagoMulta = 7;
        private int? _personaSeleccionadaIdUI = null;

        public Form1()
        {
            InitializeComponent();
            ConfigurarEventos();
            ConfigurarEstadosEspeciales();
            InicializarDatosInterfaz();
        }

        private void ConfigurarEventos()
        {
            // Usuarios
            btnAgregarUsuario.Click += btnAgregarUsuario_Click;
            btnModificarUsuario.Click += btnModificarUsuario_Click;
            btnEliminarUsuario.Click += btnEliminarUsuario_Click;
            btn_imagen_Usu.Click += btn_imagen_Usu_Click;

            // Libros
            btnCrearLi.Click += btnCrearLi_Click;
            btnBuscarLi.Click += btnBuscarLi_Click;
            btnMostrarLi.Click += btnMostrarLi_Click;
            btnActualizarLi.Click += btnActualizarLi_Click;
            btnBorrarLi.Click += btnBorrarLi_Click;

            // Autores
            btn_Autor_AgregarPic.Click += btn_Autor_AgregarPic_Click;
            btn_Autor_ModificarPic.Click += btn_Autor_ModificarPic_Click;
            btn_Autor_EliminarPic.Click += btn_Autor_EliminarPic_Click;
            btn_Autor_LimpiarPic.Click += btn_Autor_LimpiarPic_Click;
            btn_Autor_Imagen.Click += btn_Autor_Imagen_Click;

            // Género
            btn_GenModificar.Click += btn_GenModificar_Click;
            btn_GenEliminar.Click += btn_GenEliminar_Click;
            btn_GenLimpiar.Click += btn_GenLimpiar_Click;
            btn_Imagen_Gene.Click += btn_Imagen_Gene_Click;

            // Préstamos
            btnCrearPres.Click += btnCrearPres_Click;
            btnBuscarPres.Click += btnBuscarPres_Click;
            btnMostrarPres.Click += btnMostrarPres_Click;
            btnActualizarPres.Click += btnActualizarPres_Click;
            btnBorrarrPres.Click += btnBorrarrPres_Click;
            btn_Imagen_Prest.Click += btn_Imagen_Prest_Click;

            // Reservas
            btnCrearReserva.Click += btnCrearReserva_Click;
            btnModificarReserva.Click += btnModificarReserva_Click;
            btnEliminarReserva.Click += btnEliminarReserva_Click;
            btnLimpiarReserva.Click += btnLimpiarReserva_Click;
            btn_Imagen_Reser.Click += btn_Imagen_Reser_Click;

            // Multas
            btn_Multa_Agregar.Click += btn_Multa_Agregar_Click;
            btn_Multa_Modificar.Click += btn_Multa_Modificar_Click;
            btn_Multa_Eliminar.Click += btn_Multa_Eliminar_Click;
            btn_Multa_Limpiar.Click += btn_Multa_Limpiar_Click;
            btn_Imagen_Mlta.Click += btn_Imagen_Mlta_Click;

            // Editoriales
            btnCrearEdi.Click += btnCrearEdi_Click;
            btnBuscarEdi.Click += btnBuscarEdi_Click;
            btnMostrarEdi.Click += btnMostrarEdi_Click;
            btnActualizarEdi.Click += btnActualizarEdi_Click;
            btnBorarEdi.Click += btnBorarEdi_Click;
            btn_Imagen_Edi.Click += btn_Imagen_Edi_Click;

            // Administradores
            btnCrearAdm.Click += btnCrearAdm_Click;
            btnBuscarAdm.Click += btnBuscarAdm_Click;
            btnMostrarAdm.Click += btnMostrarAdm_Click;
            btnActualizarAdm.Click += btnActualizarAdm_Click;
            btnBorarAdm.Click += btnBorarAdm_Click;
            btn_Imagen_Admin.Click += btn_Imagen_Admin_Click;

            // Personas
            // El botón Crear de Persona se oculta porque Persona funciona como
            // vista vinculada de Usuarios/Autores.
            btnBuscarPer.Click += btnBuscarPer_Click;
            btnMostrarPer.Click += btnMostrarPer_Click;
            btnActualizarPer.Click += btnActualizarPer_Click;
            btnBorrarPer.Click += btnBorrarPer_Click;
            btn_Imagen_Pers.Click += btn_Imagen_Pers_Click;
        }

        private void ConfigurarEstadosEspeciales()
        {
            // Los valores visuales de los ComboBox se configuran en Form1.Designer.cs.
            // Aquí se conserva únicamente la configuración que no corresponde al diseño visual.
        }

        private string ObtenerEstadoEditorial(int id, bool esActivo)
        {
            if (_estadoEditorialUI.TryGetValue(id, out string estado))
                return estado;

            return esActivo ? "Activo" : "Inactivo";
        }

        private string ObtenerEstadoAdministrador(int id, bool esActivo)
        {
            if (_estadoAdministradorUI.TryGetValue(id, out string estado))
                return estado;

            return esActivo ? "Activo" : "Inactivo";
        }

        private bool EstadoEsActivoEditorial(string estado)
        {
            return estado.Equals("Activo", StringComparison.OrdinalIgnoreCase);
        }

        private bool EstadoEsActivoAdministrador(string estado)
        {
            return estado.Equals("Activo", StringComparison.OrdinalIgnoreCase);
        }

        private string NormalizarEstadoPersona(string estado)
        {
            if (estado.Equals("Egresado", StringComparison.OrdinalIgnoreCase)) return "Egresado";
            if (estado.Equals("Bloqueado", StringComparison.OrdinalIgnoreCase)) return "Bloqueado";
            return "Activo";
        }

        private string ObtenerEstadoPersona(int id, bool esActivo)
        {
            if (_estadoPersonaUI.TryGetValue(id, out string estado)) return estado;
            return esActivo ? "Activo" : "Bloqueado";
        }

        private string ObtenerMotivoBloqueo(int id)
        {
            if (_motivoBloqueoPersonaUI.TryGetValue(id, out string motivo)) return motivo;
            return "El acceso fue bloqueado por la administración de la biblioteca.";
        }

        private bool TieneMultaVencidaNoPagada(Usuarios usuario, out Multa multaVencida)
        {
            multaVencida = null;
            if (usuario == null) return false;
            multaVencida = _multasUI
                .Where(m => m.Usuario != null && m.Usuario.Id == usuario.Id && !m.Pagada && m.EsActivo)
                .OrderBy(m => m.FechaEmision)
                .FirstOrDefault(m => DateTime.Now.Date >= m.FechaEmision.Date.AddDays(DiasLimitePagoMulta));
            return multaVencida != null;
        }

        private decimal CalcularMultaAcumulada(int idUsuario)
        {
            return _multasUI
                .Where(m => m.Usuario != null &&
                            m.Usuario.Id == idUsuario &&
                            !m.Pagada &&
                            m.EsActivo)
                .Sum(m => m.MontoBase);
        }

        private void ActualizarMultaAcumuladaUsuarios()
        {
            foreach (Usuarios usuario in Usuarios.ObtenerTodos())
                usuario.MultaAcumulada = CalcularMultaAcumulada(usuario.Id);
        }

        private void ActualizarBloqueosPorMultas()
        {
            ActualizarMultaAcumuladaUsuarios();
            foreach (Usuarios usuario in Usuarios.ObtenerTodos())
            {
                if (TieneMultaVencidaNoPagada(usuario, out Multa multaVencida))
                {
                    usuario.EsActivo = false;
                    _usuariosBloqueadosPorMultaUI.Add(usuario.Id);
                    _estadoPersonaUI[usuario.Id] = "Bloqueado";
                    _motivoBloqueoPersonaUI[usuario.Id] = $"Multa pendiente de ${multaVencida.MontoBase:F2} por {multaVencida.Motivo}, emitida el {multaVencida.FechaEmision:dd/MM/yyyy}. El plazo de pago de {DiasLimitePagoMulta} días ya venció.";
                    Persona persona = Persona.ObtenerTodos().Find(p => p.Id == usuario.Id);
                    if (persona != null) persona.EsActivo = false;
                }
                else if (_usuariosBloqueadosPorMultaUI.Contains(usuario.Id))
                {
                    usuario.EsActivo = true;
                    _usuariosBloqueadosPorMultaUI.Remove(usuario.Id);
                    if (_estadoPersonaUI.TryGetValue(usuario.Id, out string estado) && estado.Equals("Bloqueado", StringComparison.OrdinalIgnoreCase))
                    {
                        _estadoPersonaUI[usuario.Id] = "Activo";
                        _motivoBloqueoPersonaUI.Remove(usuario.Id);
                    }
                    Persona persona = Persona.ObtenerTodos().Find(p => p.Id == usuario.Id);
                    if (persona != null && _estadoPersonaUI.TryGetValue(usuario.Id, out string estadoPersona) && estadoPersona.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                        persona.EsActivo = true;
                }
            }
        }

        private bool VerificarAccesoUsuario(Usuarios usuario, string accion)
        {
            ActualizarBloqueosPorMultas();
            if (usuario == null) { MostrarError("No se encontró el usuario."); return false; }
            if (_usuariosBloqueadosPorMultaUI.Contains(usuario.Id))
            {
                MessageBox.Show($"Acceso bloqueado para {usuario.NombreCompleto}.\n\nMotivo: {ObtenerMotivoBloqueo(usuario.Id)}\n\nNo puede {accion} hasta que liquide la multa pendiente.", "Usuario bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!usuario.EsActivo)
            {
                MessageBox.Show($"El usuario {usuario.NombreCompleto} se encuentra inactivo y no puede {accion}.", "Acceso no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnBuscarUsuarioUI_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtIDUsuario.Text.Trim(), out int id)) { MostrarError("El ID del usuario debe ser un número válido."); return; }
            ActualizarBloqueosPorMultas();
            Usuarios encontrado = Usuarios.ObtenerTodos().Find(u => u.Id == id);
            if (encontrado == null) { MessageBox.Show("No se encontró el usuario.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            txtCodUsuario.Text = encontrado.Id.ToString(); txtIDUsuario.Text = encontrado.Id.ToString();
            txtNombreUsuario.Text = encontrado.NombreCompleto; txtEdadUsuario.Text = encontrado.Edad.ToString(); txtCorreoUsuario.Text = encontrado.Correo;
            txtLibrosPrestaodosUsuario.Text = encontrado.LibrosPrestados.ToString();
            chbRol.Checked = encontrado.EsProfesor; chbEstadoUsuario.Checked = encontrado.EsActivo; txtImagenUsuario.Text = encontrado.RutaImagen;
            CargarImagen(encontrado.RutaImagen, lblFotoUsuario);
            MessageBox.Show("Usuario encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuscarReservaUI_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txb_ID_Reserva.Text.Trim(), out int id)) { MostrarError("El ID de la reserva debe ser un número válido."); return; }
            Reserva encontrado = Reserva.ObtenerTodos().Find(r => r.Id == id);
            if (encontrado == null) { MessageBox.Show("No se encontró la reserva.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            txb_ID_Reserva.Text = encontrado.Id.ToString(); txB_usuario_CReserva.Text = encontrado.Usuario.Id.ToString(); txb_librorsrva_CReserva.Text = encontrado.LibroReservado.ISBN;
            dtpFechaReserva.Value = encontrado.FechaReserva; dtpFechaEntregaReserva.Value = encontrado.FechaLimite; txtImagenREserva.Text = encontrado.RutaImagen; chbEstado.Checked = encontrado.Estado;
            CargarImagen(encontrado.RutaImagen, lblFotoREserva);
            MessageBox.Show("Reserva encontrada.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuscarAutorUI_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txB_Autor_Codigo.Text.Trim(), out int codigo))
            {
                MostrarError("El código del autor debe ser un número válido.");
                return;
            }

            Autores encontrado = Autores.ObtenerTodos().Find(a => a.Id == codigo);
            if (encontrado == null)
            {
                MessageBox.Show("No se encontró el autor.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txB_Autor_Codigo.Text = encontrado.Id.ToString();
            txB_Autor_Nombre.Text = encontrado.NombreCompleto;
            txB_Autor_Edad.Text = encontrado.Edad.ToString();
            txB_Autor_Correo.Text = encontrado.Correo;
            txB_Autor_Nacionalidad.Text = encontrado.Nacionalidad;
            txB_Autor_RutaImagen.Text = encontrado.RutaImagen;
            ckB_Estado.Checked = encontrado.Estado;
            CargarImagen(encontrado.RutaImagen, lblFotoAutor);

            MessageBox.Show("Autor encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuscarGeneroUI_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txB_IDGenero.Text.Trim(), out int id))
            {
                MostrarError("El ID del género debe ser un número válido.");
                return;
            }

            Genero encontrado = _generosUI.Find(g => g.Id == id);
            if (encontrado == null)
            {
                MessageBox.Show("No se encontró el género.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txB_IDGenero.Text = encontrado.Id.ToString();
            txB_GenNombre.Text = encontrado.Nombre;
            txB_GenDescripcion.Text = encontrado.Descripcion;
            txb_RutaIma_Gen.Text = encontrado.RutaImagen;
            ckB_Genero_Activo.Checked = encontrado.EsActivo;
            CargarImagen(encontrado.RutaImagen, lblFotoGenero);

            MessageBox.Show("Género encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuscarMultaUI_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txB_IDMulta.Text.Trim(), out int id))
            {
                MostrarError("El ID de la multa debe ser un número válido.");
                return;
            }

            Multa encontrada = _multasUI.Find(m => m.Id == id);
            if (encontrada == null)
            {
                MessageBox.Show("No se encontró la multa.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txB_IDMulta.Text = encontrada.Id.ToString();
            txB_Multa_Motivo.Text = encontrada.Motivo;
            nUD_Multa_MontoBase.Value = encontrada.MontoBase;
            dTP_Multa_FechaEmision.Value = encontrada.FechaEmision;
            ckB_Multa_Pagada.Checked = encontrada.Pagada;
            txb_RutaIma_Mlta.Text = encontrada.RutaImagen;
            ckB_MultaEstado.Checked = encontrada.EsActivo;

            if (encontrada.Usuario != null)
                cmB_Usuario.SelectedItem = encontrada.Usuario;

            CargarImagen(encontrada.RutaImagen, lblFotoMulta);
            MessageBox.Show("Multa encontrada.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =====================================================
        // POLIMORFISMO POR HERENCIA
        // =====================================================

        private string ObtenerInformacionPersonaPolimorfica(Persona persona)
        {
            // La referencia es Persona, pero el objeto real puede ser Usuarios o Autores.
            Persona referencia = persona;

            string informacion = "";
            informacion += $"Nombre: {referencia.NombreCompleto}" + Environment.NewLine;
            informacion += $"Edad: {referencia.Edad}" + Environment.NewLine;
            informacion += $"Correo: {referencia.Correo}" + Environment.NewLine;
            informacion += $"Estado: {ObtenerEstadoPersona(referencia.Id, referencia.EsActivo)}" + Environment.NewLine;

            if (referencia is Usuarios usuario)
            {
                informacion += "Tipo: Usuario" + Environment.NewLine;
                informacion += $"Libros prestados: {usuario.LibrosPrestados}" + Environment.NewLine;
                informacion += $"Multa acumulada: ${usuario.MultaAcumulada:F2}" + Environment.NewLine;
                informacion += $"Rol: {(usuario.EsProfesor ? "Profesor" : "Estudiante")}" + Environment.NewLine;
            }
            else if (referencia is Autores autor)
            {
                informacion += "Tipo: Autor" + Environment.NewLine;
                informacion += $"Nacionalidad: {autor.Nacionalidad}" + Environment.NewLine;
            }
            else
            {
                informacion += "Tipo: Persona" + Environment.NewLine;

                Usuarios usuarioRelacionado = Usuarios.ObtenerTodos()
                    .Find(u => u.Id == referencia.Id);

                if (usuarioRelacionado != null)
                {
                    ActualizarMultaAcumuladaUsuarios();
                    informacion += $"Multa acumulada: ${usuarioRelacionado.MultaAcumulada:F2}" + Environment.NewLine;
                    informacion += $"Libros relacionados: {ObtenerLibrosRelacionadosConUsuario(usuarioRelacionado)}" + Environment.NewLine;
                }
            }

            return informacion;
        }

        private void InicializarDatosInterfaz()
        {
            ActualizarContadorLibros();
            ActualizarBloqueosPorMultas();
            RefrescarListaUsuarios();
            MostrarTodosLosLibros();
            MostrarAutores();
            MostrarGeneros();
            MostrarPrestamos();
            MostrarReservas();
            MostrarMultas();
            MostrarEditoriales();
            MostrarAdministradores();
            MostrarPersonas();
            CargarUsuariosEnCombos();
        }

        // =====================================================
        // MÉTODOS AUXILIARES GENERALES
        // =====================================================

        private void MostrarError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool TryParseInt(TextBox caja, string nombre, out int valor)
        {
            if (!int.TryParse(caja.Text.Trim(), out valor))
            {
                MostrarError($"El campo {nombre} debe contener un número entero válido.");
                caja.Focus();
                return false;
            }
            return true;
        }

        private bool TryParseDecimal(TextBox caja, string nombre, out decimal valor)
        {
            if (!decimal.TryParse(caja.Text.Trim(), out valor))
            {
                MostrarError($"El campo {nombre} debe contener un número válido.");
                caja.Focus();
                return false;
            }
            return true;
        }

        private bool TryGetUsuario(string textoId, out Usuarios usuario)
        {
            usuario = null;
            if (!int.TryParse(textoId.Trim(), out int id))
                return false;

            usuario = Usuarios.ObtenerTodos().Find(u => u.Id == id);
            return usuario != null;
        }

        private bool TryGetLibro(string isbn, out Libros libro)
        {
            libro = Libros.ObtenerTodos().Find(l => l.ISBN.Equals(isbn.Trim(), StringComparison.OrdinalIgnoreCase));
            return libro != null;
        }

        private void CargarImagen(string ruta, Label destino)
        {
            if (destino == null)
                return;

            if (destino.Image != null)
            {
                Image anterior = destino.Image;
                destino.Image = null;
                anterior.Dispose();
            }

            if (string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta))
                return;

            try
            {
                using FileStream stream = new FileStream(ruta, FileMode.Open, FileAccess.Read);
                using Image imagenTemporal = Image.FromStream(stream);
                destino.Image = new Bitmap(imagenTemporal);
            }
            catch
            {
                // La ruta se conserva aunque la imagen no pueda previsualizarse.
            }
        }

        private string SeleccionarImagen(OpenFileDialog dialogo)
        {
            dialogo.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Todos los archivos|*.*";
            dialogo.Title = "Seleccionar imagen";

            if (dialogo.ShowDialog() == DialogResult.OK)
                return dialogo.FileName;

            return string.Empty;
        }

        private void ActualizarContadorLibros()
        {
            if (Libros.ObtenerTodos().Count == 0)
                _contadorIdLibros = 1;
            else
                _contadorIdLibros = Libros.ObtenerTodos().Max(l => l.Id) + 1;
        }

        private void CargarUsuariosEnCombos()
        {
            int? idMulta = null;
            int? idReserva = null;

            if (cmB_Usuario.SelectedItem is Usuarios usuarioSeleccionado)
                idMulta = usuarioSeleccionado.Id;

            if (int.TryParse(txB_usuario_CReserva.Text.Trim(), out int idReservaTemp))
                idReserva = idReservaTemp;

            cmB_Usuario.DataSource = null;
            cmB_Usuario.DataSource = Usuarios.ObtenerTodos().ToList();
            cmB_Usuario.DisplayMember = "NombreCompleto";
            cmB_Usuario.ValueMember = "Id";

            if (idMulta.HasValue)
            {
                Usuarios encontrado = Usuarios.ObtenerTodos().Find(u => u.Id == idMulta.Value);
                if (encontrado != null)
                    cmB_Usuario.SelectedItem = encontrado;
            }

            // El campo de reserva se maneja por ID escrito manualmente.
            if (idReserva.HasValue)
                txB_usuario_CReserva.Text = idReserva.Value.ToString();
        }

        private bool EsDisponible(string texto)
        {
            return texto.Equals("Disponible", StringComparison.OrdinalIgnoreCase) ||
                   texto.Equals("Disposnible", StringComparison.OrdinalIgnoreCase);
        }

        private void EscribirSeparador(TextBox caja, string titulo)
        {
            caja.AppendText("==============================================" + Environment.NewLine);
            caja.AppendText(titulo + Environment.NewLine);
            caja.AppendText("----------------------------------------------" + Environment.NewLine);
        }

        private void EscribirDato(TextBox caja, string etiqueta, object valor)
        {
            caja.AppendText($"{etiqueta}: {valor}" + Environment.NewLine);
        }

        private string NombreLibro(Libros libro)
        {
            if (libro == null)
                return "Sin libro relacionado";
            return $"{libro.Titulo} (ISBN: {libro.ISBN})";
        }

        private string ObtenerLibrosRelacionadosConUsuario(Usuarios usuario)
        {
            if (usuario == null)
                return "Sin usuario";

            List<string> libros = new List<string>();

            foreach (Prestamos prestamo in _prestamosUI.Where(p => p.Usuario != null && p.Usuario.Id == usuario.Id))
            {
                foreach (Libros libro in prestamo.LibrosPrestados)
                {
                    if (libro != null)
                    {
                        string dato = NombreLibro(libro);
                        if (!libros.Contains(dato))
                            libros.Add(dato);
                    }
                }
            }

            return libros.Count == 0 ? "Sin préstamo de libro relacionado" : string.Join(", ", libros);
        }



        private bool TryGetNivelAcceso(out int nivel)
        {
            nivel = 0;
            if (cbxNivelAcceso.SelectedIndex >= 0)
            {
                nivel = cbxNivelAcceso.SelectedIndex + 1;
                return true;
            }
            MessageBox.Show("Seleccione un nivel de acceso.", "Nivel de acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void MostrarNivelAcceso(int nivel)
        {
            if (cbxNivelAcceso != null && nivel >= 1 && nivel <= 5)
                cbxNivelAcceso.SelectedIndex = nivel - 1;
            else
                cbxNivelAcceso.Text = nivel.ToString();
        }

        // =====================================================
        // USUARIOS
        // =====================================================

        private void btnAgregarUsuario_Click(object? sender, EventArgs e)
        {
            try
            {
                ActualizarMultaAcumuladaUsuarios();

                if (!TryParseInt(txtIDUsuario, "ID del usuario", out int id) ||
                    !TryParseInt(txtEdadUsuario, "Edad", out int edad) ||
                    !TryParseInt(txtLibrosPrestaodosUsuario, "Libros prestados", out int librosPrestados))
                    return;

                decimal multa = CalcularMultaAcumulada(id);

                if (Usuarios.ObtenerTodos().Any(u => u.Id == id))
                {
                    MostrarError("Ya existe un usuario con ese ID.");
                    return;
                }

                Usuarios nuevo = new Usuarios(
                    id,
                    txtNombreUsuario.Text,
                    edad,
                    txtCorreoUsuario.Text,
                    librosPrestados,
                    multa,
                    chbRol.Checked,
                    txtImagenUsuario.Text,
                    chbEstadoUsuario.Checked
                );

                nuevo.InsertarRegistro(nuevo);
                SincronizarPersonaDesdeUsuario(nuevo);
                ActualizarBloqueosPorMultas();
                RefrescarListaUsuarios();
                CargarUsuariosEnCombos();
                CargarImagen(nuevo.RutaImagen, lblFotoUsuario);
                ActualizarVistaPersonaSiCorresponde(nuevo.Id);

                MessageBox.Show("Usuario agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al agregar usuario");
            }
        }

        private void btnModificarUsuario_Click(object? sender, EventArgs e)
        {
            try
            {
                ActualizarMultaAcumuladaUsuarios();

                if (!TryParseInt(txtIDUsuario, "ID del usuario", out int id) ||
                    !TryParseInt(txtEdadUsuario, "Edad", out int edad) ||
                    !TryParseInt(txtLibrosPrestaodosUsuario, "Libros prestados", out int librosPrestados))
                    return;

                decimal multa = CalcularMultaAcumulada(id);

                Usuarios existente = Usuarios.ObtenerTodos().Find(u => u.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró el usuario a modificar.");
                    return;
                }

                Usuarios actualizado = new Usuarios(
                    id,
                    txtNombreUsuario.Text,
                    edad,
                    txtCorreoUsuario.Text,
                    librosPrestados,
                    multa,
                    chbRol.Checked,
                    txtImagenUsuario.Text,
                    chbEstadoUsuario.Checked
                );

                existente.ActualizarRegistro(actualizado);
                SincronizarPersonaDesdeUsuario(actualizado);
                ActualizarBloqueosPorMultas();
                RefrescarListaUsuarios();
                CargarUsuariosEnCombos();
                CargarImagen(actualizado.RutaImagen, lblFotoUsuario);
                ActualizarVistaPersonaSiCorresponde(actualizado.Id);

                MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al modificar usuario");
            }
        }

        private void btnEliminarUsuario_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtIDUsuario.Text.Trim(), out int id))
                {
                    MostrarError("El ID del usuario debe ser un número válido.");
                    return;
                }

                Usuarios usuario = Usuarios.ObtenerTodos().Find(u => u.Id == id);
                if (usuario == null)
                {
                    MostrarError("No se encontró el usuario a eliminar.");
                    return;
                }

                usuario.EliminarRegistro(id.ToString());
                EliminarPersonaVinculada(id);

                if (_personaSeleccionadaIdUI == id)
                    _personaSeleccionadaIdUI = null;

                RefrescarListaUsuarios();
                CargarUsuariosEnCombos();
                LimpiarCamposUsuario();
                MostrarPersonas();

                MessageBox.Show("Usuario y su registro vinculado en Persona fueron eliminados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar usuario");
            }
        }

        private void btnLimpiarUsuario_Click(object? sender, EventArgs e)
        {
            LimpiarCamposUsuario();
        }

        private void LimpiarCamposUsuario()
        {
            txtCodUsuario.Clear();
            txtIDUsuario.Clear();
            txtNombreUsuario.Clear();
            txtLibrosPrestaodosUsuario.Clear();
            txtEdadUsuario.Clear();
            txtCorreoUsuario.Clear();
            txtNacionalidadUsuario.Clear();
            txtImagenUsuario.Clear();
            chbRol.Checked = false;
            chbEstadoUsuario.Checked = true;
            CargarImagen(string.Empty, lblFotoUsuario);
        }

        private void RefrescarListaUsuarios()
        {
            txtInfoUsuario.Clear();
            foreach (Usuarios u in Usuarios.ObtenerTodos())
            {
                EscribirSeparador(txtInfoUsuario, $"USUARIO #{u.Id}");
                EscribirDato(txtInfoUsuario, "Nombre", u.NombreCompleto);
                EscribirDato(txtInfoUsuario, "Tipo", u.EsProfesor ? "Profesor" : "Estudiante");
                EscribirDato(txtInfoUsuario, "Edad", u.Edad);
                EscribirDato(txtInfoUsuario, "Correo", u.Correo);
                EscribirDato(txtInfoUsuario, "Libros prestados", u.LibrosPrestados);
                EscribirDato(txtInfoUsuario, "Multa acumulada", $"${CalcularMultaAcumulada(u.Id):F2}");
                string estadoUsuario = _usuariosBloqueadosPorMultaUI.Contains(u.Id) ? "Bloqueado" : (u.EsActivo ? "Activo" : "Inactivo");
                EscribirDato(txtInfoUsuario, "Estado", estadoUsuario);
                if (_usuariosBloqueadosPorMultaUI.Contains(u.Id))
                    EscribirDato(txtInfoUsuario, "Motivo del bloqueo", ObtenerMotivoBloqueo(u.Id));
                EscribirDato(txtInfoUsuario, "Libros relacionados", ObtenerLibrosRelacionadosConUsuario(u));
                EscribirDato(txtInfoUsuario, "Imagen", u.RutaImagen);
                txtInfoUsuario.AppendText(Environment.NewLine);
            }
        }

        private void btn_imagen_Usu_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            string ruta = SeleccionarImagen(dialogo);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txtImagenUsuario.Text = ruta;
                CargarImagen(ruta, lblFotoUsuario);
            }
        }

        // =====================================================
        // LIBROS
        // =====================================================

        private void btnCrearLi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseDecimal(txtCosto, "Costo", out decimal costo))
                    return;

                if (Libros.ObtenerTodos().Any(l => l.ISBN.Equals(txtIsbn.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    MostrarError("Ya existe un libro con ese ISBN.");
                    return;
                }

                ActualizarContadorLibros();

                Libros nuevo = new Libros(
                    _contadorIdLibros++,
                    txtIsbn.Text,
                    txtTitulo.Text,
                    costo,
                    1,
                    cbxNovedad.Text.Equals("Si", StringComparison.OrdinalIgnoreCase),
                    txtImagen.Text,
                    EsDisponible(cbxEstado.Text)
                );

                nuevo.InsertarRegistro(nuevo);
                MostrarTodosLosLibros();
                MessageBox.Show("Libro creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al crear libro");
            }
        }

        private void btnBuscarLi_Click(object? sender, EventArgs e)
        {
            if (!TryGetLibro(txtIsbn.Text, out Libros encontrado))
            {
                MessageBox.Show("No se encontró un libro con ese ISBN.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarLibroEnFormulario(encontrado);
            MessageBox.Show("Libro encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMostrarLi_Click(object? sender, EventArgs e)
        {
            MostrarTodosLosLibros();
        }

        private void btnActualizarLi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseDecimal(txtCosto, "Costo", out decimal costo))
                    return;

                if (!TryGetLibro(txtIsbn.Text, out Libros existente))
                {
                    MostrarError("No se encontró el libro a actualizar.");
                    return;
                }

                Libros actualizado = new Libros(
                    existente.Id,
                    txtIsbn.Text,
                    txtTitulo.Text,
                    costo,
                    existente.CopiasDisponibles,
                    cbxNovedad.Text.Equals("Si", StringComparison.OrdinalIgnoreCase),
                    txtImagen.Text,
                    EsDisponible(cbxEstado.Text)
                );

                existente.ActualizarRegistro(actualizado);
                MostrarTodosLosLibros();
                MessageBox.Show("Libro actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al actualizar libro");
            }
        }

        private void btnBorrarLi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryGetLibro(txtIsbn.Text, out Libros existente))
                {
                    MostrarError("No se encontró el libro a eliminar.");
                    return;
                }

                existente.EliminarRegistro(existente.Id.ToString());
                ActualizarContadorLibros();
                MostrarTodosLosLibros();
                MessageBox.Show("Libro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al borrar libro");
            }
        }

        private Libros BuscarLibroPorIsbn(string isbn)
        {
            return Libros.ObtenerTodos().Find(l => l.ISBN.Equals(isbn.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private void CargarLibroEnFormulario(Libros libro)
        {
            txtIsbn.Text = libro.ISBN;
            txtTitulo.Text = libro.Titulo;
            txtCosto.Text = libro.PrecioRentaDiaria.ToString("F2");
            cbxNovedad.Text = libro.EsNovedad ? "Si" : "No";
            cbxEstado.Text = libro.EsActivo ? "Disponible" : "Prestado";
            txtImagen.Text = libro.RutaImagen;
            CargarImagen(libro.RutaImagen, lblFotoLIbro);
        }

        private void MostrarTodosLosLibros()
        {
            txbInfo.Clear();
            foreach (Libros l in Libros.ObtenerTodos())
            {
                EscribirSeparador(txbInfo, $"LIBRO #{l.Id}");
                EscribirDato(txbInfo, "ISBN", l.ISBN);
                EscribirDato(txbInfo, "Título", l.Titulo);
                EscribirDato(txbInfo, "Precio por día", $"${l.PrecioRentaDiaria:F2}");
                EscribirDato(txbInfo, "Copias disponibles", l.CopiasDisponibles);
                EscribirDato(txbInfo, "Novedad", l.EsNovedad ? "Sí" : "No");
                EscribirDato(txbInfo, "Estado", l.EsActivo ? "Disponible" : "Prestado");
                EscribirDato(txbInfo, "Imagen", l.RutaImagen);
                txbInfo.AppendText(Environment.NewLine);
            }
        }

        private void btnLimpiarLibroUI_Click(object? sender, EventArgs e)
        {
            txtIsbn.Clear(); txtTitulo.Clear(); txtCosto.Clear(); txtImagen.Clear();
            if (cbxNovedad.Items.Count > 0) cbxNovedad.SelectedIndex = 0;
            if (cbxEstado.Items.Count > 0) cbxEstado.SelectedIndex = 0;
            CargarImagen(string.Empty, lblFotoLIbro);
        }

        private void btnLimpiarPrestamoUI_Click(object? sender, EventArgs e)
        {
            txtID.Clear(); txtUsuario.Clear(); txtLibro.Clear(); txtImagenLi.Clear();
            dtpFechaEntrega.Value = DateTime.Now.AddDays(7);
            if (cbxStatus.Items.Count > 0) cbxStatus.SelectedIndex = 0;
            if (cbxEstadoLi.Items.Count > 0) cbxEstadoLi.SelectedIndex = 0;
            CargarImagen(string.Empty, lblFotoPrestamo);
        }

        private void btnLimpiarPersonaUI_Click(object? sender, EventArgs e)
        {
            textBox4.Clear(); txtNombreCPer.Clear(); txtEdad.Clear(); txtCorreoPer.Clear(); txtImagenPer.Clear();
            if (cbxEstadoPer.Items.Count > 0) cbxEstadoPer.SelectedIndex = 0;
            CargarImagen(string.Empty, label12);
        }

        private void btnLimpiarEditorialUI_Click(object? sender, EventArgs e)
        {
            txtIDEditorial.Clear(); txtNombreEdi.Clear(); txtPais.Clear(); txtCorreoEdi.Clear(); txtImagenEdi.Clear();
            dtpAnioFundacion.Value = DateTime.Now;
            if (cbxEstadoEdi.Items.Count > 0) cbxEstadoEdi.SelectedIndex = 0;
            CargarImagen(string.Empty, lblFotoEditorial);
        }

        private void btnLimpiarAdministradorUI_Click(object? sender, EventArgs e)
        {
            txtCodigoAdm.Clear(); txtNombreAdmin.Clear(); txtEdadAdmin.Clear(); txtCorreoAdmin.Clear(); txtDepartamento.Clear(); txtImagenAdm.Clear();
            dtpFechaIngreso.Value = DateTime.Now;
            if (cbxNivelAcceso != null && cbxNivelAcceso.Items.Count > 0) cbxNivelAcceso.SelectedIndex = 2;
            if (cbxEstadoAdm.Items.Count > 0) cbxEstadoAdm.SelectedIndex = 0;
            CargarImagen(string.Empty, lblFotoAdministrador);
        }

        // =====================================================
        // AUTORES
        // =====================================================

        private void btn_Autor_AgregarPic_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txB_Autor_Codigo, "Código del autor", out int codigo) ||
                    !TryParseInt(txB_Autor_Edad, "Edad del autor", out int edad))
                    return;

                if (Autores.ObtenerTodos().Any(a => a.Id == codigo))
                {
                    MostrarError("Ya existe un autor con ese código.");
                    return;
                }

                Autores nuevo = new Autores(
                    codigo,
                    txB_Autor_Nombre.Text,
                    edad,
                    txB_Autor_Correo.Text,
                    txB_Autor_Nacionalidad.Text,
                    txB_Autor_RutaImagen.Text,
                    ckB_Estado.Checked
                );

                nuevo.InsertarRegistro(nuevo);
                MostrarAutores();
                CargarImagen(nuevo.RutaImagen, lblFotoAutor);
                MessageBox.Show("Autor agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al agregar autor");
            }
        }

        private void btn_Autor_ModificarPic_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txB_Autor_Codigo, "Código del autor", out int codigo) ||
                    !TryParseInt(txB_Autor_Edad, "Edad del autor", out int edad))
                    return;

                Autores existente = Autores.ObtenerTodos().Find(a => a.Id == codigo);
                if (existente == null)
                {
                    MostrarError("No se encontró el autor a modificar.");
                    return;
                }

                Autores actualizado = new Autores(
                    codigo,
                    txB_Autor_Nombre.Text,
                    edad,
                    txB_Autor_Correo.Text,
                    txB_Autor_Nacionalidad.Text,
                    txB_Autor_RutaImagen.Text,
                    ckB_Estado.Checked
                );

                existente.ActualizarRegistro(actualizado);
                MostrarAutores();
                CargarImagen(actualizado.RutaImagen, lblFotoAutor);
                MessageBox.Show("Autor actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al modificar autor");
            }
        }

        private void btn_Autor_EliminarPic_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txB_Autor_Codigo.Text.Trim(), out int codigo))
                {
                    MostrarError("El código del autor debe ser un número válido.");
                    return;
                }

                new Autores().EliminarRegistro(codigo.ToString());
                MostrarAutores();
                LimpiarAutor();
                MessageBox.Show("Autor eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar autor");
            }
        }

        private void btn_Autor_LimpiarPic_Click(object? sender, EventArgs e)
        {
            LimpiarAutor();
        }

        private void LimpiarAutor()
        {
            txB_Autor_Codigo.Clear();
            txB_Autor_Nombre.Clear();
            txB_Autor_Edad.Clear();
            txB_Autor_Correo.Clear();
            txB_Autor_Nacionalidad.Clear();
            txB_Autor_RutaImagen.Clear();
            ckB_Estado.Checked = true;
            CargarImagen(string.Empty, lblFotoAutor);
        }

        private void btn_Autor_Imagen_Click(object? sender, EventArgs e)
        {
            string ruta = SeleccionarImagen(oFD_Autor_RutaImagen);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txB_Autor_RutaImagen.Text = ruta;
                CargarImagen(ruta, lblFotoAutor);
            }
        }

        private void MostrarAutores()
        {
            textBox9.Clear();
            foreach (Autores autor in Autores.ObtenerTodos())
            {
                EscribirSeparador(textBox9, $"AUTOR #{autor.Id}");
                EscribirDato(textBox9, "Nombre", autor.NombreCompleto);
                EscribirDato(textBox9, "Edad", autor.Edad);
                EscribirDato(textBox9, "Correo", autor.Correo);
                EscribirDato(textBox9, "Nacionalidad", autor.Nacionalidad);
                EscribirDato(textBox9, "Estado", autor.Estado ? "Activo" : "Inactivo");
                EscribirDato(textBox9, "Imagen", autor.RutaImagen);
                textBox9.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // GÉNERO
        // =====================================================

        private void btn_GenAgregar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txB_IDGenero, "ID del género", out int id))
                    return;

                if (_generosUI.Any(g => g.Id == id))
                {
                    MostrarError("Ya existe un género con ese ID.");
                    return;
                }

                Genero nuevo = new Genero(
                    id,
                    txB_GenNombre.Text,
                    txB_GenDescripcion.Text,
                    txb_RutaIma_Gen.Text,
                    ckB_Genero_Activo.Checked
                );

                nuevo.InsertarRegistro(nuevo);
                _generosUI.Add(nuevo);
                MostrarGeneros();
                CargarImagen(nuevo.RutaImagen, lblFotoGenero);
                MessageBox.Show("Género agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al agregar género");
            }
        }

        private void btn_GenModificar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txB_IDGenero, "ID del género", out int id))
                    return;

                Genero existente = _generosUI.Find(g => g.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró el género a modificar.");
                    return;
                }

                Genero actualizado = new Genero(
                    id,
                    txB_GenNombre.Text,
                    txB_GenDescripcion.Text,
                    txb_RutaIma_Gen.Text,
                    ckB_Genero_Activo.Checked
                );

                existente.ActualizarRegistro(actualizado);
                int indiceGenero = _generosUI.FindIndex(g => g.Id == id);
                if (indiceGenero >= 0)
                    _generosUI[indiceGenero] = actualizado;
                MostrarGeneros();
                CargarImagen(actualizado.RutaImagen, lblFotoGenero);
                MessageBox.Show("Género actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al modificar género");
            }
        }

        private void btn_GenEliminar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txB_IDGenero.Text.Trim(), out int id))
                {
                    MostrarError("El ID del género debe ser un número válido.");
                    return;
                }

                new Genero().EliminarRegistro(id.ToString());
                _generosUI.RemoveAll(g => g.Id == id);
                MostrarGeneros();
                btn_GenLimpiar_Click(sender, e);
                MessageBox.Show("Género eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar género");
            }
        }

        private void btn_GenLimpiar_Click(object? sender, EventArgs e)
        {
            txB_IDGenero.Clear();
            txB_GenNombre.Clear();
            txB_GenDescripcion.Clear();
            txb_RutaIma_Gen.Clear();
            ckB_Genero_Activo.Checked = true;
            CargarImagen(string.Empty, lblFotoGenero);
        }

        private void btn_Imagen_Gene_Click(object? sender, EventArgs e)
        {
            string ruta = SeleccionarImagen(oFD_Genero_RutaImagen);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txb_RutaIma_Gen.Text = ruta;
                CargarImagen(ruta, lblFotoGenero);
            }
        }

        private void MostrarGeneros()
        {
            textBox10.Clear();
            foreach (Genero genero in _generosUI)
            {
                EscribirSeparador(textBox10, $"GÉNERO #{genero.Id}");
                EscribirDato(textBox10, "Nombre", genero.Nombre);
                EscribirDato(textBox10, "Descripción", genero.Descripcion);
                EscribirDato(textBox10, "Estado", genero.EsActivo ? "Activo" : "Inactivo");
                EscribirDato(textBox10, "Imagen", genero.RutaImagen);
                textBox10.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // PRÉSTAMOS
        // =====================================================

        private void btnCrearPres_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtID, "ID del préstamo", out int id))
                    return;

                if (!TryGetUsuario(txtUsuario.Text, out Usuarios usuario))
                {
                    MostrarError("No se encontró el usuario indicado.");
                    return;
                }

                if (!TryGetLibro(txtLibro.Text, out Libros libro))
                {
                    MostrarError("No se encontró el libro indicado por su ISBN.");
                    return;
                }

                if (!VerificarAccesoUsuario(usuario, "solicitar préstamos"))
                    return;

                if (_prestamosUI.Any(p => p.Id == id))
                {
                    MostrarError("Ya existe un préstamo con ese ID.");
                    return;
                }

                Prestamos nuevo = new Prestamos(id, usuario, libro, txtImagenLi.Text, EsDisponible(cbxEstadoLi.Text));
                nuevo.FechaEntrega = dtpFechaEntrega.Value;
                nuevo.Status = cbxStatus.Text.Equals("Si", StringComparison.OrdinalIgnoreCase);
                nuevo.InsertarRegistro(nuevo);
                _prestamosUI.Add(nuevo);
                CargarImagen(nuevo.RutaImagen, lblFotoPrestamo);

                MostrarPrestamos();
                MessageBox.Show("Préstamo creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al crear préstamo");
            }
        }

        private void btnBuscarPres_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text.Trim(), out int id))
            {
                MostrarError("El ID del préstamo debe ser un número válido.");
                return;
            }

            Prestamos encontrado = _prestamosUI.Find(p => p.Id == id);
            if (encontrado == null)
            {
                MessageBox.Show("No se encontró el préstamo.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarPrestamoEnFormulario(encontrado);
            MessageBox.Show("Préstamo encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMostrarPres_Click(object? sender, EventArgs e)
        {
            MostrarPrestamos();
        }

        private void btnActualizarPres_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtID, "ID del préstamo", out int id))
                    return;

                Prestamos existente = _prestamosUI.Find(p => p.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró el préstamo a actualizar.");
                    return;
                }

                if (!TryGetUsuario(txtUsuario.Text, out Usuarios usuario) || !TryGetLibro(txtLibro.Text, out Libros libro))
                {
                    MostrarError("Verifica el ID del usuario y el ISBN del libro.");
                    return;
                }

                if (!VerificarAccesoUsuario(usuario, "mantener préstamos"))
                    return;

                Prestamos actualizado = new Prestamos(id, usuario, libro, txtImagenLi.Text, EsDisponible(cbxEstadoLi.Text));
                actualizado.FechaEntrega = dtpFechaEntrega.Value;
                actualizado.Status = cbxStatus.Text.Equals("Si", StringComparison.OrdinalIgnoreCase);

                existente.ActualizarRegistro(actualizado);
                int indicePrestamo = _prestamosUI.FindIndex(p => p.Id == id);
                if (indicePrestamo >= 0)
                    _prestamosUI[indicePrestamo] = actualizado;
                CargarImagen(actualizado.RutaImagen, lblFotoPrestamo);
                MostrarPrestamos();
                MessageBox.Show("Préstamo actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al actualizar préstamo");
            }
        }

        private void btnBorrarrPres_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtID.Text.Trim(), out int id))
                {
                    MostrarError("El ID del préstamo debe ser un número válido.");
                    return;
                }

                new Prestamos().EliminarRegistro(id.ToString());
                _prestamosUI.RemoveAll(p => p.Id == id);
                MostrarPrestamos();
                MessageBox.Show("Préstamo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar préstamo");
            }
        }

        private void btn_Imagen_Prest_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            string ruta = SeleccionarImagen(dialogo);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txtImagenLi.Text = ruta;
                CargarImagen(ruta, lblFotoPrestamo);
            }
        }

        private void CargarPrestamoEnFormulario(Prestamos prestamo)
        {
            txtID.Text = prestamo.Id.ToString();
            txtUsuario.Text = prestamo.Usuario.Id.ToString();
            txtLibro.Text = prestamo.LibrosPrestados[0]?.ISBN ?? string.Empty;
            dtpFechaEntrega.Value = prestamo.FechaEntrega;
            cbxStatus.Text = prestamo.Status ? "Si" : "No";
            cbxEstadoLi.Text = prestamo.EsActivo ? "Disponible" : "Prestado";
            txtImagenLi.Text = prestamo.RutaImagen;
            CargarImagen(prestamo.RutaImagen, lblFotoPrestamo);
        }

        private void MostrarPrestamos()
        {
            txbInfoPres.Clear();
            foreach (Prestamos prestamo in _prestamosUI)
            {
                Libros libro = prestamo.LibrosPrestados != null && prestamo.LibrosPrestados.Length > 0
                    ? prestamo.LibrosPrestados[0]
                    : null;

                EscribirSeparador(txbInfoPres, $"PRÉSTAMO #{prestamo.Id}");
                EscribirDato(txbInfoPres, "Usuario", $"#{prestamo.Usuario.Id} - {prestamo.Usuario.NombreCompleto}");
                string estadoPrestamoUsuario = _usuariosBloqueadosPorMultaUI.Contains(prestamo.Usuario.Id) ? "Bloqueado" : (prestamo.Usuario.EsActivo ? "Activo" : "Inactivo");
                EscribirDato(txbInfoPres, "Estado del usuario", estadoPrestamoUsuario);
                if (_usuariosBloqueadosPorMultaUI.Contains(prestamo.Usuario.Id))
                    EscribirDato(txbInfoPres, "Motivo del bloqueo", ObtenerMotivoBloqueo(prestamo.Usuario.Id));
                EscribirDato(txbInfoPres, "Libro", NombreLibro(libro));
                EscribirDato(txbInfoPres, "Fecha de entrega", prestamo.FechaEntrega.ToString("dd/MM/yyyy"));
                EscribirDato(txbInfoPres, "Préstamo activo", prestamo.Status ? "Sí" : "No");
                EscribirDato(txbInfoPres, "Estado del registro", prestamo.EsActivo ? "Disponible" : "Prestado");
                EscribirDato(txbInfoPres, "Imagen", prestamo.RutaImagen);
                txbInfoPres.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // RESERVAS
        // =====================================================

        private void btnCrearReserva_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txb_ID_Reserva, "ID de la reserva", out int id))
                    return;

                if (!TryGetUsuario(txB_usuario_CReserva.Text, out Usuarios usuario))
                {
                    MostrarError("No se encontró el usuario de la reserva.");
                    return;
                }

                if (!TryGetLibro(txb_librorsrva_CReserva.Text, out Libros libro))
                {
                    MostrarError("No se encontró el libro reservado por su ISBN.");
                    return;
                }

                if (!VerificarAccesoUsuario(usuario, "reservar libros"))
                    return;

                if (Reserva.ObtenerTodos().Any(r => r.Id == id))
                {
                    MostrarError("Ya existe una reserva con ese ID.");
                    return;
                }

                Reserva nueva = new Reserva(
                    id,
                    usuario,
                    libro,
                    dtpFechaReserva.Value,
                    dtpFechaEntregaReserva.Value,
                    txtImagenREserva.Text,
                    chbEstado.Checked
                );

                nueva.InsertarRegistro(nueva);
                CargarImagen(nueva.RutaImagen, lblFotoREserva);
                MostrarReservas();
                MessageBox.Show("Reserva agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al crear reserva");
            }
        }

        private void btnModificarReserva_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txb_ID_Reserva, "ID de la reserva", out int id))
                    return;

                Reserva existente = Reserva.ObtenerTodos().Find(r => r.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró la reserva a modificar.");
                    return;
                }

                if (!TryGetUsuario(txB_usuario_CReserva.Text, out Usuarios usuario) || !TryGetLibro(txb_librorsrva_CReserva.Text, out Libros libro))
                {
                    MostrarError("Verifica el ID del usuario y el ISBN del libro.");
                    return;
                }

                if (!VerificarAccesoUsuario(usuario, "mantener reservas"))
                    return;

                Reserva actualizada = new Reserva(
                    id,
                    usuario,
                    libro,
                    dtpFechaReserva.Value,
                    dtpFechaEntregaReserva.Value,
                    txtImagenREserva.Text,
                    chbEstado.Checked
                );

                existente.ActualizarRegistro(actualizada);
                CargarImagen(actualizada.RutaImagen, lblFotoREserva);
                MostrarReservas();
                MessageBox.Show("Reserva modificada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al modificar reserva");
            }
        }

        private void btnEliminarReserva_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txb_ID_Reserva.Text.Trim(), out int id))
                {
                    MostrarError("El ID de la reserva debe ser un número válido.");
                    return;
                }

                new Reserva().EliminarRegistro(id.ToString());
                MostrarReservas();
                btnLimpiarReserva_Click(sender, e);
                MessageBox.Show("Reserva eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar reserva");
            }
        }

        private void btnLimpiarReserva_Click(object? sender, EventArgs e)
        {
            txb_ID_Reserva.Clear();
            txB_usuario_CReserva.Clear();
            txb_librorsrva_CReserva.Clear();
            txtImagenREserva.Clear();
            chbEstado.Checked = true;
            dtpFechaReserva.Value = DateTime.Now;
            dtpFechaEntregaReserva.Value = DateTime.Now.AddDays(3);
            CargarImagen(string.Empty, lblFotoREserva);
        }

        private void btn_Imagen_Reser_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            string ruta = SeleccionarImagen(dialogo);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txtImagenREserva.Text = ruta;
                CargarImagen(ruta, lblFotoREserva);
            }
        }

        private void MostrarReservas()
        {
            txtInfoReserva.Clear();
            foreach (Reserva reserva in Reserva.ObtenerTodos())
            {
                EscribirSeparador(txtInfoReserva, $"RESERVA #{reserva.Id}");
                EscribirDato(txtInfoReserva, "Usuario", $"#{reserva.Usuario.Id} - {reserva.Usuario.NombreCompleto}");
                string estadoReservaUsuario = _usuariosBloqueadosPorMultaUI.Contains(reserva.Usuario.Id) ? "Bloqueado" : (reserva.Usuario.EsActivo ? "Activo" : "Inactivo");
                EscribirDato(txtInfoReserva, "Estado del usuario", estadoReservaUsuario);
                if (_usuariosBloqueadosPorMultaUI.Contains(reserva.Usuario.Id))
                    EscribirDato(txtInfoReserva, "Motivo del bloqueo", ObtenerMotivoBloqueo(reserva.Usuario.Id));
                EscribirDato(txtInfoReserva, "Libro", NombreLibro(reserva.LibroReservado));
                EscribirDato(txtInfoReserva, "Fecha de reserva", reserva.FechaReserva.ToString("dd/MM/yyyy"));
                EscribirDato(txtInfoReserva, "Fecha límite", reserva.FechaLimite.ToString("dd/MM/yyyy"));
                EscribirDato(txtInfoReserva, "Estado", reserva.Estado ? "Vigente" : "Cancelada/Vencida");
                EscribirDato(txtInfoReserva, "Imagen", reserva.RutaImagen);
                txtInfoReserva.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // MULTAS
        // =====================================================

        private void btn_Multa_Agregar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txB_IDMulta, "ID de la multa", out int id))
                    return;

                if (cmB_Usuario.SelectedItem is not Usuarios usuario)
                {
                    MostrarError("Selecciona un usuario para la multa.");
                    return;
                }

                if (_multasUI.Any(m => m.Id == id))
                {
                    MostrarError("Ya existe una multa con ese ID.");
                    return;
                }

                Multa nueva = new Multa(
                    id,
                    usuario,
                    txB_Multa_Motivo.Text,
                    nUD_Multa_MontoBase.Value,
                    dTP_Multa_FechaEmision.Value,
                    ckB_Multa_Pagada.Checked,
                    txb_RutaIma_Mlta.Text,
                    ckB_MultaEstado.Checked
                );

                nueva.InsertarRegistro(nueva);
                _multasUI.Add(nueva);
                ActualizarBloqueosPorMultas();
                RefrescarListaUsuarios();
                MostrarPersonas();
                CargarUsuariosEnCombos();
                CargarImagen(nueva.RutaImagen, lblFotoMulta);
                MostrarMultas();
                MessageBox.Show("Multa agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al agregar multa");
            }
        }

        private void btn_Multa_Modificar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txB_IDMulta, "ID de la multa", out int id))
                    return;

                Multa existente = _multasUI.Find(m => m.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró la multa a modificar.");
                    return;
                }

                if (cmB_Usuario.SelectedItem is not Usuarios usuario)
                {
                    MostrarError("Selecciona un usuario para la multa.");
                    return;
                }

                Multa actualizada = new Multa(
                    id,
                    usuario,
                    txB_Multa_Motivo.Text,
                    nUD_Multa_MontoBase.Value,
                    dTP_Multa_FechaEmision.Value,
                    ckB_Multa_Pagada.Checked,
                    txb_RutaIma_Mlta.Text,
                    ckB_MultaEstado.Checked
                );

                existente.ActualizarRegistro(actualizada);
                int indiceMulta = _multasUI.IndexOf(existente);
                if (indiceMulta >= 0)
                    _multasUI[indiceMulta] = actualizada;
                ActualizarBloqueosPorMultas();
                RefrescarListaUsuarios();
                MostrarPersonas();
                CargarUsuariosEnCombos();
                CargarImagen(actualizada.RutaImagen, lblFotoMulta);
                MostrarMultas();
                MessageBox.Show("Multa actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al modificar multa");
            }
        }

        private void btn_Multa_Eliminar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txB_IDMulta.Text.Trim(), out int id))
                {
                    MostrarError("El ID de la multa debe ser un número válido.");
                    return;
                }

                new Multa().EliminarRegistro(id.ToString());
                Multa multaLocal = _multasUI.Find(m => m.Id == id);
                if (multaLocal != null)
                    _multasUI.Remove(multaLocal);
                ActualizarBloqueosPorMultas();
                RefrescarListaUsuarios();
                MostrarPersonas();
                CargarUsuariosEnCombos();
                MostrarMultas();
                btn_Multa_Limpiar_Click(sender, e);
                MessageBox.Show("Multa eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar multa");
            }
        }

        private void btn_Multa_Limpiar_Click(object? sender, EventArgs e)
        {
            txB_IDMulta.Clear();
            txB_Multa_Motivo.Clear();
            nUD_Multa_MontoBase.Value = 0;
            dTP_Multa_FechaEmision.Value = DateTime.Now;
            ckB_Multa_Pagada.Checked = false;
            ckB_MultaEstado.Checked = true;
            txb_RutaIma_Mlta.Clear();
            CargarImagen(string.Empty, lblFotoMulta);
        }

        private void btn_Imagen_Mlta_Click(object? sender, EventArgs e)
        {
            string ruta = SeleccionarImagen(oFD_Multa_RutaImagen);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txb_RutaIma_Mlta.Text = ruta;
                CargarImagen(ruta, lblFotoMulta);
            }
        }

        private void MostrarMultas()
        {
            ActualizarBloqueosPorMultas();
            textBox11.Clear();
            foreach (Multa multa in _multasUI)
            {
                EscribirSeparador(textBox11, $"MULTA #{multa.Id}");
                EscribirDato(textBox11, "Usuario", multa.Usuario != null ? $"#{multa.Usuario.Id} - {multa.Usuario.NombreCompleto}" : "Sin usuario");
                EscribirDato(textBox11, "Motivo", multa.Motivo);
                EscribirDato(textBox11, "Monto", $"${multa.MontoBase:F2}");
                EscribirDato(textBox11, "Fecha de emisión", multa.FechaEmision.ToString("dd/MM/yyyy"));
                EscribirDato(textBox11, "Estado de pago", multa.Pagada ? "Pagada" : "Pendiente");
                EscribirDato(textBox11, "Estado", multa.EsActivo ? "Activa" : "Inactiva");
                EscribirDato(textBox11, "Libros relacionados con el usuario", multa.Usuario != null ? ObtenerLibrosRelacionadosConUsuario(multa.Usuario) : "Sin usuario relacionado");
                if (multa.Usuario != null && _usuariosBloqueadosPorMultaUI.Contains(multa.Usuario.Id))
                    EscribirDato(textBox11, "Acceso", "BLOQUEADO por multa vencida");
                textBox11.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // EDITORIALES
        // =====================================================

        private void btnCrearEdi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtIDEditorial, "ID de la editorial", out int id))
                    return;

                if (EditorialesContieneId(id))
                {
                    MostrarError("Ya existe una editorial con ese ID.");
                    return;
                }

                Editorial nueva = CrearEditorialDesdeFormulario(id);
                nueva.InsertarRegistro(nueva);
                _editorialesUI.Add(nueva);
                _estadoEditorialUI[id] = cbxEstadoEdi.Text;
                MostrarEditoriales();
                CargarImagen(nueva.RutaImagen, lblFotoEditorial);
                MessageBox.Show("Editorial creada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al crear editorial");
            }
        }

        private void btnBuscarEdi_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtIDEditorial.Text.Trim(), out int id))
            {
                MostrarError("El ID de la editorial debe ser un número válido.");
                return;
            }

            Editorial encontrado = _editorialesUI.Find(e => e.Id == id);
            if (encontrado == null)
            {
                MessageBox.Show("No se encontró la editorial.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarEditorialEnFormulario(encontrado);
            MessageBox.Show("Editorial encontrada.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMostrarEdi_Click(object? sender, EventArgs e)
        {
            MostrarEditoriales();
        }

        private void btnActualizarEdi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtIDEditorial, "ID de la editorial", out int id))
                    return;

                Editorial existente = _editorialesUI.Find(e => e.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró la editorial a actualizar.");
                    return;
                }

                Editorial actualizada = CrearEditorialDesdeFormulario(id);
                existente.ActualizarRegistro(actualizada);
                _estadoEditorialUI[id] = cbxEstadoEdi.Text;
                int indiceEditorial = _editorialesUI.IndexOf(existente);
                if (indiceEditorial >= 0)
                    _editorialesUI[indiceEditorial] = actualizada;
                MostrarEditoriales();
                CargarImagen(actualizada.RutaImagen, lblFotoEditorial);
                MessageBox.Show("Editorial actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al actualizar editorial");
            }
        }

        private void btnBorarEdi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtIDEditorial.Text.Trim(), out int id))
                {
                    MostrarError("El ID de la editorial debe ser un número válido.");
                    return;
                }

                new Editorial().EliminarRegistro(id.ToString());
                Editorial editorialLocal = _editorialesUI.Find(e => e.Id == id);
                if (editorialLocal != null)
                    _editorialesUI.Remove(editorialLocal);
                _estadoEditorialUI.Remove(id);
                MostrarEditoriales();
                MessageBox.Show("Editorial eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar editorial");
            }
        }

        private Editorial CrearEditorialDesdeFormulario(int id)
        {
            return new Editorial(
                id,
                txtNombreEdi.Text,
                txtPais.Text,
                dtpAnioFundacion.Value.Year,
                txtCorreoEdi.Text,
                txtImagenEdi.Text,
                EstadoEsActivoEditorial(cbxEstadoEdi.Text)
            );
        }

        private bool EditorialesContieneId(int id)
        {
            return _editorialesUI.Any(e => e.Id == id);
        }

        private void CargarEditorialEnFormulario(Editorial editorial)
        {
            txtIDEditorial.Text = editorial.Id.ToString();
            txtNombreEdi.Text = editorial.Nombre;
            txtPais.Text = editorial.Pais;
            dtpAnioFundacion.Value = new DateTime(editorial.AnioFundacion, 1, 1);
            txtCorreoEdi.Text = editorial.CorreoContacto;
            txtImagenEdi.Text = editorial.RutaImagen;
            cbxEstadoEdi.Text = ObtenerEstadoEditorial(editorial.Id, editorial.EsActivo);
            CargarImagen(editorial.RutaImagen, lblFotoEditorial);
        }

        private void btn_Imagen_Edi_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            string ruta = SeleccionarImagen(dialogo);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txtImagenEdi.Text = ruta;
                CargarImagen(ruta, lblFotoEditorial);
            }
        }

        private void MostrarEditoriales()
        {
            txbInfoEdi.Clear();
            foreach (Editorial editorial in _editorialesUI)
            {
                EscribirSeparador(txbInfoEdi, $"EDITORIAL #{editorial.Id}");
                EscribirDato(txbInfoEdi, "Nombre", editorial.Nombre);
                EscribirDato(txbInfoEdi, "País", editorial.Pais);
                EscribirDato(txbInfoEdi, "Año de fundación", editorial.AnioFundacion);
                EscribirDato(txbInfoEdi, "Correo", editorial.CorreoContacto);
                EscribirDato(txbInfoEdi, "Antigüedad", $"{editorial.CalcularAntiguedad()} años");
                EscribirDato(txbInfoEdi, "Estado", ObtenerEstadoEditorial(editorial.Id, editorial.EsActivo));
                EscribirDato(txbInfoEdi, "Imagen", editorial.RutaImagen);
                txbInfoEdi.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // ADMINISTRADORES
        // =====================================================

        private void btnCrearAdm_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtCodigoAdm, "Código del administrador", out int id) ||
                    !TryParseInt(txtEdadAdmin, "Edad", out int edad) ||
                    !TryGetNivelAcceso(out int nivel))
                    return;

                if (Administrador.ObtenerTodos().Any(a => a.Id == id))
                {
                    MostrarError("Ya existe un administrador con ese código.");
                    return;
                }

                Administrador nuevo = new Administrador(
                    id,
                    txtNombreAdmin.Text,
                    edad,
                    txtCorreoAdmin.Text,
                    nivel,
                    txtDepartamento.Text,
                    dtpFechaIngreso.Value,
                    txtImagenAdm.Text,
                    EstadoEsActivoAdministrador(cbxEstadoAdm.Text)
                );

                nuevo.InsertarRegistro(nuevo);
                _estadoAdministradorUI[id] = cbxEstadoAdm.Text;
                MostrarAdministradores();
                CargarImagen(nuevo.RutaImagen, lblFotoAdministrador);
                MessageBox.Show("Administrador creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al crear administrador");
            }
        }

        private void btnBuscarAdm_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtCodigoAdm.Text.Trim(), out int id))
            {
                MostrarError("El código del administrador debe ser un número válido.");
                return;
            }

            Administrador encontrado = Administrador.ObtenerTodos().Find(a => a.Id == id);
            if (encontrado == null)
            {
                MessageBox.Show("No se encontró el administrador.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarAdministradorEnFormulario(encontrado);
            MessageBox.Show("Administrador encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMostrarAdm_Click(object? sender, EventArgs e)
        {
            MostrarAdministradores();
        }

        private void btnActualizarAdm_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtCodigoAdm, "Código del administrador", out int id) ||
                    !TryParseInt(txtEdadAdmin, "Edad", out int edad) ||
                    !TryGetNivelAcceso(out int nivel))
                    return;

                Administrador existente = Administrador.ObtenerTodos().Find(a => a.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró el administrador a actualizar.");
                    return;
                }

                Administrador actualizado = new Administrador(
                    id,
                    txtNombreAdmin.Text,
                    edad,
                    txtCorreoAdmin.Text,
                    nivel,
                    txtDepartamento.Text,
                    dtpFechaIngreso.Value,
                    txtImagenAdm.Text,
                    EstadoEsActivoAdministrador(cbxEstadoAdm.Text)
                );

                existente.ActualizarRegistro(actualizado);
                _estadoAdministradorUI[id] = cbxEstadoAdm.Text;
                MostrarAdministradores();
                CargarImagen(actualizado.RutaImagen, lblFotoAdministrador);
                MessageBox.Show("Administrador actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al actualizar administrador");
            }
        }

        private void btnBorarAdm_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtCodigoAdm.Text.Trim(), out int id))
                {
                    MostrarError("El código del administrador debe ser un número válido.");
                    return;
                }

                new Administrador().EliminarRegistro(id.ToString());
                _estadoAdministradorUI.Remove(id);
                MostrarAdministradores();
                MessageBox.Show("Administrador eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar administrador");
            }
        }

        private void btn_Imagen_Admin_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            string ruta = SeleccionarImagen(dialogo);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txtImagenAdm.Text = ruta;
                CargarImagen(ruta, lblFotoAdministrador);
            }
        }

        private void CargarAdministradorEnFormulario(Administrador admin)
        {
            txtCodigoAdm.Text = admin.Id.ToString();
            txtNombreAdmin.Text = admin.NombreCompleto;
            txtEdadAdmin.Text = admin.Edad.ToString();
            txtCorreoAdmin.Text = admin.Correo;
            MostrarNivelAcceso(admin.NivelAcceso);
            txtDepartamento.Text = admin.Departamento;
            dtpFechaIngreso.Value = admin.FechaIngreso;
            txtImagenAdm.Text = admin.RutaImagen;
            cbxEstadoAdm.Text = ObtenerEstadoAdministrador(admin.Id, admin.Estado);
            CargarImagen(admin.RutaImagen, lblFotoAdministrador);
        }

        private void MostrarAdministradores()
        {
            txbInfoAdm.Clear();
            foreach (Administrador admin in Administrador.ObtenerTodos())
            {
                EscribirSeparador(txbInfoAdm, $"ADMINISTRADOR #{admin.Id}");
                EscribirDato(txbInfoAdm, "Nombre", admin.NombreCompleto);
                EscribirDato(txbInfoAdm, "Edad", admin.Edad);
                EscribirDato(txbInfoAdm, "Correo", admin.Correo);
                EscribirDato(txbInfoAdm, "Nivel de acceso", $"{admin.NivelAcceso} de 5");
                EscribirDato(txbInfoAdm, "Departamento", admin.Departamento);
                EscribirDato(txbInfoAdm, "Fecha de ingreso", admin.FechaIngreso.ToString("dd/MM/yyyy"));
                EscribirDato(txbInfoAdm, "Estado", ObtenerEstadoAdministrador(admin.Id, admin.Estado));
                EscribirDato(txbInfoAdm, "Imagen", admin.RutaImagen);
                txbInfoAdm.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // PERSONAS
        // =====================================================



        private void btnBuscarPer_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(textBox4.Text.Trim(), out int id))
            {
                MostrarError("El código de la persona debe ser un número válido.");
                return;
            }

            ActualizarBloqueosPorMultas();

            Usuarios? usuario = Usuarios.ObtenerTodos().Find(u => u.Id == id);
            Persona? encontrado = usuario as Persona;

            if (encontrado == null)
                encontrado = Persona.ObtenerTodos().Find(p => p.Id == id);

            if (encontrado == null)
                encontrado = Autores.ObtenerTodos().Find(a => a.Id == id);

            if (encontrado == null)
            {
                _personaSeleccionadaIdUI = null;
                txtInfoPersona.Clear();
                MessageBox.Show(
                    $"No se encontró ninguna persona, usuario o autor con el código {id}.",
                    "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _personaSeleccionadaIdUI = id;

            // Siempre se vuelve a cargar desde la fuente actual. Así una búsqueda
            // nueva reemplaza completamente la anterior y no se acumulan resultados.
            CargarPersonaEnFormulario(encontrado);
            MostrarPersonaSeleccionada(encontrado);

            MessageBox.Show(
                usuario != null
                    ? "Usuario encontrado mediante su código y cargado en Persona."
                    : encontrado is Autores
                        ? "Autor encontrado mediante su código y cargado en Persona."
                        : "Persona encontrada.",
                "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMostrarPer_Click(object? sender, EventArgs e)
        {
            // Persona no muestra todos los registros. Solo muestra el usuario/persona
            // que actualmente está seleccionado mediante Buscar.
            if (_personaSeleccionadaIdUI.HasValue)
                MostrarPersonaSeleccionada(ObtenerPersonaPorId(_personaSeleccionadaIdUI.Value));
            else
                txtInfoPersona.Clear();
        }

        private void btnActualizarPer_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(textBox4, "Código de la persona", out int id) ||
                    !TryParseInt(txtEdad, "Edad", out int edad))
                    return;

                string estadoPersona = NormalizarEstadoPersona(cbxEstadoPer.Text);
                bool activo = estadoPersona.Equals("Activo", StringComparison.OrdinalIgnoreCase);

                Usuarios? usuario = Usuarios.ObtenerTodos().Find(u => u.Id == id);
                if (usuario != null)
                {
                    // Si el registro corresponde a un Usuario, Persona actualiza al
                    // mismo objeto lógico: los cambios se reflejan en Usuarios y Persona.
                    Usuarios actualizado = new Usuarios(
                        id,
                        txtNombreCPer.Text,
                        edad,
                        txtCorreoPer.Text,
                        usuario.LibrosPrestados,
                        CalcularMultaAcumulada(id),
                        usuario.EsProfesor,
                        usuario.RutaImagen,
                        activo);

                    usuario.ActualizarRegistro(actualizado);
                    SincronizarPersonaDesdeUsuario(actualizado);
                    ActualizarBloqueosPorMultas();
                    _personaSeleccionadaIdUI = id;
                    CargarPersonaEnFormulario(actualizado);
                    MostrarPersonaSeleccionada(actualizado);
                    RefrescarListaUsuarios();
                    CargarUsuariosEnCombos();
                    MessageBox.Show("Usuario y Persona actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Persona existente = Persona.ObtenerTodos().Find(p => p.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró la persona a actualizar.");
                    return;
                }

                Persona actualizada = new Persona(id, txtNombreCPer.Text, edad, txtCorreoPer.Text)
                {
                    EsActivo = activo
                };

                _estadoPersonaUI[id] = estadoPersona;
                if (estadoPersona.Equals("Bloqueado", StringComparison.OrdinalIgnoreCase))
                    _motivoBloqueoPersonaUI[id] = "Bloqueo seleccionado manualmente desde el módulo Persona.";
                else
                    _motivoBloqueoPersonaUI.Remove(id);

                existente.ActualizarRegistro(actualizada);
                _personaSeleccionadaIdUI = id;
                CargarPersonaEnFormulario(actualizada);
                MostrarPersonaSeleccionada(actualizada);
                MessageBox.Show("Persona actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al actualizar persona");
            }
        }

        private void btnBorrarPer_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(textBox4.Text.Trim(), out int id))
                {
                    MostrarError("El código de la persona debe ser un número válido.");
                    return;
                }

                Usuarios? usuario = Usuarios.ObtenerTodos().Find(u => u.Id == id);
                Persona? persona = Persona.ObtenerTodos().Find(p => p.Id == id);

                if (usuario == null && persona == null)
                {
                    MostrarError("No se encontró un usuario o persona con ese código.");
                    return;
                }

                if (usuario != null)
                    usuario.EliminarRegistro(id.ToString());

                EliminarPersonaVinculada(id);

                _estadoPersonaUI.Remove(id);
                _motivoBloqueoPersonaUI.Remove(id);
                _usuariosBloqueadosPorMultaUI.Remove(id);

                if (_personaSeleccionadaIdUI == id)
                    _personaSeleccionadaIdUI = null;

                LimpiarCamposPersona();
                RefrescarListaUsuarios();
                CargarUsuariosEnCombos();
                MostrarPersonas();

                MessageBox.Show("El registro fue eliminado de Persona y de Usuarios.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar persona");
            }
        }

        private void CargarPersonaEnFormulario(Persona persona)
        {
            textBox4.Text = persona.Id.ToString();
            txtNombreCPer.Text = persona.NombreCompleto;
            txtEdad.Text = persona.Edad.ToString();
            txtCorreoPer.Text = persona.Correo;

            if (persona is Usuarios usuario)
            {
                ActualizarBloqueosPorMultas();
                cbxEstadoPer.Text = ObtenerEstadoPersona(usuario.Id, usuario.EsActivo);
                txtImagenPer.Text = usuario.RutaImagen;
            }
            else
            {
                cbxEstadoPer.Text = ObtenerEstadoPersona(persona.Id, persona.EsActivo);
                txtImagenPer.Text = persona is Autores autor ? autor.RutaImagen : string.Empty;
            }

            CargarImagen(txtImagenPer.Text, label12);
        }

        private Persona? ObtenerPersonaPorId(int id)
        {
            Usuarios? usuario = Usuarios.ObtenerTodos().Find(u => u.Id == id);
            if (usuario != null)
                return usuario;

            Persona? persona = Persona.ObtenerTodos().Find(p => p.Id == id);
            if (persona != null)
                return persona;

            return Autores.ObtenerTodos().Find(a => a.Id == id);
        }

        private void MostrarPersonaSeleccionada(Persona? persona)
        {
            txtInfoPersona.Clear();

            if (persona == null)
                return;

            ActualizarBloqueosPorMultas();

            Persona? personaActual = ObtenerPersonaPorId(persona.Id);
            if (personaActual == null)
                return;

            EscribirSeparador(txtInfoPersona, $"PERSONA #{personaActual.Id}");
            txtInfoPersona.AppendText(ObtenerInformacionPersonaPolimorfica(personaActual));

            string estado = ObtenerEstadoPersona(personaActual.Id, personaActual.EsActivo);
            EscribirDato(txtInfoPersona, "Estado administrativo", estado);

            Usuarios? usuarioRelacionado = Usuarios.ObtenerTodos().Find(u => u.Id == personaActual.Id);
            if (usuarioRelacionado != null)
            {
                EscribirDato(txtInfoPersona, "Multa acumulada", $"${CalcularMultaAcumulada(usuarioRelacionado.Id):F2}");
                EscribirDato(txtInfoPersona, "Libros relacionados", ObtenerLibrosRelacionadosConUsuario(usuarioRelacionado));
            }

            if (estado.Equals("Bloqueado", StringComparison.OrdinalIgnoreCase))
                EscribirDato(txtInfoPersona, "Motivo del bloqueo", ObtenerMotivoBloqueo(personaActual.Id));
        }

        private void MostrarPersonas()
        {
            // La vista Persona es una consulta individual. Nunca conserva ni acumula
            // resultados de búsquedas anteriores.
            if (!_personaSeleccionadaIdUI.HasValue)
            {
                txtInfoPersona.Clear();
                return;
            }

            MostrarPersonaSeleccionada(ObtenerPersonaPorId(_personaSeleccionadaIdUI.Value));
        }

        private void SincronizarPersonaDesdeUsuario(Usuarios usuario)
        {
            if (usuario == null)
                return;

            // Si existe una instancia independiente de Persona con el mismo código,
            // se actualizan sus datos comunes para mantener ambas vistas sincronizadas.
            Persona? persona = Persona.ObtenerTodos().Find(p => p.Id == usuario.Id);
            if (persona != null)
            {
                persona.NombreCompleto = usuario.NombreCompleto;
                persona.Edad = usuario.Edad;
                persona.Correo = usuario.Correo;
                persona.EsActivo = usuario.EsActivo;
            }

            _estadoPersonaUI[usuario.Id] = ObtenerEstadoPersonaDesdeUsuario(usuario);
        }

        private string ObtenerEstadoPersonaDesdeUsuario(Usuarios usuario)
        {
            if (_usuariosBloqueadosPorMultaUI.Contains(usuario.Id))
                return "Bloqueado";

            if (_estadoPersonaUI.TryGetValue(usuario.Id, out string estado) &&
                estado.Equals("Egresado", StringComparison.OrdinalIgnoreCase))
                return "Egresado";

            return usuario.EsActivo ? "Activo" : "Bloqueado";
        }

        private void ActualizarVistaPersonaSiCorresponde(int idUsuario)
        {
            if (_personaSeleccionadaIdUI == idUsuario)
            {
                Persona? personaActual = ObtenerPersonaPorId(idUsuario);
                if (personaActual != null)
                    CargarPersonaEnFormulario(personaActual);
                MostrarPersonaSeleccionada(personaActual);
            }
        }

        private void EliminarPersonaVinculada(int id)
        {
            Persona? persona = Persona.ObtenerTodos().Find(p => p.Id == id);
            if (persona != null)
                persona.EliminarRegistro(id.ToString());
        }

        private void LimpiarCamposPersona()
        {
            textBox4.Clear();
            txtNombreCPer.Clear();
            txtEdad.Clear();
            txtCorreoPer.Clear();
            txtImagenPer.Clear();
            cbxEstadoPer.SelectedIndex = -1;
            cbxEstadoPer.Text = string.Empty;
            CargarImagen(string.Empty, label12);
        }

        private void btn_Imagen_Pers_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            string ruta = SeleccionarImagen(dialogo);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txtImagenPer.Text = ruta;
                CargarImagen(ruta, label12);
            }
        }

        // =====================================================
        // EVENTO QUE YA EXISTÍA EN EL DESIGNER
        // =====================================================

        private void label51_Click(object? sender, EventArgs e)
        {
        }
    }
}