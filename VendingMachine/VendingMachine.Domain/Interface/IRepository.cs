﻿using System.Collections.Generic;

namespace VendingMachine.Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        void Save(T entity);
        void Delete(T entity);
        IEnumerable<T> ListAll();
        T ListById(string id);
    }
}
