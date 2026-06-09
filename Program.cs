using System;

// ==============================================
// CLASE NODO: Es la unidad básica del árbol
// ==============================================
// Cada nodo tiene un ID, un dato y dos hijos (izquierdo y derecho)
// El signo ? significa que pueden ser nulos (no tener hijos)
public class Nodo
{
    public int ID { get; set; }
    public string Dato { get; set; } = string.Empty;
    public Nodo? HijoIzquierdo { get; set; }
    public Nodo? HijoDerecho { get; set; }

    // Constructor para crear nodos rápido
    public Nodo(int id, string dato)
    {
        ID = id;
        Dato = dato;
        HijoIzquierdo = null;
        HijoDerecho = null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== ARBOL BINARIO DE BUSQUEDA =====");

        // --------------------------
        // PASO 1: CREAR EL ARBOL
        // --------------------------
        // Empezamos con la raíz
        Nodo? raiz = null;

        // Insertamos varios nodos
        raiz = InsertarNodo(raiz, new Nodo(10, "Archivo 10"));
        raiz = InsertarNodo(raiz, new Nodo(5, "Archivo 5"));
        raiz = InsertarNodo(raiz, new Nodo(15, "Archivo 15"));
        raiz = InsertarNodo(raiz, new Nodo(3, "Archivo 3"));
        raiz = InsertarNodo(raiz, new Nodo(7, "Archivo 7"));
        raiz = InsertarNodo(raiz, new Nodo(12, "Archivo 12"));
        raiz = InsertarNodo(raiz, new Nodo(20, "Archivo 20"));

        Console.WriteLine("Nodos insertados correctamente");


        // --------------------------
        // PASO 2: PRUEBA DE BUSQUEDA
        // --------------------------
        Console.Write("\nEscribe el ID que quieres buscar: ");
        if (int.TryParse(Console.ReadLine(), out int idBuscado))
        {
            string? resultado = BuscarNodo(raiz, idBuscado);

            if (resultado != null)
            {
                Console.WriteLine($"Encontrado: {resultado}");
            }
            else
            {
                Console.WriteLine("Ese ID NO existe en el árbol");
            }
        }
        else
        {
            Console.WriteLine("Escribe un número válido");
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }


    // ==============================================
    // FUNCIÓN 1: INSERTAR NODO (RECURSIVA)
    // ==============================================
    // Regla: Menores a la izquierda, mayores a la derecha
    static Nodo InsertarNodo(Nodo? raiz, Nodo nuevoNodo)
    {
        // 📌 CASO BASE: Si el espacio está vacío, ponemos el nodo aquí
        if (raiz == null)
        {
            return nuevoNodo;
        }

        //  CASO RECURSIVO: Decidir dónde ir
        if (nuevoNodo.ID < raiz.ID)
        {
            // Si es menor -> Izquierda
            raiz.HijoIzquierdo = InsertarNodo(raiz.HijoIzquierdo, nuevoNodo);
        }
        else if (nuevoNodo.ID > raiz.ID)
        {
            // Si es mayor -> Derecha
            raiz.HijoDerecho = InsertarNodo(raiz.HijoDerecho, nuevoNodo);
        }

        // Si es igual, no hacemos nada (no permitimos duplicados)
        return raiz;
    }


    // ==============================================
    // FUNCIÓN 2: BUSCAR NODO (RECURSIVA)
    // ==============================================
    // Demuestra la eficiencia O(log n)
    static string? BuscarNodo(Nodo? raiz, int idObjetivo)
    {
        //  CASO BASE 1: Llegamos al final y no está
        if (raiz == null)
        {
            return null;
        }

        //  CASO BASE 2: ¡Lo encontramos!
        if (idObjetivo == raiz.ID)
        {
            return raiz.Dato;
        }

        //  CASO RECURSIVO: Buscar en la rama correcta
        if (idObjetivo < raiz.ID)
        {
            // Es menor -> Busca solo a la IZQUIERDA (descartamos todo el derecho)
            return BuscarNodo(raiz.HijoIzquierdo, idObjetivo);
        }
        else
        {
            // Es mayor -> Busca solo a la DERECHA (descartamos todo el izquierdo)
            return BuscarNodo(raiz.HijoDerecho, idObjetivo);
        }
    }
}