namespace ScheduleApp.Forms;

/// <summary>
/// Класс главной формы
/// </summary>
public partial class MainForm : Form
{
    private int currentButtonTop = 20; 
    private const int buttonHeight = 30; 
    private const int buttonSpacing = 10;

    /// <summary>
    /// Конструктор главной формы
    /// </summary>
    public MainForm()
    {
        InitializeComponent();
        //Создание кнопок для перехода между формами
        CreateFormButton("Классы", OpenRoomForm);
        CreateFormButton("Преподаватели", OpenTeacherForm);
        CreateFormButton("Предметы", OpenSubjectForm);
        CreateFormButton("Расписание", OpenScheduleForm);
        CreateFormButton("Расписание на сегодня", OpenViewScheduleForm);

    }

    /// <summary>
    /// Метод открытия формы расписания
    /// </summary>
    private void OpenViewScheduleForm(object sender, EventArgs e)
    {
        ViewScheduleForm viewScheduleForm = new ViewScheduleForm();
        viewScheduleForm.ShowDialog();
    }
    /// <summary>
    /// Метод создания кнопки
    /// </summary>
    private void CreateFormButton(string formName, EventHandler clickEvent)
    {
        Button button = new Button();
        button.Text = formName;
        button.Click += clickEvent;

        button.Left = 10;
        button.Top = currentButtonTop;
        button.Width = 200;
        button.Height = buttonHeight;

        this.Controls.Add(button);

        currentButtonTop += buttonHeight + buttonSpacing;
    }

    /// <summary>
    /// Метод открытия формы кабинета
    /// </summary>
    private void OpenRoomForm(object sender, EventArgs e)
    {
        RoomForm roomForm = new RoomForm();
        roomForm.ShowDialog();
    }
    /// <summary>
    /// Метод открытия формы преподавателя
    /// </summary>
    private void OpenTeacherForm(object sender, EventArgs e)
    {
        TeacherForm teacherForm = new TeacherForm();
        teacherForm.ShowDialog();
    }
    /// <summary>
    /// Метод открытия формы предмета
    /// </summary>
    private void OpenSubjectForm(object sender, EventArgs e)
    {
        SubjectForm subjectForm = new SubjectForm();
        subjectForm.ShowDialog();
    }
    /// <summary>
    /// Метод открытия формы расписания
    /// </summary>
    private void OpenScheduleForm(object sender, EventArgs e)
    {
        ScheduleForm scheduleForm = new ScheduleForm();
        scheduleForm.ShowDialog();
    }
}
