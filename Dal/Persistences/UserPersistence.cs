using Dal.Dto;
using Dal.Exceptions;
using Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using static Dapper.SqlMapper;

namespace Dal.Persistences
{
    /// <summary>
    /// Métodos de extensión para el manejo de la persistencia de usuarios en la base de datos
    /// </summary>
    /// <param name="_connString">Cadena de conexión a la base de datos</param>
    public class UserPersistence(string _connString) : PersistenceBase<User>(_connString), IUserPersistence
    {
        public override ListResult<User> List(string filters, string orders, int limit, int offset)
        {
            try
            {
                fields = "UserId AS Id, Email, Name, Active";
                tables = "[User]";
                using IDbConnection connection = new SqlConnection(_connString);
                List<User> users = connection.Query<User>(GetSelectForList(filters, orders, limit, offset)).ToList();
                int total = connection.ExecuteScalar<int>(GetCountTotalSelectForList(filters, orders));
                return new(users, total);
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al consultar el listado de usuarios", ex);
            }
        }

        public override User Read(User entity)
        {
            try
            {
                entityId = entity.Id;
                using IDbConnection connection = new SqlConnection(_connString);
                User? result = connection.QuerySingleOrDefault<User>("SELECT UserId AS Id, Email, Name, Active FROM [User] WHERE UserId = @Id", entity);
                if (result == null)
                {
                    return new();
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al consultar el usuario", ex);
            }
        }

        public override User Insert(User entity)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connString);
                entity.Id = connection.QuerySingleOrDefault<short>("INSERT INTO [User] (Email, Name, Password, Active) VALUES (@Email, @Name, CONVERT(VARCHAR(MAX), HASHBYTES('SHA2_512', '@Email'), 2), @Active); SELECT CAST(SCOPE_IDENTITY() AS SMALLINT);", entity);
                entityId = entity.Id;
                return entity;
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al insertar el usuario", ex);
            }
        }

        public override User Update(User entity)
        {
            try
            {
                entityId = entity.Id;
                using IDbConnection connection = new SqlConnection(_connString);
                _ = connection.Execute("UPDATE [User] SET Email = @Email, Name = @Name, Active = @Active WHERE UserId = @Id", entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al insertar el usuario", ex);
            }
        }

        public override User Delete(User entity)
        {
            try
            {
                entityId = entity.Id;
                using IDbConnection connection = new SqlConnection(_connString);
                _ = connection.Execute("DELETE FROM [User] WHERE UserId = @Id", entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al eliminar el usuario", ex);
            }
        }

        public override string GetTableName()
        {
            return "[User]";
        }

        public User ReadByEmailAndPassword(User entity, string password)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connString);
                User? result = connection.QuerySingleOrDefault<User>(
                    "SELECT UserId AS Id, Email, Name, Active FROM [User] WHERE Email = @Email AND [Password] = CONVERT(VARCHAR(MAX), HASHBYTES('SHA2_512', CAST(@Pass AS VARCHAR)), 2) AND Active = 1",
                    new { entity.Email, Pass = password });
                if (result == null)
                {
                    entity = new();
                }
                else
                {
                    entity = result;
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al consultar el usuario", ex);
            }
        }

        public User ReadByEmail(User entity)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connString);
                User? result = connection.QuerySingleOrDefault<User>("SELECT UserId AS Id, Email, Name, Active FROM [User] WHERE Email = @Email AND Active = 1", entity);
                if (result == null)
                {
                    entity = new();
                }
                else
                {
                    entity = result;
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al consultar el usuario", ex);
            }
        }

        public User UpdatePassword(User entity, string password)
        {
            try
            {
                entityId = entity.Id;
                using IDbConnection connection = new SqlConnection(_connString);
                _ = connection.Execute("UPDATE [User] SET Password = CONVERT(VARCHAR(MAX), HASHBYTES('SHA2_512', CAST(@Pass AS VARCHAR)), 2) WHERE UserId = @Id", new { Pass = password, entity.Id });
                return entity;
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al actualizar la contraseña del usuario", ex);
            }
        }

        public ListResult<Role> ListRoles(string filters, string orders, int limit, int offset, User user)
        {
            try
            {
                fields = "RoleId, [Role]";
                tables = "VwUserRole";
                using IDbConnection connection = new SqlConnection(_connString);
                List<Role> roles = connection.Query<Role>(GetSelectForList("UserId = " + user.Id + (filters != "" ? AND : "") + filters, orders, limit, offset)).ToList();
                int total = connection.ExecuteScalar<int>(GetCountTotalSelectForList("UserId = " + user.Id + (filters != "" ? AND : "") + filters, orders));
                return new(roles, total);
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al consultar el listado de roles asignados al usuario", ex);
            }
        }

        public ListResult<Role> ListNotRoles(string filters, string orders, int limit, int offset, User user)
        {
            try
            {
                fields = "RoleId, Name";
                tables = "Role";
                using IDbConnection connection = new SqlConnection(_connString);
                List<Role> roles = connection.Query<Role>(GetSelectForList("RoleId NOT IN (SELECT RoleId FROM UserRole WHERE UserId = " + user.Id + ")" + (filters != "" ? AND : "") + filters, orders, limit, offset)).ToList();
                int total = connection.ExecuteScalar<int>(GetCountTotalSelectForList("RoleId NOT IN (SELECT RoleId FROM UserRole WHERE UserId = " + user.Id + ")" + (filters != "" ? AND : "") + filters, orders));
                return new(roles, total);
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al consultar el listado de roles no asignados al usuario", ex);
            }
        }

        public Role InsertRole(Role role, User user)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connString);
                _ = connection.Execute("INSERT INTO UserRole (UserId, RoleId) VALUES (@IdUser, @IdRole)", new { IdUser = user.Id, IdRole = role.Id });
                return role;
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al asignar el rol al usuario", ex);
            }
        }

        public Role DeleteRole(Role role, User user)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connString);
                _ = connection.Execute("DELETE FROM UserRole WHERE UserId = @IdUser AND RoleId = @IdRole", new { IdUser = user.Id, IdRole = role.Id });
                return role;
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al eliminar el rol del usuario", ex);
            }
        }
    }
}
