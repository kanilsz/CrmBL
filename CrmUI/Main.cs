using System;
using System.Data.Entity;
using System.Windows.Forms;
using CrmBL.Model;

namespace CrmUI
{
    public partial class Main : Form
    {
        CrmContext db;
        public Main()
        {
            InitializeComponent();
            db = new CrmContext();
        }
     
        private void ShowCatalog<T>(DbSet<T> dbSet) where T :class
        {
            var catalog = new Catalog<T>(dbSet);
            catalog.Show();
        }
        private void SellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCatalog<Seller>(db.Sellers);
        }

        private void ProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
  
            ShowCatalog<Product>(db.Products);
        }

        private void CustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {         
            ShowCatalog<Customer>(db.Customers);
        }

        private void CheckToolStripMenuItem_Click(object sender, EventArgs e)
        {        
            ShowCatalog<Check>(db.Checks);
        }

        private void SellerAddToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void CustomerAddToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new CustomerForm();
            if (form.ShowDialog()==DialogResult.OK)
            {
                db.Customers.Add(form.Customer);
                db.SaveChanges();
            }
        }
    }
}
