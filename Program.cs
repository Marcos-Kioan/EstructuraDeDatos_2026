using System;

// Estructura inmutable de datos con validacion
public struct RegistroDatos
{
    public int Id;
    public long HashValidacion;
    public int PesoBytes;

    public RegistroDatos(int id, long hash, int pesoBytes)
    {
        // Validacion por contrato
        if (pesoBytes <= 0)
            throw new ArgumentException(
                "PesoBytes debe ser mayor a 0. Un registro no puede tener tamaño nulo o negativo.",
                nameof(pesoBytes));

        Id = id;
        HashValidacion = hash;
        PesoBytes = pesoBytes;
    }

    public override string ToString()
    {
        return $"Id: {Id,4} | Hash: {HashValidacion,20} | Peso: {PesoBytes,5} bytes";
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("===== PROYECTO FINAL - FASE 1: SELECTION SORT =====\n");

            // Generar 40 registros aleatorios
            Random rng = new Random();
            RegistroDatos[] lotes = new RegistroDatos[40];

            for (int i = 0; i < lotes.Length; i++)
            {
                lotes[i] = new RegistroDatos(
                    id: rng.Next(1, 1001),
                    hash: rng.NextInt64(),
                    pesoBytes: rng.Next(10, 5001)
                );
            }

            // Mostrar estado inicial
            Console.WriteLine("=== ESTADO INICIAL (Desordenado) ===");
            foreach (var registro in lotes)
                Console.WriteLine(registro);

            // Ejecutar ordenamiento y obtener metricas
            var (comparaciones, intercambios) = OrdenarPorSeleccion(lotes);

            // Mostrar resultado final
            Console.WriteLine("\n=== ESTADO FINAL (Ordenado por ID) ===");
            foreach (var registro in lotes)
                Console.WriteLine(registro);

            // Mostrar analisis de rendimiento
            Console.WriteLine($"\n=== MÉTRICAS DE RENDIMIENTO ===");
            Console.WriteLine($"Total comparaciones: {comparaciones}");
            Console.WriteLine($"Total intercambios: {intercambios}");
            Console.WriteLine($"Complejidad temporal: O(n²)");
            Console.WriteLine($"Máximo teórico de intercambios: O(n)");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"[ERROR DE VALIDACIÓN] {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR INESPERADO] {ex.Message}");
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }

    // Algoritmo Selection Sort instrumentado con tuplas modernas
    static (long comparaciones, int intercambios) OrdenarPorSeleccion(RegistroDatos[] arreglo)
    {
        long comparaciones = 0;
        int intercambios = 0;
        int n = arreglo.Length;

        for (int i = 0; i < n - 1; i++)
        {
            int indiceMinimo = i;

            // Buscar el elemento menor en el resto del arreglo
            for (int j = i + 1; j < n; j++)
            {
                comparaciones++;
                if (arreglo[j].Id < arreglo[indiceMinimo].Id)
                {
                    indiceMinimo = j;
                }
            }

            // Intercambiar solo si es necesario (tupla moderna C#)
            if (indiceMinimo != i)
            {
                (arreglo[i], arreglo[indiceMinimo]) = (arreglo[indiceMinimo], arreglo[i]);
                intercambios++;
            }
        }

        return (comparaciones, intercambios);
    }
}