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
    public class Persona : EntidadBase, IAlmacenamientoCRUD
    {
        private static List<Persona> _listaPersonas = new List<Persona>();

        protected string _nombreCompleto = string.Empty;
        protected int _edad;
        protected string _correo = string.Empty;

        public string NombreCompleto
        {
            get => _nombreCompleto;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre completo no puede estar vacío.");
                _nombreCompleto = value.Trim();
            }
        }

        public int Edad
        {
            get => _edad;
            set
            {
                if (value < 0 || value > 120)
                    throw new ArgumentOutOfRangeException(nameof(value), "La edad debe estar en un rango de 0 a 120 años.");
                _edad = value;
            }
        }

        public string Correo
        {
            get => _correo;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new ArgumentException("El correo electrónico proporcionado no tiene un formato válido.");
                _correo = value.Trim();
            }
        }

        public Persona()
            : base(1, DateTime.Now, true)
        {
            this.NombreCompleto = "Sin Nombre";
            this.Edad = 18;
            this.Correo = "correo@ejemplo.com";
        }

        public Persona(int idPersona, string nombreCompleto, int edad, string correo)
            : base(idPersona, DateTime.Now, true)
        {
            this.NombreCompleto = nombreCompleto;
            this.Edad = edad;
            this.Correo = correo;
        }

        public virtual string ObtenerPerfil()
        {
            return $"ID: {this.Id} | Nombre: {this.NombreCompleto}";
        }

        public virtual string ObtenerPerfil(bool incluirContacto)
        {
            string perfilBase = ObtenerPerfil();
            return incluirContacto ? $"{perfilBase} | Correo: {this.Correo} | Edad: {this.Edad}" : perfilBase;
        }

        // ---- IAlmacenamientoCRUD ----
        public void InsertarRegistro(object objeto)
        {
            if (objeto is Persona persona)
                _listaPersonas.Add(persona);
            else
                throw new ArgumentException("El objeto no es del tipo Persona.");
        }

        public object ConsultarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            return _listaPersonas.Find(p => p.Id == idBuscado);
        }

        public void ActualizarRegistro(object objeto)
        {
            if (objeto is Persona personaActualizada)
            {
                Persona existente = _listaPersonas.Find(p => p.Id == personaActualizada.Id);
                if (existente != null)
                {
                    int indice = _listaPersonas.IndexOf(existente);
                    _listaPersonas[indice] = personaActualizada;
                }
                else
                {
                    throw new ArgumentException("No se encontró la persona a actualizar.");
                }
            }
            else
            {
                throw new ArgumentException("El objeto no es del tipo Persona.");
            }
        }

        public void EliminarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            Persona existente = _listaPersonas.Find(p => p.Id == idBuscado);
            if (existente != null)
                _listaPersonas.Remove(existente);
            else
                throw new ArgumentException("No se encontró la persona a eliminar.");
        }

        public static List<Persona> ObtenerTodos() => _listaPersonas;

        public override string ToString()
        {
            return $"[Persona] {ObtenerPerfil(true)}";
        }
    }
}