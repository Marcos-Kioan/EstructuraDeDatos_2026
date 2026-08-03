using System;

<<<<<<< HEAD
struct Transaccion
{
    public int Id;
    public double Monto;
    public long Timestamp;

    public Transaccion(int id, double monto, long timestamp)
    {
        Id = id;
        Monto = monto;
        Timestamp = timestamp;
    }

    public override string ToString()
    {
        return $"ID: {Id,4} | Monto: {Monto,10:F2} | Timestamp: {Timestamp}";
    }
}

=======
>>>>>>> ce317a33059c587964d2c46da8dafc58709384c0
class Program
{
    static void Main(string[] args)
    {
        try
        {
<<<<<<< HEAD
            Console.WriteLine("===== OPTIMIZADOR DE BITÁCORAS - INSERTION SORT =====\n");

            // Generar 50 transacciones: 45 ordenadas + 5 desordenadas
            int total = 50;
            Transaccion[] bitacora = new Transaccion[total];
            Random rng = new Random();

            // Primeras 45: ordenadas por ID
            for (int i = 0; i < 45; i++)
            {
                bitacora[i] = new Transaccion(
                    id: i + 1,
                    monto: Math.Round(rng.NextDouble() * 9999.99 + 0.01, 2),
                    timestamp: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + i * 100
                );
            }

            // Ultimas 5: desordenadas
            int[] idsDesordenados = { 48, 46, 50, 47, 49 };
            for (int i = 0; i < 5; i++)
            {
                bitacora[45 + i] = new Transaccion(
                    id: idsDesordenados[i],
                    monto: Math.Round(rng.NextDouble() * 9999.99 + 0.01, 2),
                    timestamp: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + (45 + i) * 100
                );
            }

            // Mostrar estado inicial
            Console.WriteLine("=== Transacciones ANTES de ordenar ===");
            foreach (var t in bitacora)
                Console.WriteLine(t);

            // Ejecutar ordenamiento por insercion
            int totalDesplazamientos = OrdenarPorInsercion(bitacora);

            // Mostrar resultado final
            Console.WriteLine("\n=== Transacciones DESPUES de ordenar por ID ===");
            foreach (var t in bitacora)
                Console.WriteLine(t);

            // Mostrar estadisticas
            Console.WriteLine($"\nTotal de desplazamientos realizados: {totalDesplazamientos}");
            int peorCaso = total * (total - 1) / 2;
            double eficiencia = (1 - (double)totalDesplazamientos / peorCaso) * 100;
            Console.WriteLine($"Eficiencia respecto al peor caso: {eficiencia:F1}%");
            Console.WriteLine("Complejidad: Mejor caso O(n) | Peor y promedio O(n²)");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"[ERROR] Desbordamiento: {ex.Message}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"[ERROR] Formato invalido: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR inesperado]: {ex.Message}");
=======
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
>>>>>>> ce317a33059c587964d2c46da8dafc58709384c0
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }

<<<<<<< HEAD
    // Algoritmo Insertion Sort con contador de desplazamientos
    static int OrdenarPorInsercion(Transaccion[] arreglo)
    {
        int n = arreglo.Length;
        int desplazamientos = 0;

        // El ciclo empieza en 1 porque el elemento 0 ya esta ordenado
        for (int i = 1; i < n; i++)
        {
            // Elemento actual a insertar en su lugar
            Transaccion clave = arreglo[i];
            int j = i - 1;

            // Desplazar elementos mayores hacia la derecha
            while (j >= 0 && arreglo[j].Id > clave.Id)
            {
                arreglo[j + 1] = arreglo[j];
                desplazamientos++;
                j--;
            }

            // Colocar la clave en la posicion correcta (j+1)
            arreglo[j + 1] = clave;
        }

        return desplazamientos;
=======
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
>>>>>>> ce317a33059c587964d2c46da8dafc58709384c0
    }
}