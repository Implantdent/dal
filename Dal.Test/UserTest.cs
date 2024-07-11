using Dal.Dto;
using Dal.Exceptions;
using Dal.Persistences;
using Entities;
using Microsoft.Extensions.Configuration;

namespace Dal.Test
{
    public class UserTest
    {
        private readonly UserPersistence persistence;

        /// <summary>
        /// Configuración de la aplicación de pruebas
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Inicializa la configuración de la prueba
        /// </summary>
        public UserTest()
        {
            //Arrange
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();
            persistence = new(_configuration.GetConnectionString("implantdent") ?? "");
        }

        /// <summary>
        /// Realiza la prueba de lectura de un listado de usuarios
        /// </summary>
        [Fact]
        public void List_User_Ok()
        {
            //Arrange

            //Act
            ListResult<User> list = persistence.List("", "", 2, 0);

            //Assert
            Assert.NotEqual(0, list.Total);
        }

        /// <summary>
        /// Realiza la prueba de lectura de un listado de usuarios con errores
        /// </summary>
        [Fact]
        public void List_User_Exception()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<PersistentException>(() => persistence.List("CampoNoexiste=1", "", 2, 0));
        }

        /// <summary>
        /// Realiza la prueba de lectura de un usuario
        /// </summary>
        [Fact]
        public void Read_User_Ok()
        {
            //Arrange
            User user = new()
            {
                Id = 1
            };

            //Act
            user = persistence.Read(user);

            //Assert
            Assert.Equal("Test 1", user.Name);
        }

        /// <summary>
        /// Realiza la prueba de lectura de un usuario que no existe
        /// </summary>
        [Fact]
        public void Read_User_NotFound()
        {
            //Arrange
            User user = new()
            {
                Id = 10
            };

            //Act
            user = persistence.Read(user);

            //Assert
            Assert.Equal(0, user.Id);
        }

        /// <summary>
        /// Realiza la prueba de lectura de un usuario con un error
        /// </summary>
        [Fact]
        public void Read_User_Exception()
        {
            //Arrange
            User? user = null;

            //Act

            //Assert
            Assert.Throws<PersistentException>(() => persistence.Read(user));
        }

        /// <summary>
        /// Realiza la prueba de inserción de un usuario
        /// </summary>
        [Fact]
        public void Insert_User_Ok()
        {
            //Arrange
            User user = new()
            {
                Email = "test4@test.com",
                Name = "Test 4",
                Active = true
            };

            //Act
            user = persistence.Insert(user);

            //Assert
            Assert.NotEqual(0, user.Id);
        }

        /// <summary>
        /// Realiza la prueba de inserción de un usuario con error
        /// </summary>
        [Fact]
        public void Insert_User_Exception()
        {
            //Arrange
            User user = new()//Email ya existe
            {
                Email = "test1@test.com",
                Name = "Test 1",
                Active = true
            };

            //Act

            //Assert
            Assert.Throws<PersistentException>(() => persistence.Insert(user));
        }

        /// <summary>
        /// Realiza la prueba de actualización de un usuario
        /// </summary>
        [Fact]
        public void Update_User_Ok()
        {
            //Arrange
            User user = new()
            {
                Id = 2,
                Email = "test5@test.com",
                Name = "Test 5",
                Active = false
            };

            //Act
            user = persistence.Update(user);

            //Assert
            Assert.NotEqual(0, user.Id);
        }

        /// <summary>
        /// Realiza la prueba de actualización de un usuario con error
        /// </summary>
        [Fact]
        public void Update_User_Exception()
        {
            //Arrange
            User user = new()
            {
                Id = 2,
                Email = "123456789012345678901234567890123456789012345678901234567890",
                Name = "Test 5",
                Active = false
            };

            //Act

            //Assert
            Assert.Throws<PersistentException>(() => persistence.Update(user));
        }

        /// <summary>
        /// Realiza la prueba de eliminación de un usuario
        /// </summary>
        [Fact]
        public void Delete_User_Ok()
        {
            //Arrange
            User user = new()
            {
                Id = 3
            };

            //Act
            user = persistence.Delete(user);

            //Assert
            Assert.NotEqual(0, user.Id);
        }

        /// <summary>
        /// Realiza la prueba de eliminación de un usuario con error
        /// </summary>
        [Fact]
        public void Delete_User_Exception()
        {
            //Arrange
            User? user = null;

            //Act

            //Assert
            Assert.Throws<PersistentException>(() => persistence.Delete(user));
        }

        /// <summary>
        /// Realiza la prueba de consulta de usuario activo por email y contraseña
        /// </summary>
        [Fact]
        public void ReadByEmailAndPassword_User_Ok()
        {
            //Arrange
            User user = new()
            {
                Email = "test1@test.com"
            };

            //Act
            user = persistence.ReadByEmailAndPassword(user, "Pass1");

            //Assert
            Assert.NotEqual(0, user.Id);
        }

        /// <summary>
        /// Realiza la prueba de consulta de usuario activo por email y contraseña no existente
        /// </summary>
        [Fact]
        public void ReadByEmailAndPassword_User_NotFound()
        {
            //Arrange
            User user = new()
            {
                Email = "test10@test.com"
            };

            //Act
            user = persistence.ReadByEmailAndPassword(user, "Pass10");

            //Assert
            Assert.Equal(0, user.Id);
        }

        /// <summary>
        /// Realiza la prueba de consulta de usuario activo por email y contraseña con error
        /// </summary>
        [Fact]
        public void ReadByEmailAndPassword_User_Exception()
        {
            //Arrange
            User? user = null;

            //Act

            //Assert
            Assert.Throws<PersistentException>(() => persistence.ReadByEmailAndPassword(user, "1234567890"));
        }

        /// <summary>
        /// Realiza la prueba de consulta de usuario activo por email
        /// </summary>
        [Fact]
        public void ReadByEmail_User_Ok()
        {
            //Arrange
            User user = new()
            {
                Email = "test1@test.com"
            };

            //Act
            user = persistence.ReadByEmail(user);

            //Assert
            Assert.NotEqual(0, user.Id);
        }

        /// <summary>
        /// Realiza la prueba de consulta de usuario activo por email y contraseña no existente
        /// </summary>
        [Fact]
        public void ReadByEmail_User_NotFound()
        {
            //Arrange
            User user = new()
            {
                Email = "test10@test.com"
            };

            //Act
            user = persistence.ReadByEmail(user);

            //Assert
            Assert.Equal(0, user.Id);
        }

        /// <summary>
        /// Realiza la prueba de consulta de usuario activo por email y contraseña con error
        /// </summary>
        [Fact]
        public void ReadByEmail_User_Exception()
        {
            //Arrange
            User? user = null;

            //Act

            //Assert
            Assert.Throws<PersistentException>(() => persistence.ReadByEmail(user));
        }

        /// <summary>
        /// Realiza la prueba de actualización de contraseña de usuario
        /// </summary>
        [Fact]
        public void UpdatePassword_User_Ok()
        {
            //Arrange
            User user = new()
            {
                Id = 2
            };

            //Act
            user = persistence.UpdatePassword(user, "Pass6");

            //Assert
            Assert.NotEqual(0, user.Id);
        }

        /// <summary>
        /// Realiza la prueba de actualización de contraseña de usuario con error
        /// </summary>
        [Fact]
        public void UpdatePassword_User_Exception()
        {
            //Arrange
            User? user = null;

            //Act

            //Assert
            Assert.Throws<PersistentException>(() => persistence.UpdatePassword(user, "Pass6"));
        }
    }
}