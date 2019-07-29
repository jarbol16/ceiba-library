using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BibliotecaDominio
{
    /// <summary>
    /// Clase que contione funciones de validaciones de datos para la biblioteca
    /// Estas pueden servir para todo el analisis sobre los tipos de datos necesarios
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

        /// <summary>
        /// Valida si una cadena es palindromo
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPalindromo(this string str)
        {
            if (str.ToLower().SequenceEqual(str.ToLower().Reverse()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Valida si la longitud de un string es mayor a un valor
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool SumIsMoreThan(this string str,int num)
        {
            int acum = 0;
            for (int i = 0; i < str.Length; i++)
                acum += int.Parse(str[i].ToString());
            if (acum > num)
                return true;
            return false;

        }

    }
}
