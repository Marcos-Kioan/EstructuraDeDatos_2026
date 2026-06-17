using System;

// Struct inmutable para almacenar coordenadas geográficas
readonly struct CoordenadaGPS
{
    // Propiedades de solo lectura
    public double Latitud { get; }
    public double Longitud { get; }

    // Constructor con validación de rangos
    public CoordenadaGPS(double latitude, double longitude)
    {
        // Validación de latitud: rango válido entre -90 y 90 grados
        if (latitude < -90 || latitude > 90)
            throw new ArgumentOutOfRangeException(
                nameof(latitude),
                latitude,
                "Latitud inválida. Debe estar entre -90 y 90 grados.");

        // Validación de longitud: rango válido entre -180 y 180 grados
        if (longitude < -180 || longitude > 180)
            throw new ArgumentOutOfRangeException(
                nameof(longitude),
                longitude,
                "Longitud inválida. Debe estar entre -180 y 180 grados.");

        Latitud = latitude;
        Longitud = longitude;
    }

    // Método para mostrar la ubicación en formato legible
    public void ImprimirUbicacion(string nombre = "Ubicacion")
    {
        Console.WriteLine($"{nombre} -> Lat: {Latitud:F4} | Lon: {Longitud:F4}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== SISTEMA DE COORDENADAS GPS - STRUCTS =====\n");

        // ------------------------------
        // MODULO 1: Demostracion de copia por valor
        // ------------------------------
        Console.WriteLine("--- EXPERIMENTO: Copia por Valor ---");

        // Crear primera coordenada: Ciudad de Mexico
        CoordenadaGPS c1 = new CoordenadaGPS(19.4326, -99.1332);

        // Copiar el contenido de c1 a c2
        CoordenadaGPS c2 = c1;

        // Asignar nuevos valores a c2
        c2 = new CoordenadaGPS(52.5200, 13.4050); // Berlin

        // Mostrar resultados: c1 no cambia
        c1.ImprimirUbicacion("c1 (Ciudad de Mexico)");
        c2.ImprimirUbicacion("c2 (Berlin)");

        Console.WriteLine("\nConclusión: Al copiar un struct se crea una copia independiente.\n");

        // ------------------------------
        // MODULO 2: Entrada de usuario con validacion
        // ------------------------------
        Console.WriteLine("--- INGRESA TU PROPIA UBICACION ---");

        try
        {
            Console.Write("Ingresa Latitud: ");
            double lat = double.Parse(Console.ReadLine()!);

            Console.Write("Ingresa Longitud: ");
            double lon = double.Parse(Console.ReadLine()!);

            CoordenadaGPS miUbicacion = new CoordenadaGPS(lat, lon);
            miUbicacion.ImprimirUbicacion("Tu ubicacion");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Debes ingresar solo números.");
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName == nameof(latitude))
        {
            Console.WriteLine("Error: Latitud fuera de rango. " + ex.Message);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName == nameof(longitude))
        {
            Console.WriteLine("Error: Longitud fuera de rango. " + ex.Message);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}