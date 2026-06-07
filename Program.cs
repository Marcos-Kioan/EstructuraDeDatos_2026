using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("EL DEMONIO DE LA MEMORIA: VALOR VS REFERENCIA ");

        // --------------------------
        // PRUEBA 1: TIPO DE VALOR (int)
        // --------------------------
        int numeroOriginal = 50; // Valor inicial
        Console.WriteLine($"1. TIPO DE VALOR (int)");
        Console.WriteLine($"Antes de llamar a la función: {numeroOriginal}");

        // Llamamos a la función e intentamos cambiarlo
        CambiarValor(numeroOriginal);

        Console.WriteLine($"Después de llamar a la función: {numeroOriginal}");
        Console.WriteLine(" NO CAMBIÓ: porque se pasó una COPIA del valor.");


        // --------------------------
        // PRUEBA 2: TIPO DE REFERENCIA (Arreglo)
        // --------------------------
        int[] arregloOriginal = { 10, 20, 30 }; // Valor inicial
        Console.WriteLine($"2. TIPO DE REFERENCIA (Arreglo)");
        Console.WriteLine($"Antes de llamar a la función: [{string.Join(", ", arregloOriginal)}]");

        // Llamamos a la función e intentamos cambiarlo
        CambiarReferencia(arregloOriginal);

        Console.WriteLine($"Después de llamar a la función: [{string.Join(", ", arregloOriginal)}]");
        Console.WriteLine("SÍ CAMBIÓ: porque se pasó la DIRECCIÓN en memoria.");


        
        Console.WriteLine("EXPLICACIÓN TÉCNICA:");
        Console.WriteLine("- int: Se guarda en el STACK (memoria rápida). Se copia el dato.");
        Console.WriteLine("- int[]: Los datos están en el HEAP (memoria grande). En el Stack solo vive la dirección.");
    }


    // 🔹 Función 1: Recibe un TIPO DE VALOR
    static void CambiarValor(int x)
    {
        // Aquí solo cambiamos la COPIA que llegó, NO el original
        x = 100;
    }


    // 🔹 Función 2: Recibe un TIPO DE REFERENCIA
    static void CambiarReferencia(int[] arr)
    {
        // Aquí modificamos el dato que está en la dirección original
        arr[0] = 100;
    }
}