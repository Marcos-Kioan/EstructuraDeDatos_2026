using System;
using System.Numerics; // 📚 Necesario para BigInteger

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== FACTORIAL: RECURSIVO VS ITERATIVO VS BIGINTEGER =====\n");

        // ==============================================
        // PARTE A: PRUEBA CON TIPO INT (LÍMITE 32 BITS)
        // ==============================================
        Console.WriteLine("--- PARTE A: Prueba con tipo INT (límite 2,147,483,647) ---");
        Console.WriteLine("n   | Recursivo (int)           | Iterativo (int)");
        Console.WriteLine("-------------------------------------------------------");

        // Probamos del 1 al 20 para ver dónde falla
        for (int i = 1; i <= 20; i++)
        {
            try
            {
                // Usamos bloque checked para que avise cuando se desborda
                // Si quitamos 'checked', los valores salen negativos o erróneos sin avisar
                checked
                {
                    long resultadoRec = FactorialInt(i);
                    long resultadoIte = FactorialIterativo(i);

                    Console.WriteLine($"{i,2} | {resultadoRec,25} | {resultadoIte,25}");
                }
            }
            catch (OverflowException)
            {
                // 📌 PUNTO DE QUIEBRE DOCUMENTADO:
                // A partir de n=13, el número es mayor a 2,147,483,647
                Console.WriteLine($"{i,2} |        DESBORDAMIENTO     |       DESBORDAMIENTO ");
            }
        }

        Console.WriteLine(" OBSERVACIÓN: A partir de n=13 el tipo 'int' ya no sirve, el número es demasiado grande.\n");


        // ==============================================
        // PARTE B: SOLUCIÓN PROFESIONAL CON BIGINTEGER
        // ==============================================
        Console.WriteLine("--- PARTE B: Solución con BigInteger (Sin límite teórico) ---");

        Console.Write("Ingresa un número para calcular su factorial: ");
        if (int.TryParse(Console.ReadLine(), out int numero))
        {
            // Convertimos el entero a BigInteger
            BigInteger resultadoGrande = FactorialProfesional(numero);
            
            Console.WriteLine($" {numero}! = ");
            Console.WriteLine(resultadoGrande);
            Console.WriteLine($" Cantidad de dígitos: {resultadoGrande.ToString().Length}");
        }
        else
        {
            Console.WriteLine(" Entrada inválida");
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }


    // ==============================================
    // FUNCIÓN 1: FACTORIAL RECURSIVO (USANDO INT)
    // ==============================================
    // Límite: Solo funciona bien hasta n=12
    static int FactorialInt(int n)
    {
        // Caso Base
        if (n == 0 || n == 1)
            return 1;

        // Caso Recursivo
        return n * FactorialInt(n - 1);
    }


    // ==============================================
    // FUNCIÓN 2: FACTORIAL ITERATIVO (USANDO INT)
    // ==============================================
    // Mismo límite que el anterior, pero usa bucle en vez de pila de memoria
    static int FactorialIterativo(int n)
    {
        int resultado = 1;
        for (int i = 2; i <= n; i++)
        {
            resultado *= i;
        }
        return resultado;
    }


    // ==============================================
    // FUNCIÓN 3: FACTORIAL PROFESIONAL (BIGINTEGER)
    // ==============================================
    // SIN LÍMITE: Crece todo lo necesario en memoria
    static BigInteger FactorialProfesional(BigInteger n)
    {
        // Caso Base: Usamos BigInteger.One en lugar de 1
        if (n == 0 || n == 1)
            return BigInteger.One;

        // Caso Recursivo
        return n * FactorialProfesional(n - 1);
    }
}