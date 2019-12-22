using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class storekeeper : Form
    {
        public storekeeper()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var name_label = "Список фурнитуры";
            Form viewItem = new viewItems(name_label);
            viewItem.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name_label = "Список тканей";
            Form viewItem = new viewItems(name_label);
            viewItem.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form receipt_of_materials = new receipt_of_materials();
            receipt_of_materials.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
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

            try
            {
                DateTime now = DateTime.Now;
                string sql = $"SELECT vendor_code, composition, color, width, height FROM rolls";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                var path = "D:/myProject/WS-use-C-/reports/";
                var write_path = $@"{path}{now.ToString("dd.MM.yyyy_hhmmss")}.csv";
                Console.WriteLine(write_path.ToString());
                string info = "Выгрузка остатков материалов от " + now;
                string title = "composition;color;width;height";
                using (StreamWriter sw = new StreamWriter(write_path, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(info + '\n');
                    sw.WriteLine(title + '\n');

                    while (reader.Read())
                        {
                            sw.WriteLine("\"" + reader[1] + "\";" + "\t" + "\"" + reader[2] + "\";" + "\t" + "\"" + reader[3] + ";" + "\t" + "\"" + reader[4] + ";");
                        }
                }

                reader.Close();
                conn.Close();

                DialogResult result =  MessageBox.Show(
                "Отчет успешно создан, нажмите ОК чтобы посмотореть отчет или Cancle для продолжения работы",
                "Сообщение",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);

                if (result == DialogResult.OK)
                {
                    Process.Start($"{write_path}");
                }

            }
            catch
            {
                MessageBox.Show(
                "Ошибка выгрузки отчета",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }

        }
    }
}
