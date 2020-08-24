using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductClientLibrary
{
    public class Deposit : Product
    {
        public static List<string> NormalClientDepositNames = new List<string> { "Простой вклад для физических лиц", "Вклад для пенсионеров" };
        public static List<string> VIPClientDepositNames = new List<string> { "Простой вклад для VIP", "Вклад для пенсионеров VIP" };
        public static List<string> BusnesClientDepositNames = new List<string> { "Простой вклад для бизнеса" };
        public Deposit(string Name, int value, int monthCount)
        {
            this.Value = value;
            this.MonthCount = monthCount;
            this.Name = Name;
            switch (Name)
            {
                case "Простой вклад для физических лиц":
                    this.Procent = 0.06;
                    break;
                case "Вклад для пенсионеров":
                    this.Procent = 0.08;
                    break;
                case "Простой вклад для VIP":
                    this.Procent = 0.08;
                    break;
                case "Вклад для пенсионеров VIP":
                    this.Procent = 0.12;
                    break;
                case "Простой вклад для бизнеса":
                    this.Procent = 0.10;
                    break;
            }
            CalculetePayment();
        }
        public Deposit():base()
        {

        }
        public override void DoInAMonth(Client client)
        {
            client.Money += this.MonthlyPayment;
            this.MonthCount--;
        }
    }
}
