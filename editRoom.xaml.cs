using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для editRoom.xaml
    /// </summary>
    public partial class editRoom : Window
    {
        // Подключаемся к БД
        hostelDBEntities _db = new hostelDBEntities();

        // Переменная ид комнаты
        int Id = 0;
        public editRoom(int RoomId)
        {
            InitializeComponent();

            Id = RoomId;
        }

        // Сохраняем комнату
        private void editRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            // Записываем в БД
            Room updateRoom = (from r in _db.Rooms where r.id == Id select r).Single();
            updateRoom.number = Convert.ToInt32(fieldNumber.Text.Trim());
            updateRoom.size = Convert.ToInt32(fieldSize.Text.Trim());
            updateRoom.type = fieldType.Text.Trim();
            updateRoom.price = Convert.ToInt32(fieldPrice.Text.Trim());
            updateRoom.status = fieldStatus.Text.Trim();
            _db.SaveChanges();

            // Обновляем таблицу и закрываем форму
            MainWindow.listRooms.ItemsSource = _db.Rooms.ToList();
            this.Close();
        }
    }
}
