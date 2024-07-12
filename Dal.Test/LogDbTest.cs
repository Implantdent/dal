using Dal.Dto;
using Dal.Exceptions;
using Dal.Persistences;
using Entities;
using Microsoft.Extensions.Configuration;

namespace Dal.Test
{
    public class LogDbTest
    {
        private readonly LogDbPersistence persistence;

        /// <summary>
        /// Configuración de la aplicación de pruebas
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Inicializa la configuración de la prueba
        /// </summary>
        public LogDbTest()
        {
            //Arrange
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();
            persistence = new(_configuration.GetConnectionString("implantdent") ?? "");
        }

        /// <summary>
        /// Realiza la prueba de lectura de un listado de logs de base de datos
        /// </summary>
        [Fact]
        public void List_LogDb_Ok()
        {
            //Arrange

            //Act
            ListResult<LogDb> list = persistence.List("", "", 2, 0);

            //Assert
            Assert.NotEqual(0, list.Total);
        }

        /// <summary>
        /// Realiza la prueba de lectura de un listado de logs de base de datos con ordenamiento
        /// </summary>
        [Fact]
        public void List_LogDbFilteredOrdered_Ok()
        {
            //Arrange

            //Act
            ListResult<LogDb> list = persistence.List("Active = 1", "UserId ASC", 2, 0);

            //Assert
            Assert.NotEqual(0, list.Total);
        }

        /// <summary>
        /// Realiza la prueba de lectura de un listado de logs de base de datos con errores
        /// </summary>
        [Fact]
        public void List_LogDb_Exception()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<PersistentException>(() => persistence.List("CampoNoexiste=1", "", 2, 0));
        }

        /// <summary>
        /// Realiza la prueba de lectura de un log de base de datos
        /// </summary>
        [Fact]
        public void Read_LogDb_Ok()
        {
            //Arrange
            LogDb log = new()
            {
                Id = 1
            };

            //Act
            log = persistence.Read(log);

            //Assert
            Assert.Equal("Test 1", log.Values);
        }

        /// <summary>
        /// Realiza la prueba de lectura de un log de base de datos que no existe
        /// </summary>
        [Fact]
        public void Read_LogDb_NotFound()
        {
            //Arrange
            LogDb log = new()
            {
                Id = 10
            };

            //Act
            log = persistence.Read(log);

            //Assert
            Assert.Equal(0, log.Id);
        }

        /// <summary>
        /// Realiza la prueba de lectura de un log de base de datos con un error
        /// </summary>
        [Fact]
        public void Read_LogDb_Exception()
        {
            //Arrange
            LogDb? log = null;

            //Act

            //Assert
            Assert.Throws<PersistentException>(() => persistence.Read(log));
        }

        /// <summary>
        /// Realiza la prueba de inserción de un log de base de datos
        /// </summary>
        [Fact]
        public void Insert_LogDb_Ok()
        {
            //Arrange
            LogDb log = new()
            {
                Action = 'I',
                TableId = 2,
                Table = "Test",
                Values = "Test insert",
                User = new User() { Id = 1 }
            };

            //Act
            log = persistence.Insert(log);

            //Assert
            Assert.NotEqual(0, log.Id);
            Assert.NotEqual(0, persistence.GetEntityId());
        }

        /// <summary>
        /// Realiza la prueba de inserción de un log de base de datos con error
        /// </summary>
        [Fact]
        public void Insert_User_Exception()
        {
            //Arrange
            LogDb? log = null;

            //Act

            //Assert
            Assert.Throws<PersistentException>(() => persistence.Insert(log));
        }

        /// <summary>
        /// Realiza la prueba de actualización de un log de base de datos
        /// </summary>
        [Fact]
        public void Update_User_Ok()
        {
            //Arrange
            LogDb log = new()
            {
                Id = 1
            };

            //Act
            log = persistence.Update(log);

            //Assert
            Assert.NotEqual(0, log.Id);
        }

        /// <summary>
        /// Realiza la prueba de eliminación de un log de base de datos
        /// </summary>
        [Fact]
        public void Delete_User_Ok()
        {
            //Arrange
            LogDb log = new()
            {
                Id = 1
            };

            //Act
            log = persistence.Delete(log);

            //Assert
            Assert.NotEqual(0, log.Id);
        }

        /// <summary>
        /// Realiza la prueba de consulta el nombre de la tabla
        /// </summary>
        [Fact]
        public void GetTableName_User_Ok()
        {
            //Arrange
            //Act
            //Assert
            Assert.Equal("LogDb", persistence.GetTableName());
        }
    }
}