using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using VendingMachine.Domain;
using VendingMachine.Domain.Interface;

namespace VendingMachine.Repository
{
    public class InventoryRepositoryADO : IRepository<Inventory>
    {
        private Context context;
        private string strQuery = string.Empty;

        private int Insert(Inventory invent)
        {
            strQuery = " INSERT INTO Inventory (Name, Quantity, Price) ";
            strQuery += string.Format(" VALUES ('{0}',{1},{2})",
                    invent.Name, invent.Quantity.ToString(), invent.Price.ToString()
                );

            using (context = new Context())
            {
                return context.ExecuteCommand(strQuery);
            }
        }

        private int Update(Inventory invent)
        {
            strQuery = " UPDATE Inventory SET ";
            strQuery += string.Format(" Name = '{0}',", invent.Name.ToString());
            strQuery += string.Format(" Quantity = {0},", invent.Quantity.ToString());
            strQuery += string.Format(" Price = {0}", invent.Price.ToString());
            strQuery += string.Format(" WHERE Id = {0}", invent.Id);

            using (context = new Context())
            {
                return context.ExecuteCommand(strQuery);
            }
        }

        public int Save(Inventory invent)
        {
            if (invent.Id > 0)
                return Update(invent);
            else
                return Insert(invent);
        }

        public int Delete(Inventory invent)
        {
            using (context = new Context())
            {
                strQuery = string.Format(" DELETE FROM Inventory WHERE Id = {0}", invent.Id.ToString());
                return context.ExecuteCommand(strQuery);
            }
        }

        public IEnumerable<Inventory> ListAll()
        {
            using (context = new Context())
            {
                strQuery = "SELECT Id, Name, Quantity, Price FROM Inventory ";
                var returnDataReader = context.ExecuteCommandWithReturn(strQuery);
                return ConvertReaderToList(returnDataReader);
            }
        }

        public Inventory ListById(string id)
        {
            using (context = new Context())
            {
                strQuery = string.Format("SELECT Id, Name, Quantity, Price FROM Inventory WHERE Id = {0}", id);
                var returnDataReader = context.ExecuteCommandWithReturn(strQuery);
                return ConvertReaderToList(returnDataReader).FirstOrDefault();
            }
        }

        private List<Inventory> ConvertReaderToList(SqlDataReader reader)
        {
            var items = new List<Inventory>();
            while (reader.Read())
            {
                var tempObj = new Inventory()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    Quantity = int.Parse(reader["Quantity"].ToString()),
                    Price = decimal.Parse(reader["Price"].ToString())
                };

                items.Add(tempObj);
            }
            reader.Close();
            return items;
        }
    }
}
