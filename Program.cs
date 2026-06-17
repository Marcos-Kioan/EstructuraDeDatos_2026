using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== FIBONACCI: RECURSIVIDAD VS MEMOIZATION =====\n");

        Console.Write("Ingresa un numero (entre 35 y 43): ");
        string entrada = Console.ReadLine();

        int n;
        bool esValido = int.TryParse(entrada, out n);

        if (!esValido || n < 0)
        {
            Console.WriteLine("ERROR: Solo numeros enteros positivos.");
            return;
        }

        if (n > 45)
        {
            Console.WriteLine("Cuidado: El metodo simple tardara mucho tiempo.");
        }


        // --------------------------
        // METODO 1: RECURSIVO SIMPLE
        // --------------------------
        Console.WriteLine("\n--- METODO 1: RECURSIVO SIMPLE (LENTO) ---");
        Stopwatch reloj1 = Stopwatch.StartNew();

        long resultado1 = FibSimple(n);

        reloj1.Stop();
        Console.WriteLine("Resultado: " + resultado1);
        Console.WriteLine("Tiempo: " + reloj1.ElapsedMilliseconds + " ms");


        // --------------------------
        // METODO 2: CON MEMOIZATION
        // --------------------------
        Console.WriteLine("\n--- METODO 2: OPTIMIZADO (RAPIDO) ---");
        
        long[] memoria = new long[n + 1];
        for (int i = 0; i <= n; i++)
        {
            memoria[i] = -1;
        }

        Stopwatch reloj2 = Stopwatch.StartNew();

        long resultado2 = FibMemo(n, memoria);

        reloj2.Stop();
        Console.WriteLine("Resultado: " + resultado2);
        Console.WriteLine("Tiempo: " + reloj2.ElapsedMilliseconds + " ms");


        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }


    static long FibSimple(int num)
    {
        if (num == 0) return 0;
        if (num == 1) return 1;

        return FibSimple(num - 1) + FibSimple(num - 2);
    }


    static long FibMemo(int num, long[] memoria)
    {
        if (num == 0) return 0;
        if (num == 1) return 1;

        if (memoria[num] != -1)
        {
            return memoria[num];
        }

        memoria[num] = FibMemo(num - 1, memoria) + FibMemo(num - 2, memoria);
        
        return memoria[num];
    }
}