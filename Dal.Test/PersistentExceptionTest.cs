using Dal.Exceptions;

namespace Dal.Test
{
    public class PersistentExceptionTest
    {
        /// <summary>
        /// Realiza la prueba de creación de PersistentException por defecto
        /// </summary>
        [Fact]
        public void CreateBasic_PersistentException_Ok()
        {
            //Arrange
            PersistentException ex = new();
            //Act

            //Assert
            Assert.IsType<PersistentException>(ex);
        }

        /// <summary>
        /// Realiza la prueba de creación de PersistentException con mensaje
        /// </summary>
        [Fact]
        public void CreateWithMessage_PersistentException_Ok()
        {
            //Arrange
            PersistentException ex = new("Prueba");
            //Act

            //Assert
            Assert.IsType<PersistentException>(ex);
        }

        /// <summary>
        /// Realiza la prueba de creación de PersistentException con mensaje y exepción interna
        /// </summary>
        [Fact]
        public void CreateInnerException_PersistentException_Ok()
        {
            //Arrange
            PersistentException ex = new("Prueba", new Exception());
            //Act

            //Assert
            Assert.IsType<PersistentException>(ex);
        }
    }
}