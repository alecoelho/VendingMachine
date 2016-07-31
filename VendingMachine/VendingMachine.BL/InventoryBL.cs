using System.Collections.Generic;
using VendingMachine.Domain;
using VendingMachine.Domain.Interface;

namespace VendingMachine.BL
{
    public class InventoryBL
    {
        private readonly IRepository<Inventory> repo;

        public InventoryBL(IRepository<Inventory> repository)
        {
            repo = repository;
        }

        public int Save(Inventory invent)
        {
            return repo.Save(invent);
        }

        public int Delete(Inventory invent)
        {
            return repo.Delete(invent);
        }

        public IEnumerable<Inventory> ListAll()
        {
            return repo.ListAll();
        }

        public Inventory ListById(string id)
        {
            return repo.ListById(id);
        }
    }
}
