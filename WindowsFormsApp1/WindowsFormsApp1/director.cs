using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class director : Form
    {
        public director()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name_label = "Список изделий";
            Form viewItem = new viewItems(name_label);
            viewItem.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form all_order = new all_order();
            all_order.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Отчеты ......
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
