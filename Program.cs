using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== ALGORITMOS RECURSIVOS =====\n");

        // --------------------------
        // PRUEBA DEL FACTORIAL
        // --------------------------
        Console.Write("Escribe un número para calcular su factorial: ");
        string dato1 = Console.ReadLine();

        if (int.TryParse(dato1, out int numeroFactorial))
        {
            try
            {
                long resultadoFact = CalcularFactorial(numeroFactorial);
                Console.WriteLine($"{numeroFactorial}! = {resultadoFact}");
            }
            catch (ArgumentException error)
            {
                Console.WriteLine($"Error: {error.Message}");
            }
        }
        else
        {
            Console.WriteLine("Error: Debes escribir un número entero válido.");
        }


        // --------------------------
        // PRUEBA DE FIBONACCI
        // --------------------------
        Console.Write("\nEscribe la posición para ver la serie Fibonacci: ");
        string dato2 = Console.ReadLine();

        if (int.TryParse(dato2, out int numeroFib))
        {
            try
            {
                long resultadoFib = GenerarFibonacci(numeroFib);
                Console.WriteLine($"Fibonacci en la posición {numeroFib} es: {resultadoFib}");
            }
            catch (ArgumentException error)
            {
                Console.WriteLine($"Error: {error.Message}");
            }
        }
        else
        {
            Console.WriteLine("Error: Debes escribir un número entero válido.");
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }


    // ==============================================
    // FUNCIÓN 1: FACTORIAL RECURSIVO
    // ==============================================
    // Calcula n! = 1 * 2 * 3 * ... * n
    static long CalcularFactorial(int n)
    {
        // Primero validamos que no sea negativo
        if (n < 0)
        {
            throw new ArgumentException("No se puede calcular el factorial de un número negativo.");
        }

        //  CASO BASE: Si es 0 o 1, el resultado siempre es 1
        if (n == 0 || n == 1)
        {
            return 1;
        }

        //  CASO RECURSIVO: n * factorial(n-1)
        return n * CalcularFactorial(n - 1);
    }


    // ==============================================
    // FUNCIÓN 2: FIBONACCI RECURSIVO
    // ==============================================
    // Secuencia: 0, 1, 1, 2, 3, 5, 8...
    static long GenerarFibonacci(int n)
    {
        // Validación de entrada
        if (n < 0)
        {
            throw new ArgumentException("La posición debe ser un número mayor o igual a 0.");
        }

        //  CASOS BASE (tiene dos)
        if (n == 0) return 0; // Primer valor
        if (n == 1) return 1; // Segundo valor

        // 🔄 CASO RECURSIVO: Suma de los dos anteriores
        return GenerarFibonacci(n - 1) + GenerarFibonacci(n - 2);
    }
}