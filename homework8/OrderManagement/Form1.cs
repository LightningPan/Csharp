using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrderService;

namespace OrderManagement
{
    public partial class MainForm : Form
    {
        OrderService.OrderService orderService;
        public MainForm()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            orderService = new OrderService.OrderService();
            OrderService.Order order = new Order(1,"a", new List<OrderItem>());
            order.AddItem(new OrderService.OrderItem(1,"apple",100.0,10));
            orderService.AddOrder(order);
            OrderService.Order order2 = new Order(2, "b", new List<OrderItem>());
            order2.AddItem(new OrderService.OrderItem(1, "applepen", 100.0, 10));
            orderService.AddOrder(order2);
            orderBindingSource.DataSource = orderService.Orders;

        }

      
        
        private void dataGridViewOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void itemsBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void orderBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            
            
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            switch (comboBoxQuery.Text) {
                case "CustomerName":
                    orderBindingSource.DataSource = orderService.QueryOrdersByCustomerName(textQuery.Text);
                    break;
                case "GoodsName":
                    orderBindingSource.DataSource = orderService.QueryOrdersByGoodsName(textQuery.Text);
                    break;
                case "":
                    orderBindingSource.DataSource = orderService.Orders;
                    orderBindingSource.ResetBindings(false);
                    break;
            }
            orderBindingSource.ResetBindings(true);
        }

        private void comboBoxQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            OrderService.Order order = orderBindingSource.Current as OrderService.Order;
            if (order == null) {
                MessageBox.Show("请选择一个订单");
                return;
            }
            orderService.RemoveOrder(order.OrderId);
            orderBindingSource.DataSource = orderService.Orders;
            orderBindingSource.ResetBindings(false);
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK)) {
                string filename = openFileDialog1.FileName;
                orderService.Import(filename);
                orderBindingSource.DataSource = orderService.Orders;
                orderBindingSource.ResetBindings(false);
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                String fileName = saveFileDialog1.FileName;
                orderService.Export(fileName);
            }

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Order order = orderBindingSource.Current as Order;
            if (order == null)
            {
                MessageBox.Show("请选择一个订单进行修改");
                return;
            }
            EditForm form2 = new EditForm(order);
            if (form2.ShowDialog() == DialogResult.OK) {
                orderService.UpdateOrder(form2.CurrentOrder);
                
            }
            orderBindingSource.DataSource = orderService.Orders;
            orderBindingSource.ResetBindings(false);
        }
    }
}
