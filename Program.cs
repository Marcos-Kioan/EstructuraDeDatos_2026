using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("===== ORDENAMIENTO POR BURBUJA - CALIFICACIONES =====\n");

            // Generar 100 calificaciones aleatorias entre 0 y 100
            int[] calificaciones = new int[100];
            Random rng = new Random();
            for (int i = 0; i < calificaciones.Length; i++)
            {
                calificaciones[i] = rng.Next(0, 101);
            }

            // Mostrar estado inicial
            Console.WriteLine("=== Estado inicial: calificaciones desordenadas ===");
            ImprimirArreglo(calificaciones);

            // Ejecutar ordenamiento y contar intercambios
            int totalIntercambios = OrdenarPorBurbuja(calificaciones);

            // Mostrar estado final
            Console.WriteLine("\n=== Estado final: calificaciones ordenadas (menor a mayor) ===");
            ImprimirArreglo(calificaciones);

            // Mostrar estadistica
            Console.WriteLine($"\nTotal de intercambios realizados: {totalIntercambios}");
            Console.WriteLine("Complejidad del algoritmo: O(n²)");
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"\n[ERROR] Indice fuera de rango: {ex.Message}");
            Console.WriteLine("Revisa los limites de tus ciclos for anidados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR inesperado]: {ex.Message}");
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }

    // Algoritmo Bubble Sort optimizado con bandera y contador
    static int OrdenarPorBurbuja(int[] arreglo)
    {
        int n = arreglo.Length;
        int intercambios = 0;
        bool huboCambio;

        for (int i = 0; i < n - 1; i++)
        {
            huboCambio = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arreglo[j] > arreglo[j + 1])
                {
                    // Intercambio usando tupla (C# moderno)
                    (arreglo[j], arreglo[j + 1]) = (arreglo[j + 1], arreglo[j]);
                    intercambios++;
                    huboCambio = true;
                }
            }
            // Si no hubo cambios, ya esta ordenado: terminamos antes
            if (!huboCambio)
                break;
        }
        return intercambios;
    }

    // Funcion auxiliar para imprimir el arreglo
    static void ImprimirArreglo(int[] arreglo)
    {
        foreach (int valor in arreglo)
        {
            Console.Write($"{valor}, ");
        }
        Console.WriteLine("\b\b "); // Quitar la ultima coma
    }
}