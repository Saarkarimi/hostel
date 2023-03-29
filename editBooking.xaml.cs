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
    /// Логика взаимодействия для editBooking.xaml
    /// </summary>
    public partial class editBooking : Window
    {
        // Подключаемся к БД
        hostelDBEntities _db = new hostelDBEntities();

        // Переменная ид брони
        int Id = 0;
        public editBooking(int BookingId)
        {
            InitializeComponent();

            Id = BookingId;
        }

        // Сохраняем бронь
        private void editBookingBtn_Click(object sender, RoutedEventArgs e)
        {
            // Записываем в БД
            Reservation updateReservation = (from r in _db.Reservations where r.id == Id select r).Single();
            updateReservation.client = comboboxClient.Text.Trim();
            updateReservation.room = comboboxRoom.Text.Trim();
            updateReservation.date_start = Convert.ToString(dateStart.Text.Trim());
            updateReservation.date_end = Convert.ToString(dateEnd.Text.Trim());
            updateReservation.price = Convert.ToInt32(fieldPrice.Text.Trim());
            _db.SaveChanges();

            // Обновляем таблицу и закрываем форму
            MainWindow.listReservations.ItemsSource = _db.Clients.ToList();
            this.Close();
        }
    }
}
