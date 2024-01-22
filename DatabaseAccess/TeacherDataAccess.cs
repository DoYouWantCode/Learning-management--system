using ScheduleApp.Models;
using System.Data.SQLite;

namespace ScheduleApp.DatabaseAccess;

/// <summary>
/// Класс для доступа к данным о преподавателях.
/// </summary>
public class TeacherDataAccess
{
    /// <summary>
    /// Получить список всех преподавателей.
    /// </summary>
    /// <returns>Список всех преподавателей.</returns>
    public List<Teacher> GetAllTeachers()
    {
        List<Teacher> teachers = new List<Teacher>();
        string query = "SELECT Id, FirstName, LastName, Subject FROM Teachers;";

        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    teachers.Add(new Teacher
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Subject = reader.IsDBNull(3) ? null : reader.GetString(3)
                    });
                }
            }
        }

        return teachers;
    }

    /// <summary>
    /// Получить количество преподавателей по предмету.
    /// </summary>
    /// <param name="subject">Предмет.</param>
    /// <returns>Количество преподавателей по предмету.</returns>
    public int GetTeachersCountBySubject(string subject)
    {
        return GetAllTeachers().Where(t => t.Subject == subject).Count();
    }

    /// <summary>
    /// Получить преподавателя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор преподавателя.</param>
    /// <returns>Преподаватель с указанным идентификатором.</returns>
    public Teacher GetTeacherById(int id)
    {
        Teacher teacher = null;
        string query = "SELECT Id, FirstName, LastName, Subject FROM Teachers WHERE Id = @Id;";

        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        teacher = new Teacher
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Subject = reader.IsDBNull(3) ? null : reader.GetString(3)
                        };
                    }
                }
            }
        }

        return teacher;
    }

    /// <summary>
    /// Добавить преподавателя.
    /// </summary>
    /// <param name="teacher">Преподаватель для добавления.</param>
    public void AddTeacher(Teacher teacher)
    {
        string query = "INSERT INTO Teachers (FirstName, LastName, Subject) VALUES (@FirstName, @LastName, @Subject);";

        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                command.Parameters.AddWithValue("@LastName", teacher.LastName);
                command.Parameters.AddWithValue("@Subject", (object)teacher.Subject ?? DBNull.Value);
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Обновить информацию о преподавателе.
    /// </summary>
    /// <param name="teacher">Преподаватель для обновления.</param>
    public void UpdateTeacher(Teacher teacher)
    {
        string query = "UPDATE Teachers SET FirstName = @FirstName, LastName = @LastName, Subject = @Subject WHERE Id = @Id;";

        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", teacher.Id);
                command.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                command.Parameters.AddWithValue("@LastName", teacher.LastName);
                command.Parameters.AddWithValue("@Subject", (object)teacher.Subject ?? DBNull.Value);
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Удалить преподавателя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор преподавателя.</param>
    public void DeleteTeacher(int id)
    {
        string query = "DELETE FROM Teachers WHERE Id = @Id;";

        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
