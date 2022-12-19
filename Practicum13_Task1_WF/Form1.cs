using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Practicum13_Task1_WF
{
    public partial class Form1 : Form
    {
        List<Trans> transport = new List<Trans>();
        int amount;
        int count;
        string brand;
        int number = 0, speed = 0, loadCapacity = 0;
        bool isCarriage = true;
        bool isTrailer = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void CountIsFull()
        {
            if (count == amount)
            {
                MessageBox.Show("Необходимое кол-во т/с внесено в базу данных", "Успех!");
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;

                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                groupBox5.Enabled = false;

                richTextBox1.Clear();
                foreach (var e in transport)
                {
                    richTextBox1.Text += e.OutInfo();
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            numericUpDown4.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Enabled = true;
            groupBox5.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = false;
            numericUpDown4.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Enabled = false;
            groupBox5.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = true;
            numericUpDown4.Enabled = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            isCarriage = true;
            numericUpDown4.Enabled = true;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            isCarriage = false;
            numericUpDown4.Enabled = false;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            isTrailer = true;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            isTrailer = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            brand = textBox1.Text;
            number = (int)numericUpDown2.Value;
            speed = (int)numericUpDown3.Value;
            loadCapacity = (int)numericUpDown4.Value;

            Motorcycle bike = new Motorcycle(brand, number, speed, loadCapacity, isCarriage);
            transport.Add(bike);
            count++;

            textBox1.Clear();
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown4.Enabled = true;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            CountIsFull();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            brand = textBox1.Text;
            number = (int)numericUpDown2.Value;
            speed = (int)numericUpDown3.Value;
            loadCapacity = (int)numericUpDown4.Value;

            Truck truck = new Truck(brand, number, speed, loadCapacity, isTrailer);
            MessageBox.Show(truck.GetLoadCapacity());
            transport.Add(truck);
            count++;

            textBox1.Clear();
            radioButton6.Checked = false;
            radioButton7.Checked = false;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            CountIsFull();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int loadCap = (int)numericUpDown5.Value;
            richTextBox2.Clear();
            int count = 0;
            foreach (var t in transport)
            {
                if (t.LoadCapacity >= loadCap) 
                {
                    richTextBox2.Text += t.OutInfo();
                    count++;
                } 
            }
            if (count == 0) MessageBox.Show("Подходящих т/с не найдено", "Ошибка!");
        }

		private void button6_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();

            //фильтр по типу файлов
            dlg.Filter = "Text files(*.txt)|*.txt";
            //директория папки, которая открывается по нажатию кнопки
            dlg.InitialDirectory = "E:\\УП\\Practicum13\\Practicum13\\bin\\Debug";
            
            if (dlg.ShowDialog() == DialogResult.Cancel) return;

            string fileName = dlg.FileName;
            string filePath = Path.GetFullPath(fileName);
			string[] arr = File.ReadAllLines(filePath);

			for (int i = 0; i < arr.Length; i++) {
				if (arr[i] == "Автомобиль") 
                {
					brand = arr[i + 1];
					number = Convert.ToInt32(arr[i + 2]);
					speed = Convert.ToInt32(arr[i + 2]);
					loadCapacity = Convert.ToInt32(arr[i + 3]);
					Car car = new Car(brand, number, speed, loadCapacity);
					transport.Add(car);
					richTextBox1.Text += car.OutInfo();
				}

                else if (arr[i] == "Мотоцикл") 
                {
                    brand = arr[i + 1];
                    number = Convert.ToInt32(arr[i + 2]);
					speed = Convert.ToInt32(arr[i + 3]);
                    string answ = arr[i + 4];
                            switch (answ)
                            {
                                case "Да":
                                    isCarriage = true;
                                    loadCapacity = Convert.ToInt32(arr[i + 5]);
                                    break;
                                case "Нет":
                                    isCarriage = false;
                                    break; 
                            }
                    Motorcycle bike = new Motorcycle(brand, number, speed, loadCapacity, isCarriage);
                    bike.GetLoadCapacity();
                    transport.Add(bike);
                    richTextBox1.Text += bike.OutInfo();
				}

                else if (arr[i] == "Грузовик") 
                {
                    brand = arr[i + 1];
                    number = Convert.ToInt32(arr[i + 2]);
					speed = Convert.ToInt32(arr[i + 3]);
                    loadCapacity = Convert.ToInt32(arr[i + 4]);

                    string str = arr[i + 5];
                            switch (str)
                            {
                                case "Да":
                                    isTrailer = true;
                                    break;
                                case "Нет":
                                    isTrailer = false;
                                    break;
                            }
                    Truck truck = new Truck(brand, number, speed, loadCapacity, isTrailer);
                    truck.GetLoadCapacity();
                    transport.Add(truck);
                    richTextBox1.Text += truck.OutInfo();
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                amount = (int)numericUpDown1.Value;
                if (amount <= 0) throw new Exception("Количество т/с не может быть 0-м или отрицательным");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
                return;
            }

            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
            groupBox5.Enabled = true;

            count = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            brand = textBox1.Text;
            number = (int)numericUpDown2.Value;
            speed = (int)numericUpDown3.Value;
            loadCapacity = (int)numericUpDown4.Value;

            Car car = new Car(brand, number, speed, loadCapacity);
            transport.Add(car);
            count++;

            textBox1.Clear();
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            CountIsFull();
        }
    }

    //абстрактный класс
    abstract class Trans
    {
        public abstract int LoadCapacity { get; }
        public abstract string OutInfo();
        public abstract string GetLoadCapacity();
    }

    //реализующие классы
    class Car : Trans
    {
        string brand;
        int number;
        int speed;
        int loadCapacity;
        public override int LoadCapacity
        {
            get { return loadCapacity; }
        }

        public Car(string brand, int number, int speed, int loadCapacity)
        {
            this.brand = brand;
            this.number = number;
            this.speed = speed;
            this.loadCapacity = loadCapacity;
        }

        public override string OutInfo()
        {
            return $"\n------------------------\n" +
                $"Информация о машине:\n" +
                $"Марка: {brand}\n" +
                $"Номер: {number}\n" +
                $"Скорость: {speed}\n" +
                $"Грузоподъемность: {loadCapacity}";
        }
        public override string GetLoadCapacity()
        {
            return $"Грузоподъемность: {loadCapacity}";
        }
    }

    class Motorcycle : Trans
    {
        string brand;
        int number;
        int speed;
        int loadCapacity;
        bool isСarriage;
        public override int LoadCapacity
        {
            get { return loadCapacity; }
        }

        public Motorcycle(string brand, int number, int speed, int loadCapacity, bool isCarriage)
        {
            this.brand = brand;
            this.number = number;
            this.speed = speed;
            this.loadCapacity = loadCapacity;
            this.isСarriage = isCarriage;
        }

        public override string OutInfo()
        {
            string carriage = (isСarriage) ? "Да" : "Нет"; ;
            return $"\n------------------------\n" +
                $"Информация о мотоцикле:\n" +
                $"Марка: {brand}\n" +
                $"Номер: {number}\n" +
                $"Скорость: {speed}\n" +
                $"Грузоподъемность: {loadCapacity}\n" +
                $"Есть коляска: {carriage}";
        }

        public override string GetLoadCapacity()
        {
            if (isСarriage)
            {
                return $"Так как есть коляска, то грузоподъемность мотоцикла {loadCapacity}";
            }
            else
            {
                loadCapacity = 0;
                return $"Так как коляски нет, то грузоподъемность мотоцикла {loadCapacity}";
            }
        }
    }

    class Truck : Trans
    {
        string brand;
        int number;
        int speed;
        int loadCapacity;
        bool isTrailer;
        public override int LoadCapacity
        {
            get { return loadCapacity; }
        }
        public Truck(string brand, int number, int speed, int loadCapacity, bool isTrailer)
        {
            this.brand = brand;
            this.number = number;
            this.speed = speed;
            this.loadCapacity = loadCapacity;
            this.isTrailer = isTrailer;
        }
        public override string OutInfo()
        {
            string trailer = (isTrailer) ? "Да" : "Нет";
            return $"\n------------------------\n" +
                $"Информация о грузовике:\n" +
                $"Марка: {brand}\n" +
                $"Номер: {number}\n" +
                $"Скорость: {speed}\n" +
                $"Грузоподъемность: {loadCapacity}\n" +
                $"Есть прицеп: {trailer}";
        }

        public override string GetLoadCapacity()
        {
            if (isTrailer)
            {
                loadCapacity *= 2;
                return $"Так как есть прицеп, то грузоподъемность грузовика: {loadCapacity}";
            }
            else
            {
                return $"Так как прицепа нет, то грузоподъемность грузовика: {loadCapacity}";
            }
        }
    }
}
