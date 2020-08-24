using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductClientLibrary;
using Seriolization;

namespace ClientRepositoryLibrary
{
    public class ClientRepository
    {
        /// <summary>
        /// Репозиторий клиентов
        /// </summary>
        public List<Client> clients;
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ClientRepository()
        {
            this.clients = new List<Client> { };
        }
        /// <summary>
        /// Конструктор создания репозитория из файла
        /// </summary>
        /// <param name="Path">Имя файла</param>
        public ClientRepository(string Path)
        {
            this.clients = ToJSON.DeserializeToJson<List<Client>>(Path);
        }
        /// <summary>
        /// Метод добавления клиента
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            Random r = new Random();
            while (true)
            {
                if (clients.All(e => e.BankBook != client.BankBook))
                {
                    clients.Add(client);
                    break;
                }
                else
                {
                    client.BankBook = r.Next(100_000, 200_000).ToString();
                }
            }
        }
        public void SaveRepository()
        {
            string Path = "Repository.json";
            lock (this.clients)
            {
                ToJSON.SerializeToJsonList(this.clients, Path);
            }
    }
        public void SaveReositoryAsinc()
        {
            var a = new Action(SaveRepository);
            Task task = new Task(a);
            task.Start();
        }

    }
}
