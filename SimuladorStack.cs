using System;

public class SimuladorStack
{
    // ==============================================
    // EJERCICIO A: CUENTA REGRESIVA
    // ==============================================
    public static void ImprimirCuentaRegresiva(int numero)
    {
        // CASO BASE: Si llega a 0 o menos, se detiene
        if (numero < 1)
        {
            Console.WriteLine(" CASO BASE ALCANZADO");
            return;
        }

        // FASE 1: APILANDO (Antes de la llamada recursiva)
        Console.WriteLine($"APILANDO: Marco con valor {numero}");

        // Llamada recursiva: Problema más pequeño (n-1)
        ImprimirCuentaRegresiva(numero - 1);

        //  FASE 2: LIBERANDO (Después de la llamada recursiva)
        // Esto se ejecuta en orden INVERSO (LIFO)
        Console.WriteLine($" LIBERANDO: Marco con valor {numero}");
    }


    // ==============================================
    // EJERCICIO B: SUMATORIA RECURSIVA
    // ==============================================
    public static int SumarHasta(int n)
    {
        // 📌 CASO BASE: La suma de 1 es 1
        if (n == 1)
        {
            return 1;
        }

        //  CASO RECURSIVO: n + suma de lo anterior
        return n + SumarHasta(n - 1);
    }
}