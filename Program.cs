using System;

readonly struct CoordenadaGPS
{
    public double Latitud { get; }
    public double Longitud { get; }

    public CoordenadaGPS(double lat, double lon)
    {
        // CORREGIDO: nameof usa el nombre real del parametro: lat y lon
        if (lat < -90 || lat > 90)
            throw new ArgumentOutOfRangeException(
                nameof(lat),
                "Latitud invalida. Debe estar entre -90 y 90 grados.");

        if (lon < -180 || lon > 180)
            throw new ArgumentOutOfRangeException(
                nameof(lon),
                "Longitud invalida. Debe estar entre -180 y 180 grados.");

        Latitud = lat;
        Longitud = lon;
    }

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

        Console.WriteLine("--- EXPERIMENTO: Copia por Valor ---");

        CoordenadaGPS c1 = new CoordenadaGPS(19.4326, -99.1332);
        CoordenadaGPS c2 = c1;
        c2 = new CoordenadaGPS(52.5200, 13.4050);

        c1.ImprimirUbicacion("c1 (Ciudad de Mexico)");
        c2.ImprimirUbicacion("c2 (Berlin)");

        Console.WriteLine("\nConclusion: Al copiar un struct se crea una copia independiente.\n");

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
            Console.WriteLine("Error: Debes ingresar solo numeros.");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}