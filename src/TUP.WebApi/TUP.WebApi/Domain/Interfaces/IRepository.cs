namespace TUP.WebApi.Domain.Interfaces
{
    /// <summary>
    /// Общий интерфейс для всех репозиториев
    /// </summary>
    /// <typeparam name="T">Тип доменного объекта, для которого существует этот репозиторий</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Возвращает объект по его ID
        /// </summary>
        /// <param name="id">ID объекта</param>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Возвращает все объекты данного типа
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Добавляет объект и возвращает его ID
        /// </summary>
        /// <param name="entity">Новое содержимое объекта</param>        
        Task<Guid> InsertAsync(T entity);

        /// <summary>
        /// Изменяет объект
        /// </summary>
        /// <param name="id">Id объекта, который надо изменить</param>        
        /// <param name="entity">Новое содержимое объекта</param>
        Task UpdateAsync(Guid id, T entity);

        /// <summary>
        /// Удаляет объект по id
        /// </summary>
        /// <param name="id">Id удаляемого объекта</param>
        Task DeleteAsync(Guid id);
    }
}
