using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductClientLibrary
{
    public class Product
    {
        /// <summary>
        /// Название продукта
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Процентная ставка по продукту
        /// </summary>
        public double Procent { get; set; }
        /// <summary>
        /// Сумма продукта
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Ежемесячный платеж
        /// </summary>
        public double MonthlyPayment { get; set; }
        /// <summary>
        /// Количество месяцев
        /// </summary>
        public int MonthCount { get; set; }
        public Product()
        {
            
        }

        public void CalculetePayment()
        {
            this.MonthlyPayment = Value * ((this.Procent / 12) * Math.Pow((1 + (this.Procent / 12)), MonthCount)) / (Math.Pow(1 + (this.Procent / 12), MonthCount) - 1);
        }
        virtual public void DoInAMonth(Client client)
        {

        }
        public void DoInAYear(Client client)
        {
            for (int i = 0; i < 12; i++)
            {
                DoInAMonth(client);
            }
        }
    }
}
