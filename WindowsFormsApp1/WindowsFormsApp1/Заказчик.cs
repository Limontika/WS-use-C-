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
    public partial class Заказчик : Form
    {
        private int id_user;
        private int id_role;

        public Заказчик(int id, int role)
        {
            InitializeComponent();
            id_user = id;
            id_role = role;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form all_order = new all_order(id_user, id_role);
            all_order.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form order = new order(id_user);
            order.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
