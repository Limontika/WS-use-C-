using MySql.Data.MySqlClient;
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
    public partial class viewItems : Form
    {

        private string txt;

        public viewItems(string text)
        {
            InitializeComponent();
            label1.Text = text;
            txt = text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void viewItems_Load(object sender, EventArgs e)
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

            switch (txt)
            {
                case "Список тканей":
                    
                    try
                    {
                        string sql = $"SELECT vendor_code, composition, color, width, height FROM rolls";
                        MySqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4]);
                        }
                        reader.Close();
                        conn.Close();

                    }
                    catch
                    {
                        MessageBox.Show(
                        $"На складе нету {txt}",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    }
                    break;

                case "Список фурнитуры":

                    dataGridView1.Columns[2].HeaderText = "Количество";

                    try
                    {
                        string sql = $"SELECT vendor_code, name, quantity, width, height FROM products";
                        MySqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4]);
                        }
                        reader.Close();
                        conn.Close();

                    }
                    catch
                    {
                        MessageBox.Show(
                        $"На складе нету {txt}",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    }
                    break;

                case "Список изделий":

                    dataGridView1.Columns[2].HeaderText = "Количество";

                    try
                    {
                        string sql = $"SELECT vendor_code, name, quantity, width, height FROM products";
                        MySqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4]);
                        }
                        reader.Close();
                        conn.Close();

                    }
                    catch
                    {
                        MessageBox.Show(
                        $"На складе нету {txt}",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    }
                    break;
            }
            
        }
    }
}
