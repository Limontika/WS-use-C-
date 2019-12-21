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
    public partial class all_order : Form
    {

        private int id_user;
        private int id_role;
        private string sql;

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

            string[] status = new string[4] { "Новый", "Принят к проверке", "В работе", "Выполнен" };

            var temp = MessageBox.Show(
            "Изменить статус?",
            "Сообщение",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1);

            if (temp == DialogResult.No)
            {
                return;
            }

            try
            {
                if (dataGridView1.CurrentCell.Value.ToString() != status[3])
                {

                    int i = 0;
                    foreach (var item in status)
                    {
                        if (dataGridView1.CurrentCell.Value.ToString() == item && i + 1 < status.Length)
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
                                string sql = $"UPDATE orders SET state='{status[i + 1]}', updated_at='{now}' WHERE id={dataGridView1.Rows[int.Parse(dataGridView1.CurrentCellAddress.Y.ToString())].Cells[0].Value}";
                                Console.WriteLine(sql);
                                MySqlCommand cmd = conn.CreateCommand();
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();

                                conn.Close();
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

                            dataGridView1.CurrentCell.Value = status[i + 1];

                            MessageBox.Show(
                            $"Сатус изменен на {dataGridView1.CurrentCell.Value}",
                            "Сообщение",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                            break;
                        }
                        i++;
                    }
                }
                else
                {
                    MessageBox.Show(
                    $"Не возможно изменить статус, заказ уже выполнен!",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                }
            }

            catch (Exception er)
            {
                MessageBox.Show(
                $"Статус не изменился, попробуйте снова. Ошибка({er})",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
        }

        private void all_order_Load(object sender, EventArgs e)
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
                if (id_role == 1)
                {
                    this.button1.Hide();
                    sql = $"SELECT id, state, customer_id, manager_id, created_at, updated_at FROM orders WHERE customer_id= {id_user}";
                }
                else if (id_role == 3)
                {
                    this.button4.Hide();
                    sql = $"SELECT id, state, customer_id, manager_id, created_at, updated_at FROM orders WHERE manager_id= {id_user}";
                }

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                }
                reader.Close();
                conn.Close();

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
            dataGridView1.Rows.Clear();

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
                if (id_role == 1)
                {
                    sql = $"SELECT id, state, customer_id, manager_id, created_at, updated_at FROM orders WHERE customer_id= {id_user}";
                }
                else if (id_role == 3)
                {
                    sql = $"SELECT id, state, customer_id, manager_id, created_at, updated_at FROM orders WHERE manager_id= {id_user}";
                }

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                }
                reader.Close();
                conn.Close();

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

        private void button4_Click(object sender, EventArgs e)
        {

            var temp = MessageBox.Show(
            "Отменить заказ?",
            "Сообщение",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1);

            if (temp == DialogResult.No)
            {
                return;
            }

            string status = "Отменен";

            try
            {
                if (dataGridView1.CurrentCell.Value.ToString() != status && dataGridView1.CurrentCell.Value.ToString() == "Новый")
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
                        string sql = $"UPDATE orders SET state='{status}', updated_at='{now}' WHERE id={dataGridView1.Rows[int.Parse(dataGridView1.CurrentCellAddress.Y.ToString())].Cells[0].Value}";
                        Console.WriteLine(sql);
                        MySqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        conn.Close();
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

                    dataGridView1.CurrentCell.Value = status;

                    MessageBox.Show(
                    $"Сатус изменен на {dataGridView1.CurrentCell.Value}",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);


                }
                else
                {
                    MessageBox.Show(
                    $"Невозможно отменить заказ. Заказ уже отменен или находится в РАБОТЕ!",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                }
            }

            catch (Exception er)
            {
                MessageBox.Show(
                $"Ничего не выбрано, попробуйте снова",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
        }
    }
}
