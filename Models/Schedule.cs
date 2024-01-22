namespace ScheduleApp.Models;

/// <summary>
/// Класс расписания
/// </summary>
public class Schedule
{
    /// <summary>
    /// ID расписания
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// ID предмета
    /// </summary>
    public int SubjectId { get; set; }
    /// <summary>
    /// ID преподавателя
    /// </summary>
    public int TeacherId { get; set; }
    /// <summary>
    /// ID кабинета 
    /// </summary>
    public int RoomId { get; set; }
    /// <summary>
    /// Время начала 
    /// </summary>
    public DateTime StartTime { get; set; }
    /// <summary>
    /// Время окончания
    /// </summary>
    public DateTime EndTime { get; set; }
    /// <summary>
    /// День недели
    /// </summary>
    public DayOfWeek DayOfWeek { get; set; }
}
