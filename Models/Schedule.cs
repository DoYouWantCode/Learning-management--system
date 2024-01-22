namespace ScheduleApp.Models;

/// <summary>
/// ����� ����������
/// </summary>
public class Schedule
{
    /// <summary>
    /// ID ����������
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// ID ��������
    /// </summary>
    public int SubjectId { get; set; }
    /// <summary>
    /// ID �������������
    /// </summary>
    public int TeacherId { get; set; }
    /// <summary>
    /// ID �������� 
    /// </summary>
    public int RoomId { get; set; }
    /// <summary>
    /// ����� ������ 
    /// </summary>
    public DateTime StartTime { get; set; }
    /// <summary>
    /// ����� ���������
    /// </summary>
    public DateTime EndTime { get; set; }
    /// <summary>
    /// ���� ������
    /// </summary>
    public DayOfWeek DayOfWeek { get; set; }
}
