using BibliotecaDominio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDominio
{
    public class Bibliotecario
    {
        public const string ISBN_NOT_FORMAT = "El valor ingresado no es un ISBN";
        public const string EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE = "El libro no se encuentra disponible";
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
            throw new Exception("se debe implementar este método");
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
    }
}
