using ScheduleApp.Models;
using System.Data.SQLite;

namespace ScheduleApp.DatabaseAccess;

public class RoomDataAccess
{
    /// <summary>
    /// Получает список всех аудиторий из базы данных.
    /// </summary>
    /// <returns>Список аудиторий.</returns>
    public List<Room> GetAllRooms()
    {
        List<Room> rooms = new List<Room>();
        // Запрос SQL для получения всех аудиторий.
        string query = "SELECT Id, RoomNumber, Capacity FROM Rooms;";

        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                // Чтение результатов запроса и создание объектов Room.
                while (reader.Read())
                {
                    rooms.Add(new Room
                    {
                        Id = reader.GetInt32(0),
                        RoomNumber = reader.GetString(1),
                        Capacity = reader.GetString(2)
                    });
                }
            }
        }

        return rooms;
    }

    /// <summary>
    /// Получает аудиторию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор аудитории.</param>
    /// <returns>Аудитория или null, если аудитория не найдена.</returns>
    public Room GetRoomById(int id)
    {
        Room room = null;
        // Запрос SQL для получения аудитории по идентификатору.
        string query = "SELECT Id, RoomNumber, Capacity FROM Rooms WHERE Id = @Id;";

        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            using (var command = new SQLiteCommand(query, connection))
            {
                // Установка параметра запроса для идентификатора аудитории.
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    // Чтение результата запроса и создание объекта Room.
                    if (reader.Read())
                    {
                        room = new Room
                        {
                            Id = reader.GetInt32(0),
                            RoomNumber = reader.GetString(1),
                            Capacity = reader.GetString(2)
                        };
                    }
                }
            }
        }

        return room;
    }

    /// <summary>
    /// Добавляет новую аудиторию в базу данных.
    /// </summary>
    /// <param name="room">Объект аудитории для добавления.</param>
    public void AddRoom(Room room)
    {
        // Запрос SQL для вставки новой аудитории в базу данных.
        string query = "INSERT INTO Rooms (RoomNumber, Capacity) VALUES (@RoomNumber, @Capacity);";

        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            using (var command = new SQLiteCommand(query, connection))
            {
                // Установка параметров запроса для значений аудитории.
                command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                command.Parameters.AddWithValue("@Capacity", room.Capacity);
                // Выполнение запроса.
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Обновляет информацию об аудитории в базе данных.
    /// </summary>
    /// <param name="room">Объект аудитории с обновленными данными.</param>
    public void UpdateRoom(Room room)
    {
        // Запрос SQL для обновления информации об аудитории.
        string query = "UPDATE Rooms SET RoomNumber = @RoomNumber, Capacity = @Capacity WHERE Id = @Id;";

        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            using (var command = new SQLiteCommand(query, connection))
            {
                // Установка параметров запроса для значений аудитории.
                command.Parameters.AddWithValue("@Id", room.Id);
                command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                command.Parameters.AddWithValue("@Capacity", room.Capacity);
                // Выполнение запроса.
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Удаляет аудиторию из базы данных по заданному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой аудитории.</param>
    public void DeleteRoom(int id)
    {
        // SQL запрос для удаления аудитории по идентификатору.
        string query = "DELETE FROM Rooms WHERE Id = @Id;";

        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            using (var command = new SQLiteCommand(query, connection))
            {
                // Установка параметра запроса для идентификации аудитории.
                command.Parameters.AddWithValue("@Id", id);
                // Выполнение SQL команды, которая удаляет аудиторию.
                command.ExecuteNonQuery();
            }
        }
    }
}
