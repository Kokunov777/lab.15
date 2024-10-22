using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceManager
{
    // базовый класс для устройство 
    class Device
    {
        protected string Brand { get; set; }
        protected string Model { get; set; }

        public Device(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }
        public virtual void Print() 
        {
            Console.WriteLine($"Бренд: {Brand}");
            Console.WriteLine($"Модель: {Model}");
        }
    }

    // Класс для персональных компьютеров
    class PersonalComputer : Device
    {
        protected int Ram { get; set; }
        protected int Storage { get; set; }
        public PersonalComputer(string brand, string model, int ram, int storage)
           : base(brand, model)
        {
            Ram = ram;
            Storage = storage;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine($"ОЗУ: {Ram} ГБ");
            Console.WriteLine($"Хранилище: {Storage} ГБ");
        }
    }

    // Класс для ноутбуков
    class Laptop : PersonalComputer
    {
        protected double Weight { get; set; }
        protected double ScreenSize { get; set; }

        public Laptop(string brand, string model, int ram, int storage, double weight, double screenSize)
            : base(brand, model, ram, storage)
        {
            Weight = weight;
            ScreenSize = screenSize;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine($"Вес: {Weight} кг");
            Console.WriteLine($"Размер экрана: {ScreenSize} дюймов");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            List<Device> devices = new List<Device>();

            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1) Добавить устройство");
                Console.WriteLine("2) Удалить устройство");
                Console.WriteLine("3) Вывести список устройств");
                Console.WriteLine("4) Выход");

                Console.Write("Введите выбор: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Неверный выбор. Введите число.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        devices.Add(CreateDevice());
                        break;
                    case 2:
                        RemoveDevice(devices);
                        break;
                    case 3:
                        PrintDevices(devices);
                        break;
                    case 4:
                        Console.WriteLine("Выход...\n");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.\n");
                        break;
                }
            }
        }

        static Device CreateDevice()
        {
            Console.WriteLine("Выберите тип устройства:\n");
            Console.WriteLine("1) Простое устройство");
            Console.WriteLine("2) Персональный компьютер");
            Console.WriteLine("3) Ноутбук");

            Console.Write("Введите выбор: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Неверный выбор. Введите число.");
                return null;
            }

            Console.Write("Введите бренд: ");
            string brand = Console.ReadLine();
            Console.Write("Введите модель: ");
            string model = Console.ReadLine();

            switch (choice)
            {
                case 1:
                    return new Device(brand, model);
                case 2:
                    int ram = GetValidInt("Введите объем ОЗУ (ГБ, от 1 до 128): ", 1, 128);
                    int storage = GetValidInt("Введите объем хранилища (ГБ, от 1 до 16384): ", 1, 16384);
                    return new PersonalComputer(brand, model, ram, storage);
                case 3:
                    ram = GetValidInt("Введите объем ОЗУ (ГБ, от 1 до 64): ", 1, 64);
                    storage = GetValidInt("Введите объем хранилища (ГБ, от 1 до 8192): ", 1, 8192);
                    double weight = GetValidDouble("Введите вес (кг, от 0.5 до 5): ", 0.5, 5);
                    double screenSize = GetValidDouble("Введите размер экрана (дюймы, от 10 до 18): ", 10, 18);
                    return new Laptop(brand, model, ram, storage, weight, screenSize);
                default:
                    Console.WriteLine("Неверный выбор!\n");
                    return null;
            }
        }

        // Функция для получения корректного целого числа в заданном диапазоне
        static int GetValidInt(string prompt, int min, int max)
        {
            int value;
            do
            {
                Console.Write(prompt);
                if (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max)
                {
                    Console.WriteLine($"Введите целое число в диапазоне от {min} до {max}.");
                }
            } while (value < min || value > max);

            return value;
        }

        // Функция для получения корректного числа с плавающей точкой в заданном диапазоне
        static double GetValidDouble(string prompt, double min, double max)
        {
            double value;
            do
            {
                Console.Write(prompt);
                if (!double.TryParse(Console.ReadLine(), out value) || value < min || value > max)
                {
                    Console.WriteLine($"Введите число с плавающей точкой в диапазоне от {{min}} до {{max}}.");
                }
            } while (value < min || value > max);

            return value;
        }


            static void PrintDevices(List<Device> devices)
        {
            if (devices.Count == 0)
            {
                Console.WriteLine("Список устройств пуст.\n");
                return;
            }

            foreach (var device in devices)
            {
                device.Print();
                Console.WriteLine();
            }
        }

        static void RemoveDevice(List<Device> devices)
        {
            if (devices.Count == 0)
            {
                Console.WriteLine("Список устройств пуст.\n");
                return;
            }

            PrintDevices(devices);

            Console.Write("Введите номер устройства для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > devices.Count)
            {
                Console.WriteLine("Неверный номер устройства.\n");
                return;
            }

            devices.RemoveAt(index - 1);
            Console.WriteLine("Устройство удалено.\n");
        }
    }
}

