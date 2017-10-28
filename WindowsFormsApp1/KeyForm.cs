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
    public partial class KeyForm : Form
    {
        Func b;
        public KeyForm(string dataPas)
        {
            // data принимает пароль из формы LogForm.
            this.data = dataPas;
            b = new Func();
            InitializeComponent();
        }
        string data;

        private void btnKey_Click(object sender, EventArgs e)
        {
            /*  1) Считывание пароля из файла.
                2) Шифрование принятого пароля "data = txtPas.Text".
                3) Проверка зашифрованного пароля и пароля в файле(3 попытки).
                Если Введёный == Сохранённый пароли сопадают:
                4) Генерация НОВОГО ключа.
                5) Шифрование пароля.
                6) Запись в файл.
                7) Передача ключа в конструктор формы "LabelKeyForm1".
                8) Открытие формы "LabelKeyForm1".
                Иначе 
                4) Сообщение об ошибке(еще 2 попытки).
                5) Открытие формы "LogForm" для ввода пароля.
            */

            string EncodePas; // Новый сгенерированный пароль.
            string EnterPas; // Введёный (Зашифрованный) пароль.
            String UserData; // Сохранённый в файле (Зашифрованный) пароль.

            if (txtKey.Text == "") // Если поле для ввода клча пустое. 
            {
                MessageBox.Show("Поле ключа не может быть пустым!");
            }

            // (1).
            UserData = b.ReadFromFile();

            // Если длинна введенного пароля больше чем зашифрованного. 
            // Такая проверка нужна потому что алгоритм не будет работать,
            // и если не обработать это условие ошибка вылезет в функции "Encode"(выход за границы массива).
            if ( (data.Length > UserData.Length) || (data.Length < UserData.Length) )
            {
                // (4).
                MessageBox.Show(String.Format("Неправильный пароль или ключ!\nОсталось попыток: {0}", Func.PopytkaNum));
                Func.PopytkaNum--;

                if (Func.PopytkaNum < 0)
                {
                    MessageBox.Show("Попытки закончились!");
                    Application.Exit();
                }

                LogForm f = new LogForm();

                // (5).
                this.Hide();
                f.ShowDialog();
                this.Close();
            }

            else
            { 
                
                // (2).
                EnterPas = b.Encode(data, txtKey.Text);

                // (3).
                if (UserData == EnterPas) // Проверка Введённого и  Сохранённого в файле пароля.
                {

                    // (4).
                    Func.Key = b.GenKey();

                    // (5).
                    EncodePas = b.Encode(data, Func.Key);

                    // (6).
                    b.WriteInFile(EncodePas);

                    // (7).
                    LabelKeyForm1 f = new LabelKeyForm1();

                    // (8).
                    this.Hide();
                    f.ShowDialog();
                    this.Close();

                }
                else
                {
                    // (4).
                    MessageBox.Show(String.Format("Неправильный пароль или ключ!\nОсталось попыток: {0}", Func.PopytkaNum));
                    Func.PopytkaNum--;

                    if (Func.PopytkaNum < 0)
                    {
                        MessageBox.Show("Попытки закончились!");
                        Application.Exit();
                    }

                    LogForm f = new LogForm();

                    // (5).
                    this.Hide();
                    f.ShowDialog();
                    this.Close();
                }
            }
        }

    }
}
