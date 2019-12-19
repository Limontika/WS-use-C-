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
            if (textBox1.Text.Length == 0) button1.Enabled = false;
            else button1.Enabled = true;
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
                Form customer = new Заказчик();
                customer.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";

            }else if (textBox1.Text == "2")
            {
                Form storekeeper = new storekeeper();
                storekeeper.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else if (textBox1.Text == "3")
            {
                Form manager = new manager();
                manager.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else if (textBox1.Text == "4")
            {
                Form director = new director();
                director.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
            }else
            {
                   MessageBox.Show(
                   "Логин или пароль неправильны",
                   "Сообщение",
                   MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Error,
                   MessageBoxDefaultButton.Button1);
            }
        }
    }
}
