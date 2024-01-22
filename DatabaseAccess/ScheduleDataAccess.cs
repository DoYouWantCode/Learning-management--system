using ScheduleApp.Models;
using System.Data.SQLite;

namespace ScheduleApp.DatabaseAccess;
public class ScheduleDataAccess
{
    // Получает все расписания из базы данных.
    public List<Schedule> GetAllSchedules()
    {
        // Создаем список для хранения результатов.
        List<Schedule> schedules = new List<Schedule>();
        // Запрос на выборку всех записей из таблицы Schedules.
        string query = "SELECT * FROM Schedules;";

        // Получаем соединение с базой данных и открываем его.
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            // Создаем команду для выполнения запроса с использованием соединения.
            using (var command = new SQLiteCommand(query, connection))
            // Получаем данные, выполняя запрос и используя для этого reader.
            using (var reader = command.ExecuteReader())
            {
                // Читаем построчно данные, полученные от базы данных.
                while (reader.Read())
                {
                    // Добавляем в список новый объект Schedule с данными из текущей строки.
                    schedules.Add(new Schedule
                    {
                        Id = reader.GetInt32(0),
                        SubjectId = reader.GetInt32(1),
                        TeacherId = reader.GetInt32(2),
                        RoomId = reader.GetInt32(3),
                        StartTime = reader.GetDateTime(4),
                        EndTime = reader.GetDateTime(5),
                        DayOfWeek = (DayOfWeek)reader.GetInt32(6)
                    });
                }
            }
        }

        // Возвращаем список расписаний.
        return schedules;
    }

    // Добавляет новое расписание в базу данных.
    public void AddSchedule(Schedule schedule)
    {
        // Запрос на вставку новой записи в таблицу Schedules.
        string query = "INSERT INTO Schedules (SubjectId, TeacherId, RoomId, StartTime, EndTime, DayOfWeek) " +
                       "VALUES (@SubjectId, @TeacherId, @RoomId, @StartTime, @EndTime, @DayOfWeek);";

        // Получаем соединение с базой данных и открываем его.
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            // Создаем команду для выполнения запроса с использованием соединения.
            using (var command = new SQLiteCommand(query, connection))
            {
                // Заполняем параметры запроса значениями из объекта schedule.
                command.Parameters.AddWithValue("@SubjectId", schedule.SubjectId);
                command.Parameters.AddWithValue("@TeacherId", schedule.TeacherId);
                command.Parameters.AddWithValue("@RoomId", schedule.RoomId);
                command.Parameters.AddWithValue("@StartTime", schedule.StartTime);
                command.Parameters.AddWithValue("@EndTime", schedule.EndTime);
                command.Parameters.AddWithValue("@DayOfWeek", schedule.DayOfWeek);
                // Выполняем запрос, вставляя запись в базу данных.
                command.ExecuteNonQuery();
            }
        }
    }

    // Обновляет существующее расписание в базе данных.
    public void UpdateSchedule(Schedule schedule)
    {
        // Запрос на обновление записи в таблице Schedules.
        string query = "UPDATE Schedules SET SubjectId = @SubjectId, TeacherId = @TeacherId, " +
                       "RoomId = @RoomId, StartTime = @StartTime, EndTime = @EndTime, DayOfWeek = @DayOfWeek " +
                       "WHERE Id = @Id;";

        // Получаем соединение с базой данных и открываем его.
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            // Создаем команду для выполнения запроса с использованием соединения.
            using (var command = new SQLiteCommand(query, connection))
            {
                // Заполняем параметры запроса значениями из объекта schedule.
                command.Parameters.AddWithValue("@SubjectId", schedule.SubjectId);
                command.Parameters.AddWithValue("@TeacherId", schedule.TeacherId);
                command.Parameters.AddWithValue("@RoomId", schedule.RoomId);
                command.Parameters.AddWithValue("@StartTime", schedule.StartTime);
                command.Parameters.AddWithValue("@EndTime", schedule.EndTime);
                command.Parameters.AddWithValue("@DayOfWeek", schedule.DayOfWeek);
                command.Parameters.AddWithValue("@Id", schedule.Id);
                // Выполняем запрос, обновляя запись в базе данных.
                command.ExecuteNonQuery();
            }
        }
    }

    // Удаляет расписание из базы данных по идентификатору.
    public void DeleteSchedule(int scheduleId)
    {
        // Запрос на удаление записи из таблицы Schedules.
        string query = "DELETE FROM Schedules WHERE Id = @Id;";

        // Получаем соединение с базой данных и открываем его.
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", scheduleId);
                command.ExecuteNonQuery();
            }
        }
    }

    // Возвращает расписание по идентификатору.
    public Schedule GetScheduleById(int scheduleId)
    {
        // Инициализируем объект Schedule.
        Schedule schedule = null;
        string query = "SELECT Id, SubjectId, TeacherId, RoomId, StartTime, EndTime, DayOfWeek FROM Schedules WHERE Id = @Id;";

        // Получаем соединение с базой данных и открываем его.
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            // Создаем команду для выполнения запроса с использованием соединения.
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", scheduleId);

                // Выполняем запрос и получаем данные.
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        schedule = new Schedule
                        {
                            Id = reader.GetInt32(0),
                            SubjectId = reader.GetInt32(1),
                            TeacherId = reader.GetInt32(2),
                            RoomId = reader.GetInt32(3),
                            StartTime = reader.GetDateTime(4),
                            EndTime = reader.GetDateTime(5),
                        DayOfWeek = (DayOfWeek)reader.GetInt32(6)
                        };
                    }
                }
            }
        }

        return schedule;
    }
}