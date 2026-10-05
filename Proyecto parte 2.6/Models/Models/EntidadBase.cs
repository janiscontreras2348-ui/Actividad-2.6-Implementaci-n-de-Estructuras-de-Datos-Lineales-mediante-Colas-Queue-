/*
 * ENCABEZADO DE AUTORÍA
 * Equipo N°: 3
 * Integrantes:
 * 1. CONTRERAS Rodriguez Janis Isabel
 * 2. TORRES Barrera Jose Angel
 * 3. MACÍAS Cruz Meredith Miranda
 */
using System;

namespace Biblioteca.Models
{
    public abstract class EntidadBase
    {
        private int _id;
        private DateTime _fechaRegistro;
        private bool _esActivo;

        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El ID debe ser mayor a 0.");
                _id = value;
            }
        }

        public DateTime FechaRegistro
        {
            get => _fechaRegistro;
            set => _fechaRegistro = value;
        }

        public bool EsActivo
        {
            get => _esActivo;
            set => _esActivo = value;
        }

        protected EntidadBase() { }

        protected EntidadBase(int id, DateTime fechaRegistro, bool esActivo)
        {
            Id = id;
            FechaRegistro = fechaRegistro;
            EsActivo = esActivo;
        }
    }
}