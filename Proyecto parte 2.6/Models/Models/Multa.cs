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
    public class Multa : EntidadBase, IAlmacenamientoCRUD
    {
        private static List<Multa> _listaMultas = new List<Multa>();

        private Usuarios _usuario = new Usuarios();
        private string _motivo = string.Empty;
        private decimal _montoBase;
        private DateTime _fechaEmision;
        private bool _pagada;
        private string _rutaImagen = string.Empty;

        public Usuarios Usuario
        {
            get => _usuario;
            set => _usuario = value ?? new Usuarios();
        }

        public string Motivo
        {
            get => _motivo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El motivo de la multa no puede estar vacío.");
                _motivo = value.Trim();
            }
        }

        public decimal MontoBase
        {
            get => _montoBase;
            set
            {
                if (value < 0)
                    throw new ArgumentException("El monto de la multa no puede ser negativo.");
                _montoBase = value;
            }
        }

        public DateTime FechaEmision
        {
            get => _fechaEmision;
            set => _fechaEmision = value;
        }

        public bool Pagada
        {
            get => _pagada;
            set => _pagada = value;
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "multa_default.png" : value.Trim();
        }

        public Multa() : base(1, DateTime.Now, true)
        {
            this._usuario = new Usuarios();
            this.Motivo = "Retraso en devolución";
            this.MontoBase = 0m;
            this.FechaEmision = DateTime.Now;
            this.Pagada = false;
            this.RutaImagen = "multa_default.png";
        }

        public Multa(int idMulta, Usuarios usuario, string motivo, decimal montoBase, DateTime fechaEmision, bool pagada, string rutaImagen, bool estado)
            : base(idMulta, DateTime.Now, estado)
        {
            this.Usuario = usuario;
            this.Motivo = motivo;
            this.MontoBase = montoBase;
            this.FechaEmision = fechaEmision;
            this.Pagada = pagada;
            this.RutaImagen = rutaImagen;
        }

        public decimal CalcularMontoConRecargo()
        {
            decimal tasaRecargoFija = 0.10m;
            return this.MontoBase + (this.MontoBase * tasaRecargoFija);
        }

        public decimal CalcularMontoConRecargo(decimal tasaRecargo)
        {
            if (tasaRecargo < 0)
                throw new ArgumentException("La tasa de recargo no puede ser negativa.");
            return this.MontoBase + (this.MontoBase * (tasaRecargo / 100m));
        }

        public void InsertarRegistro(object objeto)
        {
            if (objeto is Multa multa)
                _listaMultas.Add(multa);
            else
                throw new ArgumentException("El objeto no es del tipo Multa.");
        }

        public object ConsultarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            return _listaMultas.Find(m => m.Id == idBuscado);
        }

        public void ActualizarRegistro(object objeto)
        {
            if (objeto is Multa multaActualizada)
            {
                Multa existente = _listaMultas.Find(m => m.Id == multaActualizada.Id);
                if (existente != null)
                {
                    int indice = _listaMultas.IndexOf(existente);
                    _listaMultas[indice] = multaActualizada;
                }
                else
                {
                    throw new ArgumentException("No se encontró la multa a actualizar.");
                }
            }
            else
            {
                throw new ArgumentException("El objeto no es del tipo Multa.");
            }
        }

        public void EliminarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            Multa existente = _listaMultas.Find(m => m.Id == idBuscado);
            if (existente != null)
                _listaMultas.Remove(existente);
            else
                throw new ArgumentException("No se encontró la multa a eliminar.");
        }

        public override string ToString()
        {
            string estadoPago = Pagada ? "Pagada" : "Pendiente";
            return $"[Multa #{Id}] Usuario: {Usuario.NombreCompleto} | Motivo: {Motivo} | Monto: ${MontoBase:F2} | {estadoPago}";
        }
    }
}