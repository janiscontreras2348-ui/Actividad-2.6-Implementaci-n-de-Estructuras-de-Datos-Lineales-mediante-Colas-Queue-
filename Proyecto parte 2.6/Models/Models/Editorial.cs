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
    public class Editorial : EntidadBase, IAlmacenamientoCRUD
    {
        private static List<Editorial> _listaEditoriales = new List<Editorial>();

        private string _nombre = string.Empty;
        private string _pais = string.Empty;
        private int _anioFundacion;
        private string _correoContacto = string.Empty;
        private string _rutaImagen = string.Empty;

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre de la editorial no puede estar vacío.");
                _nombre = value.Trim();
            }
        }

        public string Pais
        {
            get => _pais;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El país no puede estar vacío.");
                _pais = value.Trim();
            }
        }

        public int AnioFundacion
        {
            get => _anioFundacion;
            set
            {
                if (value <= 0 || value > DateTime.Now.Year)
                    throw new ArgumentException("El año de fundación no es válido.");
                _anioFundacion = value;
            }
        }

        public string CorreoContacto
        {
            get => _correoContacto;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new ArgumentException("El correo de contacto no tiene un formato válido.");
                _correoContacto = value.Trim();
            }
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "editorial_default.png" : value.Trim();
        }

        public Editorial() : base(1, DateTime.Now, true)
        {
            this.Nombre = "Editorial Sin Nombre";
            this.Pais = "Desconocido";
            this.AnioFundacion = DateTime.Now.Year;
            this.CorreoContacto = "contacto@editorial.com";
            this.RutaImagen = "editorial_default.png";
        }

        public Editorial(int idEditorial, string nombre, string pais, int anioFundacion, string correoContacto, string rutaImagen, bool estado)
            : base(idEditorial, DateTime.Now, estado)
        {
            this.Nombre = nombre;
            this.Pais = pais;
            this.AnioFundacion = anioFundacion;
            this.CorreoContacto = correoContacto;
            this.RutaImagen = rutaImagen;
        }

        public int CalcularAntiguedad()
        {
            return DateTime.Now.Year - this.AnioFundacion;
        }

        public int CalcularAntiguedad(int anioReferencia)
        {
            if (anioReferencia < this.AnioFundacion)
                throw new ArgumentException("El año de referencia no puede ser anterior a la fundación.");
            return anioReferencia - this.AnioFundacion;
        }

        public void InsertarRegistro(object objeto)
        {
            if (objeto is Editorial editorial)
                _listaEditoriales.Add(editorial);
            else
                throw new ArgumentException("El objeto no es del tipo Editorial.");
        }

        public object ConsultarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            return _listaEditoriales.Find(e => e.Id == idBuscado);
        }

        public void ActualizarRegistro(object objeto)
        {
            if (objeto is Editorial editorialActualizada)
            {
                Editorial existente = _listaEditoriales.Find(e => e.Id == editorialActualizada.Id);
                if (existente != null)
                {
                    int indice = _listaEditoriales.IndexOf(existente);
                    _listaEditoriales[indice] = editorialActualizada;
                }
                else
                {
                    throw new ArgumentException("No se encontró la editorial a actualizar.");
                }
            }
            else
            {
                throw new ArgumentException("El objeto no es del tipo Editorial.");
            }
        }

        public void EliminarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            Editorial existente = _listaEditoriales.Find(e => e.Id == idBuscado);
            if (existente != null)
                _listaEditoriales.Remove(existente);
            else
                throw new ArgumentException("No se encontró la editorial a eliminar.");
        }

        public override string ToString()
        {
            string estadoStr = EsActivo ? "Activa" : "Inactiva";
            return $"[Editorial #{Id}] {Nombre} | País: {Pais} | Fundada: {AnioFundacion} | Estado: {estadoStr}";
        }
    }
}