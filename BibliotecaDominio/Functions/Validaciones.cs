using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDominio
{
    /// <summary>
    /// Clase que contione funciones de validaciones de datos para la biblioteca
    /// </summary>
    public static class Validaciones
    {
        /// <summary>
        /// MEtodo extensivo para validar que un string sea solo numeros
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(this string str)
        {
            return long.TryParse(str, out _);
        }
    }
}
