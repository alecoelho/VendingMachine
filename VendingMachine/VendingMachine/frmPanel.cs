using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VendingMachine.BL;
using VendingMachine.Domain;
using VendingMachine.ErrorHandling;

namespace VendingMachine
{
    public partial class frmPanel : Form
    {
        #region Events
        public frmPanel()
        {
            InitializeComponent();
            populateItems();
        }
        private void ptb2Dollars_Click(object sender, EventArgs e)
        {
            addAmount(2);
        }
        private void ptb1Dollar_Click(object sender, EventArgs e)
        {
            addAmount(1);
        }
        private void ptb5Dollars_Click(object sender, EventArgs e)
        {
            addAmount(5);
        }
        private void ptb10Dollars_Click(object sender, EventArgs e)
        {
            addAmount(10);
        }
        private void ptbCoke_Click(object sender, EventArgs e)
        {
            executePurchase(EnumItem.Coke, lblCokeInventory, lblCokePrice);
        }
        private void ptbSprite_Click(object sender, EventArgs e)
        {
            executePurchase(EnumItem.Sprite, lblSpriteInventory, lblSpritePrice);
        }
        private void ptbWater_Click(object sender, EventArgs e)
        {
            executePurchase(EnumItem.Water, lblWaterInventory, lblWaterPrice);
        }
        private void btnCancelPurchase_Click(object sender, EventArgs e)
        {
            setInfoDefault("Please, get your money back", "0");
        }
        private void btnUpdateInventory_Click(object sender, EventArgs e)
        {
            frmInventory form = new frmInventory();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();

            populateItems();
        }
        #endregion

        #region Methods
        private void populateItems()
        {
            try
            {
                var blInventory = InventoryFactory.InventoryADO();
                var listInventory = blInventory.ListAll();

                lblWaterInventory.Text = listInventory.Where(x => x.Id == EnumItem.Water.GetHashCode()).ToList()[0].Quantity.ToString();
                lblWaterPrice.Text = listInventory.Where(x => x.Id == EnumItem.Water.GetHashCode()).ToList()[0].Price.ToString();

                lblCokeInventory.Text = listInventory.Where(x => x.Id == EnumItem.Coke.GetHashCode()).ToList()[0].Quantity.ToString();
                lblCokePrice.Text = listInventory.Where(x => x.Id == EnumItem.Coke.GetHashCode()).ToList()[0].Price.ToString();

                lblSpriteInventory.Text = listInventory.Where(x => x.Id == EnumItem.Sprite.GetHashCode()).ToList()[0].Quantity.ToString();
                lblSpritePrice.Text = listInventory.Where(x => x.Id == EnumItem.Sprite.GetHashCode()).ToList()[0].Price.ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError("frmPanel", "populateItems", ex.Message);
                MessageBox.Show("Sorry, internal system error! Please, contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void addAmount(decimal iValue)
        {
            try
            {
                decimal dAmount = decimal.Parse(lblAmount.Text) + iValue;
                setInfoDefault("Choose an item", dAmount.ToString());
            }
            catch (Exception ex)
            {
                ErrorLogger.logError("frmPanel", "addAmount", ex.Message);
                MessageBox.Show("Sorry, internal system error! Please, contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }
        private void executePurchase(EnumItem item, Label lblInventory, Label lblPrice)
        {
            decimal dAmount;
            decimal dPrice;
            int iInventory;

            try
            {
                dAmount = decimal.Parse(lblAmount.Text);
                dPrice = decimal.Parse(lblPrice.Text);
                iInventory = int.Parse(lblInventory.Text);
            }
            catch (Exception ex)
            {
                ErrorLogger.logError("frmPanel", "executePurchase", ex.Message);
                MessageBox.Show("Sorry, internal system error! Please, contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (!verifyItems(dAmount, dPrice, iInventory))
                return;

            if (!updateInventory(item.GetHashCode().ToString()))
                return;

            displayImageSoldItem(item);

            lblInventory.Text = (iInventory - 1).ToString();
            lblMsg.Text = "Your change is $ " + (dAmount - dPrice).ToString();
            lblAmount.Text = "0";
        }
        private bool verifyItems(decimal dAmount, decimal dPrice, int iInventory)
        {
            if (dAmount < dPrice)
            {
                setInfoDefault("Sorry, insufficient amount ", lblAmount.Text);
                return false;
            }
            if (iInventory < 1)
            {
                setInfoDefault("Sorry, missing item ", lblAmount.Text);
                return false;
            }

            return true;
        }
        private bool updateInventory(string sIdItem)
        {
            try
            {
                var blInventory = InventoryFactory.InventoryADO();
                Inventory oInve = blInventory.ListById(sIdItem);
                oInve.Quantity -= 1;
                blInventory.Save(oInve);
            }
            catch (Exception ex)
            {
                ErrorLogger.logError("frmPanel", "updateInventory", ex.Message);
                MessageBox.Show("Sorry, internal system error! Please, contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }
        private void displayImageSoldItem(EnumItem item)
        {
            switch (item)
            {
                case EnumItem.Coke:
                    ptbSoldItem.Image = Properties.Resources.coke_can;
                    break;

                case EnumItem.Sprite:
                    ptbSoldItem.Image = Properties.Resources.sprite1;
                    break;

                case EnumItem.Water:
                    ptbSoldItem.Image = Properties.Resources.water;
                    break;

                default:
                    ptbSoldItem.Image = null;
                    break;
            }
        }
        private void setInfoDefault(string sMsg, string sCurrentAmount)
        {
            lblAmount.Text = sCurrentAmount;
            lblMsg.Text = sMsg;
            ptbSoldItem.Image = null;
        }
        #endregion
    }
}
