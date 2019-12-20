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
    public partial class all_order : Form
    {

        private int id_user;
        private int id_role;

        public all_order(int id, int role)
        {
            InitializeComponent();
            id_user = id;
            id_role = role;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form order = new order(id_user);
            order.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
