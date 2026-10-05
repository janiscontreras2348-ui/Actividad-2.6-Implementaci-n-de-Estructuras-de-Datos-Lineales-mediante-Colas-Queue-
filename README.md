# Models: Gestión de Biblioteca Escolar

**Universidad de Guadalajara · Centro Universitario de Tonalá**
**Asignatura:** 2026B_IH050_POE (Programación Orientada a Eventos)
**Actividad 2.6:** Implementación de Estructuras de Datos Lineales mediante Colas (Queue)

## Equipo N° 3

- CONTRERAS Rodríguez Janis Isabel
- MACÍAS Cruz Meredith Miranda
- TORRES Barrera José Ángel

---

## Descripción

Esta carpeta contiene la capa de **Modelos** del proyecto *Gestión de Biblioteca Escolar*. En esta actividad se agregó una **cola FIFO** (`Queue<T>`) a la clase `Reserva` para manejar la **cola de espera de reservas de libros**: la primera persona que reserva un libro es la primera en ser atendida.

## Escenario FIFO

```
Enqueue (entra al final)                     Dequeue (sale por el frente)
        │                                              ▲
        ▼                                              │
   ┌─────────┬─────────┬─────────┐
   │  #54    │  #22    │  #87    │   ← frente (Peek)
   └─────────┴─────────┴─────────┘
```

## Encapsulamiento de la cola

En la clase `Reserva`:

- **Campo privado:** `private Queue<Reserva> _colaReservas;`
- **Propiedad de solo lectura:** `ColaReservas` expone únicamente `get`, así nadie puede reemplazar ni destruir la cola desde fuera.
- **Inicialización:** `new Queue<Reserva>()` en los dos constructores, para que la cola exista desde que nace el objeto.

## Funciones de control de la cola

Todas se resuelven con los métodos nativos de `Queue<T>`, ciclos `foreach` y condicionales tradicionales. **No se usan expresiones lambda, delegados ni LINQ.**

| # | Método | Método nativo | Qué hace |
|---|--------|---------------|----------|
| 1 | `Encolar(Reserva reserva)` | `Enqueue` | Agrega una reserva al final de la cola. Lanza `ArgumentNullException` si es nula. |
| 2 | `Desencolar()` | `Dequeue` | Saca y devuelve la reserva del frente. Si está vacía, devuelve `null`. |
| 3 | `InspeccionarFrente()` | `Peek` | Muestra quién sigue sin sacarlo de la cola. |
| 4 | `ContarElementos()` | `Count` | Devuelve cuántas reservas hay en espera. |
| 5 | `ExisteEnCola(int id)` | `foreach` | Revisa si ya hay una reserva con ese Id. |
| 6 | `VaciarCola()` | `Clear` | Elimina todas las reservas pendientes. |
| 7 | `VolcarCola()` | `CopyTo` | Copia la cola a un arreglo `Reserva[]` sin vaciarla. El índice 0 es el frente. |

## Integración con el formulario

El formulario heredado `FrmReserva` usa el modelo así:

- **Registrar** → `Encolar`, validando antes con `ExisteEnCola`.
- **Atender Siguiente** → `Desencolar`.
- **Vaciar Cola** → `VaciarCola`.
- **ListBox (`lstCola`)** → se limpia con `Items.Clear()` y se rellena con un `foreach` sobre el arreglo de `VolcarCola()`.
- **Rótulo "Siguiente en ser atendido"** → se actualiza con `InspeccionarFrente`.
- **Botones Atender y Vaciar** → se desactivan (`Enabled = false`) cuando `ContarElementos()` es 0.

> El modelo se declara `static` en el formulario (`private static Reserva _modelo`) para que la cola no se pierda cuando `FrmPrincipal` vuelve a crear el formulario al cambiar de módulo.

## Clases relacionadas

- `Reserva`: contiene la cola y sus 7 funciones.
- `Usuarios` y `Libros`: se buscan por Id o nombre/título al registrar una reserva.
- `EntidadBase`: clase padre de la que hereda `Reserva` (maneja el `Id`).

## Cómo ejecutar

1. Abrir `Models.slnx` en Visual Studio Community.
2. Compilar la solución (`Ctrl + Shift + B`).
3. Ejecutar (`F5`) y entrar al módulo **Reservas**.
4. Registrar varias reservas, atender la siguiente y vaciar la cola para ver el comportamiento FIFO.

---

*Octubre de 2026*
