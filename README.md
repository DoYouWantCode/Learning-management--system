Cистема управления процессом обучения

Для запуска и работы необходимо установитьhttps://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.1-windows-x64-installer?cid=getdotnetcore

  
После установки (Никаких бд создавать не надо,если базы данных нет в папке с программой она сгенерирует его при вводе данных):
1)Запускаем .exe в папке проекта bin/Debug  
2)Появляется меню

Меню: позволяет перемещаться в разные окна.
Окна для редактирования: Классы, Преподаватели, Предметы, Расписание, Расписание на сегодня.

В окне Классы(или RoomForm) можно просмотреть существующие в базе классы или задать новые, вписав номер и вместительность класса(цифрами, естественно).

В окне Преподаватели можно сделать всё то же самое. Просмотр, редактирование и добавление новых данных. В первый текстбокс нужно вписывать имя, во второй фамилию и в третьем предмет(тоже строка), за которым закреплён преподаватель.

В окне Предметы можно задать название предмета и его описание.

В окне Расписание задавать данные о расписании можно с помощью следующих элементов(сверху вниз):
День недели(после нажатия на стрелку появляется список).
Предмет(ID, т.е. цифра. можно узнать в соответствующем окне).
Преподаватель(ID).
Кабинет(ID).
Начало и конец урока(выбор времени).

В окне просмотра расписания на сегодня выводит таблицу с ID Предмета, Преподавателя, Класса и данные о временном промежутке(время и день недели).