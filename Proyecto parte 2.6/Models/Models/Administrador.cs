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
    public class Administrador : Persona, IAlmacenamientoCRUD
    {
        private static List<Administrador> _listaAdministradores = new List<Administrador>();

        private int _nivelAcceso;
        private string _departamento = string.Empty;
        private DateTime _fechaIngreso;
        private string _rutaImagen = string.Empty;
        private bool _estado;

        public int NivelAcceso
        {
            get => _nivelAcceso;
            set
            {
                if (value < 1 || value > 5)
                    throw new ArgumentException("El nivel de acceso debe estar entre 1 y 5.");
                _nivelAcceso = value;
            }
        }

        public string Departamento
        {
            get => _departamento;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El departamento no puede estar vacío.");
                _departamento = value.Trim();
            }
        }

        public DateTime FechaIngreso
        {
            get => _fechaIngreso;
            set => _fechaIngreso = value;
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "admin_default.png" : value.Trim();
        }

        public bool Estado
        {
            get => _estado;
            set => _estado = value;
        }

        public Administrador() : base()
        {
            this.NivelAcceso = 3;
            this.Departamento = "General";
            this.FechaIngreso = DateTime.Now;
            this.RutaImagen = "admin_default.png";
            this.Estado = true;
        }

        public Administrador(int idPersona, string nombreCompleto, int edad, string correo, int nivelAcceso, string departamento, DateTime fechaIngreso, string rutaImagen, bool estado)
            : base(idPersona, nombreCompleto, edad, correo)
        {
            this.NivelAcceso = nivelAcceso;
            this.Departamento = departamento;
            this.FechaIngreso = fechaIngreso;
            this.RutaImagen = rutaImagen;
            this.Estado = estado;
        }

        public bool TienePermiso()
        {
            int nivelMinimoEstandar = 3;
            return this.NivelAcceso >= nivelMinimoEstandar;
        }

        public bool TienePermiso(int nivelRequerido)
        {
            if (nivelRequerido < 1 || nivelRequerido > 5)
                throw new ArgumentException("El nivel requerido debe estar entre 1 y 5.");
            return this.NivelAcceso >= nivelRequerido;
        }

        // ---- IAlmacenamientoCRUD (implementación propia, misma lógica que Usuarios) ----
        public new void InsertarRegistro(object objeto)
        {
            if (objeto is Administrador admin)
                _listaAdministradores.Add(admin);
            else
                throw new ArgumentException("El objeto no es del tipo Administrador.");
        }

        public new object ConsultarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            return _listaAdministradores.Find(a => a.Id == idBuscado);
        }

        public new void ActualizarRegistro(object objeto)
        {
            if (objeto is Administrador adminActualizado)
            {
                Administrador existente = _listaAdministradores.Find(a => a.Id == adminActualizado.Id);
                if (existente != null)
                {
                    int indice = _listaAdministradores.IndexOf(existente);
                    _listaAdministradores[indice] = adminActualizado;
                }
                else
                {
                    throw new ArgumentException("No se encontró el administrador a actualizar.");
                }
            }
            else
            {
                throw new ArgumentException("El objeto no es del tipo Administrador.");
            }
        }

        public new void EliminarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            Administrador existente = _listaAdministradores.Find(a => a.Id == idBuscado);
            if (existente != null)
                _listaAdministradores.Remove(existente);
            else
                throw new ArgumentException("No se encontró el administrador a eliminar.");
        }

        public static List<Administrador> ObtenerTodos() => _listaAdministradores;

        public override string ToString()
        {
            string estadoStr = Estado ? "Activo" : "Inactivo";
            return $"[Administrador #{Id}] {NombreCompleto} | Depto: {Departamento} | Nivel: {NivelAcceso} | Estado: {estadoStr}";
        }
    }
}