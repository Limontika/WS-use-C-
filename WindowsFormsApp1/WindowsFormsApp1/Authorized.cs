using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private int id_user;
        private int id_role;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            registration reg = new registration();
            reg.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = DB.GetDBConnection();
            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show(
                "Проблемы с подключением к БД",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }

            String loginUser = textBox1.Text;
            String passUser = textBox2.Text;
            try
            {
                string sql = $"SELECT id, role_id FROM users WHERE login='{loginUser}' AND password='{passUser}'";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                int.TryParse(reader[0].ToString(), out id_user);
                int.TryParse(reader[1].ToString(), out id_role);
            }
            catch
            {
                MessageBox.Show(
                "Логин или пароль неправильны",
                "Сообщение",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }

            if (id_role == 1)
            {
                Form customer = new Заказчик(id_user, id_role);
                customer.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else if (id_role == 2)
            {
                Form storekeeper = new storekeeper();
                storekeeper.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else if (id_role == 3)
            {
                Form manager = new manager(id_user, id_role);
                manager.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else if (id_role == 4)
            {
                Form director = new director(id_user, id_role);
                director.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
    }
}
