using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Domain;
using VendingMachine.BL;
using System.Linq;

namespace VendingMachine.Test
{
    [TestClass]
    public class InventoryTest
    {
        InventoryBL blInventory;
        Inventory oInve;

        [TestInitialize]
        public void TestSetUp()
        {
            blInventory = InventoryFactory.InventoryADO();            
        }

        [TestMethod]
        public void Test_InsertItem()
        {
            oInve = new Inventory();

            //Arrange
            int expected = 1;
            
            oInve.Name = "Cappuccino";
            oInve.Quantity = 10;
            oInve.Price = 0.50M;

            //Act
            int actual = blInventory.Save(oInve);

            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Test_UpdateItem()
        {
            oInve = new Inventory();

            //Arrange
            int expected = 1;

            oInve.Id = 2;
            oInve.Name = "Coke";
            oInve.Quantity  = 10;
            oInve.Price = 2.0M;

            //Act
            int actual = blInventory.Save(oInve);

            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Test_DeleteItem()
        {
            oInve = new Inventory();

            //Arrange
            int expected = 1;
            
            oInve.Name = "Orange Juice";
            oInve.Quantity = 10;
            oInve.Price = 2.0M;

            //Act
            blInventory.Save(oInve);
            var listItems = blInventory.ListAll();
            oInve = listItems.Where(x => x.Name.Trim() == oInve.Name.Trim()).ToList()[0];

            int actual = blInventory.Delete(oInve);

            //Assert
            Assert.IsNotNull(oInve);
            Assert.IsNotNull(listItems);
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Test_GetItem()
        {
            //Arrange
            string sID = "2";

            //Act
            oInve = blInventory.ListById(sID);

            //Assert
            Assert.IsNotNull(oInve);
            Assert.AreEqual(sID, oInve.Id.ToString());
        }

        [TestMethod]
        public void Test_ListAllItems()
        {
            //Arrange
            int iTotal = 4;

            //Act
            var listItems = blInventory.ListAll();

            //Assert
            Assert.IsNotNull(listItems);
            Assert.AreEqual(iTotal, listItems.Count());
        }
    }
}
