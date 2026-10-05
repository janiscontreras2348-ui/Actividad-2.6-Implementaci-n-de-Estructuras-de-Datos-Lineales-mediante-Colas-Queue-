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
    public class Autores : Persona, IAlmacenamientoCRUD
    {
        private static List<Autores> _listaAutores = new List<Autores>();

        private string _nacionalidad = string.Empty;
        private string _rutaImagen = string.Empty;
        private bool _estado;

        public string Nacionalidad
        {
            get => _nacionalidad;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La nacionalidad no puede estar vacía.");
                _nacionalidad = value.Trim();
            }
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "autor_default.png" : value.Trim();
        }

        public bool Estado
        {
            get => _estado;
            set => _estado = value;
        }

        public int Codigo
        {
            get => Id;
            set => Id = value;
        }

        public string Nombre
        {
            get => NombreCompleto;
            set => NombreCompleto = value;
        }

        public Autores() : base()
        {
            this.NombreCompleto = "Autor Anónimo";
            this.Nacionalidad = "Desconocida";
            this.RutaImagen = "autor_default.png";
            this.Estado = true;
        }

        public Autores(int idPersona, string nombreCompleto, int edad, string correo, string nacionalidad, string rutaImagen, bool estado)
            : base(idPersona, nombreCompleto, edad, correo)
        {
            this.Nacionalidad = nacionalidad;
            this.RutaImagen = rutaImagen;
            this.Estado = estado;
        }

        public Autores(int codigo, string nombre, string nacionalidad)
            : base(codigo, nombre, 40, "autor@biblioteca.com")
        {
            this.Nacionalidad = nacionalidad;
            this.RutaImagen = "autor_default.png";
            this.Estado = true;
        }

        public Autores(int codigo, string nombre, string nacionalidad, string extra)
            : this(codigo, nombre, nacionalidad) { }

        public Autores(int codigo, string nombre, string nacionalidad, int extra)
            : this(codigo, nombre, nacionalidad) { }

        public decimal CalcularRegalias(decimal ventasTotales)
        {
            if (ventasTotales < 0)
                throw new ArgumentException("Las ventas totales no pueden ser negativas.");
            return ventasTotales * 0.10m;
        }

        public decimal CalcularRegalias(decimal ventasTotales, decimal porcentaje)
        {
            if (ventasTotales < 0)
                throw new ArgumentException("Las ventas totales no pueden ser negativas.");
            if (porcentaje < 0)
                porcentaje = 0;
            return ventasTotales * (porcentaje / 100m);
        }

        public void ImprimirA()
        {
            Console.WriteLine($"   Nombre: {NombreCompleto}\n   Código: {Id}\n   Nacionalidad: {Nacionalidad}");
        }

        // ---- IAlmacenamientoCRUD (implementación propia, misma lógica que Usuarios) ----
        public new void InsertarRegistro(object objeto)
        {
            if (objeto is Autores autor)
                _listaAutores.Add(autor);
            else
                throw new ArgumentException("El objeto no es del tipo Autores.");
        }

        public new object ConsultarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            return _listaAutores.Find(a => a.Id == idBuscado);
        }

        public new void ActualizarRegistro(object objeto)
        {
            if (objeto is Autores autorActualizado)
            {
                Autores existente = _listaAutores.Find(a => a.Id == autorActualizado.Id);
                if (existente != null)
                {
                    int indice = _listaAutores.IndexOf(existente);
                    _listaAutores[indice] = autorActualizado;
                }
                else
                {
                    throw new ArgumentException("No se encontró el autor a actualizar.");
                }
            }
            else
            {
                throw new ArgumentException("El objeto no es del tipo Autores.");
            }
        }

        public new void EliminarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            Autores existente = _listaAutores.Find(a => a.Id == idBuscado);
            if (existente != null)
                _listaAutores.Remove(existente);
            else
                throw new ArgumentException("No se encontró el autor a eliminar.");
        }

        public static List<Autores> ObtenerTodos() => _listaAutores;

        public override string ToString()
        {
            return $"Código: {Id} | Autor: {NombreCompleto} | Nacionalidad: {Nacionalidad} | Estado: {(Estado ? "Activo" : "Inactivo")}";
        }
    }
}