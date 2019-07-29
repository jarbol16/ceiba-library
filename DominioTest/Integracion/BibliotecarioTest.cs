using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaDominio;
using BibliotecaRepositorio.Contexto;
using BibliotecaRepositorio.Repositorio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominioTest.TestDataBuilders;
using Microsoft.EntityFrameworkCore;

namespace DominioTest.Integracion
{

    [TestClass]
    public class BibliotecarioTest
    {
        public const String CRONICA_UNA_MUERTE_ANUNCIADA = "Cronica de una muerte anunciada";
        private  BibliotecaContexto contexto;
        private  RepositorioLibroEF repositorioLibro;
        private RepositorioPrestamoEF repositorioPrestamo;


        [TestInitialize]
        public void setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContexto>();
            contexto = new BibliotecaContexto(optionsBuilder.Options);
            repositorioLibro  = new RepositorioLibroEF(contexto);
            repositorioPrestamo = new RepositorioPrestamoEF(contexto, repositorioLibro);
        }

        [TestMethod]
        public void PrestarLibroTest()
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder().ConTitulo(CRONICA_UNA_MUERTE_ANUNCIADA).ConIsbn("3458").Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            // Act
            bibliotecario.Prestar(libro.Isbn, "Juan");

            // Assert
            Assert.AreEqual(bibliotecario.EsPrestado(libro.Isbn), true);
            Assert.IsNotNull(repositorioPrestamo.ObtenerLibroPrestadoPorIsbn(libro.Isbn));

        }

        [TestMethod]
        public void PrestarLibroNoDisponibleTest()
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder().ConTitulo(CRONICA_UNA_MUERTE_ANUNCIADA).Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            // Act
            bibliotecario.Prestar(libro.Isbn,"Juan");
            try
            {
                bibliotecario.Prestar(libro.Isbn, "Juan");
                Assert.Fail();
            }
            catch (Exception err)
            {
                // Assert
                Assert.AreEqual("El libro no se encuentra disponible", err.Message);
            }
        
        }


        [TestMethod]
        public void ValidateFechaNula()
        {
            Libro libro = new LibroTestDataBuilder().ConTitulo(CRONICA_UNA_MUERTE_ANUNCIADA).ConIsbn("123456").Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);
            bibliotecario.Prestar(libro.Isbn, "pedrito");
            Assert.AreEqual(bibliotecario.EsPrestado(libro.Isbn), true);
            /*
            Prestamo prestamo = repositorioPrestamo.Obtener(libro.Isbn);
            Assert.AreEqual(null, prestamo.FechaEntregaMaxima);
            */
        }


        /// <summary>
        /// Prueba para validar que se construya bien la fecha de entrega
        /// </summary>
        /// <param name="prestamo">Fecha en la que se hace el prestamo</param>
        /// <param name="entrega">Ña fecha en la que se debe devolver</param>
        [DataTestMethod]
        [DataRow("24/05/2017", "09/06/2017")]
        [DataRow("26/05/2017", "12/06/2017")]
        public void ValidarFechaDEntrega(string prestamo, string entrega)
        {
            DateTime now = DateTime.Parse(prestamo);
            DateTime next = DateTime.Parse(entrega);
            Assert.AreEqual(next.Date, Bibliotecario.BuildDateOfDelivery(now).Date);
        }

        /// <summary>
        /// Validacion de excepcion de pedir un libro en la biblioteca con un isbn palindromo
        /// </summary>
        [TestMethod]
        public void IsPalindromo()
        {
            Libro libro = new LibroTestDataBuilder().ConTitulo(CRONICA_UNA_MUERTE_ANUNCIADA).ConIsbn("11211").Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);
            try
            {
                bibliotecario.Prestar("11211", "juan");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(Bibliotecario.STR_IS_PALINDROMO, ex.Message);
            }
        }

    }
}
