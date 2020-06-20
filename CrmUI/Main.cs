using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrmBL.Model;

namespace CrmUI
{
    public partial class Main : Form
    {
        CrmContext db;
        Cart cart;
        Customer customer;
        CashDesk cashDesk;
        public Main()
        {
            InitializeComponent();
            db = new CrmContext();

            cart = new Cart(customer);
            cashDesk = new CashDesk(1, db.Sellers.FirstOrDefault(), db)
            {
                IsModel = false
            };
        }

        private void ShowCatalog<T>(DbSet<T> dbSet) where T : class
        {
            var catalog = new Catalog<T>(dbSet, db);
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
            if (form.ShowDialog() == DialogResult.OK)
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

        private void Main_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                listBox1.Invoke((Action)delegate
               {
                   listBox1.Items.AddRange(db.Products.ToArray());
                   UpdateLists();
               });
            });
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Product product)
            {
                cart.Add(product);
                listBox2.Items.Add(product);
                UpdateLists();
            }
        }

        private void UpdateLists()
        {
            listBox2.Items.Clear();
            listBox2.Items.AddRange(cart.GetAll().ToArray());
            label1.Text = $"Загально: {cart.Price}";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (customer != null)
            {

                cashDesk.Enqueue(cart);
                var price = cashDesk.Dequeue();
                if (price != 0)
                {
                    listBox2.Items.Clear();
                    cart = new Cart(customer);

                    MessageBox.Show($"Придбання виконано успішно. Загальна вартість: {price}", "Придбання виконано", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ви нічого не купили", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Авторизуйтесь, будь-ласка", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new Login();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                var tempCustomer = db.Customers.FirstOrDefault(c => c.Name.Equals(form.Customer.Name));
                if (tempCustomer != null)
                {
                    customer = tempCustomer;
                }
                else
                {
                    db.Customers.Add(form.Customer);
                    db.SaveChanges();
                    customer = form.Customer;
                }

                cart.Customer = customer;

            }

            linkLabel1.Text = $"День добрий, {customer.Name}!";
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem is Product product)
            {
                cart.Remove(product);
                listBox2.Items.Remove(product);
                UpdateLists();
            }
        }
    }
}
