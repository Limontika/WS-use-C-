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
    public partial class order : Form
    {
        private int id_user;
        private long id_order;
        private int cost;
        private int rows = 1;

        public order(int id)
        {
            InitializeComponent();
            id_user = id;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form constr = new constr();
            constr.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void order_Load(object sender, EventArgs e)
        {

            List<object> products = new List<object>();

            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;


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

            try
            {
                string sql = $"SELECT vendor_code, name, quantity, width, height FROM products";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(reader[1] + " " + reader[3] + "x" + reader[4]);
                }
                reader.Close();
                conn.Close();
                comboBox1.Items.AddRange(products.ToArray());
                /*int.TryParse(reader[0].ToString(), out name_products);*/
            }
            catch
            {
                MessageBox.Show(
                "Данные не найдены",
                "Сообщение",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }

            object[] quanyti = new object[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            
            comboBox2.Items.AddRange(quanyti);
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
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

            int index = 0;
            int quanyti = 0;
            int height = 0;
            int width = 0;
            int vendor_code = 0;
            string name = "";

            int.TryParse(comboBox1.SelectedIndex.ToString(), out index);
            int.TryParse(comboBox2.SelectedItem.ToString(), out quanyti);

            try
            {
                string sql = $"SELECT vendor_code, name, width, height FROM products WHERE vendor_code = {index+1}";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    name = reader[1].ToString();
                    int.TryParse(reader[2].ToString(), out width);
                    int.TryParse(reader[3].ToString(), out height);
                    int.TryParse(reader[0].ToString(), out vendor_code);
                }
                reader.Close();
                conn.Close();

            }
            catch
            {
                MessageBox.Show(
                "Не выбрано изделия для оформления заказа",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }

            int S = height * width;
            S = S / 10;
            int temp = S * quanyti;
            cost += temp ;

            textBox1.Text = cost.ToString();

            int i = 0;

            while (i != dataGridView1.RowCount)
            {
                if(int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) == vendor_code)
                {
                    dataGridView1.Rows[i].Cells[2].Value = quanyti + int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    return;
                }
                i++;
            }
            

            dataGridView1.Rows.Add(rows, comboBox1.SelectedItem.ToString(), quanyti, temp, vendor_code);
            rows += 1;

        }

        private void button3_Click(object sender, EventArgs e)
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

            int count = dataGridView1.RowCount;

            try
            {
                DateTime now = DateTime.Now;
                string sql = $"INSERT orders( customer_id, created_at, updated_at) VALUES ({id_user},'{now}','{now}'); ";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                id_order = cmd.LastInsertedId;
                int i = 0;

                while ( i != count)
                {
                    sql = $"INSERT orders_has_products (orders_id, products_vendor_code, quantity) VALUES ({id_order}, {dataGridView1.Rows[i].Cells[4].Value}, {dataGridView1.Rows[i].Cells[2].Value}); ";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    i++;
                } 
                
                conn.Close();
               
                MessageBox.Show(
                "Заказ успешно создан",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            }

            catch (Exception er)
            {
                MessageBox.Show(
                $"Не удалось передать заказ, повторите попытку (Ошибка {er})",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(item);
                }
                
                int i = 0;

                while (i != dataGridView1.RowCount)
                {

                    dataGridView1.Rows[i].Cells[0].Value = i+1;
                    i++;
                }

                rows = i+1;

                MessageBox.Show(
                "Успешно удален продукт из заказа!",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            }

            catch (Exception er)
            {
                MessageBox.Show(
                $"Невозможно удалить продукт, ничего не выбрано!",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
        }
    }
}
