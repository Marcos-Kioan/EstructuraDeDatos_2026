using System;

<<<<<<< HEAD
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
=======
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
>>>>>>> 0c5ad0346207536e9cb736a676fe92d2b52232f6
    }

    public override string ToString()
    {
<<<<<<< HEAD
        return $"Id: {Id,4} | Hash: {HashValidacion,20} | Peso: {PesoBytes,5} bytes";
=======
        return $"ID: {Id,4} | Monto: {Monto,10:F2} | Timestamp: {Timestamp}";
>>>>>>> 0c5ad0346207536e9cb736a676fe92d2b52232f6
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
=======
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
>>>>>>> 0c5ad0346207536e9cb736a676fe92d2b52232f6
                );
            }

            // Mostrar estado inicial
<<<<<<< HEAD
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
=======
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
>>>>>>> 0c5ad0346207536e9cb736a676fe92d2b52232f6
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }

<<<<<<< HEAD
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
=======
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
>>>>>>> 0c5ad0346207536e9cb736a676fe92d2b52232f6
    }
}