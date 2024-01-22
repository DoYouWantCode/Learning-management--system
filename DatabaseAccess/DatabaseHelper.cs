using System.Data.SQLite;

namespace ScheduleApp.DatabaseAccess;
/// <summary>
/// Класс помощник, предоставляющий статические методы для взаимодействия с базой данных SQLite.
/// </summary>
public static class DatabaseHelper
{
    // Строка подключения к базе данных SQLite.
    private const string ConnectionString = "Data Source=schedule.db";

    /// <summary>
    /// Создает и возвращает новое подключение к базе данных SQLite, используя предопределенную строку подключения.
    /// </summary>
    /// <returns>Новый экземпляр SQLiteConnection.</returns>
    public static SQLiteConnection GetConnection()
    {
        return new SQLiteConnection(ConnectionString);
    }

    /// <summary>
    /// Инициализирует базу данных, создавая необходимые таблицы, если они еще не существуют.
    /// </summary>
    public static void InitializeDatabase()
    {
        using (var connection = GetConnection())
        {
            connection.Open();

            // SQL запрос для создания таблицы "Предметы" (Subjects), если она не существует.
            string createSubjectTableQuery =
                @"CREATE TABLE IF NOT EXISTS Subjects (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT
                );";

            // SQL запрос для создания таблицы "Преподаватели" (Teachers), если она не существует.
            string createTeacherTableQuery =
                @"CREATE TABLE IF NOT EXISTS Teachers (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FirstName TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    Subject TEXT
                );";

            // SQL запрос для создания таблицы "Аудитории" (Rooms), если она не существует.
            string createRoomTableQuery =
                @"CREATE TABLE IF NOT EXISTS Rooms (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    RoomNumber TEXT NOT NULL,
                    Capacity TEXT
                );";

            // SQL запрос для создания таблицы "Расписания" (Schedules), если она не существует.
            string createClassTableQuery =
                @"CREATE TABLE IF NOT EXISTS Schedules (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    SubjectId INTEGER NOT NULL,
                    TeacherId INTEGER NOT NULL,
                    RoomId INTEGER NOT NULL,
                    StartTime TEXT NOT NULL,
                    EndTime TEXT NOT NULL,
                    DayOfWeek INTEGER NOT NULL,
                    FOREIGN KEY (SubjectId) REFERENCES Subjects (Id),
                    FOREIGN KEY (TeacherId) REFERENCES Teachers (Id),
                    FOREIGN KEY (RoomId) REFERENCES Rooms (Id)
                );";

            // Выполнение запросов на создание таблиц.
            ExecuteNonQuery(createSubjectTableQuery);
            ExecuteNonQuery(createTeacherTableQuery);
            ExecuteNonQuery(createRoomTableQuery);
            ExecuteNonQuery(createClassTableQuery);
        }
    }

    /// <summary>
    /// Выполняет SQL запрос без возвращения данных.
    /// </summary>
    /// <param name="query">Строка SQL запроса для выполнения.</param>
    public static void ExecuteNonQuery(string query)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Выполняет SQL запрос без возвращения данных с возможностью передачи параметров.
    /// </summary>
    /// <param name="query">Строка SQL запроса для выполнения.</param>
    /// <param name="parameters">Необязательный словарь параметров запроса.</param>
    public static void ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
    {
        using (var connection = GetConnection())
        {
            connection.Open();

            using (var command = new SQLiteCommand(query, connection))
            {
                // Если предоставлены параметры, добавляем их к команде.
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Выполняет SQL запрос и возвращает SQLiteDataReader для чтения данных.
    /// </summary>
    /// <param name="query">Строка SQL запроса для выполнения.</param>
    /// <param name="parameters">Необязательный словарь параметров запроса.</param>
    /// <returns>SQLiteDataReader для чтения результатов запроса.</returns>
    public static SQLiteDataReader ExecuteQuery(string query, Dictionary<string, object> parameters = null)
    {
        var connection = GetConnection();
        connection.Open();

        using (var command = new SQLiteCommand(query, connection))
        {
            // Если предоставлены параметры, добавляем их к команде.
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
            }

            // Выполнение запроса и возврат объекта-читателя для доступа к данным.
            return command.ExecuteReader();
        }
    }
}