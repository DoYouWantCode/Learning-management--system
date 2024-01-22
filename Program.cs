using ScheduleApp.Forms;

namespace ScheduleApp;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        DatabaseAccess.DatabaseHelper.InitializeDatabase();
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}