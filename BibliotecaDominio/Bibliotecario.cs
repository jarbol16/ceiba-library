using BibliotecaDominio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibliotecaDominio
{
    public class Bibliotecario
    {
        public const string STR_IS_PALINDROMO = "los libros palíndromos solo se pueden utilizar en la biblioteca";
        public const string ISBN_NOT_FORMAT = "El valor ingresado no es un ISBN";
        public const string EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE = "El libro no se encuentra disponible";
        public const string LIBRO_NO_DISPONIBLE = "La biblioteca no tiene el libro";


        private  IRepositorioLibro libroRepositorio;
        private  IRepositorioPrestamo prestamoRepositorio;

        public Bibliotecario(IRepositorioLibro libroRepositorio, IRepositorioPrestamo prestamoRepositorio)
        {
            this.libroRepositorio = libroRepositorio;
            this.prestamoRepositorio = prestamoRepositorio;
        }

        /// <summary>
        /// Metodo de pestamo de libro
        /// </summary>
        /// <param name="isbn">identifiacador de libro</param>
        /// <param name="nombreUsuario">Nombre del usuario</param>
        public void Prestar(string isbn, string nombreUsuario)
        {
            /*if (!isbn.IsNumber())
                throw new Exception(ISBN_NOT_FORMAT);*/

            Libro libro = ValidacionesDePrestamo(isbn);
            Prestamo prestamo;
            if (isbn.SumIsMoreThan(30))
            {
                prestamo = new Prestamo(DateTime.Now, libro, BuildDateOfDelivery(DateTime.Now), nombreUsuario);
            }
            else
            {
                prestamo = new Prestamo(DateTime.Now, libro, null, nombreUsuario);
            }
            this.prestamoRepositorio.Agregar(prestamo);
            
        }


        public Libro ValidacionesDePrestamo(string isbn)
        {
            Libro libro = this.libroRepositorio.ObtenerPorIsbn(isbn);
            if (libro == null)
                throw new Exception(LIBRO_NO_DISPONIBLE);
            if (isbn.IsPalindromo())
                throw new Exception(STR_IS_PALINDROMO);
            if (this.prestamoRepositorio.Obtener(isbn) != null)
                throw new Exception(EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE);
            return libro;
        }

        public bool EsPrestado(string isbn)
        {
            if (isbn.IsNumber())
            {
                return this.prestamoRepositorio.ObtenerLibroPrestadoPorIsbn(isbn) == null ? false : true;
            }
            else
            {
                throw new Exception(ISBN_NOT_FORMAT);
            }
            return false;
        }

        /// <summary>
        /// Calcula la fecha en la que el usuario debera entregar el libro
        /// </summary>
        /// <returns></returns>
        public static DateTime BuildDateOfDelivery(DateTime now)
        {
            DateTime dateDelivery = now;
            bool moreDays = true;
            int numDays = 1;
            while (moreDays)
            {
                if (!dateDelivery.DayOfWeek.Equals(DayOfWeek.Sunday))
                    numDays++;
                dateDelivery = dateDelivery.AddDays(1);
                if (numDays == 15)
                    moreDays = false;
            }
            if (dateDelivery.DayOfWeek.Equals(DayOfWeek.Sunday))
                return dateDelivery.AddDays(1);
            return dateDelivery;
        }
    }
}
