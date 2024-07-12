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
    public class UserPersistence(string _connString) : PersistenceBase<User>(_connString)
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
                using IDbConnection connection = new SqlConnection(_connString);
                _ = connection.Execute("DELETE FROM [User] WHERE UserId = @Id", entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al eliminar el usuario", ex);
            }
        }

        /// <summary>
        /// Consulta un usuario dado su email y contraseña
        /// </summary>
        /// <param name="entity">Usuario a consultar</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>Usuario con los datos cargados desde la base de datos</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al consultar el usuario</exception>
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

        /// <summary>
        /// Consulta un usuario existe dado su login y si su estado es activo
        /// </summary>
        /// <param name="entity">Usuario a consultar</param>
        /// <returns>Usuario si existe y está activo en la base de datos</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al consultar el usuario</exception>
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

        /// <summary>
        /// Actualiza la contraseña de un usuario en la base de datos
        /// </summary>
        /// <param name="entity">Usuario a actualizar</param>
        /// <param name="password">Nueva contraseña del usuario</param>
        /// <returns>Usuario actualizado</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al actualizar el usuario</exception>
        public User UpdatePassword(User entity, string password)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connString);
                _ = connection.Execute("UPDATE [User] SET Password = CONVERT(VARCHAR(MAX), HASHBYTES('SHA2_512', CAST(@Pass AS VARCHAR)), 2) WHERE UserId = @Id", new { Pass = password, entity.Id });
                return entity;
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al actualizar la contraseña del usuario", ex);
            }
        }

        /// <summary>
        /// Trae un listado de roles asignados a un usuario desde la base de datos
        /// </summary>
        /// <param name="filters">Filtros aplicados a la consulta</param>
        /// <param name="orders">Ordenamientos aplicados a la base de datos</param>
        /// <param name="limit">Límite de registros a traer</param>
        /// <param name="offset">Corrimiento desde el que se cuenta el número de registros</param>
        /// <param name="user">Usuario al que se le consultan los roles asignados</param>
        /// <returns>Listado de roles asignados al usuario</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al consultar los roles</exception>
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

        /// <summary>
        /// Trae un listado de roles no asignados a un usuario desde la base de datos
        /// </summary>
        /// <param name="filters">Filtros aplicados a la consulta</param>
        /// <param name="orders">Ordenamientos aplicados a la base de datos</param>
        /// <param name="limit">Límite de registros a traer</param>
        /// <param name="offset">Corrimiento desde el que se cuenta el número de registros</param>
        /// <param name="user">Usuario al que se le consultan los roles no asignados</param>
        /// <returns>Listado de roles no asignados al usuario</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al consultar los roles</exception>
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

        /// <summary>
        /// Asigna un rol a un usuario en la base de datos
        /// </summary>
        /// <param name="role">Rol que se asigna al usuario</param>
        /// <param name="user">Usuario al que se le asigna el rol</param>
        /// <returns>Rol asignado</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al asignar el rol al usuario</exception>
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

        /// <summary>
        /// Elimina un rol de un usuario de la base de datos
        /// </summary>
        /// <param name="role">Rol a eliminarle al usuario</param>
        /// <param name="user">Usuario al que se le elimina el rol</param>
        /// <returns>Rol eliminado</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al eliminar el rol del usuario</exception>
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
