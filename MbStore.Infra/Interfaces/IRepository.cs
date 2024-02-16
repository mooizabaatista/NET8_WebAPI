using MbStore.Domain.Entities.Base;

namespace MbStore.Infra.Interfaces;

public interface IRepository<T> where T : EntityBase
{
    Task<IEnumerable<T>> GetAsync();
    Task<T> GetAsync(Guid id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
}
