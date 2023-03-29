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
    /// Логика взаимодействия для addRoom.xaml
    /// </summary>
    public partial class addRoom : Window
    {
        // Подключаемся к БД
        hostelDBEntities _db = new hostelDBEntities();
        public addRoom()
        {
            InitializeComponent();
        }

        // Сохраняем комнату
        private void addRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из формы
            Room newRoom = new Room()
            {
                number = Convert.ToInt32(fieldNumber.Text.Trim()),
                size = Convert.ToInt32(fieldSize.Text.Trim()),
                type = fieldType.Text.Trim(),
                price = Convert.ToInt32(fieldPrice.Text.Trim()),
                status = fieldStatus.Text.Trim()
            };

            // Добавлям в БД и обновляем список гостей
            _db.Rooms.Add(newRoom);
            _db.SaveChanges();
            MainWindow.listRooms.ItemsSource = _db.Rooms.ToList();
            this.Close();
        }
    }
}
