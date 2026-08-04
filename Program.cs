using System;
using System.Diagnostics;

// ESTRUCTURA REUTILIZADA DE LA FASE 1 (SIN MODIFICACIONES)
public struct RegistroDatos
{
    public int Id;
    public long HashValidacion;
    public int PesoBytes;

    public RegistroDatos(int id, long hash, int pesoBytes)
    {
        if (pesoBytes <= 0)
            throw new ArgumentException(
                "PesoBytes debe ser mayor a 0.",
                nameof(pesoBytes));

        Id = id;
        HashValidacion = hash;
        PesoBytes = pesoBytes;
    }

    public override string ToString()
    {
        return $"Id: {Id,5} | Hash: {HashValidacion,20} | Peso: {PesoBytes,5} bytes";
    }
}

class Program
{
    // CONTADORES INSTRUMENTADOS
    static long contadorComparaciones;
    static int contadorIntercambiosSeleccion;
    static int contadorLlamadasQuickSort;

    static void Main(string[] args)
    {
        Console.WriteLine("===== PROYECTO FINAL - FASE 2: QUICKSORT VS SELECCIÓN =====\n");

        const int TAMANO = 10_000;
        Console.WriteLine($"Generando lote de {TAMANO:N0} registros...\n");

        // GENERAR DATOS REPRODUCIBLES
        RegistroDatos[] arregloOriginal = GenerarArregloAleatorio(TAMANO);
        
        // CLONAR PARA AMBOS ALGORITMOS (MISMAS CONDICIONES)
        RegistroDatos[] copiaSeleccion = (RegistroDatos[])arregloOriginal.Clone();
        RegistroDatos[] copiaQuickSort = (RegistroDatos[])arregloOriginal.Clone();

        // ==============================================
        // BENCHMARK 1: SELECTION SORT (FASE 1)
        // ==============================================
        Console.WriteLine("--- EJECUTANDO SELECCIÓN DIRECTA ---");
        contadorComparaciones = 0;
        contadorIntercambiosSeleccion = 0;
        
        Stopwatch swSeleccion = Stopwatch.StartNew();
        OrdenarPorSeleccion(copiaSeleccion);
        swSeleccion.Stop();

        long tiempoSeleccion = swSeleccion.ElapsedMilliseconds;
        long operacionesSeleccion = contadorComparaciones + contadorIntercambiosSeleccion;

        // ==============================================
        // BENCHMARK 2: QUICKSORT (FASE 2)
        // ==============================================
        Console.WriteLine("--- EJECUTANDO QUICKSORT ---");
        contadorLlamadasQuickSort = 0;
        
        Stopwatch swQuick = Stopwatch.StartNew();
        QuickSort(copiaQuickSort, 0, copiaQuickSort.Length - 1);
        swQuick.Stop();

        long tiempoQuick = swQuick.ElapsedMilliseconds;

        // ==============================================
        // REPORTE COMPARATIVO FINAL
        // ==============================================
        Console.WriteLine("\n" + new string('=', 70));
        Console.WriteLine("                REPORTE COMPARATIVO DE ORDENAMIENTO");
        Console.WriteLine(new string('=', 70));
        Console.WriteLine($"Registros procesados: {TAMANO:N0}");
        Console.WriteLine();
        Console.WriteLine("ALGORITMO        | COMPARACIONES | INTERCAMBIOS | TIEMPO (ms)");
        Console.WriteLine("-----------------|---------------|--------------|------------");
        Console.WriteLine($"Selección       | {contadorComparaciones,13:N0} | {contadorIntercambiosSeleccion,12:N0} | {tiempoSeleccion,10}");
        Console.WriteLine($"QuickSort       | Ver O(n log n)| Llamadas: {contadorLlamadasQuickSort,5:N0} | {tiempoQuick,10}");
        Console.WriteLine("-----------------|---------------|--------------|------------");
        
        if (tiempoQuick > 0)
        {
            double mejora = (double)tiempoSeleccion / tiempoQuick;
            Console.WriteLine($" QuickSort fue {mejora:F0} veces más rápido que Selección");
        }
        Console.WriteLine("\nComplejidad teórica: Selección O(n²) | QuickSort O(n log n) promedio");

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }

    // ==============================================
    // MÉTODOS DE ORDENAMIENTO
    // ==============================================

    // SELECTION SORT (REUTILIZADO DE FASE 1)
    static void OrdenarPorSeleccion(RegistroDatos[] arreglo)
    {
        int n = arreglo.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int min = i;
            for (int j = i + 1; j < n; j++)
            {
                contadorComparaciones++;
                if (arreglo[j].Id < arreglo[min].Id)
                    min = j;
            }
            if (min != i)
            {
                (arreglo[i], arreglo[min]) = (arreglo[min], arreglo[i]);
                contadorIntercambiosSeleccion++;
            }
        }
    }

    // QUICKSORT - CONTROL RECURSIVO
    static void QuickSort(RegistroDatos[] arr, int bajo, int alto)
    {
        contadorLlamadasQuickSort++; // INSTRUMENTACIÓN
        if (bajo < alto)
        {
            int indicePivote = Particionar(arr, bajo, alto);
            QuickSort(arr, bajo, indicePivote - 1);
            QuickSort(arr, indicePivote + 1, alto);
        }
    }

    // MÉTODO DE PARTICIONADO (ESQUEMA LOMUTO)
    static int Particionar(RegistroDatos[] arr, int bajo, int alto)
    {
        RegistroDatos pivote = arr[alto]; // Pivote = último elemento
        int i = bajo - 1;

        for (int j = bajo; j < alto; j++)
        {
            if (arr[j].Id <= pivote.Id)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]); // Tupla moderna
            }
        }

        // COLOCAR PIVOTE EN SU POSICIÓN DEFINITIVA
        (arr[i + 1], arr[alto]) = (arr[alto], arr[i + 1]);
        return i + 1;
    }

    // GENERADOR CON SEMILLA FIJA PARA REPRODUCIBILIDAD
    static RegistroDatos[] GenerarArregloAleatorio(int cantidad)
    {
        Random rnd = new Random(42); // SEMILLA FIJA = MISMO RESULTADO SIEMPRE
        RegistroDatos[] arreglo = new RegistroDatos[cantidad];

        for (int i = 0; i < cantidad; i++)
        {
            arreglo[i] = new RegistroDatos(
                id: rnd.Next(1, 100_001),
                hash: rnd.NextInt64(),
                pesoBytes: rnd.Next(10, 5001)
            );
        }
        return arreglo;
    }
}