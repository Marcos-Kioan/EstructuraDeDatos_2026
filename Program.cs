using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== MANEJO DE MEMORIA: REF, OUT Y REFERENCIAS =====\n");

        // ==============================================
        // MÓDULO 1: USO DE ref - INTERCAMBIAR VALORES
        // ==============================================
        Console.WriteLine("--- MÓDULO 1: Intercambio con ref ---");
        int num1 = 10;
        int num2 = 25;

        Console.WriteLine($"Antes de intercambiar: num1 = {num1}, num2 = {num2}");
        
        // Usamos la palabra 'ref' al llamar al método
        Intercambiar(ref num1, ref num2);
        
        Console.WriteLine($"Después de intercambiar: num1 = {num1}, num2 = {num2}");
        Console.WriteLine("→ Cambiaron los valores originales porque pasamos la referencia.\n");


        // ==============================================
        // MÓDULO 2: USO DE out - DEVOLVER VARIOS DATOS
        // ==============================================
        Console.WriteLine("--- MÓDULO 2: Cálculo con out ---");
        int dividendo = 17;
        int divisor = 5;

        // 'out' nos permite obtener un segundo valor (el residuo)
        int cociente = CalcularYValidar(dividendo, divisor, out int residuo);

        Console.WriteLine($"{dividendo} entre {divisor} da:");
        Console.WriteLine($"Cociente: {cociente}");
        Console.WriteLine($"Residuo: {residuo}");
        Console.WriteLine("→ Usamos 'out' para sacar dos resultados de una sola función.\n");


        // ==============================================
        // MÓDULO 3: REFERENCIAS DE OBJETOS
        // ==============================================
        Console.WriteLine("--- MÓDULO 3: Referencias de Objetos ---");

        // Creamos el primer alumno
        Alumno alumno1 = new Alumno();
        alumno1.Nombre = "Dany";

        // ¡Aquí está la clave! No creamos uno nuevo, asignamos la referencia
        Alumno alumno2 = alumno1;

        Console.WriteLine($"Nombre en alumno1: {alumno1.Nombre}");
        Console.WriteLine($"Nombre en alumno2: {alumno2.Nombre}");

        // Cambiamos el nombre en la SEGUNDA variable
        alumno2.Nombre = "3Treum";

        Console.WriteLine($"\nDespués de modificar solo alumno2:");
        Console.WriteLine($"Nombre en alumno1: {alumno1.Nombre}"); // ¡Cambió aquí también!
        Console.WriteLine($"Nombre en alumno2: {alumno2.Nombre}");

        Console.WriteLine("\n→ Explicación: No hay dos objetos, solo uno con dos etiquetas (referencias).");
        Console.WriteLine("   Ambos apuntan al mismo lugar en la memoria HEAP.\n");

        Console.WriteLine("Presiona cualquier tecla para salir...");
        Console.ReadKey();
    }


    // ==============================================
    // FUNCIÓN MÓDULO 1: INTERCAMBIAR con ref
    // ==============================================
    // 'ref' significa que trabaja sobre la dirección de memoria original
    static void Intercambiar(ref int a, ref int b)
    {
        int temporal = a;
        a = b;
        b = temporal;
    }


    // ==============================================
    // FUNCIÓN MÓDULO 2: CALCULAR con out
    // ==============================================
    // 'out' se usa para devolver un valor extra obligatorio
    static int CalcularYValidar(int dividendo, int divisor, out int residuo)
    {
        // Debemos asignar obligatoriamente el valor a 'residuo'
        residuo = dividendo % divisor;
        
        // El valor principal se devuelve normalmente
        return dividendo / divisor;
    }


    // ==============================================
    // CLASE PARA EL MÓDULO 3
    // ==============================================
    public class Alumno
    {
        public string Nombre { get; set; } = string.Empty;
    }
}