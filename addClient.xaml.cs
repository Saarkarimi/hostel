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
    /// Логика взаимодействия для addClient.xaml
    /// </summary>
    public partial class addClient : Window
    {
        // Подключаемся к БД
        hostelDBEntities _db = new hostelDBEntities();
        public addClient()
        {
            InitializeComponent();
        }

        // Сохраняем гостя
        private void addClientBtn_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из формы
            Client newClient = new Client()
            {
                family = fieldFamily.Text.Trim(),
                name = fieldName.Text.Trim(),
                patronymic = fieldPatronymic.Text.Trim(),
                phone = fieldPhone.Text.Trim()
            };

            // Добавлям в БД и обновляем список гостей
            _db.Clients.Add(newClient);
            _db.SaveChanges();
            MainWindow.listClients.ItemsSource = _db.Clients.ToList();
            this.Close();
        }
    }
}
