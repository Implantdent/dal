using Dal.Dto;
using Dal.Persistences;
using Entities;

namespace Dal.Test
{
    public class UserTest
    {
        private readonly UserPersistence persistence = new("Data Source=PC-100500;Initial Catalog=implantdent;User ID=sa;Password=Santi693*;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Command Timeout=0");

        /// <summary>
        /// Realiza la prueba de lectura de un listado de usaurios
        /// </summary>
        [Fact]
        public void ListTest()
        {
            //Arrange
            //Act
            ListResult<User> list = persistence.List("", "", 2, 0);

            //Assert
            Assert.NotEqual(0, list.Total);
        }

        /// <summary>
        /// Realiza la prueba de lectura de un usuario
        /// </summary>
        [Fact]
        public void ReadTest()
        {
            //Arrange
            User user = new()
            {
                Id = 1
            };

            //Act
            user = persistence.Read(user);

            //Assert
            Assert.NotEqual(0, user.Id);
        }

        /// <summary>
        /// Realiza la prueba de inserción de un usuario
        /// </summary>
        [Fact]
        public void InsertTest()
        {
            //Arrange
            User user = new()
            {
                Email = "kximenabaena@gmail.com",
                Name = "Karol Ximena Baena Barreto",
                Active = true
            };

            //Act
            user = persistence.Insert(user);

            //Assert
            Assert.NotEqual(0, user.Id);
        }

        /// <summary>
        /// Realiza la prueba de elimnación de un usuario
        /// </summary>
        [Fact]
        public void DeleteTest()
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
    }
}