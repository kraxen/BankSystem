using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductClientLibrary
{
    public class Client
    {
        #region Свойства/Поля
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Номер лицевого счета
        /// </summary>
        public string BankBook { get; set; }
        /// <summary>
        /// Хороший или обычный клиент
        /// </summary>
        public string Positivity { get; set; }
        /// <summary>
        /// Возраст клиента
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Количество денег на счете
        /// </summary>
        public double Money { get; set; }
        /// <summary>
        /// Список продуктов клиента
        /// </summary>
        public List<Product> Products { get; }
        /// <summary>
        /// Вид клиента (Физ лицо, Вип, Юр лицо)
        /// </summary>
        public string ClientType { get; set; }
        /// <summary>
        /// поле, показывающее есть ли деньги на счете
        /// </summary>
        public bool IsMoney { get; set; }
        /// <summary>
        /// Виды клиентов
        /// </summary>
        public static List<string> ClientTypes = new List<string> { "Физ.лицо", "VIP", "Юр.лицо" };
        #endregion
        /// <summary>
        /// Констурктор создания клиента
        /// </summary>
        /// <param name="Name">Имя клиента</param>
        /// <param name="Age">Возраст клиента</param>
        public Client(string Name, int Age, double money, string ClientType)
        {
            Random r = new Random();
            this.Name = Name;
            this.Age = Age;
            this.Money = money;
            this.BankBook = r.Next(100_000, 200_000).ToString();
            this.ClientType = ClientType;
            this.Products = new List<Product> { };
            this.IsMoney = true;
            if (r.Next(1, 10) == 1)
            {
                this.Positivity = "Хороший";
            }
            else
            {
                this.Positivity = "Обычный";
            }
        }
        /// <summary>
        /// Конструктор создания рандомного клиента
        /// </summary>
        public Client()
        {
            Random r = new Random();
            List<string> randomNames = new List<string> { "Вася", "Петя", "Катя", "Дима", "Женя", "Аня" };
            this.Name = randomNames[r.Next(0, 6)];
            this.Age = r.Next(18, 100);
            this.BankBook = r.Next(100_000, 200_000).ToString();
            this.Money = (uint)r.Next(0, 500);
            this.ClientType = ClientTypes[r.Next(0, 3)];
            this.Products = new List<Product> { };
            this.IsMoney = true;
            if (r.Next(1, 10) == 1)
            {
                this.Positivity = "Хороший";
            }
            else
            {
                this.Positivity = "Обычный";
            }
        }
        /// <summary>
        /// Метод добавления продукта
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        {
            this.Products.Add(product);
        }
        /// <summary>
        /// Метод изменения количества денег через месяц
        /// </summary>
        public void InAMounth()
        {
            foreach (Product e in this.Products)
            {
                if (this.Positivity == "Хороший")
                {
                    if (e.GetType().ToString() == "Credit")
                    {
                        e.Procent -= 2;
                    }
                    if (e.GetType().ToString() == "Deposit")
                    {
                        e.Procent += 2;
                    }
                }
                e.DoInAMonth(this);
            }
        }
        /// <summary>
        /// Метод изменения количества денег через год
        /// </summary>
        public void InAYear()
        {
            foreach (Product e in this.Products)
            {
                if (this.Positivity == "Хороший")
                {
                    if (e.GetType().ToString() == "Credit")
                    {
                        e.Procent -= 2;
                    }
                    if (e.GetType().ToString() == "Deposit")
                    {
                        e.Procent += 2;
                    }
                }
                e.DoInAYear(this);
            }
        }
        /// <summary>
        /// Метод пополнения счета
        /// </summary>
        /// <param name="money">Количество пополнения</param>
        public void AddMoney(double money)
        {
            this.Money += money;
        }
        /// <summary>
        /// Метод снятия денег со счета
        /// </summary>
        /// <param name="money">Количество денег</param>
        /// <returns></returns>
        public void TakeMoney(double money)
        {
            if (this.Money >= money)
            {
                this.Money -= money;
                this.IsMoney = true;
            }
            else this.IsMoney = false;
        }
    }
}
