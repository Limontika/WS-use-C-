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
    public partial class receipt_of_materials : Form
    {

        private int cost;
        private int rows = 1;

        public receipt_of_materials()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void receipt_of_materials_Load(object sender, EventArgs e)
        {
            List<object> materials = new List<object>();

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
                string sql = $"SELECT vendor_code, composition, color, width, height FROM rolls";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    materials.Add(reader[1] + " " + reader[2]);
                }
                reader.Close();
                conn.Close();
                comboBox1.Items.AddRange(materials.ToArray());
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

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

            int index = 0;
            int quanyti = 0;
            int height = 0;
            int width = 0;
            int vendor_code = 0;
            string name = "";
            string color = "";

            int.TryParse(comboBox1.SelectedIndex.ToString(), out index);
            int.TryParse(comboBox2.SelectedItem.ToString(), out quanyti);

            try
            {
                string sql = $"SELECT vendor_code, composition, color, width, height FROM rolls WHERE vendor_code = {index + 1}";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    name = reader[1].ToString();
                    color = reader[2].ToString();
                    int.TryParse(reader[3].ToString(), out width);
                    int.TryParse(reader[4].ToString(), out height);
                    int.TryParse(reader[0].ToString(), out vendor_code);
                }
                reader.Close();
                conn.Close();

            }
            catch
            {
                MessageBox.Show(
                "Не выбрано материалов для оформления заказа",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }


            int temp = int.Parse(textBox1.Text) * quanyti;
            cost += temp;

            textBox2.Text = cost.ToString();

            int i = 0;

            while (i != dataGridView1.RowCount)
            {
                if (int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) == vendor_code)
                {
                    dataGridView1.Rows[i].Cells[2].Value = quanyti + int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    return;
                }
                i++;
            }


            dataGridView1.Rows.Add(rows, comboBox1.SelectedItem.ToString(), color, width, height, quanyti, temp, vendor_code);
            rows += 1;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int count = dataGridView1.RowCount;

            try
            {

                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    int index = dataGridView2.Rows.Add(r.Clone() as DataGridViewRow);
                    foreach (DataGridViewCell o in r.Cells)
                    {
                        dataGridView2.Rows[index].Cells[o.ColumnIndex].Value = o.Value;
                    }
                }

                dataGridView1.Rows.Clear();

                rows = 1;

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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();

                MessageBox.Show(
                "Принято на склад",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            }
            catch (Exception er)
            {
                MessageBox.Show(
                $"Что-то пошло не так (Ошибка {er})",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
        }
    }
}
