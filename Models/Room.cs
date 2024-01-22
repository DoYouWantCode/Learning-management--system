namespace ScheduleApp.Models;

/// <summary>
/// Класс кабинета
/// </summary>
public class Room
{
    /// <summary>
    /// ID кабинета
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Номер кабинета
    /// </summary>
    public string RoomNumber { get; set; }
    /// <summary>
    /// Вместимость кабинета
    /// </summary>
    public string Capacity { get; set; }
}
