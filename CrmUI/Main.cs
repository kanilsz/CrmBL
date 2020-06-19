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
            var catalog = new Catalog<T>(dbSet,db);
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
            var form = new SellerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Sellers.Add(form.Seller);
                db.SaveChanges();
            }
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

        private void productAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Products.Add(form.Product);
                db.SaveChanges();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ModelForm();
            form.Show();
        }
    }
}
