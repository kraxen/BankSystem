using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductClientLibrary
{
    public class Credit : Product
    {
        /// <summary>
        /// Названия кредитов для физ лиц
        /// </summary>
        public static List<string> CreditNamesNormalClients = new List<string> { "Кредит наличными для физ лиц", "Рефинансирование", "Ипотека", "Кредит пенсионерам" };
        /// <summary>
        /// Названия кредитов для VIP
        /// </summary>
        public static List<string> CreditNamesVIPClients = new List<string> { "Кредит наличными для VIP", "Рефинансирование VIP", "Ипотека VIP", "Кредит пенсионерам VIP" };
        /// <summary>
        /// Названия кредитов для юр лиц
        /// </summary>
        public static List<string> CreditNamesBusnesClients = new List<string> { "Кредит наличными для юр лиц", "Рефинансирование Юр лиц", "Кредит на открытие бизнеса" };

        public Credit(string name, int value, int monthCount)
        {
            this.MonthCount = monthCount;
            this.Value = value;
            this.Name = name;
            switch (Name)
            {
                case "Кредит наличными для физ лиц":
                    this.Procent = 0.24;
                    break;
                case "Рефинансирование":
                    this.Procent = 0.15;
                    break;
                case "Ипотека":
                    this.Procent = 0.06;
                    break;
                case "Кредит пенсионерам":
                    this.Procent = 0.12;
                    break;
                case "Кредит наличными для VIP":
                    this.Procent = 0.10;
                    break;
                case "Рефинансирование VIP":
                    this.Procent = 0.05;
                    break;
                case "Ипотека VIP":
                    this.Procent = 0.03;
                    break;
                case "Кредит пенсионерам VIP":
                    this.Procent = 0.05;
                    break;
                case "Кредит наличными для юр лиц":
                    this.Procent = 0.10;
                    break;
                case "Рефинансирование Юр лиц":
                    this.Procent = 0.8;
                    break;
                case "Кредит на открытие бизнеса":
                    this.Procent = 0.12;
                    break;
            }
            CalculetePayment();
        }
        public Credit():base()
        {

        }
        public override void DoInAMonth(Client client)
        {
            client.Money -= this.MonthlyPayment;
            this.MonthCount--;
        }
    }
}
