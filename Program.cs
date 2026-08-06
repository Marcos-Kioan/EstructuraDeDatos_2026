using System;

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

    public void InsertarFinal(RegistroDatos nuevoRegistro)
    {
        if (nuevoRegistro.Equals(default))
            throw new ArgumentNullException(nameof(nuevoRegistro));

        NodoRegistro nuevoNodo = new NodoRegistro(nuevoRegistro);
        if (cabeza == null)
            cabeza = nuevoNodo;
        else
        {
            NodoRegistro actual = cabeza;
            while (actual.Siguiente != null)
                actual = actual.Siguiente;
            actual.Siguiente = nuevoNodo;
        }
        contadorRegistros++;
    }

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

class Ordenador
{
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

class Buscador
{
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

class Program
{
    static TablaDinamica dataCore = new TablaDinamica();
    static RegistroDatos[]? indiceOrdenado = null;

    static void Main(string[] args)
    {
        Console.WriteLine("===== DATACORE ENGINE v4.0 - PROYECTO FINAL =====\n");
        bool activo = true;
        do
        {
            Console.WriteLine("========== MENÚ PRINCIPAL ==========");
            Console.WriteLine("1. Insertar nuevo registro");
            Console.WriteLine("2. Eliminar registro por ID");
            Console.WriteLine("3. Mostrar todos");
            Console.WriteLine("4. Ordenar y crear índice");
            Console.WriteLine("5. Buscar por ID (Búsqueda Binaria)");
            Console.WriteLine("6. Salir");
            Console.WriteLine("====================================");
            Console.Write("Elige una opción: ");
            string op = Console.ReadLine();

            switch (op)
            {
                case "1":
                    Console.Write("Escribe el ID: ");
                    int id = int.Parse(Console.ReadLine());
                    RegistroDatos nuevo = new RegistroDatos(id, DateTime.Now.Ticks, new Random().Next(100, 5000));
                    dataCore.InsertarFinal(nuevo);
                    indiceOrdenado = null;
                    Console.WriteLine("✅ Registro agregado");
                    break;

                case "2":
                    Console.Write("ID a eliminar: ");
                    int idElim = int.Parse(Console.ReadLine());
                    if (dataCore.EliminarPorId(idElim))
                    {
                        indiceOrdenado = null;
                        Console.WriteLine("✅ Eliminado");
                    }
                    else Console.WriteLine("❌ No existe");
                    break;

                case "3":
                    RegistroDatos[] lista = dataCore.ObtenerComoArreglo();
                    foreach (var r in lista) Console.WriteLine(r);
                    break;

                case "4":
                    indiceOrdenado = dataCore.ObtenerComoArreglo();
                    if (indiceOrdenado.Length == 0)
                    {
                        Console.WriteLine("📭 No hay registros");
                        break;
                    }
                    Ordenador.QuickSort(indiceOrdenado, 0, indiceOrdenado.Length - 1);
                    Console.WriteLine("✅ Ordenado");
                    foreach (var r in indiceOrdenado) Console.WriteLine(r);
                    break;

                case "5":
                    if (indiceOrdenado == null)
                    {
                        Console.WriteLine("⚠️ Primero ordena (opción 4)");
                        break;
                    }
                    Console.Write("ID a buscar: ");
                    int idBus = int.Parse(Console.ReadLine());
                    var res = Buscador.BuscarRegistroIndexado(indiceOrdenado, idBus);
                    if (res.resultado.HasValue)
                        Console.WriteLine($"✅ Encontrado en {res.comparaciones} pasos:\n{res.resultado.Value}");
                    else
                        Console.WriteLine($"❌ No encontrado en {res.comparaciones} pasos");
                    break;

                case "6":
                    Console.WriteLine("👋 Fin del programa");
                    activo = false;
                    break;

                default:
                    Console.WriteLine("⚠️ Opción inválida");
                    break;
            }

            if (activo)
            {
                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }

        } while (activo);
    }
}