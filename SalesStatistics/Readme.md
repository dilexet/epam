Используемые библиотеки необходимые для работ программы: 
1) CsvHelper
2) Serilog
3) Serilog.Settings.AppSettings
4) Serilog.Sinks.Console
5) Serilog.Sinks.File
6) EntityFramework

Должен быть устновлен какой-либо SqlServer

**  Перед запуском необходимо обновить базу данных, классы миграции находятся в проекте .DataAccessLayer
В файле конфигурации 2 варинта строки подключения, по умолчанию "Test"

При работе службы может возникнуть ошибка:
FIX : ERROR : Cannot open database requested by the login. The login failed. Login failed for user ‘NT AUTHORITY\NETWORK SERVICE’.
Go to SQL Server >> Security >> Logins and right click on NT AUTHORITY\NETWORK SERVICE and select Properties.
In newly opened screen of Login Properties, go to the “User Mapping” tab. Then, on the “User Mapping” tab, select the desired database – especially the database for which this error message is displayed. On the lower screen, check the role db_owner. Click OK.
