using System;
using System.Collections.Generic;
using System.Linq;

// Esta clase representa un producto de nuestro inventario
// Tiene las 4 cosas que nos piden: ID, nombre, precio y cantidad
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public double Precio { get; set; }
    public int CantidadEnStock { get; set; }

    // Constructor para crear un producto nuevo rápido y fácil
    public Producto(int id, string nombre, double precio, int cantidad)
    {
        Id = id;
        Nombre = nombre;
        Precio = precio;
        CantidadEnStock = cantidad;
    }

    // Esto sirve para que al imprimir el objeto se vea bonito y ordenado
    public override string ToString()
    {
        return $"ID: {Id} | {Nombre} | Precio: ${Precio:F2} | En existencia: {CantidadEnStock}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== SISTEMA DE CONTROL DE INVENTARIO =====\n");

        // --------------------------
        // 1. USAMOS LIST<T>
        // --------------------------
        // Primera forma de llenar la lista: directo al crearla
        List<Producto> listaProductos = new List<Producto>()
        {
            new Producto(1, "Laptop Lenovo", 15999.00, 10),
            new Producto(2, "Mouse inalámbrico", 349.00, 25),
            new Producto(3, "Teclado mecánico", 899.00, 0),
            new Producto(4, "Monitor 24 pulgadas", 4500.00, 5),
            new Producto(5, "Audífonos Sony", 1200.00, 0)
        };

        // Segunda forma: agregando uno por uno después
        listaProductos.Add(new Producto(6, "Cámara web HD", 750.00, 12));

        Console.WriteLine($"Tenemos registrados: {listaProductos.Count} productos\n");


        // --------------------------
        // 2. CONSULTAS CON LINQ
        // --------------------------
        // Ordenar productos del más caro al más barato
        Console.WriteLine("--- Productos ordenados por precio (de mayor a menor) ---");
        var ordenadosPorPrecio = listaProductos.OrderByDescending(p => p.Precio).ToList();

        foreach (var item in ordenadosPorPrecio)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\n--- Productos que están agotados ---");
        // Filtrar solo los que tienen cantidad en 0
        var productosAgotados = listaProductos.Where(p => p.CantidadEnStock == 0).ToList();

        if (productosAgotados.Count == 0)
        {
            Console.WriteLine("¡Todo bien! No hay productos agotados.");
        }
        else
        {
            foreach (var item in productosAgotados)
            {
                Console.WriteLine(item);
            }
        }


        // --------------------------
        // 3. CONSULTA COMBINADA: FILTRAR Y ORDENAR
        // --------------------------
        Console.WriteLine("\n--- Productos con Precio > $50, ordenados por Cantidad (mayor a menor) ---");
        
        // Usamos .Where() para filtrar y .OrderByDescending() para ordenar
        var productosFiltraidosYOrdenados = listaProductos
            .Where(p => p.Precio > 50)                      // Filtro: Precio mayor a 50
            .OrderByDescending(p => p.CantidadEnStock)      // Orden: Cantidad descendente
            .ToList();
        
        // Imprimimos con foreach
        foreach (var producto in productosFiltraidosYOrdenados)
        {
            Console.WriteLine(producto);
        }

        // Alternativa en una sola línea (sintaxis de método)
        Console.WriteLine("\n--- Misma consulta en una línea ---");
        foreach (var p in listaProductos.Where(x => x.Precio > 50).OrderByDescending(x => x.CantidadEnStock))
        {
            Console.WriteLine(p);
        }


        // --------------------------
        // 4. USAMOS DICCIONARIO
        // --------------------------
        Console.WriteLine("\n--- Búsqueda rápida por ID ---");

        // Convertimos la lista a diccionario, la llave será el ID del producto
        // Así buscamos mucho más rápido
        Dictionary<int, Producto> diccionarioInventario = listaProductos.ToDictionary(p => p.Id);

        // Llamamos a nuestra función para buscar
        BuscarProducto(diccionarioInventario);
    }


    // Función sencilla para pedir el ID y buscarlo
    static void BuscarProducto(Dictionary<int, Producto> inventario)
    {
        Console.Write("Escribe el ID del producto que quieres ver: ");
        string datoIngresado = Console.ReadLine();

        // Revisamos que lo que escribió el usuario sea un número
        if (int.TryParse(datoIngresado, out int idBuscar))
        {
            // Buscamos en el diccionario, si existe lo mostramos
            if (inventario.TryGetValue(idBuscar, out Producto resultado))
            {
                Console.WriteLine($"\n¡Encontrado! Detalles:\n{resultado}");
            }
            else
            {
                Console.WriteLine("Lo siento, ese ID no existe en nuestro inventario.");
            }
        }
        else
        {
            Console.WriteLine("Error: debes escribir solo números.");
        }
    }
}