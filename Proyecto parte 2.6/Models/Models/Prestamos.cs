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

namespace Biblioteca.Models
{
    public class Prestamos : EntidadBase, IAlmacenamientoCRUD
    {
        private static List<Prestamos> _listaPrestamos = new List<Prestamos>();

        private Usuarios _usuario = new Usuarios();
        private Libros[] _librosPrestados = new Libros[3];
        private DateTime _fechaEntrega;
        private bool _status;
        private string _rutaImagen = string.Empty;

        // Pila LIFO: historial de préstamos y devoluciones (el último en registrarse es el primero en deshacerse)
        private Stack<string> _historialCambios;

        public Usuarios Usuario { get => _usuario; set => _usuario = value ?? new Usuarios(); }
        public Libros[] LibrosPrestados { get => _librosPrestados; set => _librosPrestados = value ?? new Libros[3]; }
        public DateTime FechaEntrega { get => _fechaEntrega; set => _fechaEntrega = value; }
        public bool Status { get => _status; set => _status = value; }

        // Solo lectura: nadie fuera de la clase puede reemplazar o modificar la pila directamente
        public Stack<string> HistorialCambios { get { return _historialCambios; } }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "prestamo_default.png" : value.Trim();
        }

        public Prestamos() : base(1, DateTime.Now, true)
        {
            this._historialCambios = new Stack<string>();
            this._usuario = new Usuarios();
            this.FechaEntrega = DateTime.Now.AddDays(7);
            this.Status = true;
            this.RutaImagen = "prestamo_default.png";
        }

        public Prestamos(int id, Usuarios usuario, Libros libroInicial, string rutaImagen, bool estado)
            : base(id, DateTime.Now, estado)
        {
            this._historialCambios = new Stack<string>();
            this._usuario = usuario ?? new Usuarios();
            this.LibrosPrestados[0] = libroInicial;
            this.FechaEntrega = DateTime.Now.AddDays(7);
            this.Status = true;
            this.RutaImagen = rutaImagen;
        }

        // ===================== FUNCIONES DE CONTROL DE LA PILA =====================

        // 1. Apilar acción (Push): tipo = "PRESTAMO" o "DEVOLUCION"
        public void RegistrarAccion(string tipo)
        {
            string registro = tipo + " | " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            _historialCambios.Push(registro);

            // Se actualiza el estado del préstamo según la acción
            if (tipo == "DEVOLUCION")
                this.Status = false;   // ENTREGADO
            else
                this.Status = true;    // PENDIENTE
        }

        // 2. Desapilar y revertir (Pop): valida primero que haya elementos y aplica el cambio inverso
        public string DeshacerUltimo()
        {
            if (_historialCambios.Count == 0)
                return null;

            string ultimo = _historialCambios.Pop();

            if (ultimo.StartsWith("DEVOLUCION"))
                this.Status = true;    // se deshace la devolución: vuelve a PENDIENTE
            else
                this.Status = false;   // se deshace el préstamo: ya no está pendiente

            return ultimo;
        }

        // 3. Inspeccionar cima (Peek)
        public string VerCima()
        {
            if (_historialCambios.Count > 0)
                return _historialCambios.Peek();
            return "(sin acciones)";
        }

        // 4. Conteo de historial (Count)
        public int ContarHistorial()
        {
            return _historialCambios.Count;
        }

        // 5. Validar existencia de estado (ciclo foreach tradicional, sin LINQ ni lambdas)
        public bool ExisteEstado(string estado)
        {
            foreach (string registro in _historialCambios)
            {
                if (registro.StartsWith(estado))
                    return true;
            }
            return false;
        }

        // 6. Vaciar historial (Clear)
        public void VaciarHistorial()
        {
            _historialCambios.Clear();
        }

        // 7. Volcado a arreglo lineal: del cambio más nuevo al más viejo, sin alterar la pila original
        public string[] VolcarAArreglo()
        {
            string[] arreglo = new string[_historialCambios.Count];
            int i = 0;
            foreach (string registro in _historialCambios)
            {
                arreglo[i] = registro;
                i++;
            }
            return arreglo;
        }

        // ===========================================================================

        public string ImprimirP()
        {
            string estado = Status ? "PENDIENTE" : "ENTREGADO";
            return $"Ticket: {Id} | Usuario: {Usuario.Nombre} | Estado: {estado}";
        }

        public string ImprimirP(object parametroExtra)
        {
            return ImprimirP();
        }

        public void InsertarRegistro(object objeto)
        {
            if (objeto is Prestamos prestamo)
                _listaPrestamos.Add(prestamo);
            else
                throw new ArgumentException("El objeto no es del tipo Prestamos.");
        }

        public object ConsultarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            return _listaPrestamos.Find(p => p.Id == idBuscado);
        }

        public void ActualizarRegistro(object objeto)
        {
            if (objeto is Prestamos prestamoActualizado)
            {
                Prestamos existente = _listaPrestamos.Find(p => p.Id == prestamoActualizado.Id);
                if (existente != null)
                {
                    int indice = _listaPrestamos.IndexOf(existente);
                    _listaPrestamos[indice] = prestamoActualizado;
                }
                else
                {
                    throw new ArgumentException("No se encontró el préstamo a actualizar.");
                }
            }
            else
            {
                throw new ArgumentException("El objeto no es del tipo Prestamos.");
            }
        }

        public void EliminarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            Prestamos existente = _listaPrestamos.Find(p => p.Id == idBuscado);
            if (existente != null)
                _listaPrestamos.Remove(existente);
            else
                throw new ArgumentException("No se encontró el préstamo a eliminar.");
        }

        public override string ToString() => ImprimirP();
    }
}