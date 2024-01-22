using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScheduleApp.DatabaseAccess;

namespace ScheduleApp.Forms;

public partial class ViewScheduleForm : Form
{
    // Компонент ListView для отображения расписания.
    private ListView listViewSchedule;
    
    // Объект доступа к данным расписания.
    private ScheduleDataAccess scheduleDataAccess;

    // Конструктор класса ViewScheduleForm.
    public ViewScheduleForm()
    {
        // Инициализация компонентов формы.
        InitializeComponent();

        // Создание объекта для доступа к данным расписания.
        scheduleDataAccess = new ScheduleDataAccess();

        // Создание нового экземпляра ListView.
        listViewSchedule = new ListView();

        // Настройка свойства Dock для заполнения всего доступного пространства формы.
        listViewSchedule.Dock = DockStyle.Fill;

        // Заполнение ListView данными о расписании на сегодня.
        PopulateTodaySchedule();

        // Настройка режима отображения элементов ListView как детализированный список.
        listViewSchedule.View = View.Details;

        // Добавление столбцов в ListView для отображения различных аспектов расписания.
        listViewSchedule.Columns.Add("ID Предмета");
        listViewSchedule.Columns.Add("ID Учителя");
        listViewSchedule.Columns.Add("ID Класса");
        listViewSchedule.Columns.Add("Время начала");
        listViewSchedule.Columns.Add("Время окончания");
        listViewSchedule.Columns.Add("День недели");

        // Добавление listViewSchedule в коллекцию элементов управления формы.
        this.Controls.Add(listViewSchedule);
    }

    // Метод для заполнения ListView данными о расписании на сегодня.
    private void PopulateTodaySchedule()
    {
        // Получение сегодняшних расписаний и фильтрация на основе сравнения даты начала с текущим днем.
        var todaySchedules = scheduleDataAccess.GetAllSchedules()
                                               .Where(s => s.StartTime.Date == DateTime.Today.Date)
                                               .ToList();

        // Перебор полученных расписаний.
        foreach (var schedule in todaySchedules)
        {
            // Создание для каждого элемента ListViewItem с соответствующими данными.
            var item = new ListViewItem(new[] {
                schedule.SubjectId.ToString(),
                schedule.TeacherId.ToString(),
                schedule.RoomId.ToString(),
                schedule.StartTime.ToString("t"),
                schedule.EndTime.ToString("t"),
                schedule.DayOfWeek.ToString()
            });

            // Добавление созданных элементов ListViewItem в listViewSchedule.
            listViewSchedule.Items.Add(item);
        }
    }
}
