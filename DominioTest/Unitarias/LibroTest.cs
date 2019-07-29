using DominioTest.TestDataBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaDominio;
namespace DominioTest.Unitarias
{
    [TestClass]
    public class LibroTest
    {
        private const int ANIO = 2012;
        private const string TITULO = "Cien años de soledad";
        private const string ISBN = "1234";
        public LibroTest()
        {

        }

        [TestMethod]
        public void CrearLibroTest()
        {
            // Arrange
            LibroTestDataBuilder libroTestBuilder = new LibroTestDataBuilder().ConTitulo(TITULO).
                ConAnio(ANIO).ConIsbn(ISBN);


            // Act
            Libro libro = libroTestBuilder.Build();

            // Assert
            Assert.AreEqual(TITULO, libro.Titulo);
            Assert.AreEqual(ISBN, libro.Isbn);
            Assert.AreEqual(ANIO, libro.Anio);
        }

        /// <summary>
        /// Valida los ISBN para identificar si es palindromo
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="response"></param>
        [DataTestMethod]
        [DataRow("11211", true)]
        [DataRow("65842", false)]
        public void ValidateStrPalindromo(string isbn,bool response)
        {
            Libro libro = new Libro(isbn, "Tom Sawyer", 2014);
            Assert.AreEqual(response, libro.Isbn.IsPalindromo());
        }

        /// <summary>
        /// Valdia la suma de los caracteres del ISBN
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="suma"></param>
        [DataTestMethod]
        [DataRow("11211", false)]
        [DataRow("987899", true)]
        public void ValidarSumaMayor(string isbn,bool suma)
        {
            Libro libro = new Libro(isbn, "Tom Sawyer", 2014);
            Assert.AreEqual(suma, libro.Isbn.SumIsMoreThan(30));
        }
        
    }
}
