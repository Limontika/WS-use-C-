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
    public partial class cutting : Form
    {
        private string sql;
        private object[] mass_vendorCode;
        
        public cutting()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PrintDialog print = new PrintDialog();
            print.ShowDialog();
        }

        private void cutting_Load(object sender, EventArgs e)
        {

            List<object> products = new List<object>();
            List<object> materials = new List<object>();
            //List<object> vendCode = new List<object>();

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
                sql = $"SELECT vendor_code, name, quantity, width, height FROM products";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(reader[1] + " " + reader[3] + "x" + reader[4]);
                }
                reader.Close();

                sql = $"SELECT vendor_code, composition, color, width, height FROM rolls";
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                int tmp = 0;
                while (reader.Read())
                {
                    materials.Add(reader[1] + " " + reader[2] + " " + reader[3] + "x" + reader[4]);
                    //int.TryParse(reader[0].ToString(), out tmp);
                    //vendCode.Add(tmp);
                }
                reader.Close();
                conn.Close();
                comboBox1.Items.AddRange(products.ToArray());
                comboBox2.Items.AddRange(materials.ToArray());
                //vendCode.CopyTo(mass_vendorCode);
            }
            catch(Exception er)
            {
                MessageBox.Show(
                $"Данные не найдены {er}",
                "Сообщение",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
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
            int height = 0;
            int width = 0;
            string color = "";


            int.TryParse(comboBox2.SelectedIndex.ToString(), out index);
            Console.WriteLine(mass_vendorCode[index]);

            try
            {
                string sql = $"SELECT width, height, color FROM rolls WHERE vendor_code = {index+1}";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int.TryParse(reader[0].ToString(), out width);
                    int.TryParse(reader[1].ToString(), out height);
                    color = reader[2].ToString();

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

            pictureBox1.Width = width;
            pictureBox1.Height = height;
            switch (color)
            {
                case "Синий":
                    pictureBox1.BackColor = Color.Blue;
                    break;
                case "Светло-синий":
                    pictureBox1.BackColor = Color.BlueViolet;
                    break;
                case "Оранжевый":
                    pictureBox1.BackColor = Color.Orange;
                    break;
                case "Лиловый":
                    pictureBox1.BackColor = Color.Purple;
                    break;
                case "Красный":
                    pictureBox1.BackColor = Color.Red;
                    break;
                case "Зелёный":
                    pictureBox1.BackColor = Color.Green;
                    break;
                case "Жёлтый":
                    pictureBox1.BackColor = Color.Yellow;
                    break;
                default: 
                    pictureBox1.BackColor = Color.Gray;
                    break;
            } 
        }
    }
}
