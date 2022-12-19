using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Practicum13
{
    //абстрактный класс
    abstract class Trans
    {
        public abstract int LoadCapacity { get; }
        public abstract void OutInfo();
        public abstract void GetLoadCapacity();
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

        public override void OutInfo()
        {
            Console.WriteLine("\nИнформация о машине: ");
            Console.WriteLine($"Марка: {brand}");
            Console.WriteLine($"Номер: {number}");
            Console.WriteLine($"Скорость: {speed}");
            Console.WriteLine($"Грузоподъемность: {loadCapacity}");
        }
        public override void GetLoadCapacity()
        {
            Console.WriteLine($"Грузоподъемность: {loadCapacity}");
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

        public override void OutInfo()
        {
            Console.WriteLine("\nИнформация о мотоцикле: ");
            Console.WriteLine($"Марка: {brand}");
            Console.WriteLine($"Номер: {number}");
            Console.WriteLine($"Скорость: {speed}");
            Console.WriteLine($"Грузоподъемность: {loadCapacity}");
            string carriage = (isСarriage) ? "Да" : "Нет";
            Console.WriteLine($"Есть коляска: {carriage}");
        }

        public override void GetLoadCapacity()
        {
            if (isСarriage)
            {
                Console.WriteLine($"Так как есть коляска, то грузоподъемность мотоцикла {loadCapacity}");
            }
            else
            {
                loadCapacity = 0;
                Console.WriteLine($"Так как коляски нет, то грузоподъемность мотоцикла {loadCapacity}");
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
        public override void OutInfo()
        {
            Console.WriteLine("\nИнформация о грузовике: ");
            Console.WriteLine($"Марка: {brand}");
            Console.WriteLine($"Номер: {number}");
            Console.WriteLine($"Скорость: {speed}");
            Console.WriteLine($"Грузоподъемность: {loadCapacity}");
            string trailer = (isTrailer) ? "Да" : "Нет";
            Console.WriteLine($"Есть прицеп: {trailer}");
        }

        public override void GetLoadCapacity()
        {
            if (isTrailer)
            {
                loadCapacity *= 2;
                Console.WriteLine($"Так как есть прицеп, то грузоподъемность грузовика: {loadCapacity}");
            }
            else
            {
                Console.WriteLine($"Так как прицепа нет, то грузоподъемность грузовика: {loadCapacity}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List <Trans> transport = new List<Trans>();
            int amount = 0;
            string brand;
            int number = 0, speed = 0, loadCapacity = 0;
            bool isCarriage = true;
            bool isTrailer = true;

            int opetation;
            Console.WriteLine("Как Вы желаете получить данные?\n1 - Взять данные из файла\n2 - Ввести данные в ручную");
            Console.Write("Введите номер желаемой операции: ");
            opetation = int.Parse(Console.ReadLine());
			switch (opetation) {
				case 1:
                    //чтение из файла database.txt
                    //заполнить в формате 
                    //[1]Тип т/с (Автомобиль, Мотоцикл, Грузовик)
                    //[2]Марка
                    //[3]Номер
                    //[4]Скорость
                    //[5]Грузоподъемность (если Автомобиль или Грузовик)
                    //[6]Есть ли прицеп/коляска (если Грузовик, если Мотоцикл - вписать на строке [5])
                    string fileName = "database.txt";
                    string filePath = Path.GetFullPath(fileName);
			        string[] arr = File.ReadAllLines(filePath);

			for (int i = 0; i < arr.Length; i++) {
				if (arr[i] == "Автомобиль") 
                {
					brand = arr[i + 1];
					number = Convert.ToInt32(arr[i + 2]);
					speed = Convert.ToInt32(arr[i + 3]);
					loadCapacity = Convert.ToInt32(arr[i + 4]);
					Car car = new Car(brand, number, speed, loadCapacity);
					transport.Add(car);
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
				}
			}
					break;
                case 2:
                    while (true)
            {
                try
                {
                    Console.Write("Введите количество транспортных средств: ");
                    amount = int.Parse(Console.ReadLine());
                    if (amount < 1) throw new Exception("Значение не может равняться нулю или меньше!");
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка! Введите целочисленное значение");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
                    break;
			}

            for (int i = 0; i < amount; i++)
            {
                int choose;
                while (true)
                {
                    try
                    {
                        Console.WriteLine("\nКакое т/с нужно добавить?\n1 - Легковая машина, 2 - Мотоцикл, 3 - Грузовик");
                        Console.Write("Введите нужное значение: ");
                        choose = int.Parse(Console.ReadLine());
                        if (choose < 1 || choose > 3) throw new Exception("Введите цифру в диапазоне от 1 до 3!");
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка! Введите целочисленное значение");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                switch (choose)
                {
                    case 1:
                        Console.Write("Введите марку т/с: ");
                        brand = Console.ReadLine();
                        try
                        {
                            Console.Write("Введите номер т/с: ");
                            number = int.Parse(Console.ReadLine());
                            Console.Write("Введите скорость т/с: ");
                            speed = int.Parse(Console.ReadLine());
                            Console.Write("Введите грузоподъемность т/с: ");
                            loadCapacity = int.Parse(Console.ReadLine());
                            if (number < 0 || speed < 0 || loadCapacity < 0) throw new Exception("Значение не может быть отрицательным!");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка! Введите численное значение");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        Car car = new Car(brand, number, speed, loadCapacity);
                        transport.Add(car);
                        break;

                    case 2:
                        Console.Write("Введите марку т/с: ");
                        brand = Console.ReadLine();
                        try
                        {
                            Console.Write("Введите номер т/с: ");
                            number = int.Parse(Console.ReadLine());
                            Console.Write("Введите скорость т/с: ");
                            speed = int.Parse(Console.ReadLine());
                            Console.Write("Имеется ли у т/с мотоколяска? (Да/Нет): ");
                            string answ = Console.ReadLine();
                            switch (answ)
                            {
                                case "Да":
                                    isCarriage = true;
                                    try
                                    {
                                        Console.Write("Введите грузоподъемность: ");
                                        loadCapacity = int.Parse(Console.ReadLine());
                                        if (loadCapacity < 0) throw new Exception("Грузоподъемность не может быть отрицательной!");
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Ошибка! Нужно ввести численное значение");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case "Нет":
                                    isCarriage = false;
                                    break;
                            }
                            if (number < 0 || speed < 0 || loadCapacity < 0) throw new Exception("Значение не может быть отрицательным!");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка! Введите численное значение");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        Motorcycle bike = new Motorcycle(brand, number, speed, loadCapacity, isCarriage);
                        bike.GetLoadCapacity();
                        transport.Add(bike);
                        break;

                    case 3:
                        Console.Write("Введите марку т/с: ");
                        brand = Console.ReadLine();
                        try
                        {
                            Console.Write("Введите номер т/с: ");
                            number = int.Parse(Console.ReadLine());
                            Console.Write("Введите скорость т/с: ");
                            speed = int.Parse(Console.ReadLine());
                            Console.Write("Введите грузоподъемность т/с: ");
                            loadCapacity = int.Parse(Console.ReadLine());
                            if (number < 0 || speed < 0 || loadCapacity < 0) throw new Exception("Значение не может быть отрицательным!");
                            Console.Write("Имеется ли у грузовика прицеп? (Да/Нет): ");
                            string str = Console.ReadLine();
                            switch (str)
                            {
                                case "Да":
                                    isTrailer = true;
                                    break;
                                case "Нет":
                                    isTrailer = false;
                                    break;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка! Введите численное значение");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Truck truck = new Truck(brand, number, speed, loadCapacity, isTrailer);
                        truck.GetLoadCapacity();
                        transport.Add(truck);
                        break;
                }
            }
            Console.WriteLine("\nИнформация из базы данных по всем внесенным т/с\n");
            foreach (var e in transport)
            {
                e.OutInfo();
            }

            int loadCap;
            while (true)
            {
                try
                {
                    Console.Write("\nВведите требуемое значение грузоподъемности: ");
                    loadCap = int.Parse(Console.ReadLine());
                    if (loadCap < 0) throw new Exception("Значение грузоподъемности не может быть отрицательным!");
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка! Введите численное значение");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            int count = 0;
            Console.WriteLine("Т/с, с подходящей грузоподъемностью:\n");
            foreach (var t in transport)
            {
                if (t.LoadCapacity == loadCap) 
                {
                    t.OutInfo();
                    count++;
                }
            }
            if (count == 0) Console.WriteLine("Подходящих т/с не обнаружено");
        }
    }
}
