using Dal.Dto;
using Entities;

namespace Dal
{
    /// <summary>
    /// Métodos que debe implementar una peristencia de una entidad
    /// </summary>
    public interface IPersistence<T> where T : IEntity
    {
        /// <summary>
        /// Trae un listado de entidades
        /// </summary>
        /// <param name="filters">Filtros aplicados a la consulta</param>
        /// <param name="orders">Ordenamientos aplicados a la consulta</param>
        /// <param name="limit">Límite de registros a consultar</param>
        /// <param name="offset">Registro inicial a traer</param>
        /// <returns>Listado de entidades leídas</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al leer el listado de entidades</exception>
        ListResult<T> List(string filters, string orders, int limit, int offset);

        /// <summary>
        /// Carga los datos de una entidad desde la base de datos
        /// </summary>
        /// <param name="entity">Entidad que se desea cargar de la base de datos</param>
        /// <exception cref="PersistentException">Si hubo una excepción al leer la entidad</exception>
        T Read(T entity);

        /// <summary>
        /// Inserta una entidad en la base de datos
        /// </summary>
        /// <param name="entity">Entidad a insertar</param>
        /// <returns>Entidad insertada</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al insertar la entidad</exception>
        T Insert(T entity);

        /// <summary>
        /// Actualiza una entidad de la base de datos
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        /// <returns>Entidad actualizada</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al actualizar la entidad</exception>
        T Update(T entity);

        /// <summary>
        /// Elimina una entidad de la base de datos
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        /// <returns>Entidad eliminada</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al eliminar la entidad</exception>
        T Delete(T entity);

        /// <summary>
        /// Retorna el nombre de la tabla a la que hace referencia la persistencia.
        /// Necesario para el registro de eventos en la base de datos
        /// </summary>
        /// <returns>Nombre de la tabla</returns>
        string GetTableName();

        /// <summary>
        /// Retorna el identificador de la entidad más recientemente procesada
        /// Necesario para el registro de eventos en la base de datos
        /// </summary>
        /// <returns>Identificador de la entidad procesada</returns>
        long GetEntityId();
    }
}
