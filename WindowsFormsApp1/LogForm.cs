using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; // Библиотека для работы с файлами.
using Functions;

namespace WindowsFormsApp1
{
    public partial class LogForm : Form
    {
        private Func b;
        public LogForm()
        {
            b = new Func();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Файл НЕ существует.
            if (!(File.Exists("\\test.txt")))
            {
                /*  
                    1) Генерация ключа.
                    2) Шифрование пароля.
                    3) Запись в файл.
                    4) Открытие формы "LabelKeyForm1".
                    5) Открытие формы "LabelKeyForm1".
                */
                string EncodePas;

                // (1).
                Func.Key = b.GenKey();

                // (2).
                EncodePas = b.Encode(txtPas.Text, Func.Key);

                // (3).
                b.WriteInFile(EncodePas);

                // (4).
                LabelKeyForm1 f = new LabelKeyForm1();

                // (5).
                this.Hide();
                f.ShowDialog();
                this.Close();
                
            }
            // Файл существует.
            else
            {
                /*  
                    1) Передача пароля в конструктор формы "KeyForm".
                    2) Открытие формы "KeyForm" для ввода ключа.
                */

                // (1).
                KeyForm f = new KeyForm(this.txtPas.Text);

                // (2).
                this.Hide();
                f.ShowDialog();
                this.Close();
            }
            
        }

    }
}
