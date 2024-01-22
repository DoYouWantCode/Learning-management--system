using ScheduleApp.DatabaseAccess;
using ScheduleApp.Models;

namespace ScheduleApp.Forms;

/// <summary>
/// Форма для управления расписанием занятий.
/// </summary>
public partial class ScheduleForm : Form
{
    private ScheduleDataAccess _dataAccess;
    private ListView schedulesListView;
    private Button addButton;
    private Button editButton;
    private Button deleteButton;
    private TextBox subjectIdTextBox;
    private TextBox teacherIdTextBox;
    private TextBox roomIdTextBox;
    private DateTimePicker startTimePicker;
    private DateTimePicker endTimePicker;
    private ComboBox dayOfWeekComboBox;

    /// <summary>
    /// Конструктор формы ScheduleForm.
    /// Инициализирует новый экземпляр класса с компонентами формы.
    /// </summary>
    public ScheduleForm()
    {
        InitializeComponent();
        _dataAccess = new ScheduleDataAccess();
        InitializeListView();
        InitializeInputFields();
        InitializeButtons();
        LoadSchedules();
    }

    /// <summary>
    /// Инициализирует поля ввода на форме.
    /// </summary>
    private void InitializeInputFields()
    {
        // Инициализация и расположение текстовых полей и элементов выбора даты/времени
        Label subjectLabel = new Label() { Text = "Предмет:", Size = new Size(100, 20) };
        subjectIdTextBox = new TextBox();
        Label teacherLabel = new Label() { Text = "Преподаватель:", Size = new Size(100, 20) };
        teacherIdTextBox = new TextBox();
        Label roomLabel = new Label() { Text = "Кабинет:", Size = new Size(100, 20) };
        roomIdTextBox = new TextBox();
        Label startTimeLabel = new Label() { Text = "Начало:", Size = new Size(100, 20) };
        startTimePicker = new DateTimePicker();
        Label endTimeLabel = new Label() { Text = "Конец:", Size = new Size(100, 20) };
        endTimePicker = new DateTimePicker();
        Label dayOfWeekLabel = new Label() { Text = "День недели:", Size = new Size(100, 20) };
        dayOfWeekComboBox = new ComboBox();
        subjectLabel.Location = new Point(200, 40);
        subjectIdTextBox.Location = new Point(200, 70);
        subjectIdTextBox.Size = new Size(200, 20);
        teacherLabel.Location = new Point(200, 90);
        teacherIdTextBox.Location = new Point(200, 120);
        teacherIdTextBox.Size = new Size(200, 20);
        roomLabel.Location = new Point(200, 140);
        roomIdTextBox.Location = new Point(200, 170);
        roomIdTextBox.Size = new Size(200, 20);
        startTimeLabel.Location = new Point(200, 190);
        startTimePicker.Location = new Point(200, 210);
        endTimeLabel.Location = new Point(200, 230);
        endTimePicker.Location = new Point(200, 250);
        dayOfWeekLabel.Location = new Point(200, 0);
        dayOfWeekComboBox.Items.AddRange(Enum.GetNames(typeof(DayOfWeek)));
        dayOfWeekComboBox.Size = new Size(200,100);
        dayOfWeekComboBox.Location = new Point(200,20);

        Controls.Add(subjectIdTextBox);
        Controls.Add(teacherIdTextBox);
        Controls.Add(roomIdTextBox);
        Controls.Add(startTimePicker);
        Controls.Add(endTimePicker);
        Controls.Add(dayOfWeekComboBox);
        Controls.Add(subjectLabel);
        Controls.Add(teacherLabel);
        Controls.Add(roomLabel);
        Controls.Add(startTimeLabel);
        Controls.Add(endTimeLabel);
        Controls.Add(dayOfWeekLabel);
    }
    /// <summary>
    /// Обработчик событий нажатия на кнопку добавления расписания.
    /// Добавляет новое расписание в базу данных и обновляет список на форме.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void AddButton_Click(object sender, EventArgs e)
    {
        // Создание объекта Schedule и его добавление в базу данных
        Schedule schedule = new Schedule
        {
            SubjectId = int.Parse(subjectIdTextBox.Text),
            TeacherId = int.Parse(teacherIdTextBox.Text),
            RoomId = int.Parse(roomIdTextBox.Text),
            StartTime = startTimePicker.Value,
            EndTime = endTimePicker.Value,
            DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayOfWeekComboBox.SelectedItem.ToString())
        };
        _dataAccess.AddSchedule(schedule);
        LoadSchedules();
    }
    /// <summary>
    /// Обработчик событий нажатия на кнопку редактирования расписания.
    /// Обновляет выбранное расписание в базе данных и обновляет список на форме.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void EditButton_Click(object sender, EventArgs e)
    {
        // Редактирование выбранного объекта Schedule и его обновление в базе данных
        if (schedulesListView.SelectedItems.Count > 0)
        {
            var item = schedulesListView.SelectedItems[0];
            var schedule = _dataAccess.GetScheduleById(int.Parse(item.Text));
            schedule.SubjectId = int.Parse(subjectIdTextBox.Text);
            schedule.TeacherId = int.Parse(teacherIdTextBox.Text);
            schedule.RoomId = int.Parse(roomIdTextBox.Text);
            schedule.StartTime = startTimePicker.Value;
            schedule.EndTime = endTimePicker.Value;
            schedule.DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayOfWeekComboBox.SelectedItem.ToString());
            _dataAccess.UpdateSchedule(schedule);
            LoadSchedules();
        }
    }
    /// <summary>
    /// Инициализирует представление списка на форме.
    /// Создает колонки и настраивает внешний вид списка.
    /// </summary>
    private void InitializeListView()
    {
        schedulesListView = new ListView();
        
        schedulesListView.View = View.Details;
        schedulesListView.FullRowSelect = true;
        schedulesListView.Columns.Add("ID", -2, HorizontalAlignment.Left);
        schedulesListView.Columns.Add("ID Предмета", -2, HorizontalAlignment.Left);
        schedulesListView.Columns.Add("ID Преподавателя", -2, HorizontalAlignment.Left);
        schedulesListView.Columns.Add("ID Класса", -2, HorizontalAlignment.Left);
        schedulesListView.Columns.Add("Время начала", -2, HorizontalAlignment.Left);
        schedulesListView.Columns.Add("Время окончания", -2, HorizontalAlignment.Left);
        schedulesListView.Columns.Add("День недели", -2, HorizontalAlignment.Left);
        schedulesListView.Size = new Size(200,100);
        Controls.Add(schedulesListView);
    }
    /// <summary>
    /// Инициализирует кнопки на форме и их обработчики событий.
    /// </summary>
    private void InitializeButtons()
    {
        addButton = new Button();
        addButton.Location = new Point(0, 150);
        addButton.Text = "Добавить";
        addButton.Click += new EventHandler(AddButton_Click);
        Controls.Add(addButton);
        editButton = new Button();
        editButton.Location = new Point(0, 170);
        editButton.Text = "Изменить";
        editButton.Click += new EventHandler(EditButton_Click);
        Controls.Add(editButton);
        deleteButton = new Button();
        deleteButton.Location = new Point(0, 190);
        deleteButton.Text = "Удалить";
        deleteButton.Click += new EventHandler(DeleteButton_Click);
        Controls.Add(deleteButton);
    }
    /// <summary>
    /// Загружает расписания из базы данных и добавляет их в список на форме.
    /// </summary>
    private void LoadSchedules()
    {
        schedulesListView.Items.Clear();
        var schedules = _dataAccess.GetAllSchedules();
        foreach (var schedule in schedules)
        {
            var listViewItem = new ListViewItem(schedule.Id.ToString());
            listViewItem.SubItems.Add(schedule.SubjectId.ToString());
            listViewItem.SubItems.Add(schedule.TeacherId.ToString());
            listViewItem.SubItems.Add(schedule.RoomId.ToString());
            listViewItem.SubItems.Add(schedule.StartTime.ToString());
            listViewItem.SubItems.Add(schedule.EndTime.ToString());
            listViewItem.SubItems.Add(schedule.DayOfWeek.ToString());
            schedulesListView.Items.Add(listViewItem);
        }
    }
    /// <summary>
    /// Удаляет выбранный расписание из базы данных и обновляет список на форме.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void DeleteButton_Click(object sender, EventArgs e)
    {
        if (schedulesListView.SelectedItems.Count > 0)
        {
            //Проверка подтверждения удаления
            var confirmResult = MessageBox.Show("Вы уверены?",
                                                "Подтвердите удаление",
                                                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                var item = schedulesListView.SelectedItems[0];
                int scheduleId = int.Parse(item.Text);
                _dataAccess.DeleteSchedule(scheduleId);
                LoadSchedules();
            }
        }
        else
        {
            //Воспроизводится в случае не выбора объекта
            MessageBox.Show("Выберите объект для удаления.");
        }
    }
}
