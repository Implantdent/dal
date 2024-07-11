using Dal.Dto;
using Dal.Exceptions;
using Dapper;
using Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using static Dapper.SqlMapper;

namespace Dal.Persistences
{
    /// <summary>
    /// Métodos de extensión para el manejo de la persistencia de logs de base de datos en la base de datos
    /// </summary>
    /// <param name="_connString">Cadena de conexión a la base de datos</param>
    public class LogDbPersistence(string _connString) : PersistenceBase<LogDb>(_connString)
    {
        public override ListResult<LogDb> List(string filters, string orders, int limit, int offset)
        {
            try
            {
                fields = "LogDbId AS Id, Date, Action, TableId, [Table], [Sql], UserId AS Id, Email, Name, Active";
                tables = "VwLogDb";
                using IDbConnection connection = new SqlConnection(_connString);
                List<LogDb> users = connection.Query<LogDb, User, LogDb>(
                    GetSelectForList(filters, orders, limit, offset),
                    (l, u) =>
                    {
                        l.User = u;
                        return l;
                    },
                    splitOn: "Id"
                    ).ToList();
                int total = connection.ExecuteScalar<int>(GetCountTotalSelectForList(filters, orders));
                return new(users, total);
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al consultar el listado de logs de base de datos", ex);
            }
        }

        public override LogDb Read(LogDb entity)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connString);
                IEnumerable<LogDb> result = connection.Query<LogDb, User, LogDb>("SELECT LogDbId AS Id, Date, Action, TableId, [Table], [Sql], UserId AS Id, Email, Name, Active FROM VwLogDb WHERE LogDbId = @Id",
                    (l, u) =>
                    {
                        l.User = u;
                        return l;
                    },
                    entity,
                    splitOn: "Id"
                    );
                if (result.Any())
                {
                    return result.ToList()[0];
                }
                else
                {
                    return new();
                }
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al consultar el log de base de datos", ex);
            }
        }

        public override LogDb Insert(LogDb entity)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connString);
                entity.Id = connection.QuerySingleOrDefault<short>("INSERT INTO LogDb (Date, Action, TableId, [Table], [Sql], UserId) VALUES (GETDATE(), @Action, @TableId, @Table, @Sql, @UserId); SELECT SCOPE_IDENTITY();",
                    new { entity.Action, entity.TableId, entity.Table, entity.Sql, UserId = entity.User.Id });
                return entity;
            }
            catch (Exception ex)
            {
                throw new PersistentException("Error al insertar el log de base de datos", ex);
            }
        }

        public override LogDb Update(LogDb entity)
        {
            return entity;
        }

        public override LogDb Delete(LogDb entity)
        {
            return entity;
        }
    }
}
