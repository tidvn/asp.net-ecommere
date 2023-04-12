using System.Collections.Generic;
namespace TDStore.Models.Repository
{
    public interface IRepositoryGeneric<T,K>
    {
        List<T> GetAll();
        T GetById(K key);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(K key);
    }
}
