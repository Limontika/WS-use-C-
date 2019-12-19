using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class registration : Form
    {
        public registration()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex def_passwd = new Regex(@"(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}");
            if (textBox2.Text != "" && textBox3.Text != "")
            {
                if (textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show(
                        "Пароли не совпадают",
                        "Сообщение",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                }
                else if (def_passwd.IsMatch(textBox2.Text) && textBox2.Text == textBox3.Text)
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
                        string sql = $"INSERT users (login, password, role_id) VALUES ('{textBox1.Text}', '{textBox2.Text}', 1);";
                        MySqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        MessageBox.Show(
                        $"Пользователь '{textBox1.Text}' был успешно добавлен",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                    }
                    catch
                    {
                        MessageBox.Show(
                        "Неполучилось создать пользователя, обратитесь к системному админестратору",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    }
                    
                }
                else
                {
                       MessageBox.Show(
                       "Неправильный логин или пароль",
                       "Сообщение",
                       MessageBoxButtons.OKCancel,
                       MessageBoxIcon.Error,
                       MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show(
                  "Введите пароли ПОЖАЛУЙСТА!",
                  "Сообщение",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error,
                  MessageBoxDefaultButton.Button1);

            }
            
        }

        private void registration_Load(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) button1.Enabled = false;
            else button1.Enabled = true;
        }
    }
}
