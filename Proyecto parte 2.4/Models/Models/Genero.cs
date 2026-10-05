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
    public class Genero : EntidadBase, IAlmacenamientoCRUD
    {
        private static List<Genero> _listaGeneros = new List<Genero>();

        private string _nombre = string.Empty;
        private string _descripcion = string.Empty;
        private string _rutaImagen = string.Empty;

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre del género no puede estar vacío.");
                _nombre = value.Trim();
            }
        }

        public string Descripcion
        {
            get => _descripcion;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La descripción no puede estar vacía.");
                _descripcion = value.Trim();
            }
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "genero_default.png" : value.Trim();
        }

        public Genero() : base(1, DateTime.Now, true)
        {
            this.Nombre = "General";
            this.Descripcion = "Sin descripción asignada";
            this.RutaImagen = "genero_default.png";
        }

        public Genero(int idGenero, string nombre, string descripcion, string rutaImagen, bool estado)
            : base(idGenero, DateTime.Now, estado)
        {
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.RutaImagen = rutaImagen;
        }

        public string GenerarEtiqueta()
        {
            return $"#{Nombre.ToUpper().Replace(" ", "")}";
        }

        public string GenerarEtiqueta(string prefijo)
        {
            if (string.IsNullOrWhiteSpace(prefijo))
                throw new ArgumentException("El prefijo no puede estar vacío.");
            return $"#{prefijo.ToUpper()}_{Nombre.ToUpper().Replace(" ", "")}";
        }

        public void InsertarRegistro(object objeto)
        {
            if (objeto is Genero genero)
                _listaGeneros.Add(genero);
            else
                throw new ArgumentException("El objeto no es del tipo Genero.");
        }

        public object ConsultarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            return _listaGeneros.Find(g => g.Id == idBuscado);
        }

        public void ActualizarRegistro(object objeto)
        {
            if (objeto is Genero generoActualizado)
            {
                Genero existente = _listaGeneros.Find(g => g.Id == generoActualizado.Id);
                if (existente != null)
                {
                    int indice = _listaGeneros.IndexOf(existente);
                    _listaGeneros[indice] = generoActualizado;
                }
                else
                {
                    throw new ArgumentException("No se encontró el género a actualizar.");
                }
            }
            else
            {
                throw new ArgumentException("El objeto no es del tipo Genero.");
            }
        }

        public void EliminarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            Genero existente = _listaGeneros.Find(g => g.Id == idBuscado);
            if (existente != null)
                _listaGeneros.Remove(existente);
            else
                throw new ArgumentException("No se encontró el género a eliminar.");
        }

        public override string ToString()
        {
            string estadoStr = EsActivo ? "Activo" : "Inactivo";
            return $"[Género #{Id}] {Nombre} | {Descripcion} | Estado: {estadoStr}";
        }
    }
}