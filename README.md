## Запуск проекта
1. Клонируйте репозиторий
`https://github.com/Neitralov/PTMKTest.git`
2. Убедитесь, что имеете запущенный MongoDB по адресу `localhost:27017`
3. Перейдите в папку проекта `cd PTMKTest/` и начинайте!

## Режимы работы

### №1 (Создать таблицы)
Ничего не делает, поскольку MongoDB не требует предварительного создания таблиц (коллекций).

### №2 (Добавить нового сотрудника)
Пример: `dotnet run --project PTMKTest 2 "Osin Maxim Alexandrovich" 2003-04-05 Male`

### №3 (Вывести сотрудников на экран)
Пример: `dotnet run --project PTMKTest 3`

![image](https://github.com/user-attachments/assets/9d3ecd02-5ebe-4e93-bd1d-94ad0903f5f3)

### №4 (Заполнить БД миллионом случайных сотрудников + 100 сотрудников с фамилией "Фомин")
Пример: `dotnet run --project PTMKTest 4`

### №5 (Найти Фоминых в БД и вывести время этой операции)
Пример: `dotnet run --project PTMKTest 5`

![image](https://github.com/user-attachments/assets/7c83beab-c6af-4c7c-952c-252ca2f0aeb0)
