using Dal.Dto;
using Dal.Exceptions;
using Entities;

namespace Dal.Persistences
{
    /// <summary>
    /// Métodos de extensión para el manejo de la persistencia de usuarios en la base de datos
    /// </summary>
    /// <param name="_connString">Cadena de conexión a la base de datos</param>
    public interface IUserPersistence
    {
        /// <summary>
        /// Consulta un usuario dado su email y contraseña
        /// </summary>
        /// <param name="entity">Usuario a consultar</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>Usuario con los datos cargados desde la base de datos</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al consultar el usuario</exception>
        User ReadByEmailAndPassword(User entity, string password);

        /// <summary>
        /// Consulta un usuario existe dado su login y si su estado es activo
        /// </summary>
        /// <param name="entity">Usuario a consultar</param>
        /// <returns>Usuario si existe y está activo en la base de datos</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al consultar el usuario</exception>
        User ReadByEmail(User entity);

        /// <summary>
        /// Actualiza la contraseña de un usuario en la base de datos
        /// </summary>
        /// <param name="entity">Usuario a actualizar</param>
        /// <param name="password">Nueva contraseña del usuario</param>
        /// <returns>Usuario actualizado</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al actualizar el usuario</exception>
        User UpdatePassword(User entity, string password);

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
        ListResult<Role> ListRoles(string filters, string orders, int limit, int offset, User user);

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
        ListResult<Role> ListNotRoles(string filters, string orders, int limit, int offset, User user);

        /// <summary>
        /// Asigna un rol a un usuario en la base de datos
        /// </summary>
        /// <param name="role">Rol que se asigna al usuario</param>
        /// <param name="user">Usuario al que se le asigna el rol</param>
        /// <returns>Rol asignado</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al asignar el rol al usuario</exception>
        Role InsertRole(Role role, User user);

        /// <summary>
        /// Elimina un rol de un usuario de la base de datos
        /// </summary>
        /// <param name="role">Rol a eliminarle al usuario</param>
        /// <param name="user">Usuario al que se le elimina el rol</param>
        /// <returns>Rol eliminado</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al eliminar el rol del usuario</exception>
        Role DeleteRole(Role role, User user);
    }
}
