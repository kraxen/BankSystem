using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seriolization;

namespace LogsLibrary
{
    public class Logs
    {
        //Строка лога
        List<string> Log { get; set; }
        public Logs()
        {
            this.Log = new List<string> { };
        }
        public Logs(string Path)
        {
            this.Log = ToJSON.DeserializeToJson<List<string>>(Path);
        }
        /// <summary>
        /// Добавление лога
        /// </summary>
        /// <param name="BankBookIn">Счет отправителя</param>
        /// <param name="BankBookOut">Счет получателя</param>
        /// <param name="value">Сумма перевода</param>
        /// <param name="IsGood">Успешен ли перевод</param>
        public void AddLog(string BankBookIn, string BankBookOut, string value, bool IsGood)
        {
            if (IsGood)
            {
                this.Log.Add($"Перевод со счета {BankBookIn} на счет {BankBookOut} на сумму {value} выплнен.");
            }
            else
            {
                this.Log.Add($"Неудачная попытка перевода со счета {BankBookIn} на счет {BankBookOut} на сумму {value}.");
            }
            ToJSON.SerializeToJsonList(this.Log, "Logs.json");

        }
        /// <summary>
        /// Получение логов
        /// </summary>
        /// <returns>Строка с логами</returns>
        public string GetLogs()
        {
            string temp = "";
            foreach (string e in Log)
            {
                temp += e + "\n";
            }
            return temp;
        }
        public string GetLogs(string Path)
        {
            string temp = "";
            this.Log = ToJSON.DeserializeToJson<List<string>>(Path);;
            foreach (string e in Log)
            {
                temp += e + "\n";
            }
            return temp;
        }
    }
}
