using Entities;

namespace Dal.Dto
{
    /// <summary>
    /// Estructura que maneja los resultados de una consulta a la base de datos
    /// con múltiples registros y a la cual se le aplicaron límites de registros
    /// a retornar
    /// </summary>
    /// <remarks>
    /// Crea un resultado de una consulta de tipo listado
    /// </remarks>
    /// <param name="list">Listado de registros a retornar</param>
    /// <param name="total">Cantidad de registros que traería si no se aplican los límites</param>
    public class ListResult<T>(IList<T> list, int total) where T : IEntity
    {
        /// <summary>
        /// Listado de registros a retornar
        /// </summary>
        public IList<T> List { get; private set; } = list;

        /// <summary>
        /// Cantidad de registros que traería si no se aplican los límites
        /// </summary>
        public int Total { get; private set; } = total;
    }
}