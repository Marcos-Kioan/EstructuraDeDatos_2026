using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== SIMULADOR DEL CALL STACK =====\n");

        // --------------------------
        // EJERCICIO A: CUENTA REGRESIVA
        // --------------------------
        Console.WriteLine("--- EJERCICIO A: Cuenta Regresiva ---");
        Console.Write("Escribe un número positivo para la cuenta: ");
        
        if (Validador.ValidarNumero(Console.ReadLine(), out int numeroCuenta))
        {
            Console.WriteLine(" FASE DE APILANDO:");
            SimuladorStack.ImprimirCuentaRegresiva(numeroCuenta);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Error: Solo se aceptan enteros positivos.");
            Console.ResetColor();
        }


        // --------------------------
        // EJERCICIO B: SUMATORIA RECURSIVA
        // --------------------------
        Console.WriteLine("\n--- EJERCICIO B: Sumatoria Recursiva ---");
        Console.Write("Escribe un número positivo para sumar: ");

        if (Validador.ValidarNumero(Console.ReadLine(), out int numeroSuma))
        {
            int resultado = SimuladorStack.SumarHasta(numeroSuma);
            Console.WriteLine($"✅ La suma de 1 hasta {numeroSuma} es: {resultado}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Error: Solo se aceptan enteros positivos.");
            Console.ResetColor();
        }

        Console.WriteLine("Presiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
