/*
 * ENCABEZADO DE AUTORÍA
 * Equipo N°: 3
 * Integrantes:
 * 1. CONTRERAS Rodriguez Janis Isabel
 * 2. TORRES Barrera Jose Angel
 * 3. MACÍAS Cruz Meredith Miranda
 */
using System.Windows.Forms;

namespace Biblioteca
{
    public interface IPanelCRUD
    {
        void EjecutarGuardar();

        void EjecutarBuscar(string id, ErrorProvider alerta);

        void EjecutarActualizar();

        void EjecutarEliminar(StatusStrip barraEstado);
    }
}