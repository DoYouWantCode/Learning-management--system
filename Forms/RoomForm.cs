using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using ScheduleApp.DatabaseAccess;
using ScheduleApp.Models;

namespace ScheduleApp.Forms;

/// <summary>
/// Класс RoomForm отвечает за предоставление пользовательского интерфейса для управления классами.
/// </summary>
public partial class RoomForm : Form
{
    /// <summary>
    /// Экземпляр RoomDataAccess для работы с базой данных.
    /// </summary>
    private RoomDataAccess roomDataAccess;
    /// <summary>
    /// Экземпляр DataGridView для отображения списка классов.
    /// </summary>
    private DataGridView roomsDataGridView;
    /// <summary>
    /// Экземпляр TextBox для ввода номера кабинета.
    /// </summary>
    private TextBox roomNumberTextBox;
    /// <summary>
    /// Экземпляр TextBox для ввода номера здания.
    /// </summary>
    private TextBox capacityTextBox;
    /// <summary>
    /// Экземпляр кнопки добавления кабинета
    /// </summary>
    private Button addButton;
    /// <summary>
    /// Экземпляр кнопки обновления кабинета
    /// </summary>
    private Button updateButton;
    /// <summary>
    /// Экземпляр кнопки удаления кабинета
    /// </summary>
    private Button deleteButton;

    /// <summary>
    /// Конструктор
    /// </summary>
    public RoomForm()
    {
        InitializeComponent();
        roomDataAccess = new RoomDataAccess();
        InitializeRoomFormControls();
        LoadRooms();
    }

    /// <summary>
    /// Метод инициализации зачений элементов
    /// </summary>
    private void InitializeRoomFormControls()
    {
        roomsDataGridView = new DataGridView
        {
            Location = new Point(10, 10),
            Size = new Size(260, 130),
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };
        this.Controls.Add(roomsDataGridView);

        roomNumberTextBox = new TextBox
        {
            Location = new Point(10, 170),
            Size = new Size(100, 20)
        };
        this.Controls.Add(roomNumberTextBox);

        capacityTextBox = new TextBox
        {
            Location = new Point(120, 170),
            Size = new Size(100, 20)
        };
        this.Controls.Add(capacityTextBox);

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
        var roomNumberLabel = new Label
        {
            Text = "Номер кабинета",
            Location = new Point(10, 150),
            AutoSize = true
        };
        this.Controls.Add(roomNumberLabel);
        
        var capacityLabel = new Label
        {
            Text = "Вместительность",
            Location = new Point(120, 150),
            AutoSize = true
        };
        this.Controls.Add(capacityLabel);
    }

    /// <summary>
    /// Метод загрузи комнат в DataGridView
    /// </summary>
    private void LoadRooms()
    {
        roomsDataGridView.DataSource = roomDataAccess.GetAllRooms();
    }

    /// <summary>
    /// Метод обработчик события OnClick() для кнопки AddButton
    /// </summary>
    private void AddButton_Click(object sender, EventArgs e)
    {
        var room = new Room
        {
            RoomNumber = roomNumberTextBox.Text,
            Capacity = capacityTextBox.Text
        };
        roomDataAccess.AddRoom(room);
        LoadRooms();
    }

    /// <summary>
    /// Метод обработчик события OnClick() для кнопки UpdateButton
    /// </summary>
    private void UpdateButton_Click(object sender, EventArgs e)
    {
        if (roomsDataGridView.CurrentRow != null)
        {
            var room = (Room)roomsDataGridView.CurrentRow.DataBoundItem;
            room.RoomNumber = roomNumberTextBox.Text;
            room.Capacity = capacityTextBox.Text;
            roomDataAccess.UpdateRoom(room);
            LoadRooms();
        }
    }

    /// <summary>
    /// Метод обработчик события OnClick() для кнопки DeleteButton
    /// </summary>
    private void DeleteButton_Click(object sender, EventArgs e)
    {
        if (roomsDataGridView.CurrentRow != null)
        {
            int roomId = ((Room)roomsDataGridView.CurrentRow.DataBoundItem).Id;
            roomDataAccess.DeleteRoom(roomId);
            LoadRooms();
        }
    }
}