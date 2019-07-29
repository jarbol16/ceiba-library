using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaDominio;
using BibliotecaDominio.IRepositorio;
using DominioTest.TestDataBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DominioTest.Unitarias
{
    [TestClass]
    public class BibliotecarioTest
    {
        public BibliotecarioTest()
        {

        }
        public Mock<IRepositorioLibro> repositorioLibro;
        public Mock<IRepositorioPrestamo> repositorioPrestamo;

        [TestInitialize]
        public void setup()
        {
            repositorioLibro = new Mock<IRepositorioLibro>();
           repositorioPrestamo = new Mock<IRepositorioPrestamo>();
        }

        [TestMethod]
        public void EsPrestado()
        {
            // Arrange
            var libroTestDataBuilder = new LibroTestDataBuilder();
            Libro libro = libroTestDataBuilder.Build();
            
            repositorioPrestamo.Setup(r => r.ObtenerLibroPrestadoPorIsbn(libro.Isbn)).Returns(libro);

            // Act
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object,repositorioPrestamo.Object);
            var esprestado = bibliotecario.EsPrestado(libro.Isbn);

            // Assert
            Assert.AreEqual(esprestado, true);
        }

        [TestMethod]
        public void LibroNoPrestadoTest()
        {
            // Arrange
            var libroTestDataBuilder = new LibroTestDataBuilder();
            Libro libro = libroTestDataBuilder.Build();
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object, repositorioPrestamo.Object);
            repositorioPrestamo.Setup(r => r.ObtenerLibroPrestadoPorIsbn(libro.Isbn)).Equals(null);

            // Act
            var esprestado = bibliotecario.EsPrestado(libro.Isbn);

            // Assert
            Assert.IsFalse(esprestado);
        }

        /// <summary>
        /// Prueba para verificar que el isb cumple con el estandar
        /// </summary>
        /*
        [TestMethod]
        public void ValidarISBN()
        {
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object, repositorioPrestamo.Object);
            try
            {
                bibliotecario.Prestar("123a224", "juan");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(Bibliotecario.ISBN_NOT_FORMAT, ex.Message);
            }
        }*/

        ///
        /// Se valida que el libro si exista en la biblioteca
        ///
        [TestMethod]
        public void VerificarSiExisteELLibro()
        {
            var libroTest = new LibroTestDataBuilder();
            Libro libroEnBiblioteca = libroTest.Build();

            repositorioPrestamo.Setup(r => r.ObtenerLibroPrestadoPorIsbn(libroEnBiblioteca.Isbn)).Equals(null);
            repositorioLibro.Setup(r => r.ObtenerPorIsbn(libroEnBiblioteca.Isbn)).Returns(libroEnBiblioteca);

            
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object, repositorioPrestamo.Object);
            Libro libro = bibliotecario.ValidacionesDePrestamo(libroEnBiblioteca.Isbn);


            Assert.IsNotNull(libro);

        }

    }
}
