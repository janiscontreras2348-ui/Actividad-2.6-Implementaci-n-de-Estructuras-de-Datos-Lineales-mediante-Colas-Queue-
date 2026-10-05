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
    public class Reserva : EntidadBase, IAlmacenamientoCRUD
    {
        private static List<Reserva> _listaReservas = new List<Reserva>();

        private Usuarios _usuario = new Usuarios();
        private Libros _libroReservado = new Libros();
        private DateTime _fechaReserva;
        private DateTime _fechaLimite;
        private string _rutaImagen = string.Empty;
        private bool _estado;

        // Cola de reservas (el primero en reservar es el primero en atenderse)
        private Queue<Reserva> _colaReservas;

        // Solo se puede leer, no se puede reemplazar desde fuera
        public Queue<Reserva> ColaReservas
        {
            get { return _colaReservas; }
        }

        public Usuarios Usuario
        {
            get => _usuario;
            set => _usuario = value ?? new Usuarios();
        }

        public Libros LibroReservado
        {
            get => _libroReservado;
            set => _libroReservado = value ?? new Libros();
        }

        public DateTime FechaReserva
        {
            get => _fechaReserva;
            set => _fechaReserva = value;
        }

        public DateTime FechaLimite
        {
            get => _fechaLimite;
            set
            {
                if (value < _fechaReserva)
                    throw new ArgumentException("La fecha límite no puede ser anterior a la fecha de reserva.");
                _fechaLimite = value;
            }
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "reserva_default.png" : value.Trim();
        }

        public bool Estado
        {
            get => _estado;
            set => _estado = value;
        }

        // Antes: constructor sin parámetros de base y campo propio "_idReserva = 1".
        // Ahora: Id se maneja a través de EntidadBase (base(...)).
        public Reserva()
            : base(1, DateTime.Now, true)
        {
            this._colaReservas = new Queue<Reserva>();
            this._usuario = new Usuarios();
            this._libroReservado = new Libros();
            this.FechaReserva = DateTime.Now;
            this.FechaLimite = DateTime.Now.AddDays(3);
            this.RutaImagen = "reserva_default.png";
            this.Estado = false;
        }

        // Antes: "int idReserva" se guardaba en un campo propio (_idReserva).
        // Ahora: se pasa a EntidadBase y se usa la propiedad Id heredada.
        public Reserva(int id, Usuarios usuario, Libros libroReservado, DateTime fechaReserva, DateTime fechaLimite, string rutaImagen, bool estado)
            : base(id, DateTime.Now, true)
        {
            this._colaReservas = new Queue<Reserva>();
            this.Usuario = usuario;
            this.LibroReservado = libroReservado;
            this.FechaReserva = fechaReserva;
            this.FechaLimite = fechaLimite;
            this.RutaImagen = rutaImagen;
            this.Estado = estado;
        }

        public int CalcularDiasRestantes()
        {
            TimeSpan diferencia = this.FechaLimite - DateTime.Now;
            return diferencia.Days;
        }

        public int CalcularDiasRestantes(DateTime fechaReferencia)
        {
            TimeSpan diferencia = this.FechaLimite - fechaReferencia;
            return diferencia.Days;
        }

        // ---- IAlmacenamientoCRUD (antes Reserva no implementaba nada) ----
        public void InsertarRegistro(object objeto)
        {
            if (objeto is Reserva reserva)
                _listaReservas.Add(reserva);
            else
                throw new ArgumentException("El objeto no es del tipo Reserva.");
        }

        public object ConsultarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            return _listaReservas.Find(r => r.Id == idBuscado);
        }

        public void ActualizarRegistro(object objeto)
        {
            if (objeto is Reserva reservaActualizada)
            {
                Reserva existente = _listaReservas.Find(r => r.Id == reservaActualizada.Id);
                if (existente != null)
                {
                    int indice = _listaReservas.IndexOf(existente);
                    _listaReservas[indice] = reservaActualizada;
                }
                else
                {
                    throw new ArgumentException("No se encontró la reserva a actualizar.");
                }
            }
            else
            {
                throw new ArgumentException("El objeto no es del tipo Reserva.");
            }
        }

        public void EliminarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            Reserva existente = _listaReservas.Find(r => r.Id == idBuscado);
            if (existente != null)
                _listaReservas.Remove(existente);
            else
                throw new ArgumentException("No se encontró la reserva a eliminar.");
        }

        public static List<Reserva> ObtenerTodos() => _listaReservas;

        // ---- Funciones de la cola ----

        // Mete una reserva al final de la cola
        public void Encolar(Reserva reserva)
        {
            if (reserva == null)
                throw new ArgumentNullException(nameof(reserva));
            _colaReservas.Enqueue(reserva);
        }

        // Saca la reserva del frente (solo si hay alguien en la cola)
        public Reserva? Desencolar()
        {
            if (_colaReservas.Count > 0)
                return _colaReservas.Dequeue();
            return null;
        }

        // Ve quién sigue sin sacarlo de la cola
        public Reserva? InspeccionarFrente()
        {
            if (_colaReservas.Count > 0)
                return _colaReservas.Peek();
            return null;
        }

        // Cuántas reservas hay esperando
        public int ContarElementos()
        {
            return _colaReservas.Count;
        }

        // Revisa si ya existe una reserva con ese Id en la cola
        public bool ExisteEnCola(int id)
        {
            foreach (Reserva r in _colaReservas)
            {
                if (r.Id == id)
                    return true;
            }
            return false;
        }

        // Borra todo lo que hay en la cola
        public void VaciarCola()
        {
            _colaReservas.Clear();
        }

        // Copia la cola a un arreglo sin vaciarla (el primero del arreglo es el del frente)
        public Reserva[] VolcarCola()
        {
            Reserva[] arreglo = new Reserva[_colaReservas.Count];
            _colaReservas.CopyTo(arreglo, 0);
            return arreglo;
        }

        public override string ToString()
        {
            string estadoStr = Estado ? "Vigente" : "Cancelada/Vencida";
            return $"[Reserva #{Id}] Usuario: {Usuario.NombreCompleto} | Libro: {LibroReservado.Titulo} | Límite: {FechaLimite:d} | Estado: {estadoStr}";
        }
    }
}