using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderManagement
{
    public partial class EditForm : Form
    {
        public OrderService.Order CurrentOrder { get; set; }
        public EditForm()
        {
            InitializeComponent();
        }

        public EditForm(OrderService.Order order) : this() 
        {
            CurrentOrder = order;
            orderItemBindingSource.DataSource = CurrentOrder.Items;

        }
        

        private void EditForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            OrderService.OrderItem orderItem = orderItemBindingSource.Current as OrderService.OrderItem;
            if (orderItem == null) {
                MessageBox.Show("请选择订单详细");
                return;
            }
            CurrentOrder.RemoveItem(orderItem);
            orderItemBindingSource.ResetBindings(false);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
