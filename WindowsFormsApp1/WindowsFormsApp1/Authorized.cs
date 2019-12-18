using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        /*private SQLiteConnection DB;*/

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            registration reg = new registration();
            reg.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*DB = new SQLiteConnection("Data Source=D:/myProject/WS-use-C-/database/new.db");
            DB.Open();*/
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*DB.Close();*/
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                Form storekeeper = new storekeeper();
                storekeeper.Show();
                textBox1.Text = " ";
                textBox2.Text = " ";

            }else if (textBox1.Text == "2")
            {
                Form storekeeper = new storekeeper();
                storekeeper.Show();
            }
            else if (textBox1.Text == "3")
            {
                Form storekeeper = new storekeeper();
                storekeeper.Show();
            }
            else if (textBox1.Text == "4")
            {
                Form storekeeper = new storekeeper();
                storekeeper.Show();
            }
        }
    }
}
