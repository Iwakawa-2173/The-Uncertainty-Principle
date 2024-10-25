using TUP.WebApi.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace TUP.WebApi.Infrastrucutre
{
    /// <summary>
    /// Базовый класс для всех SQL-based репозиториев
    /// </summary>
    /// <typeparam name="T">Тип сущности для возврата</typeparam>
    public abstract class RepositoryBase<T>
    {
        protected readonly string connectionString;

        public RepositoryBase(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            this.connectionString = configuration.GetConnectionString("Main") ?? throw new ArgumentNullException("Ошибка конфигурации. Не заполнен параметр ConnectionStrings:MainConnectionString");
        }

        public abstract Task DeleteAsync(Int64 id);

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<T> GetByIdAsync(Int64 id);

        public abstract Task<Int64> InsertAsync(T entity);

        public abstract Task UpdateAsync(Int64 id, T entity);

        protected async Task<IEnumerable<T>> ExecuteSqlReaderAsync(string sql)
        {
            
            var result = new List<T>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var sqlCommand = new SqlCommand(sql, connection))
                {
                    var reader = await sqlCommand.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        result.Add(this.GetEntityFromReader(reader));
                    }
                }
            }

            return result;
        }

        protected async Task ExecuteSqlAsync(string sql)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var sqlCommand = new SqlCommand(sql, connection))
                {
                    await sqlCommand.ExecuteNonQueryAsync();
                }
            }
        }

        protected abstract T GetEntityFromReader(SqlDataReader reader);
    }
}