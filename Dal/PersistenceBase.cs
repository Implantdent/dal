using Dal.Dto;
using Dal.Exceptions;
using Entities;
using System.Text;

namespace Dal
{
    /// <summary>
    /// Clase base de la jerarquía de persistentes en base de datos
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que va a persistir</typeparam>
    /// <remarks>
    /// Inicializa la cadena de conexión a la base de datos
    /// </remarks>
    /// <param name="connString">Cadena de conexión a la base de datos</param>
    public abstract class PersistenceBase<T>(string connString) where T : IEntity
    {
        /// <summary>
        /// Cadena de conexión a la base de datos
        /// </summary>
        protected string _connString = connString;

        /// <summary>
        /// Listado de campos que se consultan en un listado de entidades
        /// </summary>
        protected string fields;

        /// <summary>
        /// Listado de tablas sobre las que se consultan un listado de entidades
        /// </summary>
        protected string tables;

        /// <summary>
        /// Trae un listado de entidades
        /// </summary>
        /// <param name="filters">Filtros aplicados a la consulta</param>
        /// <param name="orders">Ordenamientos aplicados a la consulta</param>
        /// <param name="limit">Límite de registros a consultar</param>
        /// <param name="offset">Registro inicial a traer</param>
        /// <returns>Listado de entidades leídas</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al leer el listado de entidades</exception>
        public abstract ListResult<T> List(string filters, string orders, int limit, int offset);

        /// <summary>
        /// Carga los datos de una entidad desde la base de datos
        /// </summary>
        /// <param name="entity">Entidad que se desea cargar de la base de datos</param>
        /// <exception cref="PersistentException">Si hubo una excepción al leer la entidad</exception>
        public abstract T Read(T entity);

        /// <summary>
        /// Inserta una entidad en la base de datos
        /// </summary>
        /// <param name="entity">Entidad a insertar</param>
        /// <returns>Entidad insertada</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al insertar la entidad</exception>
        public abstract T Insert(T entity);

        /// <summary>
        /// Elimina una entidad de la base de datos
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        /// <returns>Entidad eliminada</returns>
        /// <exception cref="PersistentException">Si hubo una excepción al eliminar la entidad</exception>
        public abstract T Delete(T entity);

        /// <summary>
        /// Crea una sentencia SELECT con filtros, ordenamientos, y límite de registros a traer
        /// </summary>
        /// <param name="filters">Filtros aplicados</param>
        /// <param name="orders">Ordenamientos aplicados</param>
        /// <param name="limit">Límite de registros a traer</param>
        /// <param name="offset">Corrimiento desde el que cuenta los registros a traer</param>
        /// <returns>Sentencia para taer un listado</returns>
        protected string GetSelectForList(string filters, string orders, int limit, int offset)
        {
            StringBuilder sql = new("SELECT " + fields + " FROM " + tables);
            if (!string.IsNullOrEmpty(filters))
            {
                sql.Append(" WHERE " + Sanitize(filters));
            }
            if (!string.IsNullOrEmpty(orders))
            {
                sql.Append(" ORDER BY " + Sanitize(orders));
            }
            if (limit != 0)
            {
                if (string.IsNullOrEmpty(orders))//No venía con ordenamiento
                {
                    sql.Append(" ORDER BY 1 ASC");
                }
                sql.Append(" OFFSET " + offset + " ROWS FETCH NEXT " + limit + " ROWS ONLY");
            }
            return sql.ToString();
        }

        /// <summary>
        /// Crea una sentencia de conteo total de registros de un SELECT con filtros, ordenamientos, y límite de registros a traer
        /// </summary>
        /// <param name="filters">Filtros aplicados</param>
        /// <param name="orders">Ordenameintos aplicados</param>
        /// <returns>Sentencia para taer el conteo total de registros de un listado</returns>
        protected string GetCountTotalSelectForList(string filters, string orders)
        {
            StringBuilder sql = new("SELECT COUNT(1) AS total FROM " + tables);
            if (!string.IsNullOrEmpty(filters))
            {
                sql.Append(" WHERE " + Sanitize(filters));
            }
            return sql.ToString();
        }

        /// <summary>
        /// Elimina los posibles caracteres que puedan provocar inyección de SQL
        /// </summary>
        /// <param name="input">Entrada a desinfectar</param>
        /// <returns>Cadena desinfectada</returns>
        private static string Sanitize(string input) => input.Replace("''", "'").Replace(";", "");
    }
}
