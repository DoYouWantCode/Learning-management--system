using ScheduleApp.Models;

namespace ScheduleApp.DatabaseAccess;

public class SubjectDataAccess
{
    // Получает список всех предметов из базы данных.
    public List<Subject> GetAllSubjects()
    {
        // Создаем список для хранения предметов.
        List<Subject> subjects = new List<Subject>();
        // Запрос SQL для выбора всех записей из таблицы Subjects.
        string query = "SELECT * FROM Subjects;";

        // Выполнение запроса и обработка результата.
        using (var reader = DatabaseHelper.ExecuteQuery(query))
        {
            // Построчное чтение результатов запроса.
            while (reader.Read())
            {
                // Добавление предмета в список с данными из базы данных.
                subjects.Add(new Subject
                {
                    Id = reader.GetInt32(0), // Идентификатор предмета.
                    Name = reader.GetString(1), // Название предмета.
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2) // Описание предмета (может быть null).
                });
            }
        }

        // Возвращаем список предметов.
        return subjects;
    }

    // Получает предмет из базы данных по идентификатору.
    public Subject GetSubjectById(int id)
    {
        // Переменная для хранения предмета.
        Subject subject = null;
        // Запрос SQL для выборки предмета по идентификатору.
        string query = "SELECT * FROM Subjects WHERE Id = @Id;";
        // Параметры для запроса.
        var parameters = new Dictionary<string, object>
        {
            { "@Id", id }
        };

        // Выполнение запроса с параметрами и обработка результата.
        using (var reader = DatabaseHelper.ExecuteQuery(query, parameters))
        {
            // Если предмет найден, считываем его данные.
            if (reader.Read())
            {
                subject = new Subject
                {
                    Id = reader.GetInt32(0), // Идентификатор предмета.
                    Name = reader.GetString(1), // Название предмета.
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2) // Описание предмета.
                };
            }
        }

        // Возвращаем предмет или null, если предмет не найден.
        return subject;
    }

    // Добавляет новый предмет в базу данных.
    public void AddSubject(Subject subject)
    {
        // Запрос SQL для вставки нового предмета в таблицу Subjects.
        string query = "INSERT INTO Subjects (Name, Description) VALUES (@Name, @Description);";
        // Параметры для запроса.
        var parameters = new Dictionary<string, object>
        {
            { "@Name", subject.Name }, // Название предмета.
            { "@Description", subject.Description ?? (object)DBNull.Value } // Описание предмета или DBNull, если описание отсутствует.
        };

        // Выполнение запроса с параметрами.
        DatabaseHelper.ExecuteNonQuery(query, parameters);
    }

    // Обновляет данные предмета в базе данных.
    public void UpdateSubject(Subject subject)
    {
        // Запрос SQL для обновления данных предмета в таблице Subjects.
        string query = "UPDATE Subjects SET Name = @Name, Description = @Description WHERE Id = @Id;";
        // Параметры для запроса.
        var parameters = new Dictionary<string, object>
        {
            { "@Id", subject.Id }, // Идентификатор предмета.
            { "@Name", subject.Name }, // Новое название предмета.
            { "@Description", subject.Description ?? (object)DBNull.Value } // Новое описание предмета или DBNull, если описание отсутствует.
        };

        // Выполнение запроса с параметрами.
        DatabaseHelper.ExecuteNonQuery(query, parameters);
    }

    // Удаляет предмет из базы данных по идентификатору.
    public void DeleteSubject(int id)
    {
        // Запрос SQL для удаления предмета из таблицы Subjects.
        string query = "DELETE FROM Subjects WHERE Id = @Id;";
        // Параметры для запроса.
        var parameters = new Dictionary<string, object>
        {
            { "@Id", id } // Идентификатор предмета для удаления.
        };

        // Выполнение запроса с параметрами.
        DatabaseHelper.ExecuteNonQuery(query, parameters);
    }
}
