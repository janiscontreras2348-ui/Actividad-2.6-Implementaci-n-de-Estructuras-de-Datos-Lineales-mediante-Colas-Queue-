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
    public interface IAlmacenamientoCRUD
    {
        void InsertarRegistro(object objeto);
        object ConsultarRegistro(string id);
        void ActualizarRegistro(object objeto);
        void EliminarRegistro(string id);
    }
}