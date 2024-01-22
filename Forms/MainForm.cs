namespace ScheduleApp.Forms;

/// <summary>
/// ����� ������� �����
/// </summary>
public partial class MainForm : Form
{
    private int currentButtonTop = 20; 
    private const int buttonHeight = 30; 
    private const int buttonSpacing = 10;

    /// <summary>
    /// ����������� ������� �����
    /// </summary>
    public MainForm()
    {
        InitializeComponent();
        //�������� ������ ��� �������� ����� �������
        CreateFormButton("������", OpenRoomForm);
        CreateFormButton("�������������", OpenTeacherForm);
        CreateFormButton("��������", OpenSubjectForm);
        CreateFormButton("����������", OpenScheduleForm);
        CreateFormButton("���������� �� �������", OpenViewScheduleForm);

    }

    /// <summary>
    /// ����� �������� ����� ����������
    /// </summary>
    private void OpenViewScheduleForm(object sender, EventArgs e)
    {
        ViewScheduleForm viewScheduleForm = new ViewScheduleForm();
        viewScheduleForm.ShowDialog();
    }
    /// <summary>
    /// ����� �������� ������
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
    /// ����� �������� ����� ��������
    /// </summary>
    private void OpenRoomForm(object sender, EventArgs e)
    {
        RoomForm roomForm = new RoomForm();
        roomForm.ShowDialog();
    }
    /// <summary>
    /// ����� �������� ����� �������������
    /// </summary>
    private void OpenTeacherForm(object sender, EventArgs e)
    {
        TeacherForm teacherForm = new TeacherForm();
        teacherForm.ShowDialog();
    }
    /// <summary>
    /// ����� �������� ����� ��������
    /// </summary>
    private void OpenSubjectForm(object sender, EventArgs e)
    {
        SubjectForm subjectForm = new SubjectForm();
        subjectForm.ShowDialog();
    }
    /// <summary>
    /// ����� �������� ����� ����������
    /// </summary>
    private void OpenScheduleForm(object sender, EventArgs e)
    {
        ScheduleForm scheduleForm = new ScheduleForm();
        scheduleForm.ShowDialog();
    }
}
