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

namespace hostel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Подключаемся к БД
        hostelDBEntities _db = new hostelDBEntities();

        // Создаем переменые таблиц клиентов, услуг
        public static DataGrid listClients;
        public static DataGrid listRooms;
        public static DataGrid listReservations;
        public MainWindow()
        {
            InitializeComponent();

            // Заполняем таблицы данными из БД
            Load();
        }

        // Функция формирует DataGrid данными на основе значений из БД
        public void Load()
        {
            myClientsGrid.ItemsSource = _db.Clients.ToList();
            listClients = myClientsGrid;

            myRoomsGrid.ItemsSource = _db.Rooms.ToList();
            listRooms = myRoomsGrid;

            myReservationsGrid.ItemsSource = _db.Reservations.ToList();
            listReservations = myReservationsGrid;
        }

        // По клику показываем диалог добавления клиента
        private void addClientBtn_Click(object sender, RoutedEventArgs e)
        {
            addClient addClientPage = new addClient();
            addClientPage.ShowDialog();
        }

        // По клику показываем диалог добавления комнаты
        private void addRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            addRoom addRoomPage = new addRoom();
            addRoomPage.ShowDialog();
        }

        // По клику показываем диалог добавления брони
        private void addBookingBtn_Click(object sender, RoutedEventArgs e)
        {
            addBooking addBookingPage = new addBooking();
            addBookingPage.ShowDialog();
        }

        // По клику показываем диалог редактирования клиента
        private void editClientBtn_Click(Object sender, RoutedEventArgs e)
        {
            // Получаем ид выбранной строки
            int Id = (myClientsGrid.SelectedItem as Client).id;

            // Заполняем окно значениями из БД
            editClient editClientPage = new editClient(Id);
            editClientPage.fieldFamily.Text = (myClientsGrid.SelectedItem as Client).family;
            editClientPage.fieldName.Text = (myClientsGrid.SelectedItem as Client).name;
            editClientPage.fieldPatronymic.Text = (myClientsGrid.SelectedItem as Client).patronymic;
            editClientPage.fieldPhone.Text = (myClientsGrid.SelectedItem as Client).phone;

            editClientPage.ShowDialog();
        }

        // По клику показываем диалог редактирования комнаты
        private void editRoomBtn_Click(Object sender, RoutedEventArgs e)
        {
            // Получаем ид выбранной строки
            int Id = (myRoomsGrid.SelectedItem as Room).id;

            // Заполняем окно значениями из БД
            editRoom editRoomPage = new editRoom(Id);
            editRoomPage.fieldNumber.Text = Convert.ToString((myRoomsGrid.SelectedItem as Room).number);
            editRoomPage.fieldSize.Text = Convert.ToString((myRoomsGrid.SelectedItem as Room).size);
            editRoomPage.fieldType.Text = (myRoomsGrid.SelectedItem as Room).type;
            editRoomPage.fieldPrice.Text = Convert.ToString((myRoomsGrid.SelectedItem as Room).price);
            editRoomPage.fieldStatus.Text = (myRoomsGrid.SelectedItem as Room).status;

            editRoomPage.ShowDialog();
        }

        // По клику показываем диалог редактирования бронирования
        private void editBookingBtn_Click(Object sender, RoutedEventArgs e)
        {
            // Получаем ид выбранной строки
            int Id = (myReservationsGrid.SelectedItem as Reservation).id;

            // Заполняем окно значениями из БД
            editBooking editBookingPage = new editBooking(Id);

            editBookingPage.ShowDialog();
        }

        // По клику удаляем клиента
        private void deleteClientBtn_Click(System.Object sender, RoutedEventArgs e)
        {
            int Id = (listClients.SelectedItem as Client).id;
            var deleteClient = _db.Clients.Where(c => c.id == Id).Single();
            _db.Clients.Remove(deleteClient);
            _db.SaveChanges();
            listClients.ItemsSource = _db.Clients.ToList();
        }

        // По клику удаляем комнату
        private void deleteRoomBtn_Click(System.Object sender, RoutedEventArgs e)
        {
            int Id = (listRooms.SelectedItem as Room).id;
            var deleteRoom = _db.Rooms.Where(r => r.id == Id).Single();
            _db.Rooms.Remove(deleteRoom);
            _db.SaveChanges();
            listRooms.ItemsSource = _db.Rooms.ToList();
        }

        // По клику удаляем бронирование
        private void deleteBookingBtn_Click(System.Object sender, RoutedEventArgs e)
        {
            int Id = (listReservations.SelectedItem as Reservation).id;
            var deleteReservation = _db.Reservations.Where(b => b.id == Id).Single();
            _db.Reservations.Remove(deleteReservation);
            _db.SaveChanges();
            listReservations.ItemsSource = _db.Reservations.ToList();
        }
    }
}
