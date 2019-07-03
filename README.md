# CalendarTelegramBot
.NET бот на Heroku
Как сделать .NET бота на Heroku
Потребуется:
### • Heroku аккаунт
### • Heroku CLI
### • Git
### • Visual Studio или Visual Code
### • .NET Core
#### Создаем Heroku приложение.
#### Важный момент. Откройте терминал, войдите в аккаунт Heroku через heroku cli heroku login
#### Далее создаём приложение в heroku. Введите его название обязательно
#### ```heroku create %your-app-name%```
#### Он сообщит что всё создал и даже укажет удаленный репозиторий Git, скопируйте его он нам пригодится скоро. Это важно.
#### Теперь нам нужно указать buildpack, чтобы наше .NET приложение могло собраться. 
#### В Heroku нет встроеного компилятора для .NET Core , поэтому мы указываем внешний сами
#### heroku buildpacks:set http://github.com/noliar/dotnet-buildpack.git -a %your-app-name%
#### Теперь клонируем с удаленного репозитория heroku ваше приложение. 
#### Там конечно будет только папка .git, но так меньше проблем с настройкой гита
#### git clone %your-remote-app-git-repository%
#### Дальше нам останется в скачаной папке создать проект. Создаем проект.
#### Создаём ASP.NET Core Web Application (.NET Core) через студию. Или через консоль вводим команду: 
```dotnet new webapi```
#### Далее правим:
```
public static void Main(string[] args)
{
var config = new ConfigurationBuilder().AddCommandLine(args).Build();
var host = new WebHostBuilder()
.UseKestrel()
.UseContentRoot(Directory.GetCurrentDirectory())
.UseConfiguration(config)
.UseIISIntegration()
.UseStartup<Startup>()
.Build();
 host.Run();
}
```
#### После чего загружай свой проект на удаленный репозиторий
#### ```git commit -m "create app for Heroku"```
#### Вот и всё!

