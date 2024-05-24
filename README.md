# Разработка модуля отгрузки готовой продукции для Сервисного металлоцентра Колпино АО «ИТЗ», г. Колпино
.NET 8.0 (EF Core 8)
Целью разработки программного продукта является создание модуля ERP-системы для управления бизнес-процессами (отчётность, поставки, операции), который бы уменьшил временные затраты и повысил эффективность работы сотрудников.
# Требования к запуску:
- Visual Studio 2022 | ([минимальные сист.требования](https://docs.microsoft.com/en-us/visualstudio/releases/2022/system-requirements))

# [Демонстрация проекта](https://drive.google.com/file/d/1DGDjIIxYQctAgJ-peIVK1WqcfQCvNRMy/view?usp=drive_link)

# Как работать с проектом:
1.	скачать проект с GitHub - Code - Open with Visual Studio (таким образом произойдёт автоматическая загрузка всех NuGet пакетов для решения);
2.	выбрать "управление пакетами с помощью консоли" и ввести "Update-Database";
3.	запуск проекта: если вдруг указан HP по умолчанию, то выбрать сверху PRGRM или решить следующим образом Решение - правой кнопкой мыши свойства и сменить на текущий выбор.
Для того, чтобы перенести базу данных на другое устройство необходимо:
1.	выбрать: «Вид - Обозреватель объектов SQL Server – SQL Server – Базы данных (нажать правой кнопкой мыши) – Извлечение приложения уровня данных – указываю, где будет сохранён файл с наименованием – в извлечение параметров выбираю извлечение схемы и данных и дополнительно пункт - подтвердить извлечение – ок».

# Какие данные получаю перед тем, как начать работу:
1.	Company (IdCompany, Name)
2.	ContainerPackage (IdContainer, TypeModel, MarkContainer)
3.	Payer (IdPayer, FIO, phone)
4.	Transport (IdTransport, Name, VehicleRegistration)
5.	Consignee (IdConsignee, FIO, IdPayer, phone, email, IdCompany)
6.	Orders (IdOrder, SystC3, LogC3, IdPayer, IdConsignee, DTReceived, ThicknessMm, WidthMm, LengthMm, Name, IdCompany, Mark).

