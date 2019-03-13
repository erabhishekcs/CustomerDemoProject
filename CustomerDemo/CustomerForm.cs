using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InterfaceCustomer;
using FactoryCustomer;

namespace CustomerDemo
{
    public partial class CustomerForm : Form
    {
        private ICustomer cust = null;
        static List<CustomerBase> custLst = null;
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            custLst = new List<CustomerBase>();
        }

        private void cmbCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // creating an object of sub classes at runtime using Factory pattern
            cust = Factory<CustomerBase>.Create(cmbCustomerType.Text); 
        }

        private void SetCustomer()
        {
            cust.CustomerName = txtCustomerName.Text;
            cust.PhoneNumber = txtPhoneNumber.Text;
            cust.BillDate = Convert.ToDateTime(txtBillingDate.Text);
            cust.BillAmount = Convert.ToDecimal(txtBillingAmount.Text);
            cust.Address = txtAddress.Text;
        }
        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCustomerType.Text != "")
                {
                    SetCustomer();
                    cust.Validate(); // Validate customer as per its requirement.
                    Add();
                }
                else
                {
                    MessageBox.Show("Kindly select customer type.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        // Add to in-memory data grid
        private void Add()
        {
            dataGridView1.DataSource = null;
            CustomerBase cust = new CustomerBase();
            Random rand = new Random();
            cust.Id = rand.Next();
            cust.CustomerType = cmbCustomerType.Text;
            cust.CustomerName = txtCustomerName.Text;
            cust.PhoneNumber = txtPhoneNumber.Text;
            cust.BillDate = Convert.ToDateTime(txtBillingDate.Text);
            cust.BillAmount = Convert.ToDecimal(txtBillingAmount.Text);
            cust.Address = txtAddress.Text;
            custLst.Add(cust);
            dataGridView1.DataSource = custLst;
        }
    }
}
