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
    /// Логика взаимодействия для addBooking.xaml
    /// </summary>
    public partial class addBooking : Window
    {
        // Подключаемся к БД
        hostelDBEntities _db = new hostelDBEntities();
        public addBooking()
        {
            InitializeComponent();

            // Добавляем в ComboBox список клиентов
            var clientsList = _db.Clients.ToList();

            // Добавляем элементы
            foreach (var client in clientsList)
            {
                comboboxClient.Items.Add(new ComboBoxItem
                {
                    Content = client.family + " " + client.name + " " + client.patronymic
                });
            }

            // Добавляем в ComboBox список комнат
            var roomsList = _db.Rooms.ToList();

            // Добавляем элементы
            foreach (var room in roomsList)
            {
                comboboxRoom.Items.Add(new ComboBoxItem
                {
                    Content = room.number
                });
            }
        }

        // Сохраняем бронь
        private void addBookingBtn_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из формы
            Reservation newReservation = new Reservation()
            {
                client = comboboxClient.Text.Trim(),
                room = comboboxRoom.Text.Trim(),
                date_start = Convert.ToString(dateStart.Text.Trim()),
                date_end = Convert.ToString(dateEnd.Text.Trim()),
                price = Convert.ToInt32(fieldPrice.Text.Trim())

            };

            // Добавлям в БД и обновляем список гостей
            _db.Reservations.Add(newReservation);
            _db.SaveChanges();
            MainWindow.listReservations.ItemsSource = _db.Reservations.ToList();
            this.Close();
        }
    }
}
