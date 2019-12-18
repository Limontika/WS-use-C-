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
            Form form = new Form1();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex def_passwd = new Regex(@"(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}");
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                if (textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show(
                        "Пароли не совпадают",
                        "Сообщение",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                }
                if (def_passwd.IsMatch(textBox2.Text) && textBox2.Text == textBox3.Text)
                {
                   
                }
                else
                {
                       MessageBox.Show(
                       "Неправильный логин или пароль",
                       "Сообщение",
                       MessageBoxButtons.OKCancel,
                       MessageBoxIcon.Error,
                       MessageBoxDefaultButton.Button1,
                       MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            else
            {
                
            }
            
        }
    }
}
