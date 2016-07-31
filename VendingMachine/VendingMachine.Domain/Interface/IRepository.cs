using System.Collections.Generic;

namespace VendingMachine.Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        int Save(T entity);
        int Delete(T entity);
        IEnumerable<T> ListAll();
        T ListById(string id);
    }
}
