using System;

// ==============================================
// ESTRUCTURA BASE (REUTILIZADA DE FASES ANTERIORES)
// ==============================================
/// <summary>
/// Estructura que almacena los datos de cada registro del sistema
/// </summary>
public struct RegistroDatos
{
    public int Id;
    public long HashValidacion;
    public int PesoBytes;

    public RegistroDatos(int id, long hash, int pesoBytes)
    {
        if (pesoBytes <= 0)
            throw new ArgumentException("PesoBytes debe ser mayor a 0.", nameof(pesoBytes));
        Id = id;
        HashValidacion = hash;
        PesoBytes = pesoBytes;
    }

    public override string ToString()
    {
        return $"Id: {Id,3} | Hash: {HashValidacion,18} | Peso: {PesoBytes,5} bytes";
    }
}

/// <summary>
/// Nodo individual para la lista simplemente enlazada
/// </summary>
public class NodoRegistro
{
    public RegistroDatos Dato { get; set; }
    public NodoRegistro? Siguiente { get; set; }

    public NodoRegistro(RegistroDatos dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

/// <summary>
/// Gestor de la lista dinámica en memoria Heap
/// </summary>
public class TablaDinamica
{
    private NodoRegistro? cabeza;
    private int contadorRegistros;

    public TablaDinamica()
    {
        cabeza = null;
        contadorRegistros = 0;
    }

    public int Cantidad => contadorRegistros;

    /// <summary>
    /// Inserta un nuevo registro al final de la lista
    /// </summary>
    public void InsertarFinal(RegistroDatos nuevoRegistro)
    {
        if (nuevoRegistro.Equals(default))
            throw new ArgumentNullException(nameof(nuevoRegistro));

        NodoRegistro nuevoNodo = new NodoRegistro(nuevoRegistro);
        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            NodoRegistro actual = cabeza;
            while (actual.Siguiente != null)
                actual = actual.Siguiente;
            actual.Siguiente = nuevoNodo;
        }
        contadorRegistros++;
    }

    /// <summary>
    /// Elimina un registro por su ID
    /// </summary>
    public bool EliminarPorId(int idTarget)
    {
        if (cabeza == null) return false;

        if (cabeza.Dato.Id == idTarget)
        {
            cabeza = cabeza.Siguiente;
            contadorRegistros--;
            return true;
        }

        NodoRegistro anterior = cabeza;
        NodoRegistro? actual = cabeza.Siguiente;

        while (actual != null)
        {
            if (actual.Dato.Id == idTarget)
            {
                anterior.Siguiente = actual.Siguiente;
                contadorRegistros--;
                return true;
            }
            anterior = actual;
            actual = actual.Siguiente;
        }
        return false;
    }

    /// <summary>
    /// Convierte la lista enlazada a un arreglo estático
    /// </summary>
    public RegistroDatos[] ObtenerComoArreglo()
    {
        RegistroDatos[] resultado = new RegistroDatos[contadorRegistros];
        NodoRegistro? actual = cabeza;
        int i = 0;
        while (actual != null)
        {
            resultado[i] = actual.Dato;
            actual = actual.Siguiente;
            i++;
        }
        return resultado;
    }
}

// ==============================================
// ALGORITMOS DE ORDENAMIENTO (REUTILIZADOS)
// ==============================================
class Ordenador
{
    /// <summary>
    /// QuickSort recursivo - O(n log n) promedio
    /// </summary>
    public static void QuickSort(RegistroDatos[] arr, int bajo, int alto)
    {
        if (bajo < alto)
        {
            int pivote = Particionar(arr, bajo, alto);
            QuickSort(arr, bajo, pivote - 1);
            QuickSort(arr, pivote + 1, alto);
        }
    }

    private static int Particionar(RegistroDatos[] arr, int bajo, int alto)
    {
        RegistroDatos valorPivote = arr[alto];
        int i = bajo - 1;
        for (int j = bajo; j < alto; j++)
        {
            if (arr[j].Id <= valorPivote.Id)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
        (arr[i + 1], arr[alto]) = (arr[alto], arr[i + 1]);
        return i + 1;
    }
}

// ==============================================
// BÚSQUEDA BINARIA INDEXADA - O(log n)
// ==============================================
class Buscador
{
    /// <summary>
    /// Realiza búsqueda binaria sobre un arreglo YA ORDENADO
    /// </summary>
    /// <returns>Registro encontrado o null, y número de comparaciones</returns>
    public static (RegistroDatos? resultado, int comparaciones) BuscarRegistroIndexado(RegistroDatos[] arregloOrdenado, int idBuscado)
    {
        int izquierda = 0;
        int derecha = arregloOrdenado.Length - 1;
        int comparaciones = 0;

        while (izquierda <= derecha)
        {
            comparaciones++;
            int medio = (izquierda + derecha) / 2;

            if (arregloOrdenado[medio].Id == idBuscado)
                return (arregloOrdenado[medio], comparaciones);

            if (arregloOrdenado[medio].Id < idBuscado)
                izquierda = medio + 1;
            else
                derecha = medio - 1;
        }

        return (null, comparaciones);
    }
}

// ==============================================
// MENÚ MAESTRO Y SISTEMA PRINCIPAL
// ==============================================
class Program
{
    static TablaDinamica dataCore = new TablaDinamica();
    static RegistroDatos[]? indiceOrdenado = null;

    static void Main(string[] args)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("          DATACORE v4.0 - PROYECTO FINAL          ");
        Console.WriteLine("==================================================\n");

        bool activo = true;
        do
        {
            MostrarMenu();
            try
            {
                Console.Write("\nSelecciona una opción: ");
                string entrada = Console.ReadLine() ?? "";
                
                if (!int.TryParse(entrada, out int opcion))
                {
                    Console.WriteLine(" Entrada inválida. Ingresa un número del 1 al 6.");
                    continue;
                }

                switch (opcion)
                {
                    case 1: InsertarRegistro(); break;
                    case 2: EliminarRegistro(); break;
                    case 3: MostrarTodos(); break;
                    case 4: ConstruirIndiceYOrdenar(); break;
                    case 5: BuscarPorId(); break;
                    case 6:
                        Console.WriteLine(" Saliendo del sistema... ¡Proyecto completado!");
                        activo = false;
                        break;
                    default:
                        Console.WriteLine(" Opción no válida. Intenta nuevamente.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error en operación: {ex.Message}");
            }

            if (activo)
            {
                Console.WriteLine("Presiona cualquier tecla para volver al menú...");
                Console.ReadKey();
                Console.Clear();
            }

        } while (activo);
    }

    static void MostrarMenu()
    {
        Console.WriteLine("==================== MENÚ PRINCIPAL ====================");
        Console.WriteLine("1. Insertar nuevo registro");
        Console.WriteLine("2. Eliminar registro por ID");
        Console.WriteLine("3. Mostrar todos los registros");
        Console.WriteLine("4. Construir índice y ordenar (QuickSort)");
        Console.WriteLine("5. Búsqueda binaria indexada");
        Console.WriteLine("6. Salir del sistema");
        Console.WriteLine("========================================================");
    }

    static void InsertarRegistro()
    {
        Console.WriteLine("\n--- NUEVO REGISTRO ---");
        Console.Write("Ingresa ID numérico: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine(" ID inválido.");
            return;
        }

        RegistroDatos nuevo = new RegistroDatos(
            id: id,
            hash: DateTime.Now.Ticks,
            pesoBytes: new Random().Next(100, 5000)
        );

        dataCore.InsertarFinal(nuevo);
        indiceOrdenado = null; // El índice queda obsoleto al agregar
        Console.WriteLine($" Registro ID {id} agregado exitosamente.");
    }

    static void EliminarRegistro()
    {
        Console.WriteLine("--- ELIMINAR REGISTRO ---");
        Console.Write("Ingresa ID a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine(" ID inválido.");
            return;
        }

        if (dataCore.EliminarPorId(id))
        {
            indiceOrdenado = null; // El índice queda obsoleto al eliminar
            Console.WriteLine($" Registro ID {id} eliminado correctamente.");
        }
        else
        {
            Console.WriteLine($" No se encontró el registro con ID {id}.");
        }
    }

    static void MostrarTodos()
    {
        Console.WriteLine($"--- LISTA TOTAL ({dataCore.Cantidad} registros) ---");
        RegistroDatos[] lista = dataCore.ObtenerComoArreglo();
        
        if (lista.Length == 0)
        {
            Console.WriteLine("📭 La lista está vacía.");
            return;
        }

        foreach (var reg in lista)
            Console.WriteLine(reg);
    }

    static void ConstruirIndiceYOrdenar()
    {
        Console.WriteLine("--- CONSTRUYENDO ÍNDICE Y ORDENANDO ---");
        indiceOrdenado = dataCore.ObtenerComoArreglo();
        
        if (indiceOrdenado.Length == 0)
        {
            Console.WriteLine("📭 No hay registros para ordenar.");
            return;
        }

        Ordenador.QuickSort(indiceOrdenado, 0, indiceOrdenado.Length - 1);
        Console.WriteLine(" Índice construido y ordenado por ID (QuickSort O(n log n)).");
        
        Console.WriteLine("Registros ordenados:");
        foreach (var reg in indiceOrdenado)
            Console.WriteLine(reg);
    }

    static void BuscarPorId()
    {
        Console.WriteLine("--- BÚSQUEDA BINARIA INDEXADA ---");
        
        if (indiceOrdenado == null)
        {
            Console.WriteLine(" Primero debes construir el índice (Opción 4).");
            return;
        }

        Console.Write("Ingresa ID a buscar: ");
        if (!int.TryParse(Console.ReadLine(), out int idBuscar))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var (resultado, comparaciones) = Buscador.BuscarRegistroIndexado(indiceOrdenado, idBuscar);

        if (resultado.HasValue)
        {
            Console.WriteLine($" ENCONTRADO en {comparaciones} comparaciones (O(log n)):");
            Console.WriteLine(resultado.Value);
        }
        else
        {
            Console.WriteLine($" Registro no encontrado. Realizadas {comparaciones} comparaciones.");
        }
    }
}