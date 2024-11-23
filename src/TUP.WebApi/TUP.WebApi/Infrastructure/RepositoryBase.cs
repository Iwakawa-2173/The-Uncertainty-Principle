using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace TUP.WebApi.Infrastructure
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

        public abstract Task DeleteAsync(Guid id);

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<T> GetByIdAsync(Guid id);

        public abstract Task<Guid> InsertAsync(T entity);

        public abstract Task UpdateAsync(Guid id, T entity);

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