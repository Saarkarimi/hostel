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
using System.Windows.Shapes;

namespace hostel
{
    /// <summary>
    /// Логика взаимодействия для editClient.xaml
    /// </summary>
    public partial class editClient : Window
    {
        // Подключаемся к БД
        hostelDBEntities _db = new hostelDBEntities();

        // Переменная ид клиента
        int Id = 0;
        public editClient(int ClientId)
        {
            InitializeComponent();

            Id = ClientId;
        }

        // Сохраняем гостя
        private void editClientBtn_Click(object sender, RoutedEventArgs e)
        {
            // Записываем в БД
            Client updateClient = (from c in _db.Clients where c.id == Id select c).Single();
            updateClient.family = fieldFamily.Text.Trim();
            updateClient.name = fieldName.Text.Trim();
            updateClient.patronymic = fieldPatronymic.Text.Trim();
            updateClient.phone = fieldPhone.Text.Trim();
            _db.SaveChanges();

            // Обновляем таблицу и закрываем форму
            MainWindow.listClients.ItemsSource = _db.Clients.ToList();
            this.Close();
        }
    }
}
