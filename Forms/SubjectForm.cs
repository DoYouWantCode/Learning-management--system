using ScheduleApp.DatabaseAccess;
using ScheduleApp.Models;

namespace ScheduleApp.Forms;

/// <summary>
/// Класс SubjectForm отвечает за предоставление пользовательского интерфейса для управления предметами.
/// Он позволяет добавлять новые предметы и удалять существующие.
/// </summary>
public partial class SubjectForm : Form
{
    // Поле для доступа к данным предметов
    private SubjectDataAccess subjectDataAccess;
    // Элементы управления для ввода данных о предмете и их управления
    private TextBox nameTextBox;
    private TextBox descriptionTextBox;
    private Button addButton;
    private Button deleteButton;
    private ListBox subjectsListBox;

    /// <summary>
    /// Конструктор класса SubjectForm. Инициализирует компоненты и загружает существующие предметы.
    /// </summary>
    public SubjectForm()
    {
        // Инициализация элементов управления формы
        this.nameTextBox = new TextBox();
        this.descriptionTextBox = new TextBox();
        this.addButton = new Button();
        this.deleteButton = new Button();
        this.subjectsListBox = new ListBox();

        // Установка свойств элементов, таких как расположение и размер
        this.nameTextBox.Location = new System.Drawing.Point(10, 30);
        this.nameTextBox.Size = new System.Drawing.Size(200, 20);

        this.descriptionTextBox.Location = new System.Drawing.Point(10, 75);
        this.descriptionTextBox.Size = new System.Drawing.Size(200, 20);

        this.addButton.Location = new System.Drawing.Point(10, 100);
        this.addButton.Size = new System.Drawing.Size(75, 23);
        this.addButton.Text = "Добавить";
        // Привязка обработчика событий для кнопки "Добавить"
        this.addButton.Click += new EventHandler(this.AddButton_Click);

        this.deleteButton.Location = new System.Drawing.Point(90, 100);
        this.deleteButton.Size = new System.Drawing.Size(75, 23);
        this.deleteButton.Text = "Удалить";
        // Привязка обработчика событий для кнопки "Удалить"
        this.deleteButton.Click += new EventHandler(this.DeleteButton_Click);

        this.subjectsListBox.Location = new System.Drawing.Point(10, 220);
        this.subjectsListBox.Size = new System.Drawing.Size(200, 95);
        this.subjectsListBox.SelectionMode = SelectionMode.One;

        // Добавление элементов управления на форму
        this.Controls.Add(this.nameTextBox);
        this.Controls.Add(this.descriptionTextBox);
        this.Controls.Add(this.addButton);
        this.Controls.Add(this.deleteButton);
        this.Controls.Add(this.subjectsListBox);

        this.Size = new System.Drawing.Size(240, 215);

        // Добавление Label к элементам управления в SubjectForm
        Label nameLabel = new Label();
        nameLabel.Text = "Имя предмета:";
        nameLabel.Location = new System.Drawing.Point(10, 10);
        nameLabel.Size = new System.Drawing.Size(200, 30);
        this.Controls.Add(nameLabel);

        Label descriptionLabel = new Label();
        descriptionLabel.Text = "Описание предмета:";
        descriptionLabel.Location = new System.Drawing.Point(10, 60);
        descriptionLabel.Size = new System.Drawing.Size(200, 30);
        this.Controls.Add(descriptionLabel);

        // Инициализация остальных компонентов формы
        InitializeComponent();

        // Создание экземпляра для доступа к данным предметов и загрузка списка предметов
        subjectDataAccess = new SubjectDataAccess();
        LoadSubjects();
    }

    /// <summary>
    /// Загружает предметы и отображает их в ListBox.
    /// </summary>
    private void LoadSubjects()
    {
        // Очистка текущего списка предметов в ListBox
        subjectsListBox.Items.Clear();
        // Получение всех предметов из базы данных
        List<Subject> subjects = subjectDataAccess.GetAllSubjects();
        // Добавление имен предметов в ListBox
        subjects.ForEach(subject => subjectsListBox.Items.Add(subject.Name));
    }

    /// <summary>
    /// Обработчик события нажатия на кнопку добавления предмета.
    /// Создает новый предмет и добавляет его в базу данных.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void AddButton_Click(object sender, EventArgs e)
    {
        // Создание объекта предмета с данными из текстовых полей
        Subject subject = new Subject
        {
            Name = nameTextBox.Text,
            Description = descriptionTextBox.Text
        };

        // Добавление предмета в базу данных
        subjectDataAccess.AddSubject(subject);
        // Перезагрузка списка предметов
        LoadSubjects();
    }

    /// <summary>
    /// Обработчик события нажатия на кнопку удаления предмета.
    /// Удаляет выбранный предмет из базы данных.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void DeleteButton_Click(object sender, EventArgs e)
    {
        // Проверяем, что в ListBox действительно выбран предмет
        if (subjectsListBox.SelectedItem != null)
        {
            // Получаем имя выбранного предмета
            string selectedSubjectName = subjectsListBox.SelectedItem.ToString();
            // Ищем предмет по его имени среди загруженных предметов
            Subject subjectToDelete = subjectDataAccess.GetAllSubjects().FirstOrDefault(s => s.Name == selectedSubjectName);
            // Если предмет найден, удаляем его из базы данных
            if (subjectToDelete != null)
            {
                subjectDataAccess.DeleteSubject(subjectToDelete.Id);
                // Перезагрузка списка предметов
                LoadSubjects();
            }
        }
    }
}