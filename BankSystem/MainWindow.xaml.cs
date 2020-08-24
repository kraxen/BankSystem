using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogsLibrary;
using ProductClientLibrary;
using ClientRepositoryLibrary;
using ExeptionLibrary;
using System.Threading;

namespace BankSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClientRepository rep = new ClientRepository("Repository.json");
        Logs logs = new Logs("Logs.json");
        
        public MainWindow()
        {
            InitializeComponent();
            cbClientTypes.ItemsSource = Client.ClientTypes;
            cmClientTypeChange.ItemsSource = Client.ClientTypes;
            lvClients.ItemsSource = rep.clients;
        }
        /// <summary>
        /// Добавление клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                try
                {
                    Int32.Parse(tbAgeAdd.Text);
                }
                catch
                {
                    MessageBox.Show("Возвраст введен некорректно");
                    tbAgeAdd.Text = "";
                }
                try
                {
                    UInt32.Parse(tbMoneyAdd.Text);
                }
                catch
                {
                    MessageBox.Show("Количество денег введено некорректно");
                    tbMoneyAdd.Text = "";
                }
                try
                {
                    cbClientTypes.SelectedItem.ToString();
                }
                catch
                {
                    MessageBox.Show("Не выбран тип сотрудника");
                }
                rep.AddClient(new Client(tbNameAdd.Text, Int32.Parse(tbAgeAdd.Text), UInt32.Parse(tbMoneyAdd.Text), cbClientTypes.SelectedItem.ToString()));
                lvClients.Items.Refresh();
                rep.SaveReositoryAsinc();
            }
            catch { }
        }
        /// <summary>
        /// Изменение данных клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    Int32.Parse(tbAgeChange.Text);
                }
                catch
                {
                    MessageBox.Show("Возвраст введен некорректно");
                    tbAgeAdd.Text = "";
                }
                try
                {
                    UInt32.Parse(tbMoneyChange.Text);
                }
                catch
                {
                    MessageBox.Show("Количество денег введено некорректно");
                    tbMoneyAdd.Text = "";
                }
                //Изменение имени
                rep.clients.Find(c => c == (lvClients.SelectedItem as Client)).Name = tbNameChange.Text;
                //Изменение возраста
                rep.clients.Find(c => c == (lvClients.SelectedItem as Client)).Age = Int32.Parse(tbAgeChange.Text);
                //Изменение денег
                rep.clients.Find(c => c == (lvClients.SelectedItem as Client)).Money = UInt32.Parse(tbMoneyChange.Text);
                //Изменение типа клиента
                rep.clients.Find(c => c == (lvClients.SelectedItem as Client)).ClientType = cmClientTypeChange.SelectedItem.ToString();
                (lvClients.SelectedItem as Client).IsMoney = true;
                lvClients.Items.Refresh();
                rep.SaveReositoryAsinc();
            }
            catch{}
        }
        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rep.clients.Remove(rep.clients.Find(c => c == (lvClients.SelectedItem as Client)));
                rep.SaveReositoryAsinc();
                lvClients.Items.Refresh();
            }
            catch { }
        }

        private void lvClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Выделение нужного поля в типе изменяемого клиента
            cmClientTypeChange.Text = (lvClients.SelectedItem as Client).ClientType;
            switch (cmClientTypeChange.Text)
            {
                case "Физ.лицо":
                    CreditNames.ItemsSource = Credit.CreditNamesNormalClients;
                    DepositNames.ItemsSource = Deposit.NormalClientDepositNames;
                    break;
                case "VIP":
                    CreditNames.ItemsSource = Credit.CreditNamesVIPClients;
                    DepositNames.ItemsSource = Deposit.VIPClientDepositNames;
                    break;
                case "Юр.лицо":
                    CreditNames.ItemsSource = Credit.CreditNamesBusnesClients;
                    DepositNames.ItemsSource = Deposit.BusnesClientDepositNames;
                    break;
            }
            ClientProducts.ItemsSource = (lvClients.SelectedItem as Client).Products;
        }

        private void btnAddCredit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    Int32.Parse(tbCreditValue.Text);
                }
                catch
                {
                    MessageBox.Show("Введена некорректная сумма кредита");
                }
                try
                {
                    Int32.Parse(tbCreditMonth.Text);
                }
                catch
                {
                    MessageBox.Show("Введено некорректное количество месяцев");
                }
                try
                {
                    if (CreditNames.SelectedItem == null) throw new ArgumentNullException();
                    (lvClients.SelectedItem as Client).AddProduct(new Credit(CreditNames.Text, Int32.Parse(tbCreditValue.Text), Int32.Parse(tbCreditMonth.Text)));
                    ClientProducts.Items.Refresh();
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Не выбран тип продукта");
                }
            }
            catch { }
            rep.SaveReositoryAsinc();
        }
        private void btnAddDeposit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    Int32.Parse(tbDepositValue.Text);
                }
                catch
                {
                    MessageBox.Show("Введена некорректная сумма депозита");
                }
                try
                {
                    Int32.Parse(tbDepositMonth.Text);
                }
                catch
                {
                    MessageBox.Show("Введено некорректное количество месяцев");
                }
                try
                {
                    if (DepositNames.SelectedItem == null) throw new ArgumentNullException();
                    (lvClients.SelectedItem as Client).AddProduct(new Deposit(DepositNames.Text, Int32.Parse(tbDepositValue.Text), Int32.Parse(tbDepositMonth.Text)));
                    ClientProducts.Items.Refresh();
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Не выбран тип продукта");
                }
            }
            catch { }
            rep.SaveReositoryAsinc();
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientProducts.SelectedItem == null) throw new ArgumentNullException();
                (lvClients.SelectedItem as Client).Products.Remove((ClientProducts.SelectedItem as Product));
                ClientProducts.Items.Refresh();
                rep.SaveReositoryAsinc();
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Не выбран продукт");
            }
        }

        private void btnMunth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Product p in (lvClients.SelectedItem as Client).Products)
                {
                    if (p.MonthCount == 1)
                    {
                        (lvClients.SelectedItem as Client).InAMounth();
                        (lvClients.SelectedItem as Client).Products.Remove(p);
                        ClientProducts.Items.Refresh();
                        lvClients.Items.Refresh();
                    }
                    else if (p.MonthCount <= 0)
                    {
                        (lvClients.SelectedItem as Client).Products.Remove(p);
                        ClientProducts.Items.Refresh();
                        lvClients.Items.Refresh();
                    }
                    else
                    {
                        (lvClients.SelectedItem as Client).InAMounth();
                        lvClients.Items.Refresh();
                        ClientProducts.Items.Refresh();
                        break;
                    }
                }
            }
            catch { }
            rep.SaveReositoryAsinc();
            ClientProducts.Items.Refresh();
        }

        private void btnYear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Product p in (lvClients.SelectedItem as Client).Products)
                {
                    if (p.MonthCount <= 0)
                    {
                        (lvClients.SelectedItem as Client).Products.Remove(p);
                        ClientProducts.Items.Refresh();
                    }
                    else
                    {
                        (lvClients.SelectedItem as Client).InAYear();
                        lvClients.Items.Refresh();
                        ClientProducts.Items.Refresh();
                    }
                }
                lvClients.Items.Refresh();
            }
            catch { }
            rep.SaveReositoryAsinc();
            ClientProducts.Items.Refresh();
        }

        private void btnAddMoney_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (lvClients.SelectedItem as Client).AddMoney(Int32.Parse(AddMoney.Text));
                lvClients.Items.Refresh();
                (lvClients.SelectedItem as Client).IsMoney = true;
                tbMoneyChange.Text = (lvClients.SelectedItem as Client).Money.ToString();
                rep.SaveReositoryAsinc();
            }
            catch 
            {
                MessageBox.Show("Сумма введена некорректно или не выбран клиент");
            }
        }

        private void btnTakeMoney_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    if (!(lvClients.SelectedItem as Client).IsMoney) throw new NoMoneyExeption();
                    (lvClients.SelectedItem as Client).TakeMoney(Int32.Parse(TakeMoney.Text));
                    tbMoneyChange.Text = (lvClients.SelectedItem as Client).Money.ToString();
                    rep.SaveReositoryAsinc();
                }
                catch (NoMoneyExeption)
                {
                    MessageBox.Show("Недостаточно средств");
                }
                lvClients.Items.Refresh();
                rep.SaveReositoryAsinc();
            }
            catch
            {
                MessageBox.Show("Сумма введена некорректно или не выбран клиент");
            }
        }

        private void btnTrans_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Double.Parse(tbTransValue.Text);
            }
            catch
            {
                MessageBox.Show("Сумма введена некорректно");
            }
            if (rep.clients.Find(c => c.BankBook == tbBankBookOut.Text) == null)
            {
                MessageBox.Show("Некорректный номер счета получателя");
            } else
            if (rep.clients.Find(c => c.BankBook == tbBankBookIn.Text) == null)
            {
                MessageBox.Show("Некорректный номер счета отправителя");
            } else
            if ((rep.clients.Find(c => c.BankBook == tbBankBookIn.Text).Money - Double.Parse(tbTransValue.Text)) < 0)
            {
                MessageBox.Show("Операция невозможна. Недостаточно средств");
                logs.AddLog(tbBankBookIn.Text, tbBankBookOut.Text, tbTransValue.Text, false);
            } else
            {
                //Осуществление перевода
                rep.clients.Find(c => c.BankBook == tbBankBookIn.Text).Money -= Double.Parse(tbTransValue.Text);
                rep.clients.Find(c => c.BankBook == tbBankBookOut.Text).Money += Double.Parse(tbTransValue.Text);
                MessageBox.Show($"Перевод со счета {tbBankBookIn.Text} на счет {tbBankBookOut.Text} на сумму {tbTransValue.Text} выполенен успешно.");
                //Обновление списка
                if((lvClients.SelectedItem as Client) != null)
                {
                    tbMoneyChange.Text = (lvClients.SelectedItem as Client).Money.ToString();
                    lvClients.Items.Refresh();
                }
                //Запись лога о переводе
                logs.AddLog(tbBankBookIn.Text, tbBankBookOut.Text, tbTransValue.Text, true);
                rep.SaveReositoryAsinc();
            }
            
        }

        private void btnGetLogs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(logs.GetLogs());
        }
    }
}
