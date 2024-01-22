using ScheduleApp.DatabaseAccess;
using ScheduleApp.Models;

namespace ScheduleApp.Forms;

public partial class TeacherForm : Form
{
    // Объект для управления данными учителей.
    private TeacherDataAccess teacherDataAccess;

    // Главный компонент таблицы для отображения списка учителей.
    private DataGridView teachersDataGridView;

    // Текстовое поле для ввода имени учителя.
    private TextBox firstNameTextBox;

    // Текстовое поле для ввода фамилии учителя.
    private TextBox lastNameTextBox;

    // Текстовое поле для ввода предмета, который ведет учитель.
    private TextBox subjectTextBox;

    // Кнопка для добавления нового учителя.
    private Button addButton;

    // Кнопка для обновления данных об учителе.
    private Button updateButton;

    // Кнопка для удаления учителя.
    private Button deleteButton;

    // Конструктор класса TeacherForm.
    public TeacherForm()
    {
        // Инициализация компонентов формы.
        InitializeComponent();

        // Создание объекта доступа к данным учителей.
        teacherDataAccess = new TeacherDataAccess();

        // Инициализация элементов управления формы.
        InitializeTeacherFormControls();

        // Загрузка данных об учителях и отображение их в таблице.
        LoadTeachers();
    }

    // Инициализация элементов управления в TeacherForm.
    private void InitializeTeacherFormControls()
    {
        // Настройка и добавление DataGridView на форму.
        teachersDataGridView = new DataGridView
        {
            Location = new Point(10, 10),
            Size = new Size(480, 130),
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };
        this.Controls.Add(teachersDataGridView);

        // Настройка и добавление текстовых полей для имени, фамилии и предмета.
        firstNameTextBox = new TextBox
        {
            Location = new Point(10, 170),
            Size = new Size(150, 20)
        };
        this.Controls.Add(firstNameTextBox);

        lastNameTextBox = new TextBox
        {
            Location = new Point(170, 170),
            Size = new Size(150, 20)
        };
        this.Controls.Add(lastNameTextBox);

        subjectTextBox = new TextBox
        {
            Location = new Point(330, 170),
            Size = new Size(150, 20)
        };
        this.Controls.Add(subjectTextBox);

        // Настройка и добавление кнопок для действий (добавить, обновить, удалить).
        addButton = new Button
        {
            Text = "Добавить",
            Location = new Point(10, 200),
            Size = new Size(75, 23)
        };
        addButton.Click += AddButton_Click;
        this.Controls.Add(addButton);

        updateButton = new Button
        {
            Text = "Обновить",
            Location = new Point(95, 200),
            Size = new Size(75, 23)
        };
        updateButton.Click += UpdateButton_Click;
        this.Controls.Add(updateButton);

        deleteButton = new Button
        {
            Text = "Удалить",
            Location = new Point(180, 200),
            Size = new Size(75, 23)
        };
        deleteButton.Click += DeleteButton_Click;
        this.Controls.Add(deleteButton);
        Label firstNameLabel = new Label
        {
            Text = "Имя:",
            Location = new Point(10, 150),
            Size = new Size(100, 20)
        };
        this.Controls.Add(firstNameLabel);

        Label lastNameLabel = new Label
        {
            Text = "Фамилия:",
            Location = new Point(170, 150),
            Size = new Size(100, 20)
        };
        this.Controls.Add(lastNameLabel);

        Label subjectLabel = new Label
        {
            Text = "Предмет:",
            Location = new Point(330, 150),
            Size = new Size(100, 20)
        };
        this.Controls.Add(subjectLabel);
    }

    // Загрузка данных об учителях из базы данных и их отображение.
    private void LoadTeachers()
    {
        teachersDataGridView.DataSource = teacherDataAccess.GetAllTeachers();
    }

    // Обработчик события клика по кнопке добавления нового учителя.
    private void AddButton_Click(object sender, EventArgs e)
    {
        string subject = subjectTextBox.Text;
        int teachersCount = teacherDataAccess.GetTeachersCountBySubject(subject);

        // Проверка на максимальное количество учителей по предмету.
        if (teachersCount >= 5)
        {
            MessageBox.Show("Невозможно закрепить преподавателя на заданный предмет.", "Достигнут лимит", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
            var teacher = new Teacher
            {
                FirstName = firstNameTextBox.Text,
                LastName = lastNameTextBox.Text,
                Subject = subject
            };
            teacherDataAccess.AddTeacher(teacher);
            LoadTeachers();
        }
    }

    // Обработчик события клика по кнопке обновления данных об учителе.
    private void UpdateButton_Click(object sender, EventArgs e)
    {
        if (teachersDataGridView.CurrentRow != null)
        {
            var teacher = (Teacher)teachersDataGridView.CurrentRow.DataBoundItem;
                        // Обновление данных выбранного учителя.
            teacher.FirstName = firstNameTextBox.Text;
            teacher.LastName = lastNameTextBox.Text;
            teacher.Subject = subjectTextBox.Text;

            // Вызов метода обновления учителя в базе данных.
            teacherDataAccess.UpdateTeacher(teacher);

            // Перезагрузка списка учителей для отображения обновленных данных.
            LoadTeachers();
        }
    }

    // Обработчик события клика по кнопке удаления учителя.
    private void DeleteButton_Click(object sender, EventArgs e)
    {
        if (teachersDataGridView.CurrentRow != null)
        {
            // Получение идентификатора выбранного учителя.
            int teacherId = ((Teacher)teachersDataGridView.CurrentRow.DataBoundItem).Id;

            // Вызов метода удаления учителя из базы данных.
            teacherDataAccess.DeleteTeacher(teacherId);

            // Перезагрузка списка учителей для отображения актуальных данных.
            LoadTeachers();
        }
    }
}