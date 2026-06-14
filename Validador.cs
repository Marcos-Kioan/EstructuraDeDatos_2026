public static class Validador
{
    // Validación robusta: int.TryParse y mayor que 0
    public static bool ValidarNumero(string entrada, out int numero)
    {
        // Intenta convertir y verifica que sea mayor a 0
        return int.TryParse(entrada, out numero) && numero > 0;
    }
}