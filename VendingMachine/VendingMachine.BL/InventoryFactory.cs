using VendingMachine.Repository;

namespace VendingMachine.BL
{
    public class InventoryFactory
    {
        /// <summary>
        /// Factory to access the database by ADO.NET
        /// </summary>
        /// <returns></returns>
        public static InventoryBL InventoryADO()
        {
            return new InventoryBL(new InventoryRepositoryADO());
        }

        //EntityFramework -- if necessary
        //...
    }
}
