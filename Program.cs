using System;

// ESTRUCTURA REUTILIZADA DE FASES ANTERIORES
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

// CLASE NODO: BLOQUE BÁSICO DE LA LISTA
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

// CLASE GESTORA DE LA LISTA DINÁMICA
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

    // INSERCIÓN AL INICIO - O(1)
    public void InsertarInicio(RegistroDatos nuevoRegistro)
    {
        if (nuevoRegistro.Equals(default))
            throw new ArgumentNullException(nameof(nuevoRegistro));

        NodoRegistro nuevoNodo = new NodoRegistro(nuevoRegistro);
        nuevoNodo.Siguiente = cabeza;
        cabeza = nuevoNodo;
        contadorRegistros++;
    }

    // INSERCIÓN AL FINAL - O(n)
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

    // ELIMINAR POR ID - O(n)
    public void EliminarPorId(int idTarget)
    {
        if (cabeza == null) return;

        // Caso especial: eliminar la cabeza
        if (cabeza.Dato.Id == idTarget)
        {
            cabeza = cabeza.Siguiente;
            contadorRegistros--;
            return;
        }

        // Caso general: recorrer buscando
        NodoRegistro anterior = cabeza;
        NodoRegistro? actual = cabeza.Siguiente;

        while (actual != null)
        {
            if (actual.Dato.Id == idTarget)
            {
                anterior.Siguiente = actual.Siguiente;
                contadorRegistros--;
                return;
            }
            anterior = actual;
            actual = actual.Siguiente;
        }
    }

    // CONVERTIR A ARREGLO - PUENTE CON FASES ANTERIORES
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

class Program
{
    // Mantenemos QuickSort de la Fase 2 para interoperabilidad
    static void QuickSort(RegistroDatos[] arr, int bajo, int alto)
    {
        if (bajo < alto)
        {
            int pivote = Particionar(arr, bajo, alto);
            QuickSort(arr, bajo, pivote - 1);
            QuickSort(arr, pivote + 1, alto);
        }
    }

    static int Particionar(RegistroDatos[] arr, int bajo, int alto)
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

    static void Main(string[] args)
    {
        Console.WriteLine("===== PROYECTO FINAL - FASE 3: LISTA DINÁMICA EN HEAP =====\n");

        TablaDinamica dataCore = new TablaDinamica();

        // 1. INSERTAR 15 REGISTROS
        Console.WriteLine("--- INSERTANDO 15 REGISTROS ---");
        for (int i = 1; i <= 15; i++)
        {
            RegistroDatos reg = new RegistroDatos(i, i * 123456789L, i * 150);
            dataCore.InsertarFinal(reg);
            Console.WriteLine($"[OK] Registro {i} agregado");
        }

        // 2. ELIMINAR 2 REGISTROS
        Console.WriteLine("\n--- ELIMINANDO Id 5 y Id 11 ---");
        dataCore.EliminarPorId(5);
        dataCore.EliminarPorId(11);
        Console.WriteLine("Eliminación completada sin errores");

        // 3. CONVERTIR A ARREGLO Y ORDENAR
        Console.WriteLine($"\n--- CONVIRTIENDO A ARREGLO ({dataCore.Cantidad} registros) ---");
        RegistroDatos[] arreglo = dataCore.ObtenerComoArreglo();

        Console.WriteLine("--- ORDENANDO CON QUICKSORT ---");
        QuickSort(arreglo, 0, arreglo.Length - 1);

        // 4. MOSTRAR RESULTADO FINAL
        Console.WriteLine("\n=== LISTA FINAL ORDENADA ===");
        foreach (var r in arreglo)
            Console.WriteLine(r);

        Console.WriteLine(" Fase 3 completada: Lista dinámica interoperable con algoritmos anteriores");
        Console.WriteLine("Presiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}