using System;
using System.Windows.Forms;
using VendingMachine.BL;
using VendingMachine.Domain;
using VendingMachine.ErrorHandling;

namespace VendingMachine
{
    public partial class frmInventory : Form
    {
        #region Events
        public frmInventory()
        {
            InitializeComponent();
            populateItems();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveItem();
        }
        #endregion

        #region Methods
        private void populateItems()
        {
            try
            {
                cmbItem.Items.Clear();
                cmbItem.DataSource = Enum.GetNames(typeof(EnumItem));
            }
            catch (Exception ex)
            {
                ErrorLogger.logError("frmInventory", "populateItems", ex.Message);
                MessageBox.Show("Sorry, internal system error! Please, contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void saveItem()
        {
            try
            {
                EnumItem item = (EnumItem)Enum.Parse(typeof(EnumItem), cmbItem.SelectedValue.ToString());

                var blInventory = InventoryFactory.InventoryADO();
                Inventory oInve = blInventory.ListById(item.GetHashCode().ToString());

                oInve.Quantity = Convert.ToInt16(nudQuantity.Value);
                oInve.Price = nudPrice.Value;

                blInventory.Save(oInve);

                MessageBox.Show("Item was saved successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorLogger.logError("frmInventory", "saveItem", ex.Message);
                MessageBox.Show("Sorry, internal system error! Please, contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion
    }
}
