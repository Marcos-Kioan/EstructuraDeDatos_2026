using System;

// DIFERENCIADOR: Estructura para encapsular los datos del polígono
struct Poligono
{
    public int Lados;
    public double MedidaLado;
    public double Apotema;
    public string Nombre;
}

class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine("=== CALCULADORA DE ÁREA DE POLÍGONOS REGULARES ===");

        // Paso 1: Seleccionar tipo de polígono
        Poligono figura = new Poligono();
        figura.Lados = SeleccionarPoligono(out string nombrePoligono);
        figura.Nombre = nombrePoligono;

        // Paso 2: Pedir datos al usuario (con validaciones)
        figura = PedirDatos(figura);

        // Paso 3: Calcular área
        double area = CalcularArea(figura);

        // Paso 4: Mostrar resultado final
        Console.WriteLine($"Resultado: El área del {figura.Nombre} es: {area:F2} unidades cuadradas");
    }

    // 🔹 Función 1: Muestra menú y devuelve número de lados
    static int SeleccionarPoligono(out string nombre)
    {
        int opcion;
        nombre = "";

        do
        {
            Console.WriteLine("\n--- MENÚ DE POLÍGONOS ---");
            Console.WriteLine("1. Pentágono (5 lados)");
            Console.WriteLine("2. Hexágono (6 lados)");
            Console.WriteLine("3. Heptágono (7 lados)");
            Console.WriteLine("4. Octágono (8 lados)");
            Console.Write("Elige una opción (1-4): ");

            // Validación de entrada
            if (int.TryParse(Console.ReadLine(), out opcion) && opcion >= 1 && opcion <= 4)
            {
                break;
            }
            Console.WriteLine(" Opción inválida. Intenta nuevamente.");
        } while (true);

        // Asignar lados y nombre
        switch (opcion)
        {
            case 1: nombre = "Pentágono"; return 5;
            case 2: nombre = "Hexágono"; return 6;
            case 3: nombre = "Heptágono"; return 7;
            case 4: nombre = "Octágono"; return 8;
            default: nombre = "Desconocido"; return 0;
        }
    }

    // 🔹 Función 2: Solicita datos con validación de números positivos
    static Poligono PedirDatos(Poligono poligono)
    {
        // Validar medida del lado
        poligono.MedidaLado = PedirNumeroPositivo($"\nIngresa la medida del lado del {poligono.Nombre}: ");
        
        // Validar apotema
        poligono.Apotema = PedirNumeroPositivo($"Ingresa la medida de la apotema del {poligono.Nombre}: ");

        return poligono;
    }

    // 🔹 Función auxiliar: Valida que sea un número decimal positivo
    static double PedirNumeroPositivo(string mensaje)
    {
        double valor;
        do
        {
            Console.Write(mensaje);
            // Usamos TryParse tal como sugiere la guía de IA
            if (double.TryParse(Console.ReadLine(), out valor) && valor > 0)
            {
                break;
            }
            Console.WriteLine("Error: Debes ingresar un número mayor a 0. Intenta otra vez.");
        } while (true);

        return valor;
    }

    // 🔹 Función 3: Calcula el área
    static double CalcularArea(Poligono poligono)
    {
        // Fórmula: Área = (Perímetro × Apotema) / 2
        double perimetro = poligono.Lados * poligono.MedidaLado;
        return (perimetro * poligono.Apotema) / 2;
    }
}    