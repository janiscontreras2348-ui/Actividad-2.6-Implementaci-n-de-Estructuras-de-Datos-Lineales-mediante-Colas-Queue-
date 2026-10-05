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
    public class Usuarios : Persona, IAlmacenamientoCRUD
    {
        private static List<Usuarios> _listaUsuarios = new List<Usuarios>();

        private int _librosPrestados;
        private decimal _multaAcumulada;
        private bool _esProfesor;
        private string _rutaImagen = string.Empty;

        public string Nombre
        {
            get => NombreCompleto;
            set => NombreCompleto = value;
        }

        public int LibrosPrestados
        {
            get => _librosPrestados;
            set
            {
                if (value < 0)
                    throw new ArgumentException("La cantidad de libros no puede ser negativa.");
                _librosPrestados = value;
            }
        }

        public decimal MultaAcumulada
        {
            get => _multaAcumulada;
            set
            {
                if (value < 0)
                    throw new ArgumentException("La multa acumulada no puede ser negativa.");
                _multaAcumulada = value;
            }
        }

        public bool EsProfesor
        {
            get => _esProfesor;
            set => _esProfesor = value;
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "usuario_default.png" : value.Trim();
        }

        public Usuarios() : base()
        {
            this.LibrosPrestados = 0;
            this.MultaAcumulada = 0m;
            this.EsProfesor = false;
            this.RutaImagen = "usuario_default.png";
        }

        public Usuarios(int idUsuario, string nombre, int librosPrestados, decimal multaAcumulada, bool esProfesor)
            : base(idUsuario, nombre, 18, "correo@ejemplo.com")
        {
            this.LibrosPrestados = librosPrestados;
            this.MultaAcumulada = multaAcumulada;
            this.EsProfesor = esProfesor;
            this.RutaImagen = "usuario_default.png";
        }

        public Usuarios(int idPersona, string nombreCompleto, int edad, string correo, int librosPrestados, decimal multaAcumulada, bool esProfesor, string rutaImagen, bool estado)
            : base(idPersona, nombreCompleto, edad, correo)
        {
            this.LibrosPrestados = librosPrestados;
            this.MultaAcumulada = multaAcumulada;
            this.EsProfesor = esProfesor;
            this.RutaImagen = rutaImagen;
            this.EsActivo = estado;
        }

        public decimal CalcularMultaTotal()
        {
            return this.MultaAcumulada;
        }

        public decimal CalcularMultaTotal(int diasRetrasoNuevos)
        {
            if (diasRetrasoNuevos < 0)
                throw new ArgumentException("Los días de retraso no pueden ser negativos.");

            decimal tarifaDiariaPorMora = this.EsProfesor ? 10.0m : 15.0m;
            return this.MultaAcumulada + (diasRetrasoNuevos * tarifaDiariaPorMora);
        }

        public void InsertarRegistro(object objeto)
        {
            if (objeto is Usuarios usuario)
                _listaUsuarios.Add(usuario);
            else
                throw new ArgumentException("El objeto no es del tipo Usuarios.");
        }

        public object ConsultarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            return _listaUsuarios.Find(u => u.Id == idBuscado);
        }

        public void ActualizarRegistro(object objeto)
        {
            if (objeto is Usuarios usuarioActualizado)
            {
                Usuarios existente = _listaUsuarios.Find(u => u.Id == usuarioActualizado.Id);
                if (existente != null)
                {
                    int indice = _listaUsuarios.IndexOf(existente);
                    _listaUsuarios[indice] = usuarioActualizado;
                }
                else
                {
                    throw new ArgumentException("No se encontró el usuario a actualizar.");
                }
            }
            else
            {
                throw new ArgumentException("El objeto no es del tipo Usuarios.");
            }
        }

        public void EliminarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            Usuarios existente = _listaUsuarios.Find(u => u.Id == idBuscado);
            if (existente != null)
                _listaUsuarios.Remove(existente);
            else
                throw new ArgumentException("No se encontró el usuario a eliminar.");
        }

        public static List<Usuarios> ObtenerTodos() => _listaUsuarios;

        public override string ToString()
        {
            string tipoUsuario = EsProfesor ? "Profesor" : "Estudiante";
            string estadoStr = EsActivo ? "Activo" : "Inactivo";
            return $"[Usuario #{Id}] {NombreCompleto} ({tipoUsuario}) | Libros: {LibrosPrestados} | Multa: ${MultaAcumulada:F2} | Estado: {estadoStr}";
        }
    }
}