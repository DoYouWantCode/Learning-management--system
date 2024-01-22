namespace ScheduleApp.Models;

/// <summary>
/// Класс преподавателя
/// </summary>
public class Teacher
{
    /// <summary>
    /// ID преподавателя
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Предмет
    /// </summary>
    public string Subject { get; set; }
}
